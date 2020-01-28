using UnityEngine;
using System.Collections;

//using Microsoft.Kinect;
using Windows.Kinect;
//using System;

public class SwipeLeft : MonoBehaviour {
    
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;

    //segment variables
    public bool swipeSegment1;
    public bool swipeSegment2;
    public bool swipeComplete;

    //rotation variables
    private Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);
    private float swipeSpeed = 5f;

    void Start(){
        if(BodySrcManager == null){
            Debug.Log("add Body Source Manager");
        }else{
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }
    }

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

            if(body.IsTracked){

                //Joint variables
                var handLeft = body.Joints[JointType.HandLeft];
                var handRight = body.Joints[JointType.HandRight];
                var head = body.Joints[JointType.Head];
                var elbowRight = body.Joints[JointType.ElbowRight];
                var shoulderLeft = body.Joints[JointType.ShoulderLeft];
                var shoulderRight = body.Joints[JointType.ShoulderRight];
                var spineMid = body.Joints[JointType.SpineMid];
                var spineShoulder = body.Joints[JointType.SpineShoulder];

                // Swipe possible only between 1.5m and 0.5m
                if(head.Position.Z < 1.5f && head.Position.Z > 0.5f){

                    //right hand in front of right elbow
                    if(handRight.Position.Z < elbowRight.Position.Z){

                        //right hand between head and spine mid
                        if (handRight.Position.Y < head.Position.Y && handRight.Position.Y > spineMid.Position.Y){

                            //right hand on the right of the right shoulder
                            if (handRight.Position.X > shoulderRight.Position.X){
                                swipeSegment1 = true;
                                swipeSegment2 = false;
                                swipeComplete = false;
                                //Debug.Log ("Part1 Occured");
                            }else{
                                swipeSegment1 = false;
                            }

                            //right hand between right shoulder and left shoulder
                            if (handRight.Position.X < shoulderRight.Position.X && handRight.Position.X > shoulderLeft.Position.X){
                                swipeSegment2 = true;
                                swipeSegment1 = false;
                                swipeComplete = false;
                                //Debug.Log ("Part2 Occured");
                            }else{
                                swipeSegment2 = false;
                            }

                            //right hand on the left of the left shoulder
                            if (handRight.Position.X < shoulderLeft.Position.X){
                                swipeComplete = true;
                                swipeSegment1 = false;
                                swipeSegment2 = false;
                                Debug.Log ("Gesture SwipeLeft Occured");

                                //rotate earth positive around y axis
                                gameObject.transform.RotateAround(target, Vector3.up, swipeSpeed);

                                //@Todo: schnellere swipeSpeed wenn zoomin

                            }else{
                                swipeComplete = false;
                            }
                        }
                    }
                }
            }
        }
    }
}