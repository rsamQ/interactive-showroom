using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;
using UnityEngine.Audio;


public class GetData : MonoBehaviour
{
    public MultiSourceManager mMultiSourceManager;

    private ushort[] mDepthData = null;
    //private int[] mintdepth = null;
    public VideoControll Video;
    public UnityEngine.AudioSource Filter;
   


    private CameraSpacePoint[] mCameraSpacePoints = null;

    private KinectSensor mSensor = null;
    private int updateframe = 0;
    private CoordinateMapper mMapper = null;

    private readonly Vector2Int mDepthResolution = new Vector2Int(512, 424);


    // Update is called once per frame
    private void Awake()
    {
        mSensor = KinectSensor.GetDefault();
        mMapper = mSensor.CoordinateMapper;

        int arraySize = mDepthResolution.x * mDepthResolution.y;

        mCameraSpacePoints = new CameraSpacePoint[arraySize];

    }

    private void Update()
    {
        updateframe++;
        if (updateframe%20==0)
        {
            getDepth();
        }
    }

    private void getDepth()
    {
        mDepthData = mMultiSourceManager.GetDepthData();
        int  max = 0;
        //int min = 0;
        //int aver = 0;
        int sum = 0;
        for (var i = 0; i < mDepthData.Length;i++)
        {
            if( mDepthData[i]> max)
            {
                max = mDepthData[i];
            }
            sum = sum + mDepthData[i];

        }
         Debug.Log((double)sum / (double)mDepthData.Length/(double) max  );
        Video.SetFramePercent((double)sum / (double)mDepthData.Length / (double)max);
        

        //double percent =
        // Video.SetFramePercent(mDepthData.Average()/ max);
        // Video.SetFrame(100);
        // Debug.Log( mDepthData.Average());

    }

}
