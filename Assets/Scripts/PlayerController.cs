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

    Rigidbody rigidbody;
    Animator animator;
  


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Run()
    {
        float getX = CrossPlatformInputManager.GetAxis("Horizontal");
        float getY = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 playerVel = new Vector3(getX * speed, rigidbody.velocity.y, getY * speed);
        rigidbody.velocity = playerVel;

        bool hasSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", hasSpeed);

    }

    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            
          //  animator.SetBool("Jumping", true);

            Vector3 jumpVel = new Vector3(0f, jumpSpeed, 0f);
            rigidbody.velocity += jumpVel;
        }
    }

    private void Update()
    {
        Run();
        FlipSprite();
        Jump();

    }

    private void FlipSprite()
    {
        bool hasSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        if (hasSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(rigidbody.velocity.x), 1f, 1f);

        }
    }




}
