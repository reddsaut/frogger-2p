using UnityEngine;

public class P1Score : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] frames;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = frames[Frog.p1Score];
    }
}
