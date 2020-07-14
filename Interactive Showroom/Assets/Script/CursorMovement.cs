using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class CursorMovement : MonoBehaviour
{


    // Sprite variables
    public Sprite mainCursor;
    public Sprite handCursor;
    private SpriteRenderer rend;
    public GameObject obj;


    
    // Kinect Data variables
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    private float multiplier = 1000.0f;



    //  
    void Start()
    {
        // Hide Cursor on Start and set cursor sprite
        Cursor.visible = false;
        rend = obj.GetComponent<SpriteRenderer>();





        // Get Body Source Manager Data
        if (BodySrcManager == null)
        {
            Debug.Log("add Body Source Manager");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }
    }



    // Update is called once per frame
    void Update()
    {
        TrackHandCursor();
    }



    void TrackHandCursor()
    {

        if (bodyManager == null)
        {
            return;
        }

        bodies = bodyManager.GetData();

        if (bodies == null)
        {
            return;
        }

        foreach (var body in bodies)
        {

            if (body == null)
            {
                continue;
            }

            float distance = 1000;
            float distanceNew = body.Joints[JointType.Head].Position.Z;

            if (distanceNew != 0 && distanceNew <= distance)
            {
                distance = distanceNew;

                if (body.IsTracked)
                {

                    //Joint variables
                    var handLeft = body.Joints[JointType.HandLeft];
                    var handRight = body.Joints[JointType.HandRight];

                    if (handLeft.Position.Y > handRight.Position.Y)
                    {
                        Movement(handLeft.Position.X, handLeft.Position.Y);
                    }
                    else if (handRight.Position.Y > handLeft.Position.Y)
                    {
                       Movement(handRight.Position.X, handRight.Position.Y);
                    }
                }
            }
        }
    }



    void Movement(float x, float y)
    {

        // Cursor on mouse position
        Vector3 mousePos = new Vector3(x * multiplier, y * multiplier, -10.0f);
        transform.position = mousePos;
    }







}





