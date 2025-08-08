using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton 
    public static UIManager Instance { get; private set; }
    
    public GameObject gameOverObj;
    private GameOver gameOver;
    public TextMeshProUGUI gameOverScore;
    
    public Animator gameOverAnimator;
    
    public GameObject gameOverUI;
    public GameObject playingUI;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [SerializeField] private Image nextImg;
    
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
        scoreText.text = GameManager.Instance.score.ToString();
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

    public void GameOverUI()
    {
        playingUI.SetActive(false);
        gameOverUI.SetActive(true);
        gameOverScore.text = GameManager.Instance.score.ToString();
        gameOverAnimator.SetTrigger("GameOver");
    }

    public void UIReset()
    {
        playingUI.SetActive(true);
        gameOverUI.SetActive(false);
        gameOver.timer = 0;
        gameOver.isGameOver = false;
        
    }

    public void NextFruit()
    {
        nextImg.color = GameManager.Instance.currentColorPalette.colors[(int)GameManager.Instance.nextFruitType];
    }
}
