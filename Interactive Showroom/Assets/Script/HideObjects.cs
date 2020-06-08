using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjects : MonoBehaviour
{
    // Public variables
    public Material mat;
    public float alpha;
    public GameObject obj;

    // Private variables
    MeshRenderer rend;
    Color myColor;

    // Start is called before the first frame update
    void Start(){
        
        // Get rim object MeshRenderer
        rend = GetComponent<MeshRenderer>();

        // Set alpha value of rim object to 0
        rend.material.SetColor("_BaseColor", myColor);
        myColor = rend.material.color;
        myColor.a = 0.0f;
        rend.material.color = myColor;
        
        // Set parent material alpha value to 0 
        alpha = 0.0f;
        mat.SetFloat("_Alpha", alpha);
    }

}
