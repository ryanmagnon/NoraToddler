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
public class AbstractGuessingGameUi: AbstractGameUi
{
    protected float StartingButtonScale;
    private ISpriteContainer[] Sps;
    protected IGuessingGame Game;

    public float WinScale = 1f;
    protected bool WinningAnimation = false;
    public bool IsAnimating { get { return WinningAnimation; } }
    public float AnimationSpeed = 0.01f;
    protected float CurrentAnimationSpeed;
    public float AnimationIncrement = 0.01f;
    public GameObject Audio_Button;
    protected GameObject WinningButton;
    public float StartingAnimationSpeed = 0.1f;

    private int LastWinningButton = -1;

    void Update()
    {
        if (WinningAnimation)
            PlayWinning();
    }

    public void Quit()
    {
        ResetButtonsToStart();
        FinishWinning();
        ClearLastWinningButton();
    }

    private void ResetButtonsToStart()
    {
        Animator a;
        GameObject b;
        for (int i = 0; i < Buttons.Length; i++)
        {
            b = Buttons[i];
            a = b.GetComponent<Animator>();
            a.StopPlayback();
            b.transform.localScale = new Vector3(StartingButtonScale, StartingButtonScale, 1);
        }
    }

    public void SetData(ISpriteContainer[] sps)
    {
        Sps = sps;
    }

    public void SetGuessingGame(IGuessingGame g)
    {
        Game = g;
    }

    public void Win(int winningButtonIndex)
    {
        ClickEnabled = false;

        WinningAnimation = true;
        CurrentAnimationSpeed = StartingAnimationSpeed;
        LastWinningButton = winningButtonIndex;
        WinningButton = Buttons[LastWinningButton];
        WinningButton.transform.SetSiblingIndex(Buttons.Length-1);
        FadeIncorrectButtons();
        FadeAudioButton();
        WinningButton.GetComponent<Animator>().Play("ScaleUp");
        disableButtonInteraction();
    }


    private void FinishWinning()
    {
        CurrentAnimationSpeed = StartingAnimationSpeed;
        WinningAnimation = false;
    }

    private void PlayWinning()
    {

        if (CurrentAnimationSpeed > 1)
            CurrentAnimationSpeed = 1;
        WinningButton.transform.localPosition = Vector2.Lerp(WinningButton.transform.localPosition, new Vector2(0, 0), CurrentAnimationSpeed);
        if (CurrentAnimationSpeed != 1)
        {
            CurrentAnimationSpeed += AnimationIncrement;
        }
        else
        {
            WinningButton.transform.localPosition = new Vector2(0, 0);
            FinishWinning();
        }
    }

    private void FadeIncorrectButtons()
    {
        GameObject b;
        Animator a;
        for (int i = 0; i < Buttons.Length; i++)
        {
            b = Buttons[i];
            if (b != WinningButton)
            {
                a = b.GetComponent<Animator>();
                a.Play("FadeOut");
            }
        }
    }

    public void FormatButtons(int currentNum, int maxNum)
    {
        for (int i = 0; i < maxNum; i++)
        {
            if (i < currentNum)
            {
                activateShapeButton(i);
                setButtonSprite(i, Sps[i].Sprite);
                setButtonPosition(i, currentNum);
            }
            else
            {
                deactivateShapeButton(i);
            }
        }
    }
    
    public void PostWinReset()
    {
        if (WinningButton != null)
            WinningButton.GetComponent<Animator>().Play("ScaleDown");
        enableButtonInteraction();
        enableAudioButton();
        ClickEnabled = true;
    }

    private void enableAudioButton()
    {
        Audio_Button.GetComponent<Animator>().Play("FadeIn");
    }

    private void FadeAudioButton()
    {
        Audio_Button.GetComponent<Animator>().Play("FadeOut");
    }
    
    public void ResetWinner()
    {
        WinningButton = Buttons[LastWinningButton];
        WinningButton.GetComponent<Animator>().Play("FadeOut");
        ClearLastWinningButton();
    }

    public void ClearLastWinningButton()
    {
        LastWinningButton = -1;
    }
    
    public void OnConfirmationComplete()
    {
        ResetWinner();
    }

    public void OnInstructionDown()
    {
        if(ClickEnabled)
            Audio_Button.GetComponent<Animator>().Play("Pressed");
    }

    public void OnInstructionUp()
    {
        if (Audio_Button.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pressed"))
            Audio_Button.GetComponent<Animator>().Play("Normal");
    }
}