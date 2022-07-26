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

        if (m_body2d.position.y <= 2.10)
        {
            m_body2d.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(waiter());
        }


        IEnumerator waiter()
        {
            yield return new WaitForSeconds(0.50f);
            GetComponent<Collider2D>().enabled = false; //disable hit box
            m_animator.Play("drone_death");
        }
    }
}