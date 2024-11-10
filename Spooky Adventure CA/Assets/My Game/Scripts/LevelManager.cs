using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager manager;
    public GameObject DeathScreen;
    public GameObject WinScreen;
    

    //The level and menu manager were done with the help of a video, this also allowed me to create my own win screen
    //https://www.youtube.com/watch?v=L_GPgTeTpZI
    //Sets the instance when started
    private void Awake(){
        manager = this;
    }

    //Game over screen is set to active
    public void GameOver(){
        DeathScreen.SetActive(true);  
    }

    //When called it will reload the scene to the beginning
    public void ReplayGame(){
        
        SceneManager.LoadScene(1);
    }

    //Game won screen is set to active
    //Win screen uses a collider (in Playercontroller) to be called when the player wins
    public void WinGame(){
        WinScreen.SetActive(true);
    }
}
