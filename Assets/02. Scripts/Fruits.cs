using System;
using System.Collections;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    SpriteRenderer fruitRenderer;
    private ObjectPool objectPool;
    private Vector3 initScale;
    private Collider2D col;
    public bool createFlag;
    public static float powerRatio = 1.225f;

    private readonly int max = System.Enum.GetValues(typeof(FruitType)).Length;
    
    public enum FruitType
    {
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5,
        Tier6,
        Tier7,
        Tier8
    }
    public FruitType type;

    private void Awake()
    {
        fruitRenderer = GetComponent<SpriteRenderer>();
        fruitRenderer.color = GameManager.Instance.currentColorPalette.colors[(int)type];
        objectPool = FindFirstObjectByType<ObjectPool>();
        initScale = fruitRenderer.transform.localScale;
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        transform.localScale = initScale;
        StartCoroutine(ColliderLatency());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(this.tag))
        {
            if(this.type == (FruitType)(max - 1)) return; // 최종 단계는 합쳐지지 않음
            
            SoundManager.Instance.MergeSound();
            // 합치는 기능
            GameManager.Instance.score += 2 * ((int)this.type + 1);
            objectPool.EnqueueObject(other.gameObject);
            this.transform.localScale *= powerRatio;
            this.type += 1;
            this.tag = this.type.ToString();
            fruitRenderer.color = GameManager.Instance.currentColorPalette.colors[(int)type];
            col.enabled = false;
            col.enabled = true;
        }
        if(!createFlag) return;
        createFlag = false;
        GameManager.Instance.AfterCreate();
    }

    public void SetFruit(FruitType fruitType)
    {
        this.type = fruitType;
        this.tag = this.type.ToString();
        fruitRenderer.color = GameManager.Instance.currentColorPalette.colors[(int)type];
        this.transform.localScale *= Mathf.Pow(powerRatio, (float)this.type );
    }

    private IEnumerator ColliderLatency()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
    }
}