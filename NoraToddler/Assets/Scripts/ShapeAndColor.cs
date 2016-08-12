using UnityEngine;
using System.Collections;

public class ShapeAndColor : MonoBehaviour {
    public enum Shapes { Star, Square, Circle, Oval, Diamond, Rectangle, Triangle };
    public enum Colors { Red, Orange, Yellow, Green, Blue, Purple };
    public Shapes Shape;
    public Colors Color;
    public Sprite Sprite;
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
}
