using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(LineRenderer))]
public class Preview : MonoBehaviour
{
    public float maxDistance = 6f;

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
        
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);

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
