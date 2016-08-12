using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IGame
{
    void Play();
    void Quit();
    int GetScreen();
    void Update();
}
