using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipeRotate : MonoBehaviour
{
    private Touch touch;

    private Vector2 touchPosition;

    private Quaternion rotationY;

    private float rotateSpeedModifier = 0.2f;

    float rotSpeed = 20;

    void OnMouseDrag(){
        
        float rotX = Input.GetAxis("Mouse X")*rotSpeed*Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y")*rotSpeed*Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up, -rotX);
        transform.RotateAround(Vector3.right, rotY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0){
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved){
                rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotateSpeedModifier, 0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }
    }
}
