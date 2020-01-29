using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class LeftHandClosed : MonoBehaviour {
    
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;

    //rotation variables
    private Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);
    private float swipeSpeed = 2f;

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
                var head = body.Joints[JointType.Head];
                var elbowLeft = body.Joints[JointType.ElbowLeft];
                var shoulderLeft = body.Joints[JointType.ShoulderLeft];
                var spineMid = body.Joints[JointType.SpineMid];

                // Swipe possible only between 1.5m and 0.5m 
                if(head.Position.Z < 1.5f && head.Position.Z > 0.5f){

                    //right hand in front of right elbow
                    if(handLeft.Position.Z < elbowLeft.Position.Z){

                        //right hand between head and spine mid
                        if (handLeft.Position.Y < head.Position.Y && handLeft.Position.Y > spineMid.Position.Y){

                            //right hand on the right of the right shoulder
                            if (handLeft.Position.X < shoulderLeft.Position.X){

                                if(body.HandLeftState == HandState.Closed){

                                    //rotate earth negative around y axis
                                    gameObject.transform.RotateAround(target, Vector3.up, -swipeSpeed);
                                    Debug.Log ("Gesture SwipeRight Occured");

                                    //@Todo: schnellere swipeSpeed wenn zoomin

                                }else{
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}