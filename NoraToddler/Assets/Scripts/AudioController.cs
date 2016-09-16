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
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

public class AudioController : MonoBehaviour {
    public PayoffAudioController Payoff_Audio_Con;
    public SFXController Sfx;
    public ShapeAudioController ShapeAudio;
    public ColorAudioController ColorAudio;
    private List<AudioSource> Queue = new List<AudioSource>();
    
    private AudioSource LastQueuedClip = null;
    private AudioSource LastInstantClip = null;

    public bool ClipIsPlaying {
        get
        {
            return LastQueuedClip && LastQueuedClip.isPlaying;
        }
    }

    //public ShapeInstructionController

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Queue.Count > 0 && !ClipIsPlaying)
        {
            PopAndPlayFromQueue();
        }
	}

    private void PopAndPlayFromQueue()
    {
        
        AudioSource IndexZeroClip = Queue[0];
        Queue = ShiftAudioSourceArray(1, Queue);
        
        Play(IndexZeroClip);
        
    }

    private List<AudioSource> ShiftAudioSourceArray(int v, List<AudioSource> q)
    {
        List<AudioSource> l = new List<AudioSource>();
        //int n = v;
        //Debug.Log("for (int "+n+" = "+v+"; "+n+" < "+q.Count+" - "+v+"; "+ n +"++)");
        if (v < q.Count)
            for (int i = 0; i < q.Count - v; i++)
            {
                l.Add(q[i+v]);
            }
        return l;
    }

    private void Play(AudioSource a)
    {
        LastQueuedClip = a;
        a.Play();
    }
    /**
     * Used for when you want an audio clip to play without overriding the last audio
     */
    public void QueueAudio(AudioSource e, bool clearQueue = false, bool requeue = false)
    {
        if (clearQueue)
            ClearQueue();
        if(requeue || !IsAudioInQueue(e))
            Queue.Add(e);
    }

    public void ColorInstruction(ShapeAndColor.Colors color)
    {
        PlayAudio(ColorAudio.InstructionAudio(color));
    }

    private void ClearQueue()
    {
        if (LastQueuedClip != null && LastQueuedClip.isPlaying)
            LastQueuedClip.Stop();
        Queue = new List<AudioSource>();
    }

    private bool IsAudioInQueue(AudioSource a)
    {
        bool r = false;
        for (int i = 0; i < Queue.Count; i++)
            if (Queue[i] == a)
            {
                r = true;
                break;
            }

        if (!r && a == LastQueuedClip && LastQueuedClip.isPlaying)
            r = true;
        return r;
    }

    public void ShapeInstruction(ShapeAndColor.Shapes shape)
    {
        PlayAudio(ShapeAudio.InstructionAudio(shape));
    }

    public void YeahAudio()
    {
        QueueAudio(Payoff_Audio_Con.Yeah(), true);
    }

    public void PayoffAudio(int i)
    {
        QueueAudio(Payoff_Audio_Con.Payoff(i));
    }

    public void ShapeAffirmation(ShapeAndColor.Shapes shape)
    {
        QueueAudio(ShapeAudio.PlayAffirmation(shape));
    }

    public void TryAgain(ShapeAndColor.Shapes shape)
    {
        QueueAudio(Payoff_Audio_Con.TryAgain());
        QueueAudio(ShapeAudio.InstructionAudio(shape));
    }

    internal void StopAllStoredSounds()
    {
        ClearQueue();
        if (LastInstantClip != null && LastInstantClip.isPlaying)
            LastInstantClip.Stop();
    }

    public void TryAgain(ShapeAndColor.Colors color)
    {
        QueueAudio(Payoff_Audio_Con.TryAgain());
        QueueAudio(ColorAudio.InstructionAudio(color));
    }

    public void Correct()
    {
        PlayAudio(Sfx.CorrectAudio);
    }

    public void ParticleAudio()
    {
        PlayAudio(Sfx.ParticleAudio, false);
    }

    public void Incorrect()
    {
        PlayAudio(Sfx.IncorrectAudio, false);
    }

    public void Applause()
    {
        QueueAudio(Sfx.ApplauseAudio);
    }

    /**
     * Used for when you want an audio clip to play without queing it.
     */
    public void PlayAudio(AudioSource a, bool StopPreviousSound = true)
    {
        if (StopPreviousSound && LastInstantClip != null && LastInstantClip.isPlaying)
            LastInstantClip.Stop();
        a.Play();
        if(StopPreviousSound)
            LastInstantClip = a;
    }

    internal void ColorAffirmation(ShapeAndColor.Colors c)
    {
        QueueAudio(ColorAudio.PlayAffirmation(c));
    }
}
