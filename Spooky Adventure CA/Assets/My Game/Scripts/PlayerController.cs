using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float JumpHeight;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private AudioSource source;
    private bool isGrounded = true;
    private bool isHit = false;
    private int jumpCount = 0;
    private int lives = 3;
    private int totalCrystals;
    private int crystalsCollected;
           



    // Start is called before the first frame update
    void Start()
    {
        //Initializes various components
        source = GetComponent<AudioSource>(); //The walking sound is in here rather than the audio manager as it worked cleaner in here and didnt repeat
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //Counts my total number of crystals in the level
        totalCrystals = GameObject.FindGameObjectsWithTag("Crystal").Length;
        //Sets the total on the UI to the number collected above
        UIManager.Instance.setCrystalsCollected(0,totalCrystals);
    }

    // Update is called once per frame
    void Update()
    {
        //Ensures the walking audio is not playing from a previous function
        source.Pause();
        //I changed how the animations for the movement would work to better accomodate jumping and hit animations
        // I followed this video to help with the animator tree and adding a jump animation, I couldthen use this to add in my own hit animation
        // https://www.youtube.com/watch?v=Gf8LOFNnils&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=13
        
        //Using 1, 0 or -1 to determine direction, chnage the original postion of the plauer to be the position times the speed, direction and time between frames
        float moveby =Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        position.x += speed * moveby *Time.deltaTime;
        transform.position = position;

        //If the player has not already jumped twice na dpresses space, call the jump method
        if(jumpCount <2 && Input.GetKeyDown(KeyCode.Space))
        {
           Jump();
           //Pause the walking audio
           source.Pause();

        }

        //If moving to the left
        if (moveby <0)
        {
            //This will flip the character to face the left (-1)
            transform.localScale = new Vector3(-1,1,1);
            //Another check to make sure the player isn't jumping
            if(isGrounded)
            {
                //Play the walking audio
                source.Play();
            }
        }
        //If moving to the right
        else if (moveby >0)
        {
            //This will flip the character to face the right (+1)
            transform.localScale = Vector3.one;
            if(isGrounded)
            {
                source.Play();
            }
        }

        //If all lives lost play the die method
        if(lives<=0)
        {
            Die();
        }

        //At every update it will set the boolean of each line to be a value
        //for running, if the moveby is 0, the player isnt moving so run will be false, otherwise play the run animation
        _animator.SetBool("Run",moveby != 0);
        //for the jump animation, if the player is not jumping, grounded will be true so no jump animation, if grounded is false play the jump animation
        _animator.SetBool("Grounded",isGrounded);
        //for the hit animation, if the player has not been hit by a ghost isHit will be flase so the naimation wont play, if the player has been hit then play the hit/hurt animation
        _animator.SetBool("Hit",isHit);
        
    }

    //Jumping method
    private void Jump()
    {   //Resets the players velocity
        _rigidbody.velocity = Vector3.zero;
        //Applies a force, no horizontal force so the player will jump up. Apllies vertical force relative to the gravityand jump height set. Then tells it to apply this instantly
        _rigidbody.AddForce(new Vector2(0, Mathf.Sqrt(-2* Physics2D.gravity.y *JumpHeight)), ForceMode2D.Impulse);
        //In the animator, sets the jump trigger to jump, this helps with transitioning smoothly and instantly between states
        _animator.SetTrigger("Jump");
        //the player is no longer grounded
        isGrounded = false;
        //Increase the jump count
        jumpCount++;
        //In the sound Manager play the jumpsound in the switch
        SoundManagerScript.playSound("jump");
    }

        //Similar to the jump function, this plays a smaller jump when the player takes damage, this ensures they do not sit in the harmful tiles as the damage is only taken once they exit the collider.
        //This idea is from: https://www.youtube.com/watch?v=n5pVDpJ1P2o&t=119
        private void miniJump()
    {
        _rigidbody.velocity = Vector3.zero;
       //Instead of -2 it is -1
        _rigidbody.AddForce(new Vector2(0, Mathf.Sqrt(-1* Physics2D.gravity.y *JumpHeight)), ForceMode2D.Impulse);
  
        isGrounded = false;
        jumpCount++;
    }


    //When the player touches a collider
    private void OnCollisionEnter2D (Collision2D collision)
    {
        //If the collider is tagged as foreground(This is the walkable layer)
        if(collision.gameObject.tag == "Foreground")
        {
            //No longer jumping and reset the jump count
            isGrounded = true;
            jumpCount=0;

        }


    }

    //When the player touches a collider with a trigger
    private void OnTriggerEnter2D (Collider2D collision)
    {
        //A collider is placed in the winning area tagged WinCollider, if this is triggered and the player has collected all crystals
        if(collision.CompareTag("WinCollider") && crystalsCollected == totalCrystals)
        {
            //Call the win method
            Win();
        }

        //If the player hits a collider with the ghost or harmful tag
        else if(collision.CompareTag("Ghost") || collision.CompareTag("Harmful"))
        {
            //Hit becomes true to change the animation and lives are reduced
            isHit = true;
            lives--;
            //If the player isn't killed to this hit, play the hit sound. This ensures the sound isn't played over the death screen
            if(lives!=0)
            {
                SoundManagerScript.playSound("hit");
            }
            //Call a minijump
            miniJump();
            //Update the lives on the UI screen
            UIManager.Instance.updateLives(lives);
        }

        //Larry (skeleton) is a bigger enemy so he will instant kill the player upon hit
        else if(collision.CompareTag("Larry"))
        {
            isHit = true;
            //Takes all lives
            lives-=3;
            UIManager.Instance.updateLives(lives);

        }
        
    }

    //If the player leaves a collider with a trigger
        private void OnTriggerExit2D (Collider2D collision)
    {
        if(collision.CompareTag("Ghost") || collision.CompareTag("Harmful"))
        {
            //Returns the player to the non hit state
            isHit = false;
        }
    }

    //Death method
    private void Die()
    {
        //Play the death sound in the sound manger switch
        SoundManagerScript.playSound("die");
        //Uses the level manager to load the game over screen
        LevelManager.manager.GameOver();
        //Destroys the player so they are unable to continue to move behind the game over screen
        Destroy(gameObject);
    }

    //When a crystal is collected
    public void crystalCollected()
    {
        //Increse the crystals found
        crystalsCollected++;
        //Play the collected sound in the sound manager switch
        SoundManagerScript.playSound("collect");
        //Updates the crystals collected in the UI screen
        UIManager.Instance.setCrystalsCollected(crystalsCollected,totalCrystals);
    }

    //Player has won method
    private void Win(){
        //Uses the level manger to load the win screen
        LevelManager.manager.WinGame();
        //Plays the win sound from the sound manger switch
        SoundManagerScript.playSound("win");
        //Destroys the player so they are unable to continue to move behind the win screen
        Destroy(gameObject);
    }
}
