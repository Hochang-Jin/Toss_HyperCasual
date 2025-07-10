using UnityEngine;

public class GameOver : MonoBehaviour
{
    public LayerMask fruitLayer;
    public float maxDistance = 7.6f;

    public float timer;
    
    
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.right;
        
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, fruitLayer);
        if (hit.collider != null)
        {
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                Gameover();
            }
                
        }
        else
        {
            if (timer <= 0)
            {
                timer = 0;
            }
            else
            {
                timer -= Time.deltaTime * 2;
            }
        }
    }

    public void Gameover()
    {
        Debug.Log("게임오버");
    }
}

