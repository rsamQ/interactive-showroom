using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    public float timeBtwSpawn = 0.5f;
    public GameObject clickEffect;
    public GameObject obj;

    void Update(){
        //CursorMovement();

      Vector3 cursorPos = Input.mousePosition;
      cursorPos.z = 5.0f;
      obj.transform.position = cursorPos;

      if(timeBtwSpawn <= 0){
        Instantiate(clickEffect, cursorPos, Quaternion.identity);
        timeBtwSpawn = 0.5f;
      }else {
        timeBtwSpawn -= Time.deltaTime;
      }
    }


    /*void CursorMovement(){

      // Cursor on mouse postion
      Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      cursorPos.z = 5.0f;
      obj.transform.position = cursorPos;

      if(timeBtwSpawn <= 0){
        Instantiate(clickEffect, cursorPos, Quaternion.identity);
        timeBtwSpawn = 0.5f;
      }else {
        timeBtwSpawn -= Time.deltaTime;
      }
    }*/
}
