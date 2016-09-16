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

public class ColorAudioController : MonoBehaviour {

    public AudioSource[] Instructions;
    public AudioSource[] Affirmations;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal AudioSource InstructionAudio(ShapeAndColor.Colors color)
    {
        AudioSource a = null;
        if (Instructions.Length >= 6)
            switch (color)
            {
                case ShapeAndColor.Colors.Red:
                    a = Instructions[0];
                    break;
                case ShapeAndColor.Colors.Orange:
                    a = Instructions[1];
                    break;
                case ShapeAndColor.Colors.Yellow:
                    a = Instructions[2];
                    break;
                case ShapeAndColor.Colors.Green:
                    a = Instructions[3];
                    break;
                case ShapeAndColor.Colors.Blue:
                    a = Instructions[4];
                    break;
                case ShapeAndColor.Colors.Purple:
                    a = Instructions[5];
                    break;
            }
        return a;
    }

    internal AudioSource PlayAffirmation(ShapeAndColor.Colors color)
    {

        AudioSource a = null;
        if (Affirmations.Length >= 6)
            switch (color)
            {
                case ShapeAndColor.Colors.Red:
                    a = Affirmations[0];
                    break;
                case ShapeAndColor.Colors.Orange:
                    a = Affirmations[1];
                    break;
                case ShapeAndColor.Colors.Yellow:
                    a = Affirmations[2];
                    break;
                case ShapeAndColor.Colors.Green:
                    a = Affirmations[3];
                    break;
                case ShapeAndColor.Colors.Blue:
                    a = Affirmations[4];
                    break;
                case ShapeAndColor.Colors.Purple:
                    a = Affirmations[5];
                    break;
            }
        else
            throw new System.Exception("Affirmations audio has fewer than 6 clips");
        return a;
    }
}
