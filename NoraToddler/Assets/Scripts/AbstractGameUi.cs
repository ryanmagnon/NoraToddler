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
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
public class AbstractGameUi : MonoBehaviour
{
    public GameObject[] Buttons;
    protected float padding;
    protected bool ClickEnabled = false;
    


    protected void setButtonPosition(int i, int currentShapeTotal)
    {
        // Returns 0 or 1 depending on whether i is less or more than half of currentNumShapes        
        int row = (int)Math.Floor(i / (currentShapeTotal * 0.5f));

        // equation to determine column per row if ever needed
        //int col = (int)Math.Floor(i % (currentShapeTotal * 0.5f)) + 1;

        RectTransform rt = Buttons[i].gameObject.GetComponent<RectTransform>();

        //used to find h center offset.       
        float xMultiplier = i % (float)(currentShapeTotal * .5) + 1 - ((float)Math.Ceiling(currentShapeTotal * 0.5f) - 0.5f * ((float)Math.Ceiling(currentShapeTotal * 0.5f) - 1));

        // offset for v center
        float yMultiplier = (row - 0.5f) * -2;

        // dimensions of button
        float width = rt.rect.width * 0.4f;
        float height = rt.rect.height * 0.2f;

        // used ifs to prevent NaN from dividing by zero
        float paddingXMultiplier = 0;
        if (Math.Abs(xMultiplier) > 0)
            paddingXMultiplier = xMultiplier / Math.Abs(xMultiplier); // returns -1 or +1
        /*
        float paddingYMultiplier = 0;
        if (Math.Abs(yMultiplier) > 0)
            paddingYMultiplier = yMultiplier / Math.Abs(yMultiplier); // returns -1 or +1
            */
        // set the x and y of button
        float x = width * xMultiplier + padding * paddingXMultiplier;
        float y = height * yMultiplier + padding;
        rt.anchoredPosition = new Vector2(x, y);
    }



    protected void setButtonSprite(int i, Sprite s)
    {
        Buttons[i].GetComponent<Image>().sprite = s;
    }

    public void HideButtons()
    {
        for (int i = 0; i < Buttons.Length; i++)
            Buttons[i].GetComponent<Animator>().Play("InstantTransparent");
    }


    protected void deactivateShapeButton(int i)
    {
        Buttons[i].SetActive(false);
    }

    protected void activateShapeButton(int i)
    {
        Buttons[i].SetActive(true);
        Animator a = Buttons[i].GetComponent<Animator>();
        a.Play("FadeIn");
    }

    protected void enableButtonInteraction()
    {
        ClickEnabled = true;
    }

    public void EnableButtons()
    {
        enableButtonInteraction();
    }

    protected void disableButtonInteraction()
    {
        ClickEnabled = false;
    }


}
