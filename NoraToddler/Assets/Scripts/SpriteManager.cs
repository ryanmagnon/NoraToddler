using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour {

    private List<ShapeAndColor> RedShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> OrangeShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> YellowShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> GreenShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> BlueShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> PurpleShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor>[] ColorSacs = new List<ShapeAndColor>[6];

    private List<ShapeAndColor> StarShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> OvalShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> TriangleShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> RectangleShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> DiamondShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> CircleShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor> SquareShapes = new List<ShapeAndColor>();
    private List<ShapeAndColor>[] ShapeSacs = new List<ShapeAndColor>[7];


    // Use this for initialization
    void Start()
    {
        initArrays();
    }

    private void fillJaggedArrays()
    {
        ColorSacs[0] = RedShapes;
        ColorSacs[1] = OrangeShapes;
        ColorSacs[2] = YellowShapes;
        ColorSacs[3] = GreenShapes;
        ColorSacs[4] = BlueShapes;
        ColorSacs[5] = PurpleShapes;
        

        ShapeSacs[0] = StarShapes;
        ShapeSacs[1] = OvalShapes;
        ShapeSacs[2] = TriangleShapes;
        ShapeSacs[3] = RectangleShapes;
        ShapeSacs[4] = DiamondShapes;
        ShapeSacs[5] = CircleShapes;
        ShapeSacs[6] = SquareShapes;

    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    public void initArrays()
    {
        loopGameObjectChildren(gameObject);
        fillJaggedArrays();
    }

    private List<ShapeAndColor> shuffleShapeAndColorList(List<ShapeAndColor> sac)
    {
        int j;
        ShapeAndColor t;
        for (int i = 0; i < sac.Count; i++)
        {
            j = RandomInt(0, sac.Count - 1);
            t = sac[i];
            sac[i] = sac[j];
            sac[j] = t;
        }
        return sac;
    }

    public ShapeAndColor[] getRandomShapes(int n)
    {
        List<ShapeAndColor> l = pickRandomShapeList();
        l = shuffleShapeAndColorList(l);
        ShapeAndColor[] a = convertListToLimitedArray(l, n);
        return a;
    }

    private ShapeAndColor[] convertListToLimitedArray(List<ShapeAndColor> l, int n)
    {
        if (l.Count < n)
            n = l.Count;
        ShapeAndColor[] a = new ShapeAndColor[n];
        for (int i = 0; i < n; i++)
            a[i] = l[i];       
        return a;
    }

    public ShapeAndColor[] getRandomColors(int n)
    {
        List<ShapeAndColor> l = pickRandomColorList();
        l = shuffleShapeAndColorList(l);
        ShapeAndColor[] a = convertListToLimitedArray(l, n);
        return a;
    }

    private int RandomInt(int min, int max)
    {
        return  (int)Math.Round(UnityEngine.Random.Range((float)min, (float)max));
    }
    private List<ShapeAndColor> pickRandomColorList()
    {
        return ColorSacs[RandomInt(0, ColorSacs.Length-1)];
    }

    private List<ShapeAndColor> pickRandomShapeList()
    {
        int rand = RandomInt(0, ShapeSacs.Length - 1);
        return ShapeSacs[rand];
    }

    private void loopGameObjectChildren(GameObject go)
    {
        foreach (Transform child in go.transform)
        {
            if ((child.gameObject.GetComponent("ShapeAndColor") as ShapeAndColor) != null)
                passShapeToArray(child.gameObject);
            if (transform.childCount > 0)
                loopGameObjectChildren(child.gameObject);
        }
    }

    private void passShapeToArray(GameObject gameObject)
    {
        ShapeAndColor sac = gameObject.GetComponent("ShapeAndColor") as ShapeAndColor;
        switch (sac.Color)
        {
            case ShapeAndColor.Colors.Red:
                RedShapes.Add(sac);
                break;
            case ShapeAndColor.Colors.Orange:
                OrangeShapes.Add(sac);
                break;
            case ShapeAndColor.Colors.Yellow:
                YellowShapes.Add(sac);
                break;
            case ShapeAndColor.Colors.Green:
                GreenShapes.Add(sac);
                break;
            case ShapeAndColor.Colors.Blue:
                BlueShapes.Add(sac);
                break;
            case ShapeAndColor.Colors.Purple:
                PurpleShapes.Add(sac);
                break;
        }

        switch (sac.Shape)
        {
            case ShapeAndColor.Shapes.Circle:
                CircleShapes.Add(sac);
                break;
            case ShapeAndColor.Shapes.Diamond:
                DiamondShapes.Add(sac);
                break;
            case ShapeAndColor.Shapes.Oval:
                OvalShapes.Add(sac);
                break;
            case ShapeAndColor.Shapes.Rectangle:
                RectangleShapes.Add(sac);
                break;
            case ShapeAndColor.Shapes.Square:
                SquareShapes.Add(sac);
                break;
            case ShapeAndColor.Shapes.Star:
                StarShapes.Add(sac);
                break;
            case ShapeAndColor.Shapes.Triangle:
                TriangleShapes.Add(sac);
                break;
        }
    }
}
