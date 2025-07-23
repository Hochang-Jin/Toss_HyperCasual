using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // singleton 구현
    public static GameManager Instance { get; private set; }

    public int count = 0;
    
    public GameObject preview;
    public SpriteRenderer previewRenderer;
    public GameObject fruit;
    [SerializeField] private GameObject parent;
    
    public ObjectPool objectPool;
    
    public Button restartButton;
    
    public Sprite[] fruitSprites;
    
    public float minX = -1.6f;
    public float maxX = 1.6f;

    private bool isDragging = false;
    private float yFixed;
    private float dragOffsetX;

    private Fruits.FruitType fruitType;

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
        restartButton.onClick.AddListener(Reset);
    }

    void Start()
    {
        if (preview != null)
            yFixed = preview.transform.position.y;
        
    }

    void Update()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouseInput();
        #elif UNITY_ANDROID || UNITY_IOS
            HandleTouchInput();
        #endif
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;
            
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float clampedX = Mathf.Clamp(mouseWorld.x, minX, maxX);

            preview.transform.position = new Vector3(clampedX, yFixed, preview.transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(!isDragging) return;
            preview.SetActive(false);
            
            FruitCreate();
            SoundManager.Instance.DropSound();
            
            RandomType();

            StartCoroutine(ChangeSpriteRoutine());
            
            isDragging = false;
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        Vector2 touchWorld = Camera.main.ScreenToWorldPoint(touch.position);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return;

                isDragging = true;
                preview.SetActive(true);
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                if (!isDragging)
                    return;

                float clampedX = Mathf.Clamp(touchWorld.x, minX, maxX);
                preview.transform.position = new Vector3(clampedX, yFixed, preview.transform.position.z);
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (!isDragging)
                    return;

                isDragging = false;
                preview.SetActive(false);

                FruitCreate();
                SoundManager.Instance.DropSound();
                RandomType();
                StartCoroutine(ChangeSpriteRoutine());
                break;
        }
    }


    private void RandomType()
    {
        int ranVal = Random.Range(0, 10);
        if (ranVal < 6)
            ranVal = 0;
        else if (ranVal < 9)
            ranVal = 1;
        else
            ranVal = 2;
        fruitType = (Fruits.FruitType)ranVal;
    }
    public void FruitCreate()
    {
        GameObject fruitObj = objectPool.DequeueObject();
        fruitObj.transform.position = preview.transform.position;
        fruitObj.transform.rotation = preview.transform.rotation;
        fruitObj.GetComponent<Fruits>().SetFruit(fruitType);
    }

    IEnumerator ChangeSpriteRoutine()
    {
        previewRenderer.sprite = fruitSprites[(int)fruitType];
        yield return new WaitForSeconds(0.1f);
        float scale = 0.608f * Mathf.Pow(1.2f, (float)fruitType);
        preview.transform.localScale = new Vector2(scale, scale);
        preview.SetActive(true);
    }

    private void Reset()
    {
        count = 0;
        isDragging = false;
        Start();
        UIManager.Instance.UIReset();
        objectPool.PoolReset();
        Time.timeScale = 1f;
        SoundManager.Instance.ResetBGM();
        preview.SetActive(true);
    }
}
