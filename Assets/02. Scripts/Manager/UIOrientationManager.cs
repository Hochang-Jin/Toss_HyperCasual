using System;
using UnityEngine;

public class UIOrientationManager : MonoBehaviour
{
    private void OnRectTransformDimensionsChange()
    {
        UpdateLayout();
    }

    public void UpdateLayout()
    {
        if (Screen.width > Screen.height) // 가로
        {
            Debug.Log("가로");
            UIManager.Instance.SetUI(1);
        }
        else
        {
            Debug.Log("세로");
            UIManager.Instance.SetUI(0);
        }
    }
}
