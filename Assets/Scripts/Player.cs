using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rig;
    public GameObject granadaModelo;
    public Transform posicaoTiro;

    public float turnSpeed;
    public float moveSpeed;
    public float jumpForce;
    public float granadeForce;

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
            GameObject granadaInstanciada = Instantiate(granadaModelo, posicaoTiro.position, Quaternion.identity);
            Rigidbody rigGranada = granadaInstanciada.GetComponent<Rigidbody>();
            Vector3 direction = transform.forward + Vector3.up;
            rigGranada.AddForce(direction * granadeForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        taNoChao = true;
    }
}
