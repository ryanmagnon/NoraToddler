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
using System.Timers;
public class ShapeGame : AbstractGuessingGame, IGame, IGuessingGame
{


    private ShapeAndColor[] SacArray;
    


    public ShapeGame(GameController game) : base(game)
    {
        MaxNumButtons = 7;
        MinNumButtons = 2;
        StartingNumButtons = 3;
        GameInstance = this;   
        UiComponent = game.ShapeGameUI.GetComponent<ShapeGameUi>();
        UiComponent.SetGuessingGame(this);
    }

    public int GetScreen()
    {
        return GameController.shapeMenu;
    }


    public void PlayInstruction(int CurrentCorrectIndex)
    {
        Audio_Controller.ShapeInstruction(SacArray[CurrentCorrectIndex].Shape);
    }
    

    public void OnIncorrect(int CurrentCorrectIndex)
    {
        Audio_Controller.TryAgain(SacArray[CurrentCorrectIndex].Shape);
    }


    public void SetSpriteData(int CurrentNumShapes)
    {
        SacArray = Sprite_Manager.getRandomColors(CurrentNumShapes);
        // set buttons to sprites in array        
        UiComponent.SetData(SacArray);
    }

    public ParticleSystem GetParticle(int i)
    {
        ShapeAndColor.Shapes s = SacArray[CurrentCorrectIndex].Shape;
        ParticleSystem p = null;
        switch (s)
        {
            case ShapeAndColor.Shapes.Circle:
                p = GameCon.ShapeParticles[0];
                break;
            case ShapeAndColor.Shapes.Diamond:
                p = GameCon.ShapeParticles[1];
                break;
            case ShapeAndColor.Shapes.Oval:
                p = GameCon.ShapeParticles[2];
                break;
            case ShapeAndColor.Shapes.Rectangle:
                p = GameCon.ShapeParticles[3];
                break;
            case ShapeAndColor.Shapes.Square:
                p = GameCon.ShapeParticles[4];
                break;
            case ShapeAndColor.Shapes.Star:
                p = GameCon.ShapeParticles[5];
                break;
            case ShapeAndColor.Shapes.Triangle:
                p = GameCon.ShapeParticles[6];
                break;
        }
        return p;
    }

    public void PlayAffirmationAudio(int CorrectIndex)
    {
        Audio_Controller.YeahAudio();
        Audio_Controller.ShapeAffirmation(SacArray[CorrectIndex].Shape);
    }
    
    

}
