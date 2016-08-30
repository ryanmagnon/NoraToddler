using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class ColorGameUi : AbstractGameUi
{
    private ShapeAndColor[] Sacs;
    private ColorGame ColorGame;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGameController(ColorGame c)
    {
        ColorGame = c;
    }

    public void SetSacArray(ShapeAndColor[] sac)
    {
        Sacs = sac;
    }

    public void FormatButtons(int currentNum, int maxNum)
    {
        for (int i = 0; i < maxNum; i++)
        {
            if (i < currentNum)
            {
                activateShapeButton(i);
                setButtonSprite(i, Sacs[i].Sprite);
                setButtonPosition(i, currentNum);
            }
            else
            {
                deactivateShapeButton(i);
            }
        }
    }

    public void OnColorClick(int index)
    {
        if (ClickEnabled)
            ColorGame.OnColorClick(index);
    }

    public void OnInstructionClick()
    {
        if (ClickEnabled)
            ColorGame.OnInstructionClick();
    }
}
