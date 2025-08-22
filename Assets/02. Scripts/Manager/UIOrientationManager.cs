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
            Debug.Log("가로");
            UIManager.Instance.SetUI(1);
            GameManager.Instance.AfterUIChanged();
        }
        else
        {
            Debug.Log("세로");
            UIManager.Instance.SetUI(0);
            GameManager.Instance.AfterUIChanged();
        }
    }
}
