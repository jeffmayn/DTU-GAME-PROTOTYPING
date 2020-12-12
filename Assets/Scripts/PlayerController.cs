using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    public int maxHealth = 5;
    public int minHealth = 0;
    public int speed = 2;
    public int jumpSpeed = 5;
    bool isAlive = true;

    Rigidbody rigidbdy;
    Animator animator;
    CapsuleCollider collidr;

  


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

    }

    private void Die()
    {
        
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
