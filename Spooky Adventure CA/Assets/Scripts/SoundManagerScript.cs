using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip catHit, catJump, catDie, collect, win, npc;
    static AudioSource  source;
    // Start is called before the first frame update
    void Start()
    {
        catHit = Resources.Load<AudioClip> ("catHit");
        catJump = Resources.Load<AudioClip> ("jump");
        catDie = Resources.Load<AudioClip> ("catDie");
        collect = Resources.Load<AudioClip> ("Crystal");
        win = Resources.Load<AudioClip> ("Win");
        npc = Resources.Load<AudioClip> ("npc");

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void playSound (string clip)
    {
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
