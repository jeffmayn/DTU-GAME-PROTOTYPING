using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
   // [SerializeField] float speed = 1f;
    [SerializeField] float swordDamage = 5f;

    Rigidbody rigidbdy;
    NavMeshAgent nmAgent;
    Animator animator;

    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbdy = GetComponent<Rigidbody>();
        nmAgent = GetComponent<NavMeshAgent>();        
    }

    public void Stop(bool fieldOfView, bool playerIsAlive)
    {
 
        if (!fieldOfView)
        {

          //  GetComponent<EnemeTaunt>().findTaunt(playerIsAlive, nmAgent);
          findTaunt();
        } else
        {
            nmAgent.isStopped = true;
        }
            
    }
    
    public void findTaunt()
    {

        GameObject tauntPos = GameObject.FindWithTag("EnemyTaunt");
        bool nearByTaunt = Vector3.Distance(transform.position, tauntPos.transform.position) < 1.0f;

        if (nearByTaunt && isAlive)
        {
            nmAgent.isStopped = true;
        }
        else
        {
            nmAgent.destination = tauntPos.transform.position;
        }
    }
    

    // Update is called once per frame
    private void Update()
    {
        if (isAlive)
        {
            Vector3 vel = nmAgent.velocity;
            FlipSprite();
            Run();
        } else
        {
            Stop(false, false);
        }
    }


    public bool isEnemyAlive()
    {
        return isAlive;
    }

    private void FlipSprite()
    {
        bool hasSpeed = Mathf.Abs(nmAgent.velocity.x) > Mathf.Epsilon;
        if (hasSpeed)
        {

            transform.localScale = new Vector3(Mathf.Sign(-nmAgent.velocity.x) * 1.5f, 1.5f, 1f);

        }
    }

    private void Run()
    {

        animator.SetBool("Running", !nmAgent.isStopped);
    }

    public void Die()
    {
        isAlive = false;
        transform.gameObject.tag = "Dead";
        animator.SetTrigger("Die");
    }

    public void Attack(GameObject player, bool canAttack)
    {
        if (canAttack && player.tag == "Player")
        {
            animator.SetTrigger("Attack");

            PlayerController health = target.GetComponent<PlayerController>();
            health.takeDamage(swordDamage);
        }
    }

    public void MoveEnemy(Vector3 dest)
    {

        if (isAlive)
        {
            nmAgent.isStopped = false;
            nmAgent.destination = dest;
        }

        //GetComponent<NavMeshAgent>().destination = target.position;
    }

}
