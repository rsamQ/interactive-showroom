using UnityEngine;
using System.Collections;

//using Microsoft.Kinect;
using Windows.Kinect;
//using System;

public class HandWaveGesture : MonoBehaviour {
    
    public GameObject BodySrcManager;
    public JointType TrackedJoint;
    private BodySourceManager bodyManager;
    public float multiplier = 10.0f;
    private Body[] bodies;
    public bool waveSegment1;
    public bool waveSegment2;
    public bool waveComplete;

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
                if(body.Joints[JointType.HandLeft].Position.Z < body.Joints[JointType.ElbowLeft].Position.Z && body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineBase].Position.Y){
                    if (body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.Head].Position.Y && body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.SpineBase].Position.Y){
                        if (body.Joints[JointType.HandLeft].Position.X < body.Joints[JointType.ShoulderLeft].Position.X){
                            waveSegment1 = true;
                            waveSegment2 = false;
                            waveComplete = false;
                            Debug.Log ("Part1 Occured");
                        }else{
                            waveSegment1 = false;
                        }
                    }
                }

                if (body.Joints[JointType.HandLeft].Position.Z < body.Joints[JointType.ElbowLeft].Position.Z && body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineBase].Position.Y){
                    if (body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.Head].Position.Y && body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.SpineBase].Position.Y){
                        if (body.Joints[JointType.HandLeft].Position.X < body.Joints[JointType.ShoulderRight].Position.X && body.Joints[JointType.HandLeft].Position.X > body.Joints[JointType.ShoulderLeft].Position.X){
                            waveSegment2 = true;
                            waveSegment1 = false;
                            waveComplete = false;
                            Debug.Log ("Part2 Occured");
                        }else{
                            waveSegment2 = false;
                        }
                    }
                }

                if (body.Joints[JointType.HandLeft].Position.Z < body.Joints[JointType.ElbowLeft].Position.Z && body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineBase].Position.Y){
                    if (body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.Head].Position.Y && body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.SpineBase].Position.Y){
                        if (body.Joints[JointType.HandLeft].Position.X > body.Joints[JointType.ShoulderRight].Position.X){
                            waveComplete = true;
                            waveSegment1 = false;
                            waveSegment2 = false;
                            Debug.Log ("Gesture Occured");

                            gameObject.transform.RotateAround(transform.position, transform.up, Time.deltaTime * - 90f);
                            //gameObject.transform.RotateAround(transform.position, transform.up, multiplier * - 45f);
                        }else{
                            waveComplete = false;
                        }
                    }
                }
                
                // Swipe Left

            }
        }
    }
}

    /*void Update(){

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
                //var pos = body.Joints[TrackedJoint].Position;

                // Hand above elbow
                if (body.Joints[JointType.HandRight].Position.Y > 
                    body.Joints[JointType.ElbowRight].Position.Y)
                {
                    // Hand right of elbow
                    if (body.Joints[JointType.HandRight].Position.X > 
                        body.Joints[JointType.ElbowRight].Position.X)
                        {
                            Debug.Log ("Part1Wave Occured");
                            waveSegment1=true;
                            waveComplete=false;
                        }
                    else
                        {
                            waveSegment1=false;
                        }
                }
                if(body.Joints[JointType.HandRight].Position.Y > 
                    body.Joints[JointType.ElbowRight].Position.Y && waveSegment1)
                {
                    if (body.Joints[JointType.HandRight].Position.X > 
                        body.Joints[JointType.ElbowRight].Position.X)
                        {
                            waveSegment1=false;
                            Debug.Log ("You have Completed a Wave");
                            waveComplete=true;

                            //gameObject.transform.RotateAround(transform.position, transform.up, Time.deltaTime * -90f);
                            
                        }else { break; }
                }
            }
        }
    }*/


    /*void WaveSegments()

    {
        if (Reader != null) 
            {
            var frame = Reader.AcquireLatestFrame ();

            if (frame != null)
                {
                if (bodies == null) {
                    bodies = new Body[Sensor.BodyFrameSource.BodyCount];
                }

                frame.GetAndRefreshBodyData (bodies);

                frame.Dispose ();
                frame = null;

                int idx = -1;
                for (int i = 0; i < Sensor.BodyFrameSource.BodyCount; i++) {
                    if (bodies [i].IsTracked) 
                    {
                        idx = i;
                    }
                }
                if (idx > -1) 
                    {
                        // Hand above elbow
                            if (bodies [idx].Joints[JointType.HandRight].Position.Y > 
                                bodies [idx].Joints[JointType.ElbowRight].Position.Y)
                            {
                                // Hand right of elbow
                                if (bodies [idx].Joints[JointType.HandRight].Position.X > 
                                    bodies [idx].Joints[JointType.ElbowRight].Position.X)
                                    {
                                        Debug.Log ("Part1Wave Occured");
                                        waveSegment1=true;
                                        waveComplete=false;
                                    }
                                else
                                    {
                                        waveSegment1=false;
                                    }
                            }
                            if(bodies [idx].Joints[JointType.HandRight].Position.Y > 
                               bodies [idx].Joints[JointType.ElbowRight].Position.Y && waveSegment1)
                            {
                                if (bodies [idx].Joints[JointType.HandRight].Position.X > 
                                    bodies [idx].Joints[JointType.ElbowRight].Position.X)
                                    {
                                        waveSegment1=false;
                                        Debug.Log ("You have Completed a Wave");
                                        waveComplete=true;
                                    }
                                else
                                    {

                                    }
                            }

                    }
                }
            }
        }*/
    