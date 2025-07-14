using System;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    SpriteRenderer fruitRenderer;
    private ObjectPool objectPool;
    private Vector3 initScale;

    public enum FruitType
    {
        Strawberry,
        Apple,
        Orange,
        Pear,
        Pineapple,
        Watermelon
    }
    public FruitType type;

    private void Awake()
    {
        fruitRenderer = GetComponent<SpriteRenderer>();
        fruitRenderer.sprite = GameManager.Instance.fruitSprites[(int)type];
        objectPool = FindFirstObjectByType<ObjectPool>();
        initScale = fruitRenderer.transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = initScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(this.tag))
        {
            if(this.type == FruitType.Watermelon) return; // 최종 단계는 합쳐지지 않음
            
            SoundManager.Instance.MergeSound();
            // 합치는 기능
            GameManager.Instance.count += 2 * ((int)this.type + 1);
            objectPool.EnqueueObject(other.gameObject);
            this.transform.localScale *= 1.2f;
            this.type += 1;
            this.tag = this.type.ToString();
            fruitRenderer.sprite = GameManager.Instance.fruitSprites[(int)type];
        }
    }

    public void SetFruit(FruitType fruitType)
    {
        this.type = fruitType;
        this.tag = this.type.ToString();
        fruitRenderer.sprite = GameManager.Instance.fruitSprites[(int)type];
        this.transform.localScale *= Mathf.Pow(1.2f, (float)this.type );
    }
}