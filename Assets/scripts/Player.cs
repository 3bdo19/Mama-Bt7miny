using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

   private Rigidbody2D rb;
   private float xinput;
   [SerializeField] private int movespeed;
   [SerializeField] private float grounddistance;
   [SerializeField] private LayerMask WhatIsGround;
   private bool IsGrounded;
   [SerializeField] private float jumpforce = 8;
   private bool IsMoving;
   protected Animator anim;
   protected bool facingRight = true;
   private int FacingDir = 1;
   protected bool CanMove;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
      GroundCollision();
      input();
      movement();
      HandleAnimations();
      HandleFlip();
    }
    

    private void HandleAnimations()
    {
      anim.SetBool("IsGrounded", IsGrounded);
      anim.SetFloat("xvelocity", rb.linearVelocity.x);
      anim.SetFloat("yvelocity", rb.linearVelocity.y);
    }
    private void input()
    {
      xinput = Input.GetAxisRaw("Horizontal"); 

      if (Input.GetKeyDown(KeyCode.Space))
       jump();
    }

    private void movement()
    {
      rb.linearVelocity = new UnityEngine.Vector2(xinput * movespeed, rb.linearVelocity.y);
    }

    private void GroundCollision()
    {
      IsGrounded = Physics2D.Raycast(transform.position, UnityEngine.Vector2.down, grounddistance, WhatIsGround);
    }

    private void OnDrawGizmos()
    {
    Gizmos.DrawLine(transform.position, transform.position + new UnityEngine.Vector3(0, -grounddistance));
    }

    private void jump()
  {
    if (IsGrounded == true)
      rb.linearVelocity = new UnityEngine.Vector2(rb.linearVelocity.x, jumpforce);
  }

  protected void HandleFlip()
  {
    if (rb.linearVelocity.x > 0 && facingRight == false)
    {
      flip();
    }
    else if(rb.linearVelocity.x < 0 && facingRight == true)
    {
      flip();
    }
  }

  private void flip()
  {
    transform.Rotate(0, 180, 0);
    facingRight = !facingRight;
    FacingDir = FacingDir * -1;
  }
}

