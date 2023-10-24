using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosao : MonoBehaviour
{
    public float speed;
    public float maxSize;
    public float explosionForce;
    
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
        var rig = other.gameObject.GetComponent<Rigidbody>();
        if (rig == null ) {
            return;
        }

        var direction = other.transform.position - transform.position;
        var force = 1 / direction.magnitude * explosionForce;

        rig.velocity = direction.normalized * force;
    }
}
