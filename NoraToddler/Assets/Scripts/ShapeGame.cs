using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Timers;
public class ShapeGame : AbstractGuessingGame, IGame
{

    
    public int StartingNumShapes = 3;
    public int MaxNumShapes = 7;
    public int MinNumShapes = 2;
    private int CurrentNumShapes;
    private int CurrentCorrectIndex;
    private bool IncorrectGuessInRound;
    private ShapeAndColor[] SacArray;
    public GameObject UiGameObject;
    public GameObject[] Buttons;
    private ShapeGameUi UiComponent;
    
    

    public ShapeGame(GameController game) : base(game)
    {
        
        UiComponent = game.ShapeGameUI.GetComponent<ShapeGameUi>();
        UiComponent.SetGameController(this);
    }

    public void Play()
    {
        CurrentNumShapes = StartingNumShapes;
        UiComponent.HideButtons();
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
        UiComponent.SetSacArray(SacArray);
        UiComponent.FormatButtons(CurrentNumShapes, MaxNumShapes);
        UiComponent.EnableButtons();
        // choose correct index
        CurrentCorrectIndex = UnityEngine.Random.Range(0, (CurrentNumShapes - 1));
        // match index to shape for audio
        PlayInstruction();
    }

    public void PlayInstruction()
    {
        Audio_Controller.ShapeInstruction(SacArray[CurrentCorrectIndex].Shape);
    }

    public void OnInstructionClick()
    {       
        PlayInstruction();
    }

    private void onIncorrect()
    {
        IncorrectGuessInRound = true;
        Audio_Controller.TryAgain(SacArray[CurrentCorrectIndex].Shape);
        Audio_Controller.Incorrect();
    }

    private void onCorrect()
    {
        //Audio_Controller.Correct();
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
        if (LastParticle && !LastParticle.IsAlive())
            OnParticlesComplete();

        if (Accolading && !Game.AccoladePlaying)
            OnAccoladeComplete();
    }

    private void OnParticlesComplete()
    {
        LastParticle.gameObject.SetActive(false);
        LastParticle = null;
        PlayAccolades();
    }

    private void PlayAccolades()
    {
        Accolading = true;
        Game.PlayRandomAccolade();
                    
    }

    public void Quit()
    {
        //reset anything that needs to be reset. 
        // Let Game controller know that we're done. 
        
        if (LastParticle != null)
            LastParticle.gameObject.SetActive(false);
        Audio_Controller.StopAllStoredSounds();
        UiComponent.Quit();
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
        // hide incorrect sprites
        // play affirmation.
        // animate-scale correct
        // repeat shape name
        // add particles
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
        PlayShapeAffirmationAudio(s);
        LastParticle = p;
        UiComponent.Win(CurrentCorrectIndex);
        
        p.gameObject.SetActive(true);
        p.Play();
        Audio_Controller.ParticleAudio();
    }

    private void PlayShapeAffirmationAudio(ShapeAndColor.Shapes s)
    {
        Audio_Controller.YeahAudio();
        Audio_Controller.ShapeAffirmation(s);
    }

    public void OnAccoladeComplete()
    {
        Accolading = false;
        UiComponent.PostWinReset();
        NewRound();
    }

    

    
    

}
