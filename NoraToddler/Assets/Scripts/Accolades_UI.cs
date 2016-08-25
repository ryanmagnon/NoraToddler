using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Accolades_UI : MonoBehaviour {
    public Sprite[] AccoladeSprites;
    public GameObject PlaceHolder;
    public GameController Game;
    public Animator a;
    public SFXController SfxController;
    private bool OpenRunning = false;
    private bool CloseRunning = false;
    public bool IsAnimating = false;
    public float Delay = 1.5f;
    private float InternalDelay = 0.5f;
    public bool TimerRunning = false;
    // Use this for initialization
    void Start () {
        a = PlaceHolder.GetComponent<Animator>();
        InternalDelay = Delay;
    }
	
    private bool InternalAnimating
    {
        get { return !a.GetCurrentAnimatorStateInfo(0).IsName("Normal"); }
    }

    

    // Update is called once per frame
    void Update () {

        if (OpenRunning && !InternalAnimating)
            RunTimer();
        if (CloseRunning && !InternalAnimating)
            Finish();
        if (TimerRunning)
            Timer();

	}

    private void Timer()
    {
        Delay -= Time.deltaTime;
        if (Delay < 0)
        {
            ResetTimer();
            AccoladeTimerComplete();
        }
    }

    private void ResetTimer()
    {
        TimerRunning = false;
        Delay = InternalDelay;
    }

    private void Finish()
    {
        IsAnimating = false;
        gameObject.SetActive(false);
    }

    public void PlayRandomAccolade()
    {
        IsAnimating = true;
        OpenRunning = true;
        gameObject.SetActive(true);
        int i = UnityEngine.Random.Range(0, AccoladeSprites.Length - 1);
        PlaceHolder.GetComponent<Image>().sprite = AccoladeSprites[i];
        a.Play("BounceIn");
    }

    public void RunClose()
    {
        a.Play("BounceOut");
        CloseRunning = true;
    }

    private void RunTimer()
    {
        OpenRunning = false;
        TimerRunning = true;        
    }
    private void AccoladeTimerComplete()
    {        
        RunClose();
    }

}
