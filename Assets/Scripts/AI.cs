using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] float fieldOfView = 5f;
    [SerializeField] float stoppingRange = 1.2f;
    [SerializeField] float attackRestingTime = 2f;

    GameObject player;
    EnemyController enemy;
    float lastAttack = 0f;
    bool canAttack = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        lastAttack += Time.deltaTime; // make the enemy pause between attack-strokes
        bool playerisAlive = player.GetComponent<PlayerController>().isPlayerAlive();
        bool inFieldOfView = Vector3.Distance(transform.position, player.transform.position) < fieldOfView;
        bool inStoppingRange = Vector3.Distance(transform.position, player.transform.position) < stoppingRange;
        bool attackingRange = inFieldOfView && Vector3.Distance(transform.position, player.transform.position) <= 2;

        // moves enemy towards player when he gets in their field of view
        // and stops the enemy when he is in front of the player (so the sprites dont overlap)
        if(inFieldOfView && !inStoppingRange)
        {
            GetComponent<EnemyController>().MoveEnemy(player.transform.position);

        } else
        {
           
            GetComponent<EnemyController>().Stop(inFieldOfView, playerisAlive);
        }

        // Attack player is only possible if hes alive
        if(transform.gameObject.tag != "Dead")
        {
            canAttack = true;
        } else
        {
            canAttack = false;
        }
        
        // if enemy is withing line of sight, attacking range and last-attack-pause is over, he will attack player
        if (playerisAlive && lastAttack > attackRestingTime && inStoppingRange && inFieldOfView)
        {
            GetComponent<EnemyController>().Attack(player, canAttack);
            lastAttack = 0;
        }

    }

    // return true if player is in enemys field of view
    private bool Distance()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < fieldOfView;
    }



}
