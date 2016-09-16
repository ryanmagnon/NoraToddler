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

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class ColorGameUi : AbstractGuessingGameUi, IGuessingGameUi
{
    

    
    // Use this for initialization
    void Start()
    {
        StartingButtonScale = Buttons[0].gameObject.GetComponent<RectTransform>().localScale.x;

    }


    public void OnColorClick(int index)
    {
        if (ClickEnabled)
            Game.OnClickGuess(index);

    }
    
    public void OnInstructionClick()
    {
        if (ClickEnabled)
            Game.PlayInstruction();
    }





}
