using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public Vector2 start = new Vector2(-10,-10);
    public float spawnTime = 7;
    public float spawnAmount = 5;
    public GameObject food;

    private Vector2 end;

    // Start is called before the first frame update
    void Start()
    {
        end = start * -1;
        StartCoroutine(GenerateFood());
    }

    IEnumerator GenerateFood()
    {
        yield return new WaitForSeconds(spawnTime);
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject g = Instantiate(food, new Vector3(Random.Range(start.x, end.x), Random.Range(start.y, end.y), 0), Quaternion.identity);
            g.transform.parent = transform;
        }
        StartCoroutine(GenerateFood());
    }
}
