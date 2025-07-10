using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Singleton 
    public static UIManager Instance { get; private set; }
    
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
    }
}
