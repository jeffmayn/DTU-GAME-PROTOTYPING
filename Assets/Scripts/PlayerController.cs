using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
  //  [SerializeField] Transform target;
    [SerializeField] float swordDamage = 25f;
    [SerializeField] Transform target;

    public float maxHealth = 5.0f;
    public float minHealth = 0.0f;
    public int speed = 2;
    public int jumpSpeed = 5;
    //[SerializeField] Vector2 dieJump = new Vector2(25f, 25f);

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

        // calculates player velocity
        Vector3 playerVel = new Vector3(getX * speed, rigidbdy.velocity.y, getY * speed);
        rigidbdy.velocity = playerVel;

        // returns true if the player has a movement speed greater than zero, then actives the runnin animation
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

    public bool isPlayerAlive()
    {
        return isAlive;
    }

    private void Update()
    {
        // if player is dead, we disable all controls of the player
        if(!isAlive) {  return; }

        Run();
        FlipSprite();
        Jump();
        Raycast();
        Attack();
    }


    public void takeDamage(float dmg)
    {
        // Calculates the players health (and avoids negative numbers)
        maxHealth = Mathf.Max(maxHealth - dmg, 0);

        // Damage the player
        FindObjectOfType<GameManager>().DecrementHP(dmg);

        // player dies when health reaches zero
        if (maxHealth <= 0) { Die(); }
    }

    private void Attack()
    {
        if (canAttack && Input.GetKeyDown(KeyCode.F) && target.tag == "Enemy")
        {
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

    public void Die()
    {
        isAlive = false;
        animator.SetTrigger("Dying");
      //  GetComponent<Rigidbody>().velocity = dieJump;
        FindObjectOfType<GameManager>().playerDeath();
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
