using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;
    void Start()
    {
        //set position of platfrom to the position of the first point
        transform.position = points[startingPoint].position;
    }

    //This code is from: https://www.youtube.com/watch?v=GtX1p4cwYOc
    // Update is called once per frame
    void Update()
    {
        //Checks the distance between the points, if the distance is less than a small pixel value
        if(Vector2.Distance(transform.position, points[i].position)< 0.02f)
        {
            //Increase the index
            i++;
            //Checks if the index has reached the last point (kind of unecessary with only 2)
            if(i == points.Length)
            {
                //reset the index
                i=0;
            }
        }
        //moves the platform towards the next point
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    //When the player collides with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //This allows the platform to move the player with it
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Removes the player being moved with the platform
        collision.transform.SetParent(null);
    }

}
