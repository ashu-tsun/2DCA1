using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float JumpHeight;
    private Rigidbody2D _rigidbody;
    private bool isGrounded = true;
    private bool isHit = false;
    private int jumpCount = 0;
    private int lives = 3;
    private int totalCrystals;
    private int crystalsCollected;
    private bool isAlive = true;
    private Animator _animator;

    [SerializeField] private string detectionTag = "Ghost";
    public GameObject Player;
    public Transform SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        totalCrystals = GameObject.FindGameObjectsWithTag("Crystal").Length;
        UIManager.Instance.setCrystalsCollected(0,totalCrystals);
    }

    // Update is called once per frame
    void Update()
    {
        float moveby =Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        position.x += speed * moveby *Time.deltaTime;
        transform.position = position;


        if(jumpCount <2 && Input.GetKeyDown(KeyCode.Space))
        {
           Jump();

        }

        if (moveby <0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else if (moveby >0)
        {
            transform.localScale = Vector3.one;
        }

        if(lives<=0)
        {
            Die();
        }

        _animator.SetBool("Run",moveby != 0);
        _animator.SetBool("Grounded",isGrounded);
        _animator.SetBool("Hit",isHit);
        
    }

    private void Jump()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(new Vector2(0, Mathf.Sqrt(-2* Physics2D.gravity.y *JumpHeight)), ForceMode2D.Impulse);
        _animator.SetTrigger("Jump");
        isGrounded = false;
        jumpCount++;
    }

        private void miniJump()
    {
        _rigidbody.velocity = Vector3.zero;
       
        _rigidbody.AddForce(new Vector2(0, Mathf.Sqrt(-1* Physics2D.gravity.y *JumpHeight)), ForceMode2D.Impulse);
        _animator.SetTrigger("Jump");
        isGrounded = false;
        jumpCount++;
    }


    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Foreground")
        {
            isGrounded = true;
            jumpCount=0;

        }


    }

        private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.CompareTag("WinCollider") && crystalsCollected == totalCrystals)
        {
            Win();
        }

        else if(collision.CompareTag(detectionTag) || collision.CompareTag("Harmful"))
        {
            isHit = true;
            lives--;
            miniJump();
            UIManager.Instance.updateLives(lives);
        }

        else if(collision.CompareTag("Larry"))
        {
            isHit = true;
            lives-=3;
            miniJump();
            UIManager.Instance.updateLives(lives);
        }
        
    }

        private void OnTriggerExit2D (Collider2D collision)
    {
        if(collision.CompareTag(detectionTag) || collision.CompareTag("Harmful"))
        {
            isHit = false;
        }
    }

    private void Die()
    {
        isAlive = false;
        _animator.SetBool("Alive",isAlive);
        LevelManager.manager.GameOver();
        Destroy(gameObject);
    }

    public void crystalCollected()
    {
        crystalsCollected++;
        UIManager.Instance.setCrystalsCollected(crystalsCollected,totalCrystals);
    }

    private void Win(){
        LevelManager.manager.WinGame();
        Destroy(gameObject);
    }
}
