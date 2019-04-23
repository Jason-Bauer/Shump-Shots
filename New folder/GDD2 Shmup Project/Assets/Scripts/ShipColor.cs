using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>The color of an object</summary>
public enum GameColor
{
    red, yellow, green, blue
}

public class ShipColor : MonoBehaviour {

    public Color red;
    public Color blue;
    public Color green;
    public Color yellow;

    private Material shipMat;
    //private Color shipColor;

    //private List<Material> colors; // list of colors
    
    public GameColor currColor; // track the index of the current player color

	// Use this for initialization
	void Start () {
        // initialize the list of colors from materials
        //colors = new List<Material>();

        // populate the list of colors
        //colors.Add(red);
        //colors.Add(yellow);
        //colors.Add(green);
        //colors.Add(blue);


        // Default ship color is red
        if (currColor != 0)
        {
            currColor = GameColor.red;
        }

        // Get the ship material component
        shipMat = GetComponent<Renderer>().material;
    }
	

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButton("B"))
        {
            currColor = GameColor.red;
            shipMat.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetButton("X"))
        {
            currColor = GameColor.blue;
            shipMat.color = Color.blue;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButton("A"))
        {
            currColor = GameColor.green;
            shipMat.color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButton("Y"))
        {
            currColor = GameColor.yellow;
            shipMat.color = Color.yellow;
        }

        // Cycle through the colors on tab / right click
        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse5))
        {
            // make sure we stay in range while incrementing each time
            currColor = (GameColor)(((int)currColor + 1) % 4); 

            // Set the current color
            switch (currColor)
            {
                case GameColor.red:
                    shipMat.color = Color.red;
                    break;

                case GameColor.yellow:
                    shipMat.color = Color.yellow;
                    break;

                case GameColor.green:
                    shipMat.color = Color.green;
                    break;

                case GameColor.blue:
                    shipMat.color = Color.blue;
                    break;

                default:
                    shipMat.color = Color.red;
                    break;
            }

        }
    }
}
