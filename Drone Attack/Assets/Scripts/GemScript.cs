using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GemScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;

    public GameObject deathMenuUI;

    public float gemMinSpeed = 2f;
    public float gemMaxSpeed = 4f;
    public float gemSpeed;

    public AudioSource gemAudio;
    public Collider2D gemCollider;
    public Rigidbody2D gemRB2D;
    public GameObject gemClone;
    public Animator gem_animator;
    public Rigidbody2D gem_rb2d;

    private void Start()
    {
        gem_animator = GetComponent<Animator>();
        gem_rb2d = GetComponent<Rigidbody2D>();
        gemSpeed = Random.Range(gemMinSpeed, gemMaxSpeed);
    }

    void FixedUpdate()
    {
        gem_rb2d.MovePosition(gem_rb2d.position + Vector2.down * Time.fixedDeltaTime * gemSpeed);



        //if (gem_rb2d.position.y <= 1.95)
        //{
        //    gem_rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
        //    StartCoroutine(waiter());
        //}

        //IEnumerator waiter()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    gem_rb2d.constraints = RigidbodyConstraints2D.None;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !deathMenuUI.activeSelf)
        {
            gem_rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}