using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class QuitterController : MonoBehaviour {

    public Toggle[] QuitButtons;
    private Boolean[] isOver = new Boolean[4] { false, false, false, false };
    private Boolean toggleActive = false;
    public GameController Game_Controller;
	// Use this for initialization
	void Start () {
        ClearQuit();
	}
	
	// Update is called once per frame
	void Update () {
        if (toggleActive && Input.GetMouseButton(0) && NoButtonPressed())
            ClearQuit();

    }

    public void ClearQuit()
    {
        for (int i = 0; i < QuitButtons.Length; i++)
            QuitButtons[i].isOn = false;
    }

    private bool NoButtonPressed()
    {
        Boolean b = true;
        for (int i = 0; i < isOver.Length; i++)
        {
            if (isOver[i] == true)
            {
                b = false;
                break;
            }
        }
        return b;
    }
    public void MouseEnter(int i)
    {
        isOver[i] = true;
    }

    public void MouseExit(int i)
    {
        isOver[i] = false;
    }

    public void QuitPress()
    {
        toggleActive = isToggleActive();
        if(toggleActive && areAllPressed())
        {
            Game_Controller.QuitToMenu();
            ClearQuit();
        }
    }

    private bool isToggleActive()
    {
        bool b = false;
        for (int i = 0; i < QuitButtons.Length; i++)
        {
            if (QuitButtons[i].isOn)
            {
                b = true;
                break;
            }
        }
        return b;
    }

    private Boolean areAllPressed()
    {
        Boolean b = true;
        for (int i = 0; i < QuitButtons.Length; i++)
        {
            if (!QuitButtons[i].isOn)
            {
                b = false;
                break;
            }
        }
        return b;
    }


}
