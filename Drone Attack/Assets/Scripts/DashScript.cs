using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public DashState dashState;
    public float dashForce;
    public float dashTimer;
    public float maxDash = 20f;
    Animator animator;

    public Vector2 savedVelocity;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetInteger("AnimState", 5);
        animator.Play("player_teleport");
    }

    void Update()
    {
        var isDashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
        switch (dashState)
        {
            case DashState.Ready:
                if (isDashKeyDown)
                {
                    savedVelocity = rb.velocity;
                    rb.AddForce(new Vector2(rb.velocity.x * dashForce, rb.velocity.y));
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= maxDash)
                {
                    animator.SetInteger("AnimState", 5);
                    animator.Play("player_teleport");
                    dashTimer = maxDash;
                    rb.velocity = savedVelocity;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }
    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
}