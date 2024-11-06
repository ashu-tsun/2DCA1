using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private string detectionTag = "Player";
    private int direction = 1;
    private float distanceMoved;
    public bool PlayerinArea = false;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        distanceMoved = distance;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveBy = (speed * Time.deltaTime);
        Vector2 pos = transform.position;
        pos.x = pos.x + moveBy * direction;
        transform.position = pos;
        distanceMoved-=moveBy;

        if(distanceMoved <= 0)
        {
            direction *= -1;
            distanceMoved = distance;
        }
        if(direction>0)
        {
            transform.localScale = Vector3.one;
        }
        else if(direction<0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        _animator.SetBool("Attack",PlayerinArea);
        //_animator.SetBool("Hit",hit);

    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.CompareTag(detectionTag))
        {
            PlayerinArea = true;
            speed+=2;

        }
    }

        private void OnTriggerExit2D (Collider2D collision)
    {
        if(collision.CompareTag(detectionTag))
        {
            PlayerinArea = false;
            speed-=2;
        }
    }
    
}
