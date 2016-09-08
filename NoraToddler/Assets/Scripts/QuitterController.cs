using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class QuitterController : MonoBehaviour {

    public Toggle[] QuitButtons;
    private Boolean[] IsOver = new Boolean[4] { false, false, false, false };
    private Boolean toggleActive = false;
    public GameController Game_Controller;
	// Use this for initialization
	void Start () {
        ClearQuit();
	}
	
	// Update is called once per frame
	void Update () {
        if (toggleActive && Input.GetMouseButton(0) && NoButtonPressed)
            ClearQuit();

    }

    public void ClearQuit()
    {
        for (int i = 0; i < QuitButtons.Length; i++)
            QuitButtons[i].isOn = false;
    }
    public bool NoButtonPressed
    {
        get
        {

            Boolean b = true;
            for (int i = 0; i < IsOver.Length; i++)
            {
                if (IsOver[i] == true)
                {
                    b = false;
                    break;
                }
            }
            return b;
        }
    }

    public void MouseEnter(int i)
    {
        IsOver[i] = true;
    }

    public void MouseExit(int i)
    {
        IsOver[i] = false;
    }

    public void QuitPress()
    {
        toggleActive = isToggleActive();
        if(toggleActive && AreAllPressed())
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

    private Boolean AreAllPressed()
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
