using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemeTaunt : MonoBehaviour
{

    [SerializeField] GameObject taunt;

    public void findTaunt(bool isAlive, NavMeshAgent nmAgent)
    {

      //  GameObject tauntPos = GameObject.FindWithTag("EnemyTaunt");
        bool nearByTaunt = Vector3.Distance(transform.position, taunt.transform.position) < 1.0f;

        if (nearByTaunt && isAlive)
        {
            nmAgent.isStopped = true;
        }
        else
        {
            nmAgent.destination = taunt.transform.position;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
