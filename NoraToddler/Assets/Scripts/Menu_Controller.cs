using UnityEngine;
using System.Collections;

public class Menu_Controller : MonoBehaviour {
    public GameController Game_Controller;
    public SFXController SfxController;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ShapeButtonClick()
	{
        SfxController.PlayClick();
        Game_Controller.RequestGame(GameController.shapeGame);
	}

    public void ColorButtonClick()
    {
        SfxController.PlayClick();
        Game_Controller.RequestGame(GameController.colorGame);
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
