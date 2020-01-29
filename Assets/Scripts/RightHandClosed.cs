using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class RightHandClosed : MonoBehaviour {
    
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
                var handRight = body.Joints[JointType.HandRight];
                var head = body.Joints[JointType.Head];
                var elbowRight = body.Joints[JointType.ElbowRight];
                var shoulderRight = body.Joints[JointType.ShoulderRight];
                var spineMid = body.Joints[JointType.SpineMid];

                // Swipe possible only between 1.5m and 0.5m
                if(head.Position.Z < 1.5f && head.Position.Z > 0.5f){

                    //right hand in front of right elbow
                    if(handRight.Position.Z < elbowRight.Position.Z){

                        //right hand between head and spine mid
                        if (handRight.Position.Y < head.Position.Y && handRight.Position.Y > spineMid.Position.Y){

                            //right hand on the right of the right shoulder
                            if (handRight.Position.X > shoulderRight.Position.X){

                                if(body.HandRightState == HandState.Closed){

                                    //rotate earth positive around y axis
                                    gameObject.transform.RotateAround(target, Vector3.up, swipeSpeed);
                                    Debug.Log ("Gesture SwipeLeft Occured");
                                }else{
                                    break;
                                }

                                //@Todo: schnellere swipeSpeed wenn zoomin
                            }
                        }
                    }
                }
            }
        }
    }
}