using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class KinectCursor : MonoBehaviour
{
    // RayCast variables
    public LayerMask layermask;
    public GameObject obj;
    private float rayLength = 20.0f;

    // Sprite variables
    public Sprite mainCursor;
    public Sprite handCursor;
    private SpriteRenderer rend;
    
    // Material variables
    private Material parent;
    private Material child;
    private MeshRenderer main;
    private float alpha;

    // Variables for timed hover gesture
    private float waitTime = 2.0f;
    private float timer = 0.0f;
    public GameObject uiCanvas;

    // Particle Effect variables
    private float timeBtwSpawn = 0.005f;
    public GameObject clickEffect;

    // Kinect Data variables
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;



    //  
    void Start(){
        Cursor.visible = false;
        rend = obj.GetComponent<SpriteRenderer>();

        uiCanvas.SetActive(false);

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
                        CursorMovement(handLeft.Position.X, handLeft.Position.Y);
                        GenerateRaycast(body);
                    }else if(handRight.Position.Y > handLeft.Position.Y){
                        CursorMovement(handRight.Position.X, handRight.Position.Y);
                        GenerateRaycast(body);
                    }
                }
            }
        }
    }



    void CursorMovement(float x, float y){

        // Cursor on mouse position
        Vector3 mousePos = new Vector3(x, y, 10.0f); // @Todo: Replace with Vector3 mousePos = new Vector3(coord.x, coord.y, coord.z);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = newPos;


        // Create instantiated Particle System at Cursor position
        if(timeBtwSpawn <= 0){
            newPos.z = newPos.z + 0.1f;
            Instantiate(clickEffect, newPos, Quaternion.identity);
            timeBtwSpawn = 0.005f;
        }else {
            timeBtwSpawn -= Time.deltaTime;
        }
    }



    void GenerateRaycast(Body body){

        // Create RayCast
        RaycastHit hit;
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // WorldToScreenPoint of object position for perspective correction
        Ray _ray = Camera.main.ScreenPointToRay(pos);


        // Show/Hide continents on RaycastHit (Cursor hover over continent)
        if(Physics.Raycast(_ray, out hit, rayLength, layermask)){

            // Timer for hover gesture
            timer += Time.deltaTime;

            // Get hit continents and set them to variables for alpha manipulation
            parent = hit.collider.gameObject.GetComponent<MeshRenderer>().material;                 // Material of hit object
            main = hit.collider.gameObject.GetComponent<MeshRenderer>();                            // Continent object
            child = main.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;    // Continent border object (child from continent)

            // 
            if(body.HandRightState != HandState.Closed && body.HandLeftState != HandState.Closed){
                rend.sprite = handCursor;
            }

            // Change object material name to string
            string parentName = parent.name;

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
            } // end of foreach 

            // show canvas ui if hover longer than 2 sec on contients
            string newName = "Canvas" + main.name;
            //Debug.Log(newName);
            if(newName ==  uiCanvas.name && timer > waitTime){
                uiCanvas.SetActive(true);
            }

            // on miss change back to main cursor and fade out continent
            }else{

                //StartCoroutine(FadeOutTwo(parent, child));
                alpha = 0.0f;
                child.SetFloat("_Alpha", alpha);
                parent.SetFloat("_Alpha", alpha);
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