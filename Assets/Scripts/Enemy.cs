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

    public Transform shotSpawner;
    public GameObject shotModel;

    public float shotInterval;
    private float currentShotTime = 0f;
    public float shotForce;
    
    public float maxPersuitDistance;
    public float maxAttackDistance;
    public float maxEvadeDistance;

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
                Attack();
                break;
            case EnemyStatus.Evade:
                Evade();
                break;
            default:
                break;
        }
    }

    void Idle()
    {
        var distance = (playerTransform.position - transform.position).magnitude;
        if (distance < maxPersuitDistance)
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

        if (distancia > maxPersuitDistance) {
            GoIdle();
        }

        if (distancia < maxAttackDistance)
        {
            GoAttack();
        }
    }

    void GoIdle()
    {
        status = EnemyStatus.Idle;
        mesh.material = idleMaterial;
    }

    void GoAttack()
    {
        status = EnemyStatus.Attack;
        mesh.material = attackMaterial;
    }

    void Attack()
    {
        var vetor = playerTransform.position - transform.position;
        var distancia = vetor.magnitude;
        var direcao = vetor.normalized;

        // Atacar
        if (currentShotTime <= 0)
        {
            // Atirar
            GameObject morteiroGO = Instantiate(shotModel, shotSpawner.position, Quaternion.identity);
            Rigidbody morteiroRB = morteiroGO.GetComponent<Rigidbody>();
            morteiroRB.AddForce(direcao * shotForce);

            // Atualizar o cronometro
            currentShotTime = shotInterval;
        }

        currentShotTime -= Time.deltaTime;
        
        // Preciso perseguir
        if (distancia > maxAttackDistance)
        {
            GoPursuit();
        }
        // Preciso fugir
        if (distancia < maxEvadeDistance)
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
        var vetor = playerTransform.position - transform.position;
        var distancia = vetor.magnitude;
        var direcao = vetor.normalized;

        body.AddForce(-direcao * speed * Time.deltaTime);

        if (distancia >= maxEvadeDistance)
        {
            GoAttack();
        }
    }
}
