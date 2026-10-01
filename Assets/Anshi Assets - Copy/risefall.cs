using UnityEngine;

public class risefall : MonoBehaviour
{
    // public GameObject pot;
    private Transform body;
    private Vector3 velocity;

    public float rate;
    public float goal;
    public float damping;

    private float startY;
    private bool hasReachedTop = false;
    private bool stopped = false;
    private bool moving = false;

public void move(GameObject obj)
{
    body = obj.transform;
    velocity = Vector3.zero;

    startY = body.position.y;
    hasReachedTop = false;
    stopped = false;
    moving = true;
}

void Update()
{
    //used gpt to help format my code so it is more readable
    if (!moving || stopped)
        return;

    body.position += velocity * Time.deltaTime;

    velocity += new Vector3(
        0f,
        rate * (goal - body.position.y) - damping * velocity.y,
        0f
    ) * Time.deltaTime;

    if (!hasReachedTop && body.position.y >= goal)
    {
        hasReachedTop = true;
    }

    if (hasReachedTop &&
        ((Mathf.Abs(body.position.y - startY) < 0.05f)) &&
        velocity.y < 0)
    {
        body.position = new Vector3(
            body.position.x,
            startY,
            body.position.z
        );

        velocity = Vector3.zero;
        stopped = true;
        moving = false;
    }
}
}