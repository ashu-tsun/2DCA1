using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NpcController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject continueButton;
    public Text dialogueText;
    public GameObject hint;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;

    //I wanted a hint system/ guide so I added a frog npc, I used a video to add the dialogue box and input to "talk" to the character
    //This video is from: https://www.youtube.com/watch?v=1nFNOyCalzo
    // Update is called once per frame
    void Update()
    {
        //If the player is in range then display the hint "Press E to talk"
        if(playerIsClose)
        {
            hint.SetActive(true);
        }
        
        //If the player presses e and is in range of the npc
        if(Input.GetKeyDown(KeyCode.E) &&playerIsClose == true)
        {
            //Play the npc sound in nsound manager
            SoundManagerScript.playSound("npc");
            //Stop showing the hint
            hint.SetActive(false);
            //If the panel is visible call zeroText
            if(dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                //If the panel isnt visible, set it to visible and start typing/ displaying text
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        //If the full text is displayed then show the continue button
        if(dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    //Reset the dialogue box, index and hide the box
    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    //Method to show the text appear slowly
    IEnumerator Typing() 
    {
        //For every character in the text of the set line
        foreach(char letter in dialogue[index].ToCharArray())
        {
            //Display a letter
            dialogueText.text += letter;
            //Wait for a few seconds depending on the word speed
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    //To move to the next line
    public void NextLine()
    {
        //Play the sound again
        SoundManagerScript.playSound("npc");
        //Hide the continue button
        continueButton.SetActive(false);
        //If not finished all dialogue
        if(index<dialogue.Length -1)
        {
            //Incrase index, clear the dialogue box and start typing the new line
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        //If at end clear everything
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //If entering the npc collider as the player, set player in range
        if(other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
        private void OnTriggerExit2D(Collider2D other)
    {
        //If the player leaves the range, set range to false, hide the hint button and hide the dialogue panel
        if(other.CompareTag("Player"))
        {
            playerIsClose = false;
            hint.SetActive(false);
            zeroText();
        }
    }
}
