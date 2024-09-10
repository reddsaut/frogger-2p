using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Frog : MonoBehaviour
{
    bool canHop = false;
    public float waitTime;
    private bool p1frog = true;
    private SpriteRenderer spriteRenderer;
    public static int p1Score;
    public static int p2Score;
    public GameObject greenWin;
    public GameObject blueWin;

    public Sprite[] jump1;
    public Sprite[] jump2;

    public Sprite[] idle1;
    public Sprite[] idle2;

    public MyCursor myCursor;

    private AudioSource audioSource;
    public AudioClip splat;
    public AudioClip boing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Idle());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (canHop)
            {
                StartCoroutine(Hop());
            }
        }
    }
    IEnumerator Hop()
    {
        audioSource.PlayOneShot(boing);
        Vector3 original = transform.position;
        canHop = false;
        for (int i = 0; i < 6; i++)
        {
            transform.position = original + Vector3.up * EaseOutBack((i+1f)/6f);
            if(p1frog){
                spriteRenderer.sprite = jump1[i];
            }
            else{
                spriteRenderer.sprite = jump2[i];
            }
            yield return new WaitForSeconds(waitTime / 6);
        }
        if(p1frog){
            spriteRenderer.sprite = jump1[0];
        }
        else{
            spriteRenderer.sprite= jump2[0];
        }
        canHop = true;
    }

    void Reset()
    {
        StopAllCoroutines();
        canHop = false;
        p1frog = !p1frog;
        StartCoroutine(Idle());
        transform.position = new Vector3(0, -4.1f, 0);
        if(p1frog){
            spriteRenderer.sprite = jump1[0];
        }
        else{
            spriteRenderer.sprite = jump2[0];
        }
        myCursor.DestroyCars();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (p1frog)
            {
                if(p1Score == 9){
                    Instantiate(greenWin, new Vector3(0, 0,0), Quaternion.identity);
                    canHop = false;
                    Destroy(myCursor);
                    Reset();
                    StartCoroutine(Delay(2, Title));
                    return;
                }
                else
                {
                    p1Score++;
                }
            }
            else
            {
                if(p2Score == 9){
                    Instantiate(blueWin, new Vector3(0, 0,0), Quaternion.identity);
                    canHop = false;
                    Destroy(myCursor);
                    Reset();
                    StartCoroutine(Delay(2, Title));
                    return;
                }
                else
                {
                    p2Score++;
                }
            }
            Reset();
        }
        else
        {
            // add a point to enemy
            if (p1frog)
            {
                if(p2Score == 9){
                    Instantiate(blueWin, new Vector3(0, 0,0), Quaternion.identity);
                    canHop = false;
                    Destroy(myCursor);
                    Reset();
                    StartCoroutine(Delay(2, Title));
                    return;
                }
                else
                {
                    p2Score++;
                }
            }
            else
            {
                if(p1Score == 9){
                    Instantiate(greenWin, new Vector3(0, 0,0), Quaternion.identity);
                    canHop = false;
                    Destroy(myCursor);
                    Reset();
                    StartCoroutine(Delay(2, Title));
                    return;
                }
                else
                {
                    p1Score++;
                }
            }
            audioSource.PlayOneShot(splat);
            Reset();
        }
    }
    float EaseOutBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;

        return 1f + c3 * Mathf.Pow(x - 1f, 3f) + c1 * Mathf.Pow(x - 1f, 2f);
    }

    IEnumerator Delay(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    void Title()
    {
        SceneManager.LoadScene("Title");
    }
    IEnumerator Idle(){
        Sprite[] idle;
        if(p1frog)
        {
            idle = idle1;
        }
        else
        {
            idle = idle2;
        }
        foreach (Sprite frame in idle){
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(1f/idle.Length); 
        }
        foreach (Sprite frame in idle){
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(1f/idle.Length); 
        }
        canHop = true;
    }

}
