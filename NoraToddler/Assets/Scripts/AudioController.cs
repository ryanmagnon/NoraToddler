using UnityEngine;
using System.Collections;
using System;

public class AudioController : MonoBehaviour {
    public PayoffAudioController PayoffAudio;
    public SFXController Sfx;
    public ShapeAudioController ShapeAudio;
    private AudioSource[] Queue;
    //public ShapeInstructionController

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ShapeInstruction(ShapeAndColor.Shapes shape)
    {
        ShapeAudio.PlayInstruction(shape);
    }

    public void YeahAudio()
    {
        PayoffAudio.Yeah();
    }

    public void ShapeAffirmation(ShapeAndColor.Shapes shape)
    {
        ShapeAudio.PlayAffirmation(shape);
    }
}
