using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class RotationGesture : MonoBehaviour
{
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    private float ox;
    private float oy;
    private float oz;
    private float ow;

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

                if(body.HandRightState == HandState.Unknown){
                    continue;
                }
                var handState = body.HandRightState;
                //Debug.Log("HandState: " + handState);

                ox = body.JointOrientations[JointType.HandRight].Orientation.X;
                oy = body.JointOrientations[JointType.HandRight].Orientation.Y;
                oz = body.JointOrientations[JointType.HandRight].Orientation.Z;
                ow = body.JointOrientations[JointType.HandRight].Orientation.W;
                Debug.Log("HandState: " + handState + "; " + "JointOrientation: " + ox + " , " + oy + " , " + oz + " , "+ ow);
            }
        }
    }

    
}
