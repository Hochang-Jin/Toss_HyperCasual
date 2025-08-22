using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "UISetting", menuName = "Scriptable Objects/UISetting")]
public class UISetting : ScriptableObject
{
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
}
