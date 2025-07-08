using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Preview : MonoBehaviour
{
    public LayerMask groundMask;
    public float maxDistance = 100f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }
    
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, groundMask);

        if (hit.collider != null)
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, origin + direction * maxDistance);
        }
    }
}
