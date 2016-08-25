using UnityEngine;
using System.Collections;

public class ShapeAudioController : MonoBehaviour {

    public AudioSource[] Instructions;
    public AudioSource[] Affirmations;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public AudioSource InstructionAudio(ShapeAndColor.Shapes shape)
    {

        AudioSource a = null;
        if (Instructions.Length >= 7)
            switch (shape)
            {
                case ShapeAndColor.Shapes.Circle:
                    a = Instructions[0];
                    break;
                case ShapeAndColor.Shapes.Diamond:
                    a = Instructions[1];
                    break;
                case ShapeAndColor.Shapes.Oval:
                    a = Instructions[2];
                    break;
                case ShapeAndColor.Shapes.Rectangle:
                    a = Instructions[3];
                    break;
                case ShapeAndColor.Shapes.Square:
                    a = Instructions[4];
                    break;
                case ShapeAndColor.Shapes.Star:
                    a = Instructions[5];
                    break;
                case ShapeAndColor.Shapes.Triangle:
                    a = Instructions[6];
                    break;
            }
        return a;
    }

    public AudioSource PlayAffirmation(ShapeAndColor.Shapes shape)
    {

        AudioSource a = null;
        if (Instructions.Length >= 7)
            switch (shape)
            {
                case ShapeAndColor.Shapes.Circle:
                    a = Affirmations[0];
                    break;
                case ShapeAndColor.Shapes.Diamond:
                    a = Affirmations[1];
                    break;
                case ShapeAndColor.Shapes.Oval:
                    a = Affirmations[2];
                    break;
                case ShapeAndColor.Shapes.Rectangle:
                    a = Affirmations[3];
                    break;
                case ShapeAndColor.Shapes.Square:
                    a = Affirmations[4];
                    break;
                case ShapeAndColor.Shapes.Star:
                    a = Affirmations[5];
                    break;
                case ShapeAndColor.Shapes.Triangle:
                    a = Affirmations[6];
                    break;
            }
        return a;
    }
}
