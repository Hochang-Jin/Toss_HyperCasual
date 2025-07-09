using System;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    SpriteRenderer fruitRenderer;

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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(this.tag))
        {
            if(this.type == FruitType.Watermelon) return;
            other.gameObject.SetActive(false);
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