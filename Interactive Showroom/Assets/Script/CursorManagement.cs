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

    // UI variables
    private GameObject raycastBlocker;
    private GameObject exitButton;

    // Particle System Trail 
    public float timeBtwSpawn = 0.005f;
    public GameObject clickEffect;


 
    void Start(){
      // Hide Cursor on Start and set cursor sprite
      Cursor.visible = false;
      spriteRend = obj.GetComponent<SpriteRenderer>();

      // Hide raycastBlocker on start, which blocks raycast
      raycastBlocker = GameObject.Find("RaycastBlocker");
      raycastBlocker.SetActive(false);

      // Hide UI elements on start (ExitButton and continent canvas)
      exitButton = GameObject.Find("ExitButton");
      exitButton.SetActive(false);

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
      RaycastAndContinentHighlight();
    }

        

    void RaycastAndContinentHighlight(){
      // Create RayCast
      RaycastHit hit;
      Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // WorldToScreenPoint of object position for perspective correction
      Ray _ray = Camera.main.ScreenPointToRay(pos);

      // Show/Hide continents on RaycastHit (Cursor hover over continent)
      if(Physics.Raycast(_ray, out hit, rayLength, layermask)){

        // Timer for 
        timer += Time.deltaTime;

        // Get hit continents
        main = hit.collider.gameObject.GetComponent<MeshRenderer>();
        
        // No hand cursor if drag cursor is active
        if(!Input.GetMouseButton(0)){
          spriteRend.sprite = handCursor;
        }

        // Change object material name to string
        string parentName = main.sharedMaterial.name;

        // Set up GameObject array for all selectable continents
        GameObject[] parents  = GameObject.FindGameObjectsWithTag("Continent") as GameObject[];

        // Get all continents on selectable layer
        foreach(GameObject parent in parents){

          // Is GameObject on Selectable rendering layer
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
        HoverGesture(main);

      // on miss change back to main cursor and fade out continent
      }else{

        // Get continent and continent rim material
        /* @Todo: fix child out of bounds if possible */
        parent = main.sharedMaterial;
        child = main.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial;

        // Set material alpha value to 0
        alpha = 0.0f;
        child.SetFloat("_Alpha", alpha);
        parent.SetFloat("_Alpha", alpha);
      }
    }


    // show canvas ui if hover longer than 2 sec on contients
    void HoverGesture(MeshRenderer main){

      string newName = "Canvas" + main.name;
      
      foreach(GameObject canvas in uiCanvas){
        
        // Is GameObject on Selectable rendering layer
        if(canvas.layer == 5){

          // Show Ui on hover on continent for over 2 seconds
          if(canvas.name == newName && timer > waitTime){
              canvas.SetActive(true);
              exitButton.SetActive(true);
              raycastBlocker.SetActive(true);
              timer = 0.0f;
          }
          // Hide UI on hover on ExitButton for over 2 seconds
          if(main.name == "ExitButton" && timer > waitTime){
            exitButton.SetActive(false);
            raycastBlocker.SetActive(false);
            timer = 0.0f;
          }
        }
      }
    }



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



    // Show continent and continent rim on raycast hit
    void Show(float alpha, MeshRenderer parentObject, MeshRenderer childObject){
      alpha = 1.0f;
      parentObject.material.SetFloat("_Alpha", alpha);
      childObject.material.SetFloat("_Alpha", alpha);
    }



    // Hide continent and continent rim on raycast hit
    void Hide(float alpha, MeshRenderer parentObject, MeshRenderer childObject){
      alpha = 0.0f;
      parentObject.material.SetFloat("_Alpha", alpha);
      childObject.material.SetFloat("_Alpha", alpha);
    }

}