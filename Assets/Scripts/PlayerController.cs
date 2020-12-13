using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
  //  [SerializeField] Transform target;
    [SerializeField] float swordDamage = 25f;
    [SerializeField] Transform target;

    public int maxHealth = 5;
    public int minHealth = 0;
    public int speed = 2;
    public int jumpSpeed = 5;
    [SerializeField] Vector2 dieJump = new Vector2(25f, 25f);

    bool isAlive = true;
    bool canAttack = false;

    Rigidbody rigidbdy;
    Animator animator;
    CapsuleCollider collidr;
    Ray lastRay;

  


    void Start()
    {
        rigidbdy = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        collidr = GetComponent<CapsuleCollider>();

    }

    private void Run()
    {
        float getX = CrossPlatformInputManager.GetAxis("Horizontal");
        float getY = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 playerVel = new Vector3(getX * speed, rigidbdy.velocity.y, getY * speed);
        rigidbdy.velocity = playerVel;

        bool hasSpeed = Mathf.Abs(rigidbdy.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", hasSpeed);

    }

    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            
          //  animator.SetBool("Jumping", true);

            Vector3 jumpVel = new Vector3(0f, jumpSpeed, 0f);
            rigidbdy.velocity += jumpVel;
        }
    }

    private void Update()
    {
        if(!isAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        Jump();
        Die();
        Raycast();
        Attack();
    }

    private void Attack()
    {
        if (canAttack && Input.GetKeyDown(KeyCode.F) && target.tag == "Enemy")
        {
            print("Attacking: " + target);
            animator.SetTrigger("Attack");
            Health health = target.GetComponent<Health>();
            health.TakeDamage(swordDamage);
        }
    }

    private void Raycast()
    {
        Vector3 direction;
        Vector3 origin = collidr.transform.position;

        direction = new Vector3((collidr.transform.localScale.x), 0f, 0f);
        Debug.DrawRay(origin, direction, Color.red);

        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        {
            canAttack = true;
            target = hit.transform;
        }
        else
        {
            canAttack = false;
        }
    }

    private void Die()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            GetComponent<Rigidbody>().velocity = dieJump;
        }
    }

    private void FlipSprite()
    {
        bool hasSpeed = Mathf.Abs(rigidbdy.velocity.x) > Mathf.Epsilon;
        if (hasSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(rigidbdy.velocity.x), 1f, 1f);

        }
    }




}
