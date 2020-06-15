using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    // Public RayCast variables
    public float rayLength;
    public LayerMask layermask;
    public GameObject obj;

    // Sprite
    private SpriteRenderer rend;
    public Sprite handCursor;
    public Sprite cursor;
    

    // Material variables
    private Material parent;
    private Material child;
    private MeshRenderer main;
    private float alpha;
    

    // Boolean for coroutine(IEnum) activation
    private bool startRoutine = false;

    public float timeBtwSpawn = 0.005f;
    public GameObject clickEffect;


    // Testing variables 
    void Start(){
      Cursor.visible = false;
      rend = obj.GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update(){

      CursorMovement();
      GenerateRaycast();

    }


    void GenerateRaycast(){

      // Create RayCast
      RaycastHit hit;
      Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // WorldToScreenPoint of object position for perspective correction
      Ray _ray = Camera.main.ScreenPointToRay(pos);


      // Show/Hide continents on RaycastHit (Cursor hover over continent)
      if(Physics.Raycast(_ray, out hit, rayLength, layermask)){

        //Debug.Log("new Name: " + hit.collider.name);

        // Get hit continents and set them to variables for alpha manipulation
        parent = hit.collider.gameObject.GetComponent<MeshRenderer>().material;          // Material of hit object
        main = hit.collider.gameObject.GetComponent<MeshRenderer>();                  // Continent object
        child = main.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;   // Continent border object (child from continent)

        rend.sprite = handCursor;

        if(!startRoutine){
          
          StartCoroutine("FadeIn");

        }

      }else if(startRoutine){
        
        rend.sprite = cursor;  
        StartCoroutine("FadeOut");

      }
    }


    void CursorMovement(){

      // Cursor on mouse position
      Vector3 mousePos = Input.mousePosition; // @Todo: Replace with Vector3 mousePos = new Vector3(coord.x, coord.y, coord.z);
      transform.position = mousePos;
      mousePos.z = 10.0f;                        // @Todo: not needed with kinect data (siehe @todo)
      Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
      transform.position = newPos;


      // Create instantiated Particle System
      if(timeBtwSpawn <= 0){
        newPos.z = newPos.z + 0.1f;
        Instantiate(clickEffect, newPos, Quaternion.identity);
        timeBtwSpawn = 0.005f;
      }else {
        timeBtwSpawn -= Time.deltaTime;
      }
    }
       

    // Fade in continents
    IEnumerator FadeIn(){

      startRoutine = true;
      
      for(alpha = 0.0f; alpha <= 1.55; alpha += 0.05f){

          child.SetFloat("_Alpha", alpha);
          parent.SetFloat("_Alpha", alpha);
          yield return new WaitForSeconds(0.005f);
      }
    }


    // Fade out continents
    IEnumerator FadeOut(){

      startRoutine = false;
      
      for (alpha = 0.80f; alpha >= -0.05f; alpha -= 0.05f){

          child.SetFloat("_Alpha", alpha);
          parent.SetFloat("_Alpha", alpha);
          yield return new WaitForSeconds(0.005f);
      }
    }

}

        /* Code for Debugging and testing without transition.
        Implemented after getting hit continent. */

        // Set alpha
        /*alpha = 1.0f;
        myColor = child.material.color;
        myColor.a = alpha;

        child.material.SetColor("_BaseColor", myColor);
       parent.SetFloat("_Alpha", alpha);

      }else{

        // Set alpha
        alpha = 0.0f;
        myColor = child.material.color;
        myColor.a = alpha;
        
        child.material.SetColor("_BaseColor", myColor);
       parent.SetFloat("_Alpha", alpha);

      }*/