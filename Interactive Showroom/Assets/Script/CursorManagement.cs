using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    // Public RayCastForContinents variables
    private float rayLength = 20.0f;
    public LayerMask layermask;
    public GameObject obj;

    // Sprite
    private SpriteRenderer spriteRend;
    public Sprite cursor;
    public Sprite handCursor;
    
    // Material variables
    private Material parent;
    private Material child;
    private MeshRenderer main;
    private float alpha;

    // Variables for timed hover gesture
    private float waitTime = 2.0f;
    private float timer = 0.0f;
    private GameObject[] uiCanvas;

    // Particle System Trail 
    public float timeBtwSpawn = 0.005f;
    public GameObject clickEffect;



    // Testing variables 
    void Start(){
      Cursor.visible = false;
      spriteRend = obj.GetComponent<SpriteRenderer>();

      uiCanvas = GameObject.FindGameObjectsWithTag("UI") as GameObject[];
      foreach(GameObject canvas in uiCanvas){
        //Debug.Log("one: " + canvas);
        if(canvas.layer == 5)
          canvas.SetActive(false);
      }
    }



    // Update is called once per frame
    void Update(){
      CursorMovement();
      GenerateRaycastForContinents();
    }

        

    void GenerateRaycastForContinents(){

      // Create RayCast
      RaycastHit hit;
      Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // WorldToScreenPoint of object position for perspective correction
      Ray _ray = Camera.main.ScreenPointToRay(pos);

      // Show/Hide continents on RaycastHit (Cursor hover over continent)
      if(Physics.Raycast(_ray, out hit, rayLength, layermask)){

        timer += Time.deltaTime;

        // Get hit continents and set them to variables for alpha manipulation
        parent = hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial;                 // Material of hit object
        main = hit.collider.gameObject.GetComponent<MeshRenderer>();                                  // Continent object
        child = main.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial;    // Continent border object (child from continent)

        // No hand cursor if drag cursor is active
        if(!Input.GetMouseButton(0)){
          spriteRend.sprite = handCursor;
        }

        // Change object material name to string
        string parentName = parent.name;
        //Debug.Log(parentName);


        // Set up GameObject array for all selectable continents
        GameObject[] parents  = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];

        // Get all continents on selectable layer
        foreach(GameObject parent in parents){

          if(parent.layer == 9){

            // Get MeshRenderer from all continent objects including rim and change them to 0
            // except for hit continent
            MeshRenderer parentObject = parent.GetComponent<MeshRenderer>();
            MeshRenderer childObject = parentObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();

            if(parentObject.material.name == parentName){
              Show(alpha, parentObject, childObject);
            }else{
              Hide(alpha, parentObject, childObject);
            }
          }
        } 
        
        // show canvas ui if hover longer than 2 sec on contients
        string newName = "Canvas" + main.name;
        foreach(GameObject canvas in uiCanvas){
          if(canvas.layer == 5){
            if(canvas.name == newName && timer > waitTime){
                canvas.SetActive(true);
                timer = 0.0f;
            }
            if(Input.GetMouseButton(0)){
                canvas.SetActive(false);
            }
          }
        }

      // on miss change back to main cursor and fade out continent
      }else{
        alpha = 0.0f;
        child.SetFloat("_Alpha", alpha);
        parent.SetFloat("_Alpha", alpha);
      }
    }



    //
    void CursorMovement(){

      // Cursor on mouse position
      Vector3 mousePos = Input.mousePosition;
      transform.position = mousePos;
      mousePos.z = 10.0f;    

      Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
      transform.position = newPos;

      // Create instantiated Particle System on cursor position after specific time
      if(timeBtwSpawn <= 0){
        newPos.z = newPos.z + 0.1f;
        Instantiate(clickEffect, newPos, Quaternion.identity);
        timeBtwSpawn = 0.005f;
      }else {
        timeBtwSpawn -= Time.deltaTime;
      }
    }


    void Show(float alpha, MeshRenderer parentObject, MeshRenderer childObject){
      alpha = 1.0f;
      parentObject.material.SetFloat("_Alpha", alpha);
      childObject.material.SetFloat("_Alpha", alpha);
    }

    void Hide(float alpha, MeshRenderer parentObject, MeshRenderer childObject){
      alpha = 0.0f;
      parentObject.material.SetFloat("_Alpha", alpha);
      childObject.material.SetFloat("_Alpha", alpha);
    }

}