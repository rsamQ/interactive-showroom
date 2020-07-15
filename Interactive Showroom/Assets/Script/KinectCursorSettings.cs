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



  // Called once at Start of Game
  void Start(){

    // Get SpriteRenderer from cursor
    rend = cursor.GetComponent<SpriteRenderer>();

    // Get Body Source Manager Data
    if(BodySrcManager == null){
        Debug.Log("add Body Source Manager");
    }else{
        bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
    }
  }



  // Update is called once per frame
  void Update(){

    // Check if BodyManager is available
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

        // Set default distance
        float distance = 1000;

        // Set 2nd distance to tracked persons head position
        float distanceNew = body.Joints[JointType.Head].Position.Z;

        //
        if (distanceNew !=0 && distanceNew <= distance){
            distance = distanceNew;

          if(body.IsTracked){

            // Joint variables
            var handLeft = body.Joints[JointType.HandLeft];
            var handRight = body.Joints[JointType.HandRight];

            // One hand over the other and hand is closed
            if(handLeft.Position.Y > handRight.Position.Y && body.HandLeftState == HandState.Closed
            || handRight.Position.Y > handLeft.Position.Y && body.HandRightState == HandState.Closed){
              
              // Drag sprite on drag gesture
              rend.sprite = dragCursor;

              // position = current position - previous position
              mPosDelta = cursor.transform.position - mPrevPos;

              // 
              if(Vector3.Dot(transform.up, Vector3.up) >= 0){
                earth.transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right * rotSpeed), Space.World);
              }else{
                earth.transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right * rotSpeed), Space.World);
              }
              earth.transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up * rotSpeed), Space.World);
            }else{

              // Standard Cursor if no drag gesture
              rend.sprite = mainCursor;
            }

          // new previous position = current position
          mPrevPos = cursor.transform.position;
        }
      }
    }
  }
}

