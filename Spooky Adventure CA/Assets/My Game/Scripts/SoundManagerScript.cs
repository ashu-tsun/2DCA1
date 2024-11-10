using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip catHit, catJump, catDie, collect, win, npc;
    static AudioSource  source;

    //I used this video: https://www.youtube.com/watch?v=8pFlnyfRfRc
    //It helped add more sounds to specific scenarios and keeps them ogranized
    // Start is called before the first frame update
    void Start()
    {
        //Initializes all audio files in the "Resources" folder
        catHit = Resources.Load<AudioClip> ("catHit");
        catJump = Resources.Load<AudioClip> ("jump");
        catDie = Resources.Load<AudioClip> ("catDie");
        collect = Resources.Load<AudioClip> ("Crystal");
        win = Resources.Load<AudioClip> ("Win");
        npc = Resources.Load<AudioClip> ("npc");

        //Initializes the audio
        source = GetComponent<AudioSource>();
    }

    public static void playSound (string clip)
    {
        //Depedning on which case is called, play a seperate audio file
        switch (clip) {
            case "hit":
            source.PlayOneShot (catHit);
            break;

            case "jump":
            source.PlayOneShot (catJump);
            break;

            case "die":
            source.PlayOneShot (catDie);
            break;

            case "collect":
            source.PlayOneShot (collect);
            break;

            case "npc":
            source.PlayOneShot (npc);
            break;

            case "win":
            source.PlayOneShot (win);
            break;
            }
    }
    
}
