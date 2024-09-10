using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D myRb;
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myRb.linearVelocityX = speed;
    }

    void Update()
    {
        
    }
}
