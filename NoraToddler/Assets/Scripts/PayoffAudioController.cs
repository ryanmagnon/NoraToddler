using UnityEngine;
using System.Collections;
using System;

public class PayoffAudioController : MonoBehaviour {
    public AudioSource[] PayoffAudio;
    public AudioSource YeahAudio;
    // Use this for initialization
    void Start () {
	
	}
	
    public AudioSource Play(int i)
    {
        AudioSource a = null;
        if (i < PayoffAudio.Length)
            a = PayoffAudio[i];
        return a;
    }

	// Update is called once per frame
	void Update () {
	    
	}

    public AudioSource Yeah()
    {
        return YeahAudio;
        
    }
}
