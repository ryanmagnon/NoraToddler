using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ColorGame : IGame
{
    

    public int GetScreen()
    {
        return GameController.colorMenu;
    }

    public void Play()
    {
        
    }

    public void Quit()
    {
        
    }
}
