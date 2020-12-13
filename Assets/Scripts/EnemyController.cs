using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 1f;

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

    public void Stop()
    {
        nmAgent.isStopped = true;
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
            Stop();
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
            print("Attacking: " + target);
            animator.SetTrigger("Attack");

            //animator.SetTrigger("Attack");
            PlayerController health = target.GetComponent<PlayerController>();
            health.takeDamage(10f);
        }
    }

    public void MoveEnemy(Vector3 dest)
    {
        
        nmAgent.isStopped = false;
        nmAgent.destination = dest;

        //GetComponent<NavMeshAgent>().destination = target.position;
    }

}
