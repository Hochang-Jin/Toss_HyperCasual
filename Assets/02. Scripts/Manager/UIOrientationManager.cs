using System;
using UnityEngine;

public class UIOrientationManager : MonoBehaviour
{
    public static UIOrientationManager Instance;
    public GameOver gameOver;

    public float scaleHeight;
    public float scaleWidth;
    
    public int setWidth; // 사용자 설정 너비
    public int setHeight; // 사용자 설정 높이

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateLayout();
    }

    private void SetResolution(int width, int height)
    {
        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        setWidth = width;
        setHeight = height;
        
        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
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
            SetResolution(1920, 1080);
            GameManager.Instance.AfterUIChanged();
        }
        else
        {
            UIManager.Instance.SetUI(0);
            SetResolution(1200, 1600);
            GameManager.Instance.AfterUIChanged();
        }
    }
}
