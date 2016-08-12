using UnityEngine;
using System.Collections;

public class SFXController : MonoBehaviour {
    public AudioSource ClickSound;
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
