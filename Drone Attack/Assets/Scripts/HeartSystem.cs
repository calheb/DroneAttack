using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    public GameObject deathMenuUI;
    [SerializeField] Transform playerSpawnPoint;

    public AudioSource heartAudio;

    public GameObject[] hearts;
    public int life;
    private int maxLife;
    public bool playerDead;

    public AudioClip impact;
    public Animator droneAnimator;
    public Rigidbody2D droneRB2D;
    public Collider2D droneCollider;

    public Animator player_Animator;
    public Rigidbody2D player_RB2D;
    public AudioSource playerAudioSource;
    public Collider2D playerCollider;

    private void Start()
    {
        life = hearts.Length - 5;
        maxLife = life + 5;
        hearts[3].gameObject.SetActive(false);
        hearts[4].gameObject.SetActive(false);
        hearts[5].gameObject.SetActive(false);
        hearts[6].gameObject.SetActive(false);
        hearts[7].gameObject.SetActive(false);
    }
    void Update()
    {
        if (playerDead == true)
        {
            player_RB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            droneRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            player_Animator.SetBool("isDead", true);
            player_Animator.SetInteger("AnimState", 4);
            player_Animator.Play("player_death");
            droneAnimator.Play("drone_death");
            playerAudioSource.PlayOneShot(impact, .003F);
            StartCoroutine(DeathWaiter());
        }
    }

    public void TakeDamage(int d)
    {
        if (life >= 1)
        {
            life -= d;
            hearts[life].gameObject.SetActive(false);
            if (life < 1)
            {
                playerDead = true;
            }
        }
    }

    public void AddLife()
    {
        if (life < maxLife && playerDead == false)
        {
            hearts[life].gameObject.SetActive(true);
            life += 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heart") && life < maxLife && playerDead == false)
        {
            AddLife();
            heartAudio.Play();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Heart") && life == maxLife && playerDead == false)
        {
            heartAudio.Play();
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && !playerDead)
        {
            HeartSystem heartSystem = GetComponent<HeartSystem>();
            heartSystem.TakeDamage(1);
            player_Animator.SetBool("isDead", true);
            player_Animator.SetInteger("AnimState", 4);
            player_Animator.Play("player_death");
            droneAnimator.Play("drone_death");
            playerAudioSource.Play();

            Destroy(collision.gameObject);
            player_Animator.SetInteger("AnimState", 3);
            StartCoroutine(RespawnWaiter());
        }   
    }


    IEnumerator DeathWaiter()
    {
        yield return new WaitForSeconds(0.50f);
        player_RB2D.transform.position = playerSpawnPoint.position;
        droneCollider.enabled = false; //disable  drone collider
        playerCollider.enabled = false; //disable player collider
        deathMenuUI.SetActive(true); //bring up the death menu screen
        Scoring.CurrentScore = 0;
    }

    IEnumerator RespawnWaiter()
    {
        yield return new WaitForSeconds(0.30f);
        player_RB2D.transform.position = playerSpawnPoint.position;
        player_Animator.Play("player_respawn");
        player_Animator.SetInteger("AnimState", 6);
    }
}


