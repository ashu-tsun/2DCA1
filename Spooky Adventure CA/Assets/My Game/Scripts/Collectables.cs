using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    
    [SerializeField] PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        //assignes the player to "player"
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When the player interacts with a collectable
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check the tag to make sure its the player
        if(collision.gameObject.tag == "Player" && this.tag== "Crystal")
        {
            //Destroy the collectable
            Destroy(this.gameObject);
            //Calll the collected method to increase the collected crystals 
            player.crystalCollected();
        }
    }
}
