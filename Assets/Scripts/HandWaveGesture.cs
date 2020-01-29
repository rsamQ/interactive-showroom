using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;

public class HandWaveGesture : MonoBehaviour {
    
    public GameObject BodySrcManager;
    //public JointType TrackedJoint;
    private BodySourceManager bodyManager;
    private Body[] bodies;


    public bool waveSegment1;
    public bool waveSegment2;
    public bool waveComplete;

    //private int counter = 0;

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

                List<string> segment = new List<string>();
                //string[] array = new string[2];

                // Hand above elbow
                if (body.Joints[JointType.HandRight].Position.Y > 
                    body.Joints[JointType.ElbowRight].Position.Y && 
                    body.Joints[JointType.HandRight].Position.X > 
                    body.Joints[JointType.ElbowRight].Position.X){

                        //array[0] = "one";
                        //Debug.Log ("Part1Wave Occured; array=" + array[0]);

                        segment.Add("one");
                }else{
                    //array[0] = "blank";
                    segment.Clear();
                    break;
                }

                if(body.Joints[JointType.HandRight].Position.Y > 
                    body.Joints[JointType.ElbowRight].Position.Y && 
                    body.Joints[JointType.HandRight].Position.X > 
                    body.Joints[JointType.ElbowRight].Position.X){

                        //array[1] = "two";
                        //Debug.Log ("Part2Wave Occured; array=" + array[1]);
                        segment.Add("two");
                }else{
                    //array[1] = "blank";
                    segment.Clear();
                    break;
                }

                if(segment[0] == "one" && segment[1] == "two"){

                    gameObject.transform.RotateAround(transform.position, transform.up, Time.deltaTime * -90f);
                    Debug.Log ("Wave Occured");
                    segment.Clear();
                }else{
                    gameObject.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 0f);
                }
                //Debug.Log(array[0] + "," + array[1]);
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

                string[] array = new string[2];

                // Hand above elbow
                if (body.Joints[JointType.HandRight].Position.Y > 
                    body.Joints[JointType.ElbowRight].Position.Y && 
                    body.Joints[JointType.HandRight].Position.X > 
                    body.Joints[JointType.ElbowRight].Position.X){

                        array[0] = "one";
                        Debug.Log ("Part1Wave Occured; array=" + array[0]);
                }else{
                    array[0] = "blank";
                }

                if(body.Joints[JointType.HandRight].Position.Y > 
                    body.Joints[JointType.ElbowRight].Position.Y && 
                    body.Joints[JointType.HandRight].Position.X > 
                    body.Joints[JointType.ElbowRight].Position.X){

                        array[1] = "two";
                        Debug.Log ("Part2Wave Occured; array=" + array[1]);
                }else{
                    array[1] = "blank";
                }

                if(array[0] == "one" && array[1] == "two"){

                    gameObject.transform.RotateAround(transform.position, transform.up, Time.deltaTime * -90f);
                    Debug.Log ("Wave Occured");
                    array[0] = "";
                    array[1] = "";
                }

                Debug.Log(array[0] + "," + array[1]);
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
    