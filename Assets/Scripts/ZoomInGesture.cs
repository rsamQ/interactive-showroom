using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System;

public class ZoomInGesture : MonoBehaviour
{
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

                    var handLeft = body.Joints[JointType.HandLeft];
                    var handRight = body.Joints[JointType.HandRight];
                    var elbowRight = body.Joints[JointType.ElbowRight];
                    var elbowLeft = body.Joints[JointType.ElbowLeft];
                    var shoulderLeft = body.Joints[JointType.ShoulderLeft];
                    var shoulderRight = body.Joints[JointType.ShoulderRight];
                    var head = body.Joints[JointType.Head];
                    var spineMid = body.Joints[JointType.SpineMid];

                
                    // Swipe possible only between 1.5m and 0.5m
                    if(head.Position.Z < 3.0f && head.Position.Z > 1.0f){

                        //right hand in front of right elbow
                        if(handRight.Position.Z < shoulderRight.Position.Z && handLeft.Position.Z < shoulderLeft.Position.Z
                        && handRight.Position.X < shoulderRight.Position.X && handLeft.Position.X > shoulderLeft.Position.X
                        && handRight.Position.Y > spineMid.Position.Y && handLeft.Position.Y > spineMid.Position.Y 
                        && body.HandRightState == HandState.Open && body.HandLeftState == HandState.Open){

                            //
                            double numberRight = (double)(decimal)handRight.Position.X;
                            double numberLeft = (double)(decimal)handLeft.Position.X;
                            numberRight = Math.Round((Double)numberRight, 3);
                            numberLeft = Math.Round((Double)numberLeft, 3);
                            //Debug.Log("HandRight_double:" + numberRight + ", HandLeft_double:" + numberLeft);

                            double distance = numberRight;

                            //
                            if((numberRight * 1000) % 2 == 0 && (numberLeft * 1000) % 2 == 0 
                            || (numberRight * 1000) % 1 == 0 && (numberLeft * 1000) % 1 == 0 
                            && Camera.main.fieldOfView > 30.0f){
                                
                                //
                                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 5.0f, Time.deltaTime * 5.0f);
                                //Debug.Log ("ZoomIn: HandRight.X = " + numberRight + ", HandLeft.X" + numberLeft + ", Field of view = " + Camera.main.fieldOfView);
                                
                            }

                            //
                            if (handRight.Position.X > 2 * elbowRight.Position.X && handLeft.Position.X < 2 * elbowLeft.Position.X){
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}