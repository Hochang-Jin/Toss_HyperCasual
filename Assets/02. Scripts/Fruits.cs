using System;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    [SerializeField] Sprite[] fruits;
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

    private void Start()
    {
        fruitRenderer = GetComponent<SpriteRenderer>();
        fruitRenderer.sprite = fruits[(int)type];
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
            fruitRenderer.sprite = fruits[(int)type];
        }
    }

    public void SetFruit(FruitType fruitType)
    {
        this.type = fruitType;
        this.tag = this.type.ToString();
        fruitRenderer.sprite = fruits[(int)fruitType];
        this.transform.localScale *= Mathf.Pow(1.2f, (float)this.type + 1f );
    }
}