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
    private SpriteRenderer spriteRend;
    public Sprite mainCursor;
    public Sprite hoverCursor;
    
    // Variables for timed hover gesture
    private float waitTime = 2.0f;
    private float timer = 0.0f;

    // Kinect Data variables
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;


    // Called only once on Start
    void Start(){

        spriteRend = obj.GetComponent<SpriteRenderer>();

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

        // Check if BodyManager is available
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

            // Set default distance
            float distance = 1000;

            // Set 2nd distance to tracked persons head position
            float distanceNew = body.Joints[JointType.Head].Position.Z;

            //
            if (distanceNew !=0 && distanceNew <= distance){
                distance = distanceNew;

                if(body.IsTracked){

                    // Joint variables
                    var handLeft = body.Joints[JointType.HandLeft];
                    var handRight = body.Joints[JointType.HandRight];

                    // Create Raycast if left Hand is higher than right hand
                    if(handLeft.Position.Y > handRight.Position.Y){
                        GenerateRaycast();

                    // Create Raycast if right Hand is higher than left hand
                    }else if(handRight.Position.Y > handLeft.Position.Y){
                        GenerateRaycast();
                    }
                }
            }
        }
    }


    
    void GenerateRaycast(){
        
        // Add 1 seconds to timer everytime GenerateRaycast() is called
        timer += Time.deltaTime;
       
        // Code to be place in a MonoBehaviour with a GraphicRaycaster component
        GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();

        // Create the PointerEventData with null for the EventSystem
        PointerEventData ped = new PointerEventData(null);

        // Set required parameters, in this case, cursor position
        ped.position = obj.transform.position;

        // Create list to receive all results
        List<RaycastResult> results = new List<RaycastResult>();

        // Raycast it
        gr.Raycast(ped, results);

        // switch to hover cursor on hit
        if(results.Count >= 1){
            spriteRend.sprite = hoverCursor;
        }else if(results.Count < 1){
            spriteRend.sprite = mainCursor;
        }

        //check results for matches
        foreach (RaycastResult result in results){

            Debug.Log(result.gameObject.name);

            // Load Quiz scene after 3 seconds on hit
            if(result.gameObject.name == "Quiz"){

                if(timer > waitTime){
                    SceneManager.UnloadSceneAsync("MainMenuScene");
                    SceneManager.LoadSceneAsync("GameScene");
                    timer = 0.0f;
                }
            }

            // Load Interactive Video scene after 3 seconds on hit
            if (result.gameObject.name == "Interactive Video" ){
                if(timer > waitTime){
                    SceneManager.UnloadSceneAsync("MainMenuScene");
                    SceneManager.LoadSceneAsync("IntroScene");
                    timer = 0.0f;
                }
            }

            // Load Main scene after 3 seconds on hit
            if (result.gameObject.name == "World Climate" ){
                if(timer > waitTime){
                    SceneManager.UnloadSceneAsync("MainMenuScene");
                    SceneManager.LoadSceneAsync("WorldScene");
                    timer = 0.0f;
                }
            }

            // Load Impressum scene after 3 seconds on hit
            if (result.gameObject.name == "Impressum" ){
                if(timer > waitTime){
                    SceneManager.UnloadSceneAsync("MainMenuScene");
                    SceneManager.LoadSceneAsync("Impressum");
                    timer = 0.0f;
                }
            }

        }
    }        
}
    


    

