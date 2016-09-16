/*
This file is part of Nora. 

Nora is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Nora is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Nora. If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AbstractGuessingGame : AbstractGame
{
    public int StartingNumButtons = 0;
    public int MaxNumButtons = 0;
    public int MinNumButtons = 0;
    private int CurrentNumShapes;
    protected int CurrentCorrectIndex;
    private bool IncorrectGuessInRound;
    
    public GameObject UiGameObject;
    public GameObject[] Buttons;
    public float GameMinutes = 20;
    private int GameTimerId;
    protected IGuessingGameUi UiComponent; // TODO: Convert this to Interface.

    protected IGuessingGame GameInstance;
    private bool GameStarted = false;

    public AbstractGuessingGame(GameController game) : base(game)
    {
    }

    public void Play()
    {
        CurrentNumShapes = StartingNumButtons;
        UiComponent.HideButtons();
        NewRound();
        GameTimerId = StartTimer(GameMinutes * 60);
        GameStarted = true;
        // quit screen when timer runs out.
    }



    public void NewRound()
    {
        IncorrectGuessInRound = false;
        // randomize shape choices
        GameInstance.SetSpriteData(CurrentNumShapes);
        UiComponent.FormatButtons(CurrentNumShapes, MaxNumButtons);
        UiComponent.EnableButtons();
        // choose correct index
        CurrentCorrectIndex = UnityEngine.Random.Range(0, (CurrentNumShapes - 1));
        // match index to shape for audio
        PlayInstruction();
    }

    public void PlayInstruction()
    {
        GameInstance.PlayInstruction(CurrentCorrectIndex);
    }

    public void OnInstructionClick()
    {
        PlayInstruction();
    }



    private void onIncorrect()
    {
        IncorrectGuessInRound = true;
        GameInstance.OnIncorrect(CurrentCorrectIndex);
        Audio_Controller.Incorrect();
    }

    private void onCorrect()
    {
        //Audio_Controller.Correct();
        // if IncorrectThisRound decrement CurrentNumShapes by one limited to min.
        if (!IncorrectGuessInRound)
        {
            if (CurrentNumShapes < MaxNumButtons)
                CurrentNumShapes++;
        }
        else
        {
            if (CurrentNumShapes > MinNumButtons)
                CurrentNumShapes--;
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
        ParticleSystem p = GameInstance.GetParticle(CurrentCorrectIndex);

        GameInstance.PlayAffirmationAudio(CurrentCorrectIndex);
        LastParticle = p;
        UiComponent.Win(CurrentCorrectIndex);

        p.gameObject.SetActive(true);
        p.Play();
        Audio_Controller.ParticleAudio();
    }

    override public void Update()
    {
        base.Update();
        if (LastParticle && !LastParticle.IsAlive())
            OnParticlesComplete();

        if (Accolading && !GameCon.AccoladePlaying)
            OnAccoladeComplete();

        if (GameStarted && GetTimer(GameTimerId) == 0)
            TimeOutQuit();
    }

    private void TimeOutQuit()
    {
        
        GameCon.TimeOutQuit();
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
        GameCon.PlayRandomAccolade();

    }

    public void Quit()
    {
        //reset anything that needs to be reset. 
        // Let Game controller know that we're done. 
        GameStarted = false;
        if (LastParticle != null)
            LastParticle.gameObject.SetActive(false);
        Audio_Controller.StopAllStoredSounds();
        UiComponent.Quit();
    }



    public void OnClickGuess(int index)
    {
        // check if matches correct index
        if (index == CurrentCorrectIndex)
            onCorrect();
        else
            onIncorrect();
    }

    public void OnAccoladeComplete()
    {
        Accolading = false;
        UiComponent.PostWinReset();
        NewRound();
    }
}
