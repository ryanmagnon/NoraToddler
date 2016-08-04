using UnityEngine;
using System.Collections;

public class InstructionsController : MonoBehaviour {
    public GameController Game_Controller;
    public SFXController Sfx_Controller;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartClicked()
    {
        Sfx_Controller.ClickSound.Play();
        Game_Controller.StartGame();
    }


}
