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
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1- parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if(temp >startPosition + length)
        {
            startPosition += length;
        }
        else if (temp< startPosition - length)
        {
            startPosition -= length;
        }
    }
}
