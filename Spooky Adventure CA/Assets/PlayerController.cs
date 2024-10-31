using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    
    [SerializeField] private float speed;
    [SerializeField] private float JumpHeight;
    private Rigidbody2D _rigidbody;
    private bool isJumping = false;
    private int jumpCount = 0;
     private int direction = 1;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator.SetFloat("Move X",0);
        _animator.SetFloat("Move Y",1);
    }

    // Update is called once per frame
    void Update()
    {
        float moveby =Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        position.x += speed * moveby *Time.deltaTime;
        transform.position = position;

        if(moveby != 0)
        {
            direction = moveby <0 ? -1:1 ;
        }
        if(jumpCount <2 && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(new Vector2(0, Mathf.Sqrt(-2* Physics2D.gravity.y *JumpHeight)), ForceMode2D.Impulse);
            isJumping = true;
            jumpCount++;

        }
        playAnimation(moveby);

    }

    private void playAnimation(float moveby){
        if(moveby ==0)
        {
            _animator.SetFloat("Move X", 0);
        }
        else if (moveby <0)
        {
            _animator.SetFloat("Move X", -1);
            _animator.SetFloat("Move Y", -1);
        }
        else {
            _animator.SetFloat("Move X", 1);
            _animator.SetFloat("Move Y", 1);
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(isJumping)
        {
            isJumping = false;
            jumpCount=0;
        }
    }

}
