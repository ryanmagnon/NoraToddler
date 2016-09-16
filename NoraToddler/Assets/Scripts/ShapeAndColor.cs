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

public class ShapeAndColor : MonoBehaviour, ISpriteContainer {
    public enum Shapes { Star, Square, Circle, Oval, Diamond, Rectangle, Triangle };
    public enum Colors { Red, Orange, Yellow, Green, Blue, Purple };
    public Shapes Shape;
    public Colors Color;
    public Sprite SpriteSource;
    public AudioClip ShapeAudio;
    public AudioClip ColorAudio;
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public Sprite Sprite
    {
        get 
        {
            return SpriteSource;
        }
        set
        {
            SpriteSource = value;
        }
    }
}

