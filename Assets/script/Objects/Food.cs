using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int expireTime = 15;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(expire());
    }

    IEnumerator expire()
    {
        yield return new WaitForSeconds(expireTime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
