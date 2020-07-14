using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;
using UnityEngine.Audio;

public class FilterControl : MonoBehaviour
{

    public float filterThreshold = 50;
    public AudioMixer filterMixer;
    public AudioMixerSnapshot[] filterSnapshots;
    public float[] weights;

    public void BlendSnapshots(float playerHealth)
    {
        weights[0] = playerHealth;
        weights[1] = filterThreshold - playerHealth;
        filterMixer.TransitionToSnapshots(filterSnapshots, weights, 0.2f);
    }
}
