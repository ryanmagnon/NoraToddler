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

public class ColorGame : AbstractGuessingGame, IGuessingGame, IGame
{
    //private SpriteManager sprite_Manager;
    

    private ShapeAndColor[] SacArray = null;
    
   
    public ColorGame(GameController game): base(game)
    {
        MaxNumButtons = 6;
        MinNumButtons = 2;
        StartingNumButtons = 3;
        GameInstance = this;
        UiComponent = game.ColorGameUI.GetComponent<ColorGameUi>();
        UiComponent.SetGuessingGame(this);
    }

    public int GetScreen()
    {
        return GameController.colorMenu;
    }

    public void PlayInstruction(int CurrentCorrectIndex)
    {
        Audio_Controller.ColorInstruction(SacArray[CurrentCorrectIndex].Color);
    }
    
    public void OnIncorrect(int CurrentCorrectIndex)
    {
        Audio_Controller.TryAgain(SacArray[CurrentCorrectIndex].Color);
    }
    
    public void SetSpriteData(int CurrentNumColors)
    {
        SacArray = Sprite_Manager.getRandomShapes(CurrentNumColors);
        // set buttons to sprites in array        
        UiComponent.SetData(SacArray);
    }

    public ParticleSystem GetParticle(int i)
    {
        ShapeAndColor.Colors c = SacArray[CurrentCorrectIndex].Color;
        ParticleSystem p = null;
        switch (c)
        {
            case ShapeAndColor.Colors.Red:
                p = GameCon.ColorParticles[0];
                break;
            case ShapeAndColor.Colors.Orange:
                p = GameCon.ColorParticles[1];
                break;
            case ShapeAndColor.Colors.Yellow:
                p = GameCon.ColorParticles[2];
                break;
            case ShapeAndColor.Colors.Green:
                p = GameCon.ColorParticles[3];
                break;
            case ShapeAndColor.Colors.Blue:
                p = GameCon.ColorParticles[4];
                break;
            case ShapeAndColor.Colors.Purple:
                p = GameCon.ColorParticles[5];
                break;
        }
        return p;
    }

    public void PlayAffirmationAudio(int CorrectIndex)
    {
        Audio_Controller.YeahAudio();
        Audio_Controller.ColorAffirmation(SacArray[CorrectIndex].Color);
    }
    
}

