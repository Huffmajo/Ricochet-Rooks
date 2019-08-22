using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
	public Renderer rend;
	public float paintLevel;
	public Color baseColor;

	private float paintLevelThreshold;

    // Start is called before the first frame update
    void Start()
    {
    	rend = GetComponent<Renderer>();
    	baseColor = Color.red;
        paintLevel = 0f;
        paintLevelThreshold = 15;
    }

    // Update is called once per frame
    void Update()
    {
    	// clamp paintLevel floor at 0
    	if (paintLevel < 0)
    	{
    		paintLevel = 0;
    	}

    	// if above magic threshold, paint tile
        if (paintLevel >= paintLevelThreshold)
        {
        	applyPaint(baseColor);
        }
        else
        {
        	applyPaint(Color.white);
        }
    }

    // check if tile is painted
    bool checkPaint(Color paintColor)
    {
    	Vector3 straightup = Vector3.up;

    	// check if tile is of color paintColor
    	if (rend.material.color == paintColor)
    	{
    		return true;
    	}
    	else
    	{
    		return false;
    	}
    }

    // apply paint to tile
    void applyPaint(Color paintColor)
    {
    	rend.material.color = paintColor;
    }
}
