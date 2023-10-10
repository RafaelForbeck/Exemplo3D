using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morteiro : MonoBehaviour
{
    public GameObject explosao;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosao, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
