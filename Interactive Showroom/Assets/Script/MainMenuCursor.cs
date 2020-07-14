using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuCursor : MonoBehaviour
{
   

    // Sprite variables
    public GameObject obj;
    
    

    // Variables for timed hover gesture
    private float waitTime = 3.0f;
    private float timer = 0.0f;

    

    // Kinect Data variables
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    private float multiplier = 1000.0f;



    //  
    void Start(){
        // Hide Cursor on Start and set cursor sprite



      

   

        // Get Body Source Manager Data
        if(BodySrcManager == null){
            Debug.Log("add Body Source Manager");
        }else{
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }
    }



    // Update is called once per frame
    void Update(){
        TrackHandCursor();
    }



    void TrackHandCursor(){

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

            float distance = 1000;
            float distanceNew = body.Joints[JointType.Head].Position.Z;

            if (distanceNew !=0 && distanceNew <= distance){
                distance = distanceNew;

                if(body.IsTracked){

                    //Joint variables
                    var handLeft = body.Joints[JointType.HandLeft];
                    var handRight = body.Joints[JointType.HandRight];

                    if(handLeft.Position.Y > handRight.Position.Y){
                        GenerateRaycast();
                    }else if(handRight.Position.Y > handLeft.Position.Y){
                        GenerateRaycast();
                    }
                }
            }
        }
    }


    
    void GenerateRaycast(){
        timer += Time.deltaTime;
       
            //Code to be place in a MonoBehaviour with a GraphicRaycaster component
            GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
            //Create the PointerEventData with null for the EventSystem
            PointerEventData ped = new PointerEventData(null);
        //Set required parameters, in this case, mouse position
        ped.position = obj.transform.position;
            //Create list to receive all results
            List<RaycastResult> results = new List<RaycastResult>();
            //Raycast it
            gr.Raycast(ped, results);

            foreach (RaycastResult result in results){
            Debug.Log(result.gameObject.name);
                if(result.gameObject.name == "Quiz")
            {
                    SceneManager.UnloadScene("MainMenuScene");
                    SceneManager.LoadScene("GameScene");

                timer = 0.0f;
            }

            if (result.gameObject.name == "Interactive Video" )
            {
                SceneManager.UnloadScene("MainMenuScene");
                SceneManager.LoadScene("IntroScene");
                timer = 0.0f;
            }

            if (result.gameObject.name == "World Climate" )
            {
                SceneManager.UnloadScene("MainMenuScene");
                SceneManager.LoadScene("World Scene");
                timer = 0.0f;
            }

            if (result.gameObject.name == "Impressum" )
            {
                SceneManager.UnloadScene("MainMenuScene");
                SceneManager.LoadScene("Impressum");
                timer = 0.0f;
            }

        }
        }        
    }
    


    

