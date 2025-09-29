using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerController controller;
    private Animator animator;

    public bool isJumping;
    public bool isAttacking;
    public float currentY;
    public bool isDead;
    public bool isAttacked;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckAnimationState();
    }

    private void CheckAnimationState()
    {
        if (isJumping)
        {
            animator.SetTrigger("Jump");
            isJumping = false;
            return;
        }

        if (isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = false;
            return;
        }

        if (isDead)
        {
            animator.SetTrigger("Dead");
            isDead = false;
            return;
        }

        if (isAttacked)
        {
            animator.SetTrigger("Hit");
            isAttacked = false;
            return;
        }

        animator.SetFloat("Velocity Y", controller.rb.velocity.y);
    }
}
