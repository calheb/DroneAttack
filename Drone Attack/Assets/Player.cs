using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;


    public LayerMask enemyLayer;
    private Animator m_animator;
    public Rigidbody2D m_body2d;
    private GroundChecker m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    public Animator DroneAnimator;
    public Rigidbody2D droneRB2D;
    public Collider2D droneCollider;
    public AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<GroundChecker>();
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.tag == "Enemy")
        {
            
            Debug.Log("WE LOST");
            m_body2d.constraints = RigidbodyConstraints2D.FreezeAll;
            droneRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            m_animator.SetBool("isDead", true);
            m_animator.SetInteger("AnimState", 4);
            m_animator.Play("player_death");
            DroneAnimator.Play("drone_death");
            audioSource.Play();
            StartCoroutine(waiter());
        }
    }
    void Update()
    {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_animator.SetInteger("AnimState", 5);
            m_animator.SetBool("teleporting", true);
        }

        //Jump
        if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetInteger("AnimState", 3);
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else if (Mathf.Abs(inputX) <= Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 0);

        if (m_body2d.position.y < -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    IEnumerator waiter()
    {
        //Debug.Log("Waiting!!!");
        yield return new WaitForSeconds(0.25f);
        droneCollider.enabled = false; //disable hit box
        if ((m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) && DroneAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            Debug.Log("not playing");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    
}
