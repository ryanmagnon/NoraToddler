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

public class AbstractGame
{
    protected GameController GameCon;
    protected ParticleSystem LastParticle = null;
    protected bool Accolading = false;
    protected AudioController Audio_Controller;
    protected SpriteManager Sprite_Manager;
    private List<float> Timers = new List<float>();

    public AbstractGame(GameController game)
    {
        GameCon = game;
        Sprite_Manager = game.Sprite_Manager;
        Audio_Controller = game.AudioController;
    }

    protected int StartTimer(float seconds)
    {
        int id = Timers.Count;
        Timers.Insert(id, seconds);
        return id;
    }

    virtual public void Update()
    {
        if (Timers.Count > 0)
            DecrementTimers();
    }

    private void DecrementTimers()
    {
        for(int i = 0; i < Timers.Count; i++)
            DecrementTimer(i);
    }

    private void DecrementTimer(int i)
    {
        Debug.Log(Timers[i]);
        
        if (Timers[i] - Time.deltaTime > 0)
            Timers[i] -= Time.deltaTime;
        else
            Timers[i] = 0;
    }

    public void ClearTimers()
    {
        Timers = new List<float>();
    }

    public float GetTimer(int id)
    {
        return Timers[id];
    }

}
