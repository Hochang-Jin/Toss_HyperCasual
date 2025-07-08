using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject preview;
    [SerializeField] private GameObject fruit;
    [SerializeField] private GameObject parent;
    
    private float minX = -1.6f, maxX = 1.6f;
    private bool isDragging = false;
    private float yFixed;

    void Start()
    {
        if (preview != null)
            yFixed = preview.transform.position.y;
    }

    void Update()
    {
        // 클릭 시작
        if (Input.GetMouseButtonDown(0))
        {
            preview.SetActive(true);
            isDragging = true;
        }

        // 마우스 누르고 있을 때 계속 이동
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float clampedX = Mathf.Clamp(mouseWorld.x, minX, maxX);

            preview.transform.position = new Vector3(clampedX, yFixed, preview.transform.position.z);
        }

        // 클릭 해제 시 드래그 종료
        if (Input.GetMouseButtonUp(0))
        {
            preview.SetActive(false);
            Instantiate(fruit, preview.transform.position, preview.transform.rotation, parent.transform);
            isDragging = false;
        }
    }
}
