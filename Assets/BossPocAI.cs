using UnityEngine;
using System.Collections;

public class BossPocAI: MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float chargeSpeed = 200f;
    public float detectionRange = 5f;
    public int maxHealth = 100;

    private int currentHealth;
    private int currentPatrolIndex = 0;
    private enum Phase { Patrol, Charge, Frenzy }
    private Phase currentPhase = Phase.Patrol;
    private Transform player;
    private Vector3 chargeTarget;
    private bool isCharging = false;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        UpdatePhase();

        switch (currentPhase)
        {
            case Phase.Patrol:
                Patrol();
                DetectPlayer();
                break;
            case Phase.Charge:
                if (!isCharging)
                    StartCharge();
                break;
            case Phase.Frenzy:
                if (!isCharging)
                    StartCharge();
                break;
        }
    }

    void UpdatePhase()
    {
        if (currentHealth > maxHealth * 0.6f)
            if (currentPhase != Phase.Charge && currentPhase != Phase.Frenzy)
                currentPhase = Phase.Patrol;
        else if (currentHealth > maxHealth * 0.3f)
            currentPhase = Phase.Charge;
        else
            currentPhase = Phase.Frenzy;
    }

    void Patrol()
    {
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            currentPhase = Phase.Charge;
        }
    }

    void StartCharge()
    {
        StartCoroutine(ChargeRoutine());
    }

    IEnumerator ChargeRoutine()
    {
        isCharging = true;

        // 1. Wind-up
        yield return new WaitForSeconds(2);

        // 2. Determine target based on fixed distance in direction of player
        Vector3 direction = (player.position - transform.position).normalized;
        chargeTarget = player.transform.position + direction * 5;

        // 3. Charge movement
        while (Vector3.Distance(transform.position, chargeTarget) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, chargeTarget, chargeSpeed * (Time.deltaTime * 4));
            yield return null;
        }

        isCharging = false;
    }


    //void Charge()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, chargeTarget, chargeSpeed * Time.deltaTime);

    //    if (Vector3.Distance(transform.position, chargeTarget) < 0.1f)
    //        isCharging = false; // finished charge
    //}

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0) Destroy(gameObject);
    }
}
