using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager manager;
    public GameObject DeathScreen;
    public GameObject WinScreen;
    
    private void Awake(){
        manager = this;
    }

    public void GameOver(){
        DeathScreen.SetActive(true);  
    }

    public void ReplayGame(){
        
        SceneManager.LoadScene(1);
    }

    public void WinGame(){
        WinScreen.SetActive(true);
    }
}
