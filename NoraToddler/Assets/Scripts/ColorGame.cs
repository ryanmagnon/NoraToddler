using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ColorGame : IGame
{
    private SpriteManager sprite_Manager;

    public ColorGame(GameController game)
    {
        sprite_Manager = game.Sprite_Manager;
    }

    public int GetScreen()
    {
        throw new NotImplementedException();
    }

    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Quit()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}

