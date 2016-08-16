using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;
public class ShapeGameUi : MonoBehaviour {
    public GameObject[] Buttons;
    // Use this for initialization
    private ShapeGame ShapeGame;
    private ShapeAndColor[] Sacs;
    private ParticleSystem LastParticle = null;
    private float padding = 30f;
    private float StartingScale;
    public float WinScale = 1f;
    private bool WinningAnimation = false;
    public float AnimationSpeed = 0.01f;
    private float CurrentAnimationSpeed;
    public float AnimationIncrement = 0.01f;
     
    private GameObject WinningButton;
    private bool ClickEnabled = false;

    public float StartingAnimationSpeed = 0.1f;

    void Start () {
	    StartingScale = Buttons[0].gameObject.GetComponent<RectTransform>().localScale.x;
        for (int i = 0; i < Buttons.Length; i++)
            Buttons[i].GetComponent<Animator>().Play("InstantTransparent");
    }
	
	// Update is called once per frame
	void Update () {
        if (LastParticle && !LastParticle.IsAlive())
            OnParticlesComplete();
        if (WinningAnimation)
            PlayWinning();          
    }

    private void PlayWinning()
    {
                
        if (CurrentAnimationSpeed > 1)
            CurrentAnimationSpeed = 1;
        WinningButton.transform.localPosition = Vector2.Lerp(WinningButton.transform.localPosition, new Vector2(0, 0), CurrentAnimationSpeed);
        if (CurrentAnimationSpeed != 1)
            CurrentAnimationSpeed += AnimationIncrement;

        
        /*
        if (CurrentAnimationSpeed > 1)
            CurrentAnimationSpeed = 1;
        WinningButton.transform.localScale = Vector3.Lerp(WinningButton.transform.localScale, new Vector3(WinScale, WinScale, WinScale), CurrentAnimationSpeed);
        
        if (CurrentAnimationSpeed != 1)
            CurrentAnimationSpeed += AnimationIncrement;
        */
    }

    public void EnableButtons()
    {
        enableButtonInteraction();
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

    private void FinishWinning()
    {
        CurrentAnimationSpeed = StartingAnimationSpeed;
        WinningAnimation = false;
        WinningButton.GetComponent<Animator>().Play("ScaleDown");
        enableButtonInteraction();
        ClickEnabled = true;
    }

    private void enableButtonInteraction()
    {
        ClickEnabled = true;
    }

    public void SetGameController(ShapeGame g)
    {
        ShapeGame = g;
    }

    public void FormatButtons(int currentNum, int maxNum)
    {        
        for (int i = 0; i < maxNum; i++)
        {
            if (i < currentNum)
            {
                activateShapeButton(i);
                setButtonSprite(i);
                setButtonPosition(i, currentNum);

            }
            else
            {
                deactivateShapeButton(i);
            }
        }
    }

    private void setButtonPosition(int i, int currentShapeTotal)
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
        float paddingYMultiplier = 0;
        if (Math.Abs(yMultiplier) > 0)
            paddingYMultiplier = yMultiplier / Math.Abs(yMultiplier); // returns -1 or +1

        // set the x and y of button
        float x = width * xMultiplier + padding * paddingXMultiplier;
        float y = height * yMultiplier + padding;
        rt.anchoredPosition = new Vector2(x, y);
    }

    private void setButtonSprite(int i)
    {
        Buttons[i].GetComponent<Image>().sprite = Sacs[i].Sprite;
    }

    private void deactivateShapeButton(int i)
    {
        Buttons[i].SetActive(false);
    }

    private void activateShapeButton(int i)
    {
        Buttons[i].SetActive(true);
        Animator a = Buttons[i].GetComponent<Animator>();
        a.Play("FadeIn");
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

    public void Win(ParticleSystem p, int winningButtonIndex)
    {
        ClickEnabled = false;
        LastParticle = p;
        WinningAnimation = true;        
        CurrentAnimationSpeed = StartingAnimationSpeed;
        WinningButton = Buttons[winningButtonIndex];
        p.gameObject.SetActive(true);
        p.Play();
        FadeIncorrectButtons();
        WinningButton.GetComponent<Animator>().Play("ScaleUp");
        disableButtonInteraction();
    }

    private void disableButtonInteraction()
    {
        ClickEnabled = false;
    }

    private void OnParticlesComplete()
    {
        LastParticle.gameObject.SetActive(false);
        ShapeGame.NewRound();
        LastParticle = null;
        FinishWinning();
    }
    
}
