using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2d;
    public CircleCollider2D droneCollider;

    void FixedUpdate()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            //Debug.Log("drone hit the ground");
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll; //get rb2d for drone
            animator.Play("drone_death");
            StartCoroutine(waiter());
        }
    }
    IEnumerator waiter()
    {
        //Debug.Log("Waiting!!!");
        yield return new WaitForSeconds(0.5f);
        GetComponent<Collider2D>().enabled = false; //disable hit box
    }
}



