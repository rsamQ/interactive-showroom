using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System;

public class DragLeft : MonoBehaviour {
    
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;

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

        float distance = 1000;
        float distanceNew = body.Joints[JointType.Head].Position.Z;

            if (distanceNew !=0 && distanceNew <= distance){
                distance = distanceNew;

                if(body.IsTracked){

                    //Joint variables
                    var handLeft = body.Joints[JointType.HandLeft];
                    var handRight = body.Joints[JointType.HandRight];
                    var head = body.Joints[JointType.Head];
                    var elbowLeft = body.Joints[JointType.ElbowLeft];
                    var spineMid = body.Joints[JointType.SpineMid];

                    // Swipe possible only between 1.5m and 0.5m
                    if(head.Position.Z < 3.0f && head.Position.Z > 1.0f){

                        //right hand in front of right elbow
                        if(handLeft.Position.Z < elbowLeft.Position.Z && body.HandLeftState == HandState.Closed &&
                        handLeft.Position.Y > spineMid.Position.Y && handRight.Position.Y < spineMid.Position.Y){

                            var num = handLeft.Position.X;
                            double number = (double)(decimal)num;
                            number = Math.Round((Double)number, 3);
                            Debug.Log("double:" + number);
                            
                            if((number * 1000) % 2 == 0){   

                                float numberF = (float)number;
                                Debug.Log("float: " + numberF);

                                //rotate earth clockwise around y axis
                                gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                                gameObject.transform.Rotate(0.0f, 0.0f - (numberF * 2.5f), 0.0f, Space.World);
                                Debug.Log ("Gesture SwipeLeft Occured; X=" + numberF);
                            }

                            if(handLeft.Position.X == elbowLeft.Position.X){
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}