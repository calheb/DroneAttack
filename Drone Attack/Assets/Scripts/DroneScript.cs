using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DroneScript : MonoBehaviour
{
    public Animator drone_animator;
    public Rigidbody2D drone_rb2d;

    public float minSpeed = 5f;
    public float maxSpeed = 10f;
    public float speed;

    public AudioSource audioSource;

    private void Start()
    {
        drone_animator = GetComponent<Animator>();
        drone_rb2d = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
    }
    void FixedUpdate()
    {
        drone_rb2d.MovePosition(drone_rb2d.position + Vector2.down * Time.fixedDeltaTime * speed);

        if (drone_rb2d.position.y <= 1.95)
        {
            drone_rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(waiter());
        }


        IEnumerator waiter()
        {
            yield return new WaitForSeconds(0.50f);
            GetComponent<Collider2D>().enabled = false; //disable hit box
            drone_animator.Play("drone_death");
        }
    }
}