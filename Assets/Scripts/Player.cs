using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rig;
    public GameObject granadaModelo;

    public float turnSpeed;
    public float moveSpeed;
    public float jumpForce;

    private bool taNoChao = false;

    void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
        transform.position += transform.forward * vertical * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && taNoChao)
        {
            rig.velocity = Vector3.up * jumpForce;
            taNoChao = false;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(granadaModelo);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        taNoChao = true;
    }
}
