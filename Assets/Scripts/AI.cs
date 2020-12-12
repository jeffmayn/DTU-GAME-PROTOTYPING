using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    GameObject player;
    EnemyController enemy;
    Transform target;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {

        bool inRange = Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        if(player != null && inRange)
        {
            GetComponent<EnemyController>().MoveEnemy(player.transform.position);
        }

    }

    private bool Distance()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance;
    }
}
