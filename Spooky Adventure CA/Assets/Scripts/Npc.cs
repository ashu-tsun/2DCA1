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

    // Update is called once per frame
    void Update()
    {
        if(playerIsClose)
        {
            hint.SetActive(true);
        }
        
        if(Input.GetKeyDown(KeyCode.E) &&playerIsClose == true)
        {
            SoundManagerScript.playSound("npc");
            hint.SetActive(false);
            if(dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if(dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing() 
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        SoundManagerScript.playSound("npc");
        continueButton.SetActive(false);
        if(index<dialogue.Length -1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
        private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsClose = false;
            hint.SetActive(false);
            zeroText();
        }
    }
}
