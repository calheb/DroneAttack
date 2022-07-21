using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerTrigger : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rb2d;
    public CircleCollider2D droneCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll; //get rb2d for drone
            //Debug.Log("drone hit the ground");
            animator.Play("drone_death");
            droneCollider.enabled = false; //disable hit box
        }
        else if (collision.tag == "Player")
        {
            Debug.Log("drone hit the player");
        }
    }
}
