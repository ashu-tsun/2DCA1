using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   //A method to allow it to change between scenes on the menu
   public void ChangeScene(string name){
    SceneManager.LoadScene(name);
   }
}
