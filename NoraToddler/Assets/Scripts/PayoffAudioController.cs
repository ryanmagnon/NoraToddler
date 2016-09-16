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
using System;

public class PayoffAudioController : MonoBehaviour {
    public AudioSource[] PayoffAudio;
    public AudioSource YeahAudio;
    public AudioSource TryAgainAudio;
    // Use this for initialization
    void Start () {
	
	}
	
    public AudioSource Payoff(int i)
    {
        AudioSource a = null;
        if (i < PayoffAudio.Length)
            a = PayoffAudio[i];

        return a;
    }

	// Update is called once per frame
	void Update () {
	    
	}

    public AudioSource TryAgain()
    {
        return TryAgainAudio;
    }

    public AudioSource Yeah()
    {
        return YeahAudio;
        
    }
}
