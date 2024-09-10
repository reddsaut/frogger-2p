using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public class MyCursor : MonoBehaviour
{
    public Sprite[] frames;
    public Sprite nuhuh;
    public float fps;
    private bool canClick = true;
    private bool hasCars = false;
    SpriteRenderer spriteRenderer;

    private int numCars = 5;
    private int numGaps = 2;

    public GameObject carPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnityEngine.Cursor.visible = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = frames[frames.Length - 1];
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = GetMouseWorldPosition();
        
        if(canClick)
        {
            CheckCars();
        }

        if(!hasCars && canClick && Input.GetMouseButtonDown(0))
        {
            SpawnCars();
            StartCoroutine(Animate());
        }
    }
    Vector3 GetMouseWorldPosition(){
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }
    IEnumerator Animate(){
        canClick = false;
        foreach (Sprite frame in frames){
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(1f/fps);
        }
        canClick = true;
    }

    void SpawnCars()
    {
        // n = number of cars
        // m = number of gaps
        // make array of n + m cars
        // randomize array
        // delete first m elements

        List<GameObject> cars = new List<GameObject>();
        int x = -12;
        int y = Mathf.RoundToInt(transform.position.y);
        for(int i = 0; i < numCars + numGaps; i++)
        {
            Vector3 position = new Vector3(x,y,0);
            cars.Add(Instantiate(carPrefab, position, Quaternion.identity));
            x -= 2;
        }

        System.Random random = new System.Random();
        for(int i=0; i < numGaps; i++)
        {
            int index = random.Next(cars.Count);
            Destroy(cars[index]);
            cars.RemoveAt(index);
        }
    }

    void CheckCars() {
        Vector3 origin = new Vector3(-12, Mathf.Round(transform.position.y), 0);
        float distance = 14;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector3.left, distance);

        if(hit.collider != null)
        {
            hasCars = true;
            spriteRenderer.sprite = nuhuh;
        }
        else
        {
            hasCars = false;
            spriteRenderer.sprite = frames[frames.Length - 1];
        }
    }
}
