using System;
using System.Collections;
using Ricimi;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Serializable]
    private class UIClass
    {
        public GameObject canvas;
        public TextMeshProUGUI gameOverScore;
        public TextMeshProUGUI winScore;
    
        public Animator gameOverAnimator;
        public Animator winAnimator;
    
        public GameObject gameOverUI;
        public GameObject playingUI;
        public GameObject winUI;
    
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI timerText;
        
        public Image nextImg;
        public Image[] tierPreviews;
    }

    private int prevIndex = 0;
    
    // Singleton 
    public static UIManager Instance { get; private set; }
    
    public GameObject gameOverObj;
    private GameOver gameOver;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI winScore;
    
    public Animator gameOverAnimator;
    public Animator winAnimator;
    
    public GameObject gameOverUI;
    public GameObject playingUI;
    public GameObject winUI;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [SerializeField] private Image nextImg;
    public Image[] tierPreviews;
    public Image[] tierPreviews2;

    [SerializeField] private UIClass[] uiClasses;
    
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

    private void Start()
    {
        TierPreview();
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

    public void WinUI()
    {
        playingUI.SetActive(false);
        winUI.SetActive(true);
        winScore.text = GameManager.Instance.score.ToString();
        winAnimator.SetTrigger("GameOver");
    }

    public void UIReset()
    {
        playingUI.SetActive(true);
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        gameOver.timer = 0;
        gameOver.isGameOver = false;
    }

    public void NextFruit()
    {
        nextImg.color = GameManager.Instance.currentColorPalette.colors[(int)GameManager.Instance.nextFruitType];
    }

    public void TierPreview()
    {
        for (int i = 0; i < tierPreviews.Length; i++)
        {
            tierPreviews[i].color = GameManager.Instance.currentColorPalette.colors[i];
            tierPreviews2[i].color = GameManager.Instance.currentColorPalette.colors[i];
        }
    }

    public void SetUI(int index)
    {
        gameOverScore = uiClasses[index].gameOverScore;
        winScore = uiClasses[index].winScore;

        gameOverAnimator = uiClasses[index].gameOverAnimator;
        winAnimator = uiClasses[index].winAnimator;

        gameOverUI = uiClasses[index].gameOverUI;
        playingUI = uiClasses[index].playingUI;
        winUI = uiClasses[index].winUI;

        scoreText = uiClasses[index].scoreText;
        timerText = uiClasses[index].timerText;

        nextImg = uiClasses[index].nextImg;
        tierPreviews = uiClasses[index].tierPreviews;

        for (int i = 0; i < uiClasses.Length; i++)
        {
            uiClasses[i].canvas.SetActive(false);
        }
        uiClasses[index].canvas.SetActive(true);
    }
}
