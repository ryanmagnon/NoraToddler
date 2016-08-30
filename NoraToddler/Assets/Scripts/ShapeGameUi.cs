using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;
public class ShapeGameUi : AbstractGameUi {
    
    // Use this for initialization
    private ShapeGame ShapeGame;
    private ShapeAndColor[] Sacs;
    
    private int LastWinningButton = -1;
    
    private float StartingScale;
    public float WinScale = 1f;
    private bool WinningAnimation = false;
    public bool IsAnimating { get { return WinningAnimation; } }
    public float AnimationSpeed = 0.01f;
    private float CurrentAnimationSpeed;
    public float AnimationIncrement = 0.01f;
    public GameObject Audio_Button; 
    private GameObject WinningButton;
    
    public float StartingAnimationSpeed = 0.1f;

    void Start () {
	    StartingScale = Buttons[0].gameObject.GetComponent<RectTransform>().localScale.x;
    }
	
	// Update is called once per frame
	void Update () {
        if (WinningAnimation)
            PlayWinning();          
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
                setButtonSprite(i, Sacs[i].Sprite);
                setButtonPosition(i, currentNum);
            }
            else
            {
                deactivateShapeButton(i);
            }
        }
    }
    private void FinishWinning()
    {
        CurrentAnimationSpeed = StartingAnimationSpeed;
        WinningAnimation = false;
    }

    public void PostWinReset()
    {
        if(WinningButton != null)
            WinningButton.GetComponent<Animator>().Play("ScaleDown");
        enableButtonInteraction();
        enableAudioButton();
        ClickEnabled = true;
    }

    private void enableAudioButton()
    {
        Audio_Button.GetComponent<Animator>().Play("FadeIn");
    }



    public void SetGameController(ShapeGame g)
    {
        ShapeGame = g;
    }

    internal void SetSacArray(ShapeAndColor[] sacArray)
    {
        Sacs = sacArray;
    }

    public void OnShapeClick(int index)
    {
        if(ClickEnabled)
            ShapeGame.OnShapeClick(index);
    }

    public void OnInstructionClick()
    {
        if(ClickEnabled)
            ShapeGame.OnInstructionClick();
    }

    public void Win(int winningButtonIndex)
    {
        ClickEnabled = false;
        
        WinningAnimation = true;        
        CurrentAnimationSpeed = StartingAnimationSpeed;
        LastWinningButton = winningButtonIndex;
        WinningButton = Buttons[LastWinningButton];
        FadeIncorrectButtons();
        FadeAudioButton();
        WinningButton.GetComponent<Animator>().Play("ScaleUp");
        disableButtonInteraction();
    }

    private void FadeAudioButton()
    {
        Audio_Button.GetComponent<Animator>().Play("FadeOut");
    }




    private void PlayAccolades()
    {
        Debug.Log("PlayAccolades Go here");
    }

    public void ResetWinner()
    {
        WinningButton = Buttons[LastWinningButton];
        WinningButton.GetComponent<Animator>().Play("FadeOut");
        LastWinningButton = -1;
    }

    public void OnConfirmationComplete()
    {
        ResetWinner();
    }

    
}
