using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosao : MonoBehaviour
{
    public float speed;
    public float maxSize;
    
    void Update()
    {
        transform.localScale += Vector3.one * speed * Time.deltaTime;
        if (transform.localScale.x > maxSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Trigou!");

    }
}
