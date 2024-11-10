using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This code is from a youtube video about the Parallax background
//https://www.youtube.com/watch?v=zit45k6CUMk
public class Parallax : MonoBehaviour
{
    private float length, startPosition;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        //Length of background
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //temp is how much the background should repeat with the paralax effect
        float temp = (cam.transform.position.x * (1- parallaxEffect));
        //how much the object needs to be offset relative to the camera
        float distance = (cam.transform.position.x * parallaxEffect);
        //Move the background with the added offset each update
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);


        //if the background has moved too far
        if(temp >startPosition + length)
        {
            //set the background to start but to the right with the length of the background 
            startPosition += length;
        }
        //if the background has moved too far to the left
        else if (temp< startPosition - length)
        {
            //set the background to start but to the left with the length of the background 
            startPosition -= length;
        }
        //This will have an endless scroll
    }
}
