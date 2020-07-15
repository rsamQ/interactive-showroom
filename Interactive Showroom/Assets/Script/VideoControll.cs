using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class VideoControll : MonoBehaviour
{
    private VideoPlayer myVideoPlayer;
    private int framecounter=0;
    private bool setframe = false;

    // Start is called before the first frame update


    void Start()
    {
       myVideoPlayer = GetComponent<VideoPlayer>();
        myVideoPlayer.playbackSpeed = 0.001F;
        //SetFrame(50);
        //SetFramePercent(0.5);
    }

    // Update is called once per frame
    void Update()
    {
        
        //myVideoPlayer.frame = 30;
        if (setframe)
        {

            myVideoPlayer = GetComponent<VideoPlayer>();
            myVideoPlayer.frame = framecounter;
            myVideoPlayer.Play();
           
            setframe = false;
            
        }
        
       
        //Debug.Log(myVideoPlayer.frameCount);

    }

    public void SetFrame(int framec)
    {
        Debug.Log("setframe"+framec);
        setframe = true;
        framecounter = framec;

       
    }
    public void SetFramePercent(double framec)
    {
        myVideoPlayer = GetComponent<VideoPlayer>();
        setframe = true;
        framecounter = (int) ((myVideoPlayer.frameCount )-(myVideoPlayer.frameCount*framec));
        Debug.Log("setframe" + framecounter);

        if (framec < 0.20)
        {
            SceneManager.UnloadSceneAsync("IntroScene");
            SceneManager.LoadSceneAsync("MainMenuScene");
        }
    }
}
