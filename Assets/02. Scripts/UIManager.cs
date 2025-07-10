using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Singleton 
    public static UIManager Instance { get; private set; }
    
    public GameObject gameOverObj;
    private GameOver gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    
    private void Awake()
    {
        // singleton 
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        gameOver = gameOverObj.GetComponent<GameOver>();
        
    }

    private void Update()
    {
        scoreText.text = GameManager.Instance.count.ToString();
        if (gameOver.timer > 0)
        {
            timerText.gameObject.SetActive(true);
            timerText.text = string.Format("조심하세요! - {0:N1}", 3f - gameOver.timer);
        }
        else
        {
            timerText.gameObject.SetActive(false);
        }
    }

    // private IEnumerator GameoverTimerRoutine()
    // {
    //     
    // }
}
