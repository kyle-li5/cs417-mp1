using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

// using UnityEngine.InputSystem;



/*
Puzzle 1: Sliding tile circuit puzzle

The plot: There's a 3 x 3 puzzle grid where each puzzle piece (I'm calling them nodes) is a cube and has a light for each side: up, down, left, right.

Each light can be either red or green, where green means "electricity" can flow (either direction) from that particular side of the node. By this logic, a node with only 1 "green side" is a sort of sink: electricity can flow in through that one side but not out.

The grid has a source node (the start of the puzzle) from which electricity originates and an end node (the goal of the puzzle) to which the player needs to route a path from the starting node.

Additional/unknowns:
    - The puzzle should be disabled until the player finds a "stored electricity" source (like a battery) and puts it into a terminal/receptacle of some sort

*/
public class GridSlot {
    public Vector3 location;
    public GameObject node;

    public GridSlot(Vector3 location) {
        this.location = location;
        this.node = null;
    }
}

public class CircuitPuzzle : MonoBehaviour {
    public GameObject node_prefab;
    // Spawn reference point, treated as upper-left hand corner grid origin
    public GameObject spawn_ref;
    public GameObject fuse_box_floor;
    public GameObject fuse;
    // Spacing between nodes and frame
    private const float MARGIN = 0.2f;
    // Puzzle grid
    private GridSlot[,] puzzle_grid;
    // Source node
    private (int, int) start_node;
    // End node
    private (int, int) goal_node;
    // // Empty slot allows for movement
    private (int, int) empty_slot;
    // no interaction allowed
    private bool puzzle_disabled;
    // Duration of animation
    private float move_duration = 1f;
    private bool is_moving;
    [SerializeField] private RoomController controller;
    
    /////////////////////////////////////////
    private IEnumerator AnimatePieceMove(int from_row, int from_col, int to_row, int to_col, float duration) {
        GridSlot src = puzzle_grid[from_row, from_col];
        GridSlot dst = puzzle_grid[to_row, to_col];

        GameObject moving_node = src.node;

        if (moving_node == null) { is_moving = false; yield break; }

        Vector3 start_position = moving_node.transform.position;
        Vector3 end_position = dst.location;

        duration = Mathf.Max(duration, 0.001f);

        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            // Smoothstep ease-in/ease-out
            t = t * t * (3f - 2f * t);

            moving_node.transform.position =
                Vector3.Lerp(start_position, end_position, t);

            yield return null;
        }

        // Guarantee exact final position.
        moving_node.transform.position = end_position;

        // Update logical grid state AFTER animation.
        dst.node = moving_node;
        src.node = null;

        // The node's old slot is now empty.
        empty_slot = (from_row, from_col);

