using System;
using UnityEngine;

public class UIOrientationManager : MonoBehaviour
{
    public static UIOrientationManager Instance;
    public GameOver gameOver;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateLayout();
    }

    private void OnRectTransformDimensionsChange()
    {
        if (gameOver.isGameOver) return;
        UpdateLayout();
    }

    public void UpdateLayout()
    {
        if (Screen.width > Screen.height) // 가로
        {
            UIManager.Instance.SetUI(1);
            Screen.SetResolution(1920,1080,false);
            GameManager.Instance.AfterUIChanged();
        }
        else
        {
            UIManager.Instance.SetUI(0);
            Screen.SetResolution(1200,1600,false);
            GameManager.Instance.AfterUIChanged();
        }
    }
}
