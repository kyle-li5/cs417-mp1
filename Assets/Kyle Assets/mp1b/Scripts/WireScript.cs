using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WireBridge : MonoBehaviour
{
    public Transform deviceConnectionPoint;
    public Transform plugConnectionPoint;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (deviceConnectionPoint != null && plugConnectionPoint != null)
        {
            // Update the start and end points of the line every frame
            lineRenderer.SetPosition(0, deviceConnectionPoint.position);
            lineRenderer.SetPosition(1, plugConnectionPoint.position);
        }
    }
}