        is_moving = false;
        if (PuzzleStateEval()) { PuzzleDisable(); Destroy(fuse_box_floor); fuse.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true; if (controller != null) { controller.ReportOnCircuitPuzzleSolved(); } }
    }
    
    private void MovePieceImmediate( int from_row, int from_col, int to_row, int to_col) {
        GridSlot src = puzzle_grid[from_row, from_col];
        GridSlot dst = puzzle_grid[to_row, to_col];

        GameObject moving_node = src.node;

        moving_node.transform.position = dst.location;

        dst.node = moving_node;
        src.node = null;

        empty_slot = (from_row, from_col);
    }
    public bool TryMoveNode(int row, int col, bool animate) {
        if (puzzle_disabled || is_moving) { return false; }

        if (row < 0 || row >= 3 || col < 0 || col >= 3) { return false; }

        if (puzzle_grid[row, col].node == null) { return false; }

        int empty_row = empty_slot.Item1;
        int empty_col = empty_slot.Item2;

        int distance = Mathf.Abs(row - empty_row) + Mathf.Abs(col - empty_col);

        // Must be directly adjacent to the empty slot.
        if (distance != 1) { return false; }

        if (animate) {
            is_moving = true;
            StartCoroutine(AnimatePieceMove(row, col, empty_row, empty_col, move_duration));
        } else { MovePieceImmediate(row, col, empty_row, empty_col); }

        return true;
    }
    public void PuzzleShuffle(int moves) {
        if (puzzle_disabled) { return; }

        int m = 0;
        while (m < moves || PuzzleStateEval()) {
            int i = Random.Range(0, 3);
            int j = Random.Range(0, 3);

            if (puzzle_grid[i, j].node == null) { continue; }

            if (TryMoveNode(i, j, false)) { m++; }
        }
    }
    public void PuzzleEnable(int shuffle_moves) {
        if (!puzzle_disabled) { return; }
        puzzle_disabled = false;

        // Hide solution and shuffling activities
        spawn_ref.SetActive(!spawn_ref.activeSelf);
        // setup the solution
        // Start at bottom-left
        puzzle_grid[2, 0].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Left, CircuitNode.LightState.Green);
        puzzle_grid[2, 0].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Right, CircuitNode.LightState.Green);
        // Across to bottom-center and up
        puzzle_grid[2, 1].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Left, CircuitNode.LightState.Green);
        puzzle_grid[2, 1].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Up, CircuitNode.LightState.Green);
        // Through center and up
        puzzle_grid[1, 1].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Down, CircuitNode.LightState.Green);
        puzzle_grid[1, 1].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Up, CircuitNode.LightState.Green);
        // Up through top-center and right 
        puzzle_grid[0, 1].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Right, CircuitNode.LightState.Green);
        puzzle_grid[0, 1].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Down, CircuitNode.LightState.Green);
        // Out to goal node
        puzzle_grid[0, 2].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Left, CircuitNode.LightState.Green);
        puzzle_grid[0, 2].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Right, CircuitNode.LightState.Green);
        // Decoys
        puzzle_grid[1, 0].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Up, CircuitNode.LightState.Green);
        puzzle_grid[1, 0].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Down, CircuitNode.LightState.Green);
        puzzle_grid[1, 2].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Down, CircuitNode.LightState.Green);
        puzzle_grid[1, 2].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Right, CircuitNode.LightState.Green);
        puzzle_grid[2, 2].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Left, CircuitNode.LightState.Green);
        puzzle_grid[2, 2].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Down, CircuitNode.LightState.Green);
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (puzzle_grid[i, j].node == null) { continue; }
                if (!puzzle_grid[i, j].node.GetComponent<CircuitNode>().up) { puzzle_grid[i, j].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Up, CircuitNode.LightState.Red); }
                if (!puzzle_grid[i, j].node.GetComponent<CircuitNode>().down) { puzzle_grid[i, j].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Down, CircuitNode.LightState.Red); }
                if (!puzzle_grid[i, j].node.GetComponent<CircuitNode>().left) { puzzle_grid[i, j].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Left, CircuitNode.LightState.Red); }
                if (!puzzle_grid[i, j].node.GetComponent<CircuitNode>().right) { puzzle_grid[i, j].node.GetComponent<CircuitNode>().SetLight(CircuitNode.Side.Right, CircuitNode.LightState.Red); }
                UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable inter = puzzle_grid[i, j].node.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
                inter.selectEntered.AddListener(OnPuzzleNodeSelect);
            }
        }
        if (shuffle_moves > 0) { PuzzleShuffle(shuffle_moves); }
        // Unhide once done
        spawn_ref.SetActive(!spawn_ref.activeSelf);
    }
    // Handler for when the player selects node
    // Where should this go? Here? Somewhere else?
    public void OnPuzzleNodeSelect(SelectEnterEventArgs args) {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (puzzle_grid[i, j].node == null) { continue; }
                if (args.interactableObject.transform.position == puzzle_grid[i, j].node.transform.position) {
                    TryMoveNode(i, j, true);
                    return;
                }
            }
        }            
    }
    public void PuzzleDisable() {
        if (puzzle_disabled) { return; }
        puzzle_disabled = true;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (puzzle_grid[i, j].node == null) { continue; }
                UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable inter = puzzle_grid[i, j].node.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
                inter.interactionLayers = InteractionLayerMask.GetMask("Nothing");
            }
        }
    }
    public bool PuzzleStateEval() {
        const int size = 3;
        GameObject start_obj = puzzle_grid[start_node.Item1, start_node.Item2].node;
        if (start_obj == null) { return false; }
        CircuitNode start = start_obj.GetComponent<CircuitNode>();
        if (start.GetLight(CircuitNode.Side.Left) != CircuitNode.LightState.Green) { return false; }
        
        bool[,] visited = new bool[size, size];
        Queue<(int, int)> queue = new Queue<(int, int)>();

        queue.Enqueue(start_node);
        visited[start_node.Item1, start_node.Item2] = true;

        while (queue.Count > 0) {
            (int row, int col) = queue.Dequeue();
            GameObject current = puzzle_grid[row, col].node;
            if (current == null) { continue; }
            CircuitNode node = current.GetComponent<CircuitNode>();
            if ((row, col) == goal_node) {
                return node.GetLight(CircuitNode.Side.Right) == CircuitNode.LightState.Green;
            }

            // up
            if (row > 0 && puzzle_grid[row - 1, col].node != null && !visited[row - 1, col]) {
                CircuitNode neighbor = puzzle_grid[row - 1, col].node.GetComponent<CircuitNode>();
                if (node.GetLight(CircuitNode.Side.Up) == CircuitNode.LightState.Green && neighbor.GetLight(CircuitNode.Side.Down) == CircuitNode.LightState.Green) {
                    visited[row - 1, col] = true;
                    queue.Enqueue((row - 1, col));
                }
            }

            // down: row + 1
            if (row < size - 1 && puzzle_grid[row + 1, col].node != null && !visited[row + 1, col]) {
                CircuitNode neighbor = puzzle_grid[row + 1, col].node.GetComponent<CircuitNode>();

                if (node.GetLight(CircuitNode.Side.Down) == CircuitNode.LightState.Green && neighbor.GetLight(CircuitNode.Side.Up) == CircuitNode.LightState.Green) {
                    visited[row + 1, col] = true;
                    queue.Enqueue((row + 1, col));
                }
            }

            // left: col - 1
            if (col > 0 && puzzle_grid[row, col - 1].node != null && !visited[row, col - 1]) {
                CircuitNode neighbor = puzzle_grid[row, col - 1].node.GetComponent<CircuitNode>();

                if (node.GetLight(CircuitNode.Side.Left) == CircuitNode.LightState.Green && neighbor.GetLight(CircuitNode.Side.Right) == CircuitNode.LightState.Green) {
                    visited[row, col - 1] = true;
                    queue.Enqueue((row, col - 1));
                }
            }

            // right: col + 1
            if (col < size - 1 && puzzle_grid[row, col + 1].node != null && !visited[row, col + 1]) {
                CircuitNode neighbor = puzzle_grid[row, col + 1].node.GetComponent<CircuitNode>();

                if (node.GetLight(CircuitNode.Side.Right) == CircuitNode.LightState.Green && neighbor.GetLight(CircuitNode.Side.Left) == CircuitNode.LightState.Green) {
                    visited[row, col + 1] = true;
                    queue.Enqueue((row, col + 1));
                }
            }
        }

        return false;
    }
    ////////////////////////////////////////////////
    // // Puzzle initialization, responsible for initial setup/spawning
    
    public void PuzzleInit() {
        puzzle_grid = new GridSlot[3, 3];
        is_moving = false;
        puzzle_disabled = true;
        empty_slot = (0, 0);
        start_node = (2, 0);
        goal_node = (0, 2);

        float spawn_start_x = spawn_ref.transform.position.x;
        float spawn_start_y = spawn_ref.transform.position.y;
        float spawn_start_z = spawn_ref.transform.position.z;
        float spawn_offset_z = node_prefab.transform.localScale.z;
        float spawn_offset_y = node_prefab.transform.localScale.y;

        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                Vector3 spawn_location = new Vector3(
                    spawn_start_x,
                    spawn_start_y - (spawn_offset_y / 2) - (MARGIN * spawn_offset_y) - (row * spawn_offset_y * (1 + MARGIN)),
                    spawn_start_z - (spawn_offset_z / 2) - (MARGIN * spawn_offset_z) - (col * spawn_offset_z * (1 + MARGIN))
                );

                puzzle_grid[row, col] = new GridSlot(spawn_location);

                if ((row, col) != empty_slot) {
                    GameObject node = Instantiate(node_prefab, spawn_location, node_prefab.transform.rotation);
                    node.transform.parent = spawn_ref.transform;
                    puzzle_grid[row, col].node = node;
                }
            }
        }
    }
    
    
    
    void Start() {
        PuzzleInit();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
