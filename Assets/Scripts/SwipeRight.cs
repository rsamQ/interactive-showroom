using UnityEngine;
using System.Collections;

//using Microsoft.Kinect;
using Windows.Kinect;
//using System;

public class SwipeRight : MonoBehaviour {
    
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    public bool swipeSegment1;
    public bool swipeSegment2;
    public bool swipeComplete;

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

                //Swipe Right
                if(body.Joints[JointType.HandLeft].Position.Z < body.Joints[JointType.ElbowLeft].Position.Z && body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineMid].Position.Y){
                    if (body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.Head].Position.Y && body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.SpineMid].Position.Y){
                        if (body.Joints[JointType.HandLeft].Position.X < body.Joints[JointType.ShoulderLeft].Position.X){
                            swipeSegment1 = true;
                            swipeSegment2 = false;
                            swipeComplete = false;
                            Debug.Log ("Part1 Occured");
                        }else{
                            swipeSegment1 = false;
                        }
                    }
                }

                if (body.Joints[JointType.HandLeft].Position.Z < body.Joints[JointType.ElbowLeft].Position.Z && body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineMid].Position.Y){
                    if (body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.Head].Position.Y && body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.SpineMid].Position.Y){
                        if (body.Joints[JointType.HandLeft].Position.X < body.Joints[JointType.ShoulderRight].Position.X && body.Joints[JointType.HandLeft].Position.X > body.Joints[JointType.ShoulderLeft].Position.X){
                            swipeSegment2 = true;
                            swipeSegment1 = false;
                            swipeComplete = false;
                            Debug.Log ("Part2 Occured");
                        }else{
                            swipeSegment2 = false;
                        }
                    }
                }

                if (body.Joints[JointType.HandLeft].Position.Z < body.Joints[JointType.ElbowLeft].Position.Z && body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineMid].Position.Y){
                    if (body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.Head].Position.Y && body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.SpineMid].Position.Y){
                        if (body.Joints[JointType.HandLeft].Position.X > body.Joints[JointType.ShoulderRight].Position.X){
                            swipeComplete = true;
                            swipeSegment1 = false;
                            swipeSegment2 = false;
                            Debug.Log ("Gesture Occured");

                            gameObject.transform.RotateAround(transform.position, transform.up, Time.deltaTime * - 180f);
                        }else{
                            swipeComplete = false;
                        }
                    }
                }
            }
        }
    }
}