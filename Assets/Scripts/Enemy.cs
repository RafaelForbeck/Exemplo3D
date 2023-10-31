using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStatus
{
    Idle,
    Run,
    Pursuit,
    Attack,
    Evade
}

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    Rigidbody body;
    public Transform playerTransform;
    public float speed;
    public EnemyStatus status = EnemyStatus.Idle;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        switch (status)
        {
            case EnemyStatus.Idle:
                Idle();
                break;
            case EnemyStatus.Run:
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
            status = EnemyStatus.Pursuit;
        }
    }

    void Run()
    {

    }

    void Pursuit()
    {
        var direcao = (playerTransform.position - transform.position).normalized;
        body.AddForce(direcao * speed * Time.deltaTime);
    }

    void Attack()
    {

    }

    void Evade()
    {

    }
}
