using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

   [SerializeField] TMP_Text crystalsText;
    [SerializeField] private Image[] Images;

    private static UIManager instance;

    private UIManager()
    {

    }
    //Initializes the instance on start
    public void Start()
    {
        instance = this;
    }
    public static UIManager Instance
    {
        get{

            return instance;
        }
    }

    //Updates the crystals collected in the UI
    public void setCrystalsCollected(int numCollected, int total)
    {
        //Set the text to be the number of crystals out of the total
        crystalsText.text = numCollected.ToString() + "/"+total.ToString();
    }

    //Updates the lives in the UI
    public void updateLives(int numLives)
    {
        switch(numLives)
        {
            //Removes a life for each time the player is hit
            case 0:
            Images[0].enabled = false;
            Images[1].enabled = false;
            Images[2].enabled = false;
            break;

            case 1:
            Images[0].enabled = true;
            Images[1].enabled = false;
            Images[2].enabled = false;
            break;

            case 2:
            Images[0].enabled = true;
            Images[1].enabled = true;
            Images[2].enabled = false;
            break;

            case 3:
            Images[0].enabled = true;
            Images[1].enabled = true;
            Images[2].enabled = true;
            break;



        }
    }
}
