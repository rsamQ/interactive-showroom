using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjects : MonoBehaviour
{
    // Public variables
    public Material mat;
    //public GameObject obj;
    public float alpha;

    // Private variables
    Material rend;
    Color myColor;

    // Start is called before the first frame update
    void Start(){
        
        // Get rim object MeshRenderer
        rend = this.GetComponent<MeshRenderer>().material;
        
        // Set parent and child material alpha value to 0 
        alpha = 0.0f;
        rend.SetFloat("_Alpha", alpha);
        mat.SetFloat("_Alpha", alpha);
    }

}
