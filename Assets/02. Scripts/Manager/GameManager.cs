using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region 멤버 변수
    // singleton 구현
    public static GameManager Instance { get; private set; }
    public GameObject gameOverObj;
    private GameOver gameOver;

    [FormerlySerializedAs("count")] public int score = 0;
    
    public GameObject preview;
    public Vector3 previewPosition;
    public SpriteRenderer previewRenderer;
    [FormerlySerializedAs("parent")] [SerializeField] private GameObject board;
    
    public ObjectPool objectPool;
    
    public Button restartButton;
    public Button restartButtonWin;
    public Button horizontalRestartButton;
    public Button horizontalRestartButtonWin;
    
    public float minX = -1.6f;
    public float maxX = 1.6f;

    private bool isDragging = false;
    private float yFixed;
    private float dragOffsetX;

    private Fruits.FruitType curFruitType;
    public Fruits.FruitType nextFruitType;

    public bool isCollision = true;

    public ColorPalette currentColorPalette;

    public short rotCount = 4;
    public float timer;
    
    #endregion
    
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
        
        if (preview != null)
                    yFixed = preview.transform.position.y;
        
        restartButton.onClick.AddListener(Reset);
        restartButtonWin.onClick.AddListener(Reset);
        horizontalRestartButton.onClick.AddListener(Reset);
        horizontalRestartButtonWin.onClick.AddListener(Reset);
        previewPosition = preview.transform.localPosition;
        gameOver = gameOverObj.GetComponent<GameOver>();

    }

    private void Start()
    {
        RandomType();
        RandomType();
        StartCoroutine(ChangeSpriteRoutine()); 
        UIManager.Instance.NextFruit();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            timer = 0;
            if(rotCount < 4)
                rotCount++;
        }
        #if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouseInput();
        #elif UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL
            HandleTouchInput();
        #endif
    }
    #region Mouse Input

    void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                    return;
                if (!isCollision)
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
                if(gameOver.isGameOver) return;
                if (board.transform.rotation.eulerAngles.z % 90 != 0) return;
                preview.SetActive(false);
                
                FruitCreate();
                
                isDragging = false;
            }
        }

    #endregion 
    
    void HandleTouchInput()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        Vector2 touchWorld = Camera.main.ScreenToWorldPoint(touch.position);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (IsPointerOverUIObject(touch.position))
                    return;
                if (!isCollision)
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
                if (!isDragging) return;
                if(gameOver.isGameOver) return;
                // 회전이 끝나지 않았으면 과일을 생성하지 않음
                if (board.transform.rotation.eulerAngles.z % 90 != 0) return;

                isDragging = false;
                preview.SetActive(false);

                FruitCreate();
                break;
        }
    }

    /// <summary>
    /// WEBGL UI 터치 막기 위한 용도
    /// </summary>
    /// <param name="screenPosition"></param>
    /// <returns></returns>
    bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
    
    private void RandomType()
    {
        int ranVal = Random.Range(0, 10);
        if (ranVal < 3) // 0, 1, 2 (30%)
            ranVal = 0;
        else if (ranVal < 6) // 3, 4, 5 (30%)
            ranVal = 1;
        else if (ranVal < 8) // 6, 7 (20%)
            ranVal = 2;
        else if (ranVal < 10) // 8, 9 (20%)
            ranVal = 3;
        

        curFruitType = nextFruitType;
        nextFruitType = (Fruits.FruitType)ranVal;
    }
    
    public void FruitCreate()
    {
        GameObject fruitObj = objectPool.DequeueObject();
        fruitObj.transform.position = preview.transform.position;
        fruitObj.transform.rotation = preview.transform.rotation;
        fruitObj.GetComponent<Fruits>().SetFruit(curFruitType);
        fruitObj.GetComponent<Fruits>().createFlag = true;
        
        SoundManager.Instance.DropSound();
        isCollision = false;
    }

    public void AfterCreate()
    {
        RandomType();
        UIManager.Instance.NextFruit();
        StartCoroutine(ChangeSpriteRoutine());
        isCollision = true;
    }

    public void AfterUIChanged()
    {
        StartCoroutine(ChangeSpriteRoutine());
        UIManager.Instance.NextFruit();
    }

    IEnumerator ChangeSpriteRoutine()
    {
        Color32 color = currentColorPalette.colors[(int)curFruitType];
        color.a = 175;
        previewRenderer.color = color;
        
        yield return new WaitForSeconds(0.1f);
        float scale = 0.608f * Mathf.Pow(Fruits.powerRatio, (float)curFruitType);
        preview.transform.localScale = new Vector2(scale, scale);
        preview.SetActive(true);
    }

    private void Reset()
    {
        rotCount = 4;
        score = 0;
        isDragging = false;
        preview.transform.position = previewPosition;
        UIManager.Instance.UIReset();
        objectPool.PoolReset();
        Time.timeScale = 1f;
        SoundManager.Instance.ResetBGM();
        RandomType();
        RandomType();
        StartCoroutine(ChangeSpriteRoutine());
        preview.SetActive(true);
    }
}
