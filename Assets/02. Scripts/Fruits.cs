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

    public enum FruitType
    {
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5,
        Tier6,
        Tier7,
        Tier8,
        Tier9,
        Tier10
    }
    public FruitType type;

    private void Awake()
    {
        fruitRenderer = GetComponent<SpriteRenderer>();
        fruitRenderer.sprite = GameManager.Instance.fruitSprites[(int)type];
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
            if(this.type == FruitType.Tier10) return; // 최종 단계는 합쳐지지 않음
            
            SoundManager.Instance.MergeSound();
            // 합치는 기능
            GameManager.Instance.count += 2 * ((int)this.type + 1);
            objectPool.EnqueueObject(other.gameObject);
            this.transform.localScale *= 1.25f;
            this.type += 1;
            this.tag = this.type.ToString();
            fruitRenderer.sprite = GameManager.Instance.fruitSprites[(int)type];
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
        fruitRenderer.sprite = GameManager.Instance.fruitSprites[(int)type];
        this.transform.localScale *= Mathf.Pow(1.25f, (float)this.type );
    }

    private IEnumerator ColliderLatency()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
    }
}