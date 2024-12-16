using UnityEngine;


public class Car : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D myRb;
    public Sprite[] skins;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myRb.linearVelocityX = speed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        System.Random random = new System.Random();
        spriteRenderer.sprite = skins[random.Next(skins.Length)];

    }

    void Update()
    {
        if(transform.position.x > 12)
        {
            Destroy(gameObject);
        }
    }
}
