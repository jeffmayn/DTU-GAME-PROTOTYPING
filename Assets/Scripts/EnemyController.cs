using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 1f;

   // Rigidbody rigidbdy;
    NavMeshAgent nmAgent;

    // Start is called before the first frame update
    void Start()
    {
       // rigidbdy = GetComponent<Rigidbody>();
        nmAgent = GetComponent<NavMeshAgent>();        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vel = nmAgent.velocity;
       
    }

    public void MoveEnemy(Vector3 dest)
    {
        nmAgent.destination = dest;

        //GetComponent<NavMeshAgent>().destination = target.position;
    }

}
