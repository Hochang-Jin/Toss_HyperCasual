using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public LayerMask fruitLayer;
    public float maxDistance = 7.6f;

    public float timer;
    public bool isGameOver = false;

    private bool isTimerON;
    
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
                SoundManager.Instance.TimerOff();
                Gameover();
                isTimerON = false;
            }
            if(isGameOver) return;
            if(isTimerON) return;
            isTimerON = true;
            SoundManager.Instance.TimerOn();
        }
        else
        {
            if (timer <= 0)
            {
                timer = 0;
                if (!isTimerON) return;
                isTimerON = false;
                SoundManager.Instance.TimerOff();
            }
            else
            {
                timer -= Time.deltaTime * 2;
            }
        }
    }

    public void Gameover()
    {
        if(isGameOver) return;
        isGameOver = true;
        UIManager.Instance.GameOverUI();
    }
}

