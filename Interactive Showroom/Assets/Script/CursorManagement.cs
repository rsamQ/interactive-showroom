using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    // Public RayCast variables
    public float rayLength;
    public LayerMask layermask;
    public GameObject obj;
    

    // Continent material variables
    private Material mat;
    private float alpha;


    // Continent border material/object variables
    private MeshRenderer main;
    private MeshRenderer child;
    private Color myColor;

    // Boolean for coroutine(IEnum) activation
    private bool startRoutine = false;


    // Update is called once per frame
    void Update(){

      // Cursor on mouse postion
      Vector3 temp = Input.mousePosition; // @Todo: Replace with Kinect data
      temp.z = 10.0f;
      this.transform.position = Camera.main.ScreenToWorldPoint(temp);
      

      // Create RayCast
      RaycastHit hit;
      Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // WorldToScreenPoint of object position for perspective correction
      Ray _ray = Camera.main.ScreenPointToRay(pos);


      // Show/Hide continents on RaycastHit (Cursor hover over continent)
      if(Physics.Raycast(_ray, out hit, rayLength, layermask)){

        //Debug.Log(hit.collider.name);

        // Get hit continents and set them to variables for alpha manipulation
        mat = hit.collider.gameObject.GetComponent<MeshRenderer>().material;          // Material of hit object
        main = hit.collider.gameObject.GetComponent<MeshRenderer>();                  // Continent object
        child = main.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();   // Continent border object (child from continent)

        if(!startRoutine){
          
          StartCoroutine("FadeIn");

        }

      }else if(startRoutine){
        
        StartCoroutine("FadeOut");

      }
    }
       

    // Fade in continents
    IEnumerator FadeIn(){

      startRoutine = true;
      
      for(alpha = 0f; alpha <= 1.55; alpha += 0.05f){

          myColor = child.material.color;
          myColor.a = alpha;
          child.material.SetColor("_BaseColor", myColor);

          mat.SetFloat("_Alpha", alpha);
          yield return new WaitForSeconds(0.005f);
      }
    }


    // Fade out continents
    IEnumerator FadeOut(){

      startRoutine = false;
      
      for (alpha = 1f; alpha >= -0.05f; alpha -= 0.05f){

          myColor = child.material.color;
          myColor.a = alpha;
          child.material.SetColor("_BaseColor", myColor);

          mat.SetFloat("_Alpha", alpha);
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
        mat.SetFloat("_Alpha", alpha);

      }else{

        // Set alpha
        alpha = 0.0f;
        myColor = child.material.color;
        myColor.a = alpha;
        
        child.material.SetColor("_BaseColor", myColor);
        mat.SetFloat("_Alpha", alpha);

      }*/