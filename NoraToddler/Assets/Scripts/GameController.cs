﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject MainMenuUi;
    public GameObject QuitterUI;
    public GameObject InstructionMenuUI;
    public GameObject ShapeGameUI;
    public GameObject ColorGameUI;
    public SpriteManager Sprite_Manager;
    public GameObject Accolades_Ui;


    private GameObject activeUI;

    public IGame CurrentGame;
    public const int shapeGame = 1;
    public const int colorGame = 2;
    public const int countingGame = 3;
    public const int alphabetGame = 4;
    public const int noGame = 0;
    private int GameRequest = noGame;

    public const int mainMenu = 0;    
    public const int instructionMenu = 1;
    public const int shapeMenu = 2;
    public const int colorMenu = 3;

    public AudioController AudioController;

    public ParticleSystem[] ShapeParticles;
    public ParticleSystem[] ColorParticles;


    public bool AccoladePlaying { get { return Accolades_Ui.GetComponent<Accolades_UI>().IsAnimating;  } }
	// Use this for initialization
	void Start ()
    {
        MainMenuUi.SetActive(true);
        activeUI = MainMenuUi;
        QuitterUI.SetActive(false);
        InstructionMenuUI.SetActive(false);
        ShapeGameUI.SetActive(false);
        ColorGameUI.SetActive(false);
        Accolades_Ui.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (CurrentGame != null)
            CurrentGame.Update();
	}

    public void RequestGame(int game)
    {
        CurrentGame = InitGame(game);
        SwapScreen(instructionMenu);
    }

    public void StartGame()
    {       
        SwapScreen(CurrentGame.GetScreen());
        CurrentGame.Play();
    }

    private void SwapScreen(int screen)
    {
        activeUI.SetActive(false);

        if (screen != mainMenu)
            QuitterUI.SetActive(true);
        else     
            QuitterUI.SetActive(false);
        switch(screen)
        {
            case mainMenu:
                MainMenuUi.SetActive(true);
                activeUI = MainMenuUi;
                break;
            case shapeMenu:
                ShapeGameUI.SetActive(true);
                activeUI = ShapeGameUI;
                break;
            case colorMenu:
                ColorGameUI.SetActive(true);
                activeUI = ColorGameUI;
                break;
            case instructionMenu:
                InstructionMenuUI.SetActive(true);
                activeUI = InstructionMenuUI;
                break;
        }
    }

    private IGame InitGame(int gameNum)
    {
        IGame g;
        switch(gameNum)
        {
            case shapeGame:
                g = new ShapeGame(this);
                break;
            case colorGame:
                g = new ColorGame(this);
                break;
            default:
                g = null;
                break;
        }
      
        return g;
    }

    public void PlayRandomAccolade()
    {
        Accolades_Ui.GetComponent<Accolades_UI>().PlayRandomAccolade();
    }

    public void QuitToMenu()
    {
        if (CurrentGame != null)
        {
            CurrentGame.Quit();
            CurrentGame = null;
        }
        SwapScreen(mainMenu);
    }
}
