﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Timers;
public class ShapeGame : IGame
{

    public SpriteManager Sprite_Manager;
    public int StartingNumShapes = 3;
    public int MaxNumShapes = 7;
    public int MinNumShapes = 2;
    private int CurrentNumShapes;
    private int CurrentCorrectIndex;
    private bool IncorrectGuessInRound;
    private ShapeAndColor[] SacArray;
    private float areaWidth;
    private float areaHeight;
    public GameObject ShapeGameUi;
    public GameObject[] Buttons;
    private float buttonPadding = 20f;
    private ShapeGameUi Ui;
    private GameController Game;
    private float WinDuration = 0f;


    public ShapeGame(GameController game)
    {
        Game = game;
        Sprite_Manager = game.Sprite_Manager;
        Ui = game.ShapeGameUI.GetComponent<ShapeGameUi>();
        Ui.SetGameController(this);
    }

    public void Play()
    {
        CurrentNumShapes = StartingNumShapes;
        NewRound();
        // start timer
        // quit screen when timer runs out.
    }

    public void NewRound()
    {
        IncorrectGuessInRound = false;
        // randomize shape choices
        SacArray = Sprite_Manager.getRandomColors(CurrentNumShapes);
        // set buttons to sprites in array        
        Ui.SetSacArray(SacArray);
        Ui.FormatButtons(CurrentNumShapes, MaxNumShapes);

        // choose correct index
        CurrentCorrectIndex = UnityEngine.Random.Range(0, (CurrentNumShapes - 1));
        // match index to shape for audio
        PlayInstruction();
    }

    public void PlayInstruction()
    {
        Debug.Log("Correct Button Index: " + CurrentCorrectIndex);
    }

    public void OnInstructionClick()
    {
        PlayInstruction();
    }

    private void onIncorrect()
    {
        IncorrectGuessInRound = true;
        //play negative sfx
        // 
        Debug.Log("incorrect");
    }

    private void onCorrect()
    {
        Debug.Log("correct");

        // hide incorrect sprites
        // play affirmation.
        // animate-scale correct
        // repeat shape name
        // add particles
        // if IncorrectThisRound decrement CurrentNumShapes by one limited to min.
        if (!IncorrectGuessInRound)
        {
            if (CurrentNumShapes < MaxNumShapes)
                CurrentNumShapes++;
        }
        else
        {
            if (CurrentNumShapes > MinNumShapes)
                CurrentNumShapes--;
        }
        // else increment CurrentNumShapes by one limited to max.
        Win();


    }

    public void Update()
    {

    }

    public void Quit()
    {

    }

    public int GetScreen()
    {
        return GameController.shapeMenu;
    }

    public void OnShapeClick(int index)
    {
        // check if matches correct index
        if (index == CurrentCorrectIndex)
            onCorrect();
        else
            onIncorrect();
    }

    public void Win()
    {
        ParticleSystem p = null;
        ShapeAndColor.Shapes s = SacArray[CurrentCorrectIndex].Shape;
        switch (s)
        {
            case ShapeAndColor.Shapes.Circle:
                p = Game.ShapeParticles[0];
                break;
            case ShapeAndColor.Shapes.Diamond:
                p = Game.ShapeParticles[1];
                break;
            case ShapeAndColor.Shapes.Oval:
                p = Game.ShapeParticles[2];
                break;
            case ShapeAndColor.Shapes.Rectangle:
                p = Game.ShapeParticles[3];
                break;
            case ShapeAndColor.Shapes.Square:
                p = Game.ShapeParticles[4];
                break;
            case ShapeAndColor.Shapes.Star:
                p = Game.ShapeParticles[5];
                break;
            case ShapeAndColor.Shapes.Triangle:
                p = Game.ShapeParticles[6];
                break;
        }
        Ui.Win(p, CurrentCorrectIndex);
        
    }
    
    private void OnParticleFinish()
    {
        NewRound();
    }


}
