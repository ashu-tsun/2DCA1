using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

   [SerializeField] TMP_Text presentsText;
    [SerializeField] private Image[] Images;

    private static UIManager instance;

    private UIManager()
    {

    }

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

    public void setCollected(int numCollected, int total)
    {
        //presentsText.text = numCollected.ToString() + "/"+total.ToString();
    }

    public void updateLives(int numLives)
    {
        switch(numLives)
        {
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
