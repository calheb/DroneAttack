using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DroneScript : MonoBehaviour
{
    public Animator m_animator;
    public Rigidbody2D m_body2d;

    public float minSpeed = 5f;
    public float maxSpeed = 10f;
    public LayerMask playerLayer;
    public float speed;

    public AudioSource audioSource;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

        speed = Random.Range(minSpeed, maxSpeed);
    }
    void FixedUpdate()
    {
        m_body2d.MovePosition(m_body2d.position + Vector2.down * Time.fixedDeltaTime * speed);

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        //if (hit.collider.CompareTag("Ground"))
        //{
        //    //Debug.Log("drone hit the ground");
        //    m_animator.Play("drone_death");
        //    m_body2d.constraints = RigidbodyConstraints2D.FreezeAll;
        //    StartCoroutine(waiter());
        //}

        if (m_body2d.position.y <= 2.10)
        {
            m_body2d.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(waiter());
        }


        IEnumerator waiter()
        {
            //Debug.Log("Waiting!!!");
            //audioSource.Play();
            yield return new WaitForSeconds(0.75f);
            m_animator.Play("drone_death");
            GetComponent<Collider2D>().enabled = false; //disable hit box
        }
    }

//    private void OnTriggerEnter2D(Collider2D playerLayer)
//    {
//        m_animator.Play("drone_death");
//        m_body2d.constraints = RigidbodyConstraints2D.FreezeAll;

//        //disable the enemy
//        GetComponent<Collider2D>().enabled = false; //disable hit box
//    }
}