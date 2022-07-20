using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroneScript : MonoBehaviour
{
    private Animator m_animator;
    private Rigidbody2D m_body2d;

    public float minSpeed = 5f;
    public float maxSpeed = 10f;

    public float speed;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

        speed = Random.Range(minSpeed, maxSpeed);
    }
    void FixedUpdate()
    {
        m_body2d.MovePosition(m_body2d.position + Vector2.down * Time.fixedDeltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit Detected.");
        m_animator.Play("drone_death");
        m_body2d.constraints = RigidbodyConstraints2D.FreezeAll;

        //disable the enemy
        GetComponent<Collider2D>().enabled = false; //disable hit box
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    m_animator.Play("drone_death");
    //    //Destroy(gameObject, 0.5f);
    //}
}
