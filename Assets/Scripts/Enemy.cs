using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStatus
{
    Idle,
    Pursuit,
    Attack,
    Evade
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Enemy : MonoBehaviour
{
    Rigidbody body;
    MeshRenderer mesh;
    public Transform playerTransform;
    public float speed;
    public EnemyStatus status = EnemyStatus.Idle;
    public Material idleMaterial;
    public Material persuitMaterial;
    public Material attackMaterial;
    public Material evadeMaterial;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        switch (status)
        {
            case EnemyStatus.Idle:
                Idle();
                break;
            case EnemyStatus.Pursuit:
                Pursuit();
                break;
            case EnemyStatus.Attack:
                break;
            case EnemyStatus.Evade:
                break;
            default:
                break;
        }
    }

    void Idle()
    {
        var distance = (playerTransform.position - transform.position).magnitude;
        if (distance < 10)
        {
            GoPursuit();
        }
    }

    void GoPursuit()
    {
        status = EnemyStatus.Pursuit;
        mesh.material = persuitMaterial;
    }

    void Pursuit()
    {
        var vetor = playerTransform.position - transform.position;
        var direcao = vetor.normalized;
        var distancia = vetor.magnitude;

        body.AddForce(direcao * speed * Time.deltaTime);

        if (distancia > 10) {
            GoIdle();
        }
    }

    void GoIdle()
    {
        status = EnemyStatus.Idle;
        mesh.material = idleMaterial;
    }

    void Attack()
    {
        // Atacar

        var vetor = playerTransform.position - transform.position;
        var distancia = vetor.magnitude;
        // Preciso perseguir
        if (distancia > 5)
        {
            GoPursuit();
        }
        // Preciso fugir
        if (distancia < 2)
        {
            GoEvade();
        }
    }

    void GoEvade()
    {
        status = EnemyStatus.Evade;
        mesh.material = evadeMaterial;
    }

    void Evade()
    {

    }
}
