using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class ZoomInGesture : MonoBehaviour
{
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    public bool zoomSegment1;
    public bool zoomSegment2;
    public bool zoomComplete;
    private int zoom = 20;
    private float smooth = 5;

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

                var handLeft = body.Joints[JointType.HandLeft];
                var handRight = body.Joints[JointType.HandRight];
                var elbowRight = body.Joints[JointType.ElbowRight];
                var elbowLeft = body.Joints[JointType.ElbowLeft];
                var shoulderLeft = body.Joints[JointType.ShoulderLeft];
                var shoulderRight = body.Joints[JointType.ShoulderRight];
                var spineMid = body.Joints[JointType.SpineMid]; // was before spinebase
                var spineShoulder = body.Joints[JointType.SpineShoulder];


                // Right and Left Hand in front of Shoulders
                if (handLeft.Position.Z < elbowLeft.Position.Z && handRight.Position.Z < elbowRight.Position.Z)
                {

                    // Hands between shoulder and hip
                    if (handRight.Position.Y < spineShoulder.Position.Y && handRight.Position.Y > spineMid.Position.Y &&
                        handLeft.Position.Y < spineShoulder.Position.Y && handLeft.Position.Y > spineMid.Position.Y)
                    {
                        // Hands between shoulders
                        if (handRight.Position.X < shoulderRight.Position.X && handRight.Position.X > shoulderLeft.Position.X &&
                            handLeft.Position.X > shoulderLeft.Position.X && handLeft.Position.X < shoulderRight.Position.X)
                        {
                            zoomSegment1 = true;
                            zoomSegment2 = false;
                            zoomComplete = false;
                            //Debug.Log ("Part1 Occured");
                        }else{
                            zoomSegment1 = false;
                        }
                    }
                }


                // Right and Left Hand in front of Shoulders
            if (handLeft.Position.Z < elbowLeft.Position.Z && handRight.Position.Z < elbowRight.Position.Z)
            {
                // Hands between shoulder and hip
                if (handRight.Position.Y < spineShoulder.Position.Y && handRight.Position.Y > spineMid.Position.Y &&
                    handLeft.Position.Y < spineShoulder.Position.Y && handLeft.Position.Y > spineMid.Position.Y)
                {
                    // Hands outside shoulders
                    if (handRight.Position.X > shoulderRight.Position.X && handLeft.Position.X < shoulderLeft.Position.X)
                    {
                        zoomSegment2 = true;
                        zoomSegment1 = false;
                        zoomComplete = false;
                        //Debug.Log ("Part2 Occured");
                    }else{
                        zoomSegment2 = false;
                    }
                }
            }


             // Right and Left Hand in front of Shoulders
            if (handLeft.Position.Z < elbowLeft.Position.Z && handRight.Position.Z < elbowRight.Position.Z)
            {
                // Hands between shoulder and hip
                if (handRight.Position.Y < spineShoulder.Position.Y && handRight.Position.Y > spineMid.Position.Y &&
                    handLeft.Position.Y < spineShoulder.Position.Y && handLeft.Position.Y > spineMid.Position.Y)
                {
                    // Hands outside elbows
                    if (handRight.Position.X > elbowRight.Position.X && handLeft.Position.X < elbowLeft.Position.X)
                    {
                        zoomComplete = true;
                        zoomSegment1 = false;
                        zoomSegment2 = false;
                        Debug.Log ("Gesture ZoomIn Occured");

                        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
                        Debug.Log("FieldofView: " + GetComponent<Camera>().fieldOfView);
                    }else{
                        zoomComplete = false;
                    }
                }
            }
            }
        }
    }
}