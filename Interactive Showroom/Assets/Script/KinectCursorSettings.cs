using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class KinectCursorSettings : MonoBehaviour
{

    // Public variables
    public GameObject earth;
    public GameObject cursor;


    // Sprite
    private SpriteRenderer rend;
    public Sprite mainCursor;
    public Sprite dragCursor;


    // Private variables
    float rotSpeed = 10f;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;


    // Kinect Data
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;



    //
    void Start(){
        rend = cursor.GetComponent<SpriteRenderer>();

        if(BodySrcManager == null){
            Debug.Log("add Body Source Manager");
        }else{
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }
    }



    // Update is called once per frame
    void Update(){

      if(bodyManager == null){
            return;
        }

        bodies = bodyManager.GetData();

        if(bodies == null){
            return;
        }

        foreach(var body in bodies){

            if(body == null){
                continue;
            }

            float distance = 1000;
            float distanceNew = body.Joints[JointType.Head].Position.Z;

            if (distanceNew !=0 && distanceNew <= distance){
                distance = distanceNew;

              if(body.IsTracked){

                //Joint variables
                var handLeft = body.Joints[JointType.HandLeft];
                var handRight = body.Joints[JointType.HandRight];

                //
                if(handLeft.Position.Y > handRight.Position.Y && body.HandLeftState == HandState.Closed
                || handRight.Position.Y > handLeft.Position.Y && body.HandRightState == HandState.Closed){    // Replace with if(handState == "closed")
                  
                  //
                  rend.sprite = dragCursor;

                  // position = aktuelle position - vorhergehende position
                  mPosDelta = cursor.transform.position - mPrevPos;

                  //
                  if(Vector3.Dot(transform.up, Vector3.up) >= 0){

                    //
                    earth.transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right * rotSpeed), Space.World);
                  }else{

                    //
                    earth.transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right * rotSpeed), Space.World);
                  }

                  //
                  earth.transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up * rotSpeed), Space.World);

                }else{

                  //
                  rend.sprite = mainCursor;
                }

                //
                mPrevPos = cursor.transform.position;
              }
            }
        }
    }
}

