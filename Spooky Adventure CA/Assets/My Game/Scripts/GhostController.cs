using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private int direction = 1;
    private float distanceMoved;
    public bool PlayerinArea = false;

    //I tried to use an AI detection for the ghosts, I ran into problems as I wanted a collider to be in the area of the ghosts adn when the player enters, the ghosts get faster
    //and have a sperate animation, but I needed a collider to allow the player to take damage when hit (as I have no projectiles), these overlapped and caused a lot of problems
    //https://www.youtube.com/watch?v=uOobLo2y3KI&t=115s
    //I watched this video but I didnt implement the colliders that way

    
    // Start is called before the first frame update
    void Start()
    {
        //Sets the distance moved to the amount the ghost needs to move
        distanceMoved = distance;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Same movement as the player without the direction
        float moveBy = (speed * Time.deltaTime);
        Vector2 pos = transform.position;
        pos.x = pos.x + moveBy * direction;
        transform.position = pos;
        //track how far the ghost has moved relative to how far it needs to move
        distanceMoved-=moveBy;

        //if the ghost doesnt need to move in that direction any more flip its direction
        if(distanceMoved <= 0)
        {
            direction *= -1;
            distanceMoved = distance;
        }

        //Same as the player, if the direction is 1 flip the animation to the right, if its -1 then flip it to the left
        if(direction>0)
        {
            transform.localScale = Vector3.one;
        }
        else if(direction<0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        //Sets the animation to attack mode if the player is in area
        _animator.SetBool("Attack",PlayerinArea);

    }
    //If the player is in the area then change the animation to attacking
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerinArea = true;

        }
    }

    //Change animation back to previous state if the player exits area
    private void OnTriggerExit2D (Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerinArea = false;
        }
    }
    
}
