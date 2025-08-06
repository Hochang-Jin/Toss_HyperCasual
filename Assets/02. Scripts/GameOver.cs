using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public LayerMask fruitLayer;
    public float maxDistance = 7.6f;

    public float timer;
    public bool isGameOver = false;

    private bool isTimerON;
    private bool isReverseTimer;
    
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.right;
        
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, fruitLayer);
        if (hit.collider != null)
        {
            if(isGameOver) return;
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                SoundManager.Instance.TimerOff();
                Gameover();
                SoundManager.Instance.EndingSound();
                isTimerON = false;
                isReverseTimer = false;
            }
            if(isGameOver) return;
            if(isTimerON && !isReverseTimer) return;
            isTimerON = true;
            isReverseTimer = false;
            SoundManager.Instance.TimerOn();
        }
        else
        {
            if (timer <= 0)
            {
                timer = 0;
                if (!isTimerON) return;
                isTimerON = false;
                isReverseTimer = false;
                SoundManager.Instance.TimerOff();
            }
            else
            {
                timer -= Time.deltaTime * 2;
                if (isReverseTimer) return;
                isReverseTimer = true;
                SoundManager.Instance.ReverseTimer();
            }
        }
    }

    public void Gameover()
    {
        if(isGameOver) return;
        isGameOver = true;
        isReverseTimer = false;
        isTimerON = false;
        UIManager.Instance.GameOverUI();
        SoundManager.Instance.BGMOff();
        GameManager.Instance.preview.SetActive(false);
    }
}

