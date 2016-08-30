using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ColorGame : AbstractGame, IGame
{
    private SpriteManager sprite_Manager;
    private int CurrentNumColors;
    public int StartingNumColors = 3;
    private bool IncorrectGuessInRound = false;

    private ShapeAndColor[] SacArray = null;
    private ColorGameUi UiComponent;
    public GameObject UiGameObject;
    private int CurrentCorrectIndex;

    public int MaxNumColors = 6;
    private int MinNumColors;
    

    public ColorGame(GameController game): base(game)
    {
        UiComponent = game.ColorGameUI.GetComponent<ColorGameUi>();
        UiComponent.SetGameController(this);
    }

    public int GetScreen()
    {
        return GameController.colorMenu;
    }

    public void OnInstructionClick()
    {
        PlayInstruction();
    }

    public void OnColorClick(int index)
    {
        // check if matches correct index
        if (index == CurrentCorrectIndex)
            onCorrect();
        else
            onIncorrect();
    }

    public void Play()
    {
        CurrentNumColors = StartingNumColors;
        UiComponent.HideButtons();
        NewRound();
        // start timer
        // quit screen when timer runs out.
    }



    public void NewRound()
    {
        IncorrectGuessInRound = false;
        // randomize shape choices
        SacArray = Sprite_Manager.getRandomShapes(CurrentNumColors);
        // set buttons to sprites in array        
        UiComponent.SetSacArray(SacArray);
        UiComponent.FormatButtons(CurrentNumColors, MaxNumColors);
        UiComponent.EnableButtons();
        // choose correct index
        CurrentCorrectIndex = UnityEngine.Random.Range(0, (CurrentNumColors - 1));
        // match index to shape for audio
        PlayInstruction();
    }

    private void PlayInstruction()
    {
        Audio_Controller.ColorInstruction(SacArray[CurrentCorrectIndex].Color);
    }

    public void Quit()
    {
        
    }

    public void Update()
    {
        
    }

    private void onIncorrect()
    {
        IncorrectGuessInRound = true;
        Audio_Controller.TryAgain(SacArray[CurrentCorrectIndex].Color);

    }

    private void onCorrect()
    {

        // if IncorrectThisRound decrement CurrentNumShapes by one limited to min.
        if (!IncorrectGuessInRound)
        {
            if (CurrentNumColors < MaxNumColors)
                CurrentNumColors++;
        }
        else
        {
            if (CurrentNumColors > MinNumColors)
                CurrentNumColors--;
        }
        // else increment CurrentNumShapes by one limited to max.
        Win();
    }

    public void Win()
    {
        // hide incorrect sprites
        // play affirmation.
        // animate-scale correct
        // repeat shape name
        // add particles
        ParticleSystem p = null;
        ShapeAndColor.Colors c = SacArray[CurrentCorrectIndex].Color;
        switch (c)
        {
            case ShapeAndColor.Colors.Red:
                p = Game.ShapeParticles[0];
                break;
            case ShapeAndColor.Colors.Orange:
                p = Game.ShapeParticles[1];
                break;
            case ShapeAndColor.Colors.Yellow:
                p = Game.ShapeParticles[2];
                break;
            case ShapeAndColor.Colors.Green:
                p = Game.ShapeParticles[3];
                break;
            case ShapeAndColor.Colors.Blue:
                p = Game.ShapeParticles[4];
                break;
            case ShapeAndColor.Colors.Purple:
                p = Game.ShapeParticles[5];
                break;
        }
        PlayColorAffirmationAudio(c);
        LastParticle = p;
        //UiComponent.Win(CurrentCorrectIndex);

        p.gameObject.SetActive(true);
        p.Play();
    }

    private void PlayColorAffirmationAudio(ShapeAndColor.Colors c)
    {
        Audio_Controller.YeahAudio();
        Audio_Controller.ColorAffirmation(c);
    }
}

