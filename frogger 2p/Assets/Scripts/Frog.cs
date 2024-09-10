using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Frog : MonoBehaviour
{
    bool canHop = true;
    public float waitTime;
    private bool p1frog = true;
    private SpriteRenderer spriteRenderer;
    private Sprite noMove;
    public static int p1Score;
    public static int p2Score;

    public Sprite[] jump;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        noMove = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W))
        {
            if(canHop){
                StartCoroutine(Cooldown());
            }
        }
    }
    IEnumerator Cooldown()
    {
        canHop = false;
        for(int i=0; i < 7; i++)
        {
            transform.position += Vector3.up / 7f;
            spriteRenderer.sprite = jump[i];
            yield return new WaitForSeconds(waitTime / 7);
        }
        spriteRenderer.sprite = noMove;
        canHop = true;
    }

    void Reset()
    {
        p1frog = !p1frog;
        transform.position = new Vector3(0, -4.1f, 0);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            if(p1frog){
                p1Score++;
            }
            else{
                p2Score++;
            }
            Reset();
        }
        else
        {
            // add a point to enemy
            if(p1frog){
                p2Score++;
            }
            else{
                p1Score++;
            }
            Reset();
        }
    }
}
