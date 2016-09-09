using UnityEngine;
using System.Collections;

public class SFXController : MonoBehaviour {
    public AudioSource ClickSound;
    public AudioSource CorrectAudio;
    public AudioSource IncorrectAudio;
    public AudioSource ApplauseAudio;
    public AudioSource ParticleAudio;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayClick()
    {
        ClickSound.Play();
    }
    
}
