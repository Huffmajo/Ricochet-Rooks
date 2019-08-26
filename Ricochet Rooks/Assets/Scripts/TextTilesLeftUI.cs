using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTilesLeftUI : MonoBehaviour
{
	public int totalTiles;
	public int paintedTiles;
	public GameObject[] tiles;
	Text tilesLeft;

    // Start is called before the first frame update
    void Start()
    {
    	tilesLeft = gameObject.GetComponent<Text>();

    	tiles = GameObject.FindGameObjectsWithTag("FloorTile");
    	totalTiles = tiles.Length;
    	paintedTiles = 0;
    	foreach (GameObject tile in tiles)
    	{
    		if (tile.GetComponent<Renderer>().material.color != Color.white)
    		{
    			paintedTiles += 1;
    		}
    	}

    	tilesLeft.text = "Tiles Left: " + paintedTiles + "/" + totalTiles;
    }

    // Update is called once per frame
    void Update()
    {
    	paintedTiles = 0;
        foreach (GameObject tile in tiles)
    	{
    		if (tile.GetComponent<Renderer>().material.color != Color.white)
    		{
    			paintedTiles += 1;
    		}
    	}

    	tilesLeft.text = "Tiles Left: " + paintedTiles + "/" + totalTiles;
    }
}
