using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System;

public class SwipeRight : MonoBehaviour {
    
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

            if(body.IsTracked){

                //Joint variables
                var handRight = body.Joints[JointType.HandRight];
                var head = body.Joints[JointType.Head];
                var elbowRight = body.Joints[JointType.ElbowRight];
                var shoulderRight = body.Joints[JointType.ShoulderRight];

                // Swipe possible only between 1.5m and 0.5m
                if(head.Position.Z < 1.5f && head.Position.Z > 0.5f){

                    //right hand in front of right elbow
                    if(handRight.Position.Z < elbowRight.Position.Z && body.HandRightState == HandState.Closed){

                        //RotateEarth(handRight.Position.X);

                        var num = handRight.Position.X;
                        double number = (double)(decimal)num;
                        number = Math.Round((Double)number, 3);
                        Debug.Log("double:" + number);

                        if((number * 1000) % 2 == 0){

                            float numberF = (float)number;
                            Debug.Log("float: " + numberF);

                            //rotate earth counter-clockwise around y axis
                            gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                            gameObject.transform.Rotate(0.0f, 0.0f - (numberF * 2.5f), 0.0f, Space.World);
                            Debug.Log ("Gesture SwipeRight Occured; X=" + numberF);

                        }

                        //right hand on the right of the right shoulder
                        if (handRight.Position.X == elbowRight.Position.X){
                            break;
                        }
                    }
                }
            }
        }
    }

    /*private void RotateEarth(float param){
        var floatNum = param;
        double doubleNum = (double)(decimal)floatNum;
        doubleNum = Math.Round((Double)doubleNum, 2);

        if((doubleNum * 1000) % 2 == 0){

            float newFloat = (float)doubleNum;

            gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            gameObject.transform.Rotate(0.0f, 0.0f - (newFloat * 2.5f), 0.0f, Space.World);
            Debug.Log ("Gesture SwipeRight Occured; X=" + newFloat);
        }
    }*/

}