using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSettings : MonoBehaviour
{

    // Public variables
    public GameObject cursor;
    public GameObject earth;

    // Sprite
    private SpriteRenderer rend;
    public Sprite dragCursor;
    public Sprite mainCursor;

    // Private variables
    float rotSpeed = 10f;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    void Start(){
      rend = cursor.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){

      if(Input.GetMouseButton(0)){    // Replace with if(handState == "closed")

        rend.sprite = dragCursor;

        // position = aktuelle position - vorhergehende position
        mPosDelta = cursor.transform.position - mPrevPos;

        //
        if(Vector3.Dot(transform.up, Vector3.up) >= 0){

          //
          earth.transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right * rotSpeed), Space.World);

        }else{

          //
          earth.transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right * rotSpeed), Space.World);

        }

        //
        earth.transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up * rotSpeed), Space.World);

      }else{
        
        rend.sprite = mainCursor;

      }


      //
      mPrevPos = cursor.transform.position;
      
    }
}


    // Not Used
    /*void Update(){
       if(Input.GetMouseButton(0)){

        changeCursor();

      }else{

        cursor.GetComponent<MeshRenderer>().enabled = true;
        cursorDrag.GetComponent<MeshRenderer>().enabled = false;
      }
    }

    
    // Switch between cursor object on holding left mouse button down
    void changeCursor(){

      cursor.GetComponent<MeshRenderer>().enabled = false;
      cursorDrag.GetComponent<MeshRenderer>().enabled = true;

      float rotX = cursorDrag.transform.position.x;
      float rotY = cursorDrag.transform.position.y; 

      earth.transform.Rotate(-rotX, rotY, 0, Space.World);
    }*/

