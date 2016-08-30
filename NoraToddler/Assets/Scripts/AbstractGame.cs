using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AbstractGame
{
    protected GameController Game;
    protected ParticleSystem LastParticle = null;
    protected bool Accolading = false;
    protected AudioController Audio_Controller;
    protected SpriteManager Sprite_Manager;

    public AbstractGame(GameController game)
    {
        Game = game;
        Sprite_Manager = game.Sprite_Manager;
        Audio_Controller = game.AudioController;
    }
}
