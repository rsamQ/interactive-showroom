using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Video : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject erde = GameObject.Find("Erde");

        var vp = erde.AddComponent<UnityEngine.Video.VideoPlayer>();
        vp.url = "Assets/2018HD_celsius_1080p30.m4v";
        vp.isLooping = true;
        vp.playOnAwake = true;
        vp.Pause();
        vp.frame = 5;
        
    }
}
