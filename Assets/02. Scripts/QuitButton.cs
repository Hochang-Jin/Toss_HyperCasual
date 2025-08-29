using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void Quit()
    {
        GameManager.Instance.Quit();
    }
}
