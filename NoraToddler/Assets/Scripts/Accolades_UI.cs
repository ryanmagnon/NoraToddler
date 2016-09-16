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
        gameObject.SetActive(false);
        CloseRunning = false;
        IsAnimating = false;
    }

    public void PlayRandomAccolade()
    {
        int i = UnityEngine.Random.Range(0, AccoladeSprites.Length - 1);
        IsAnimating = true;
        gameObject.SetActive(true);
        a.Play("BounceIn");
        OpenRunning = true;
        PlaceHolder.GetComponent<Image>().sprite = AccoladeSprites[i];
                               
        Game.AudioController.PayoffAudio(i);
            
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
        a.Play("Normal");        
    }
    private void AccoladeTimerComplete()
    {        
        RunClose();
    }

    internal void Quit()
    {
        gameObject.SetActive(false);
    }
}
