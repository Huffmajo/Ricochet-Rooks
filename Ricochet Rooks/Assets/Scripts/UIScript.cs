using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
	public Text movesText;
	public Text tilesLeft;
    public Text resultsText;
	public GameObject player;
	public GameObject[] tiles;
    public int roundedPercentage;
    public int goldPar = 29;
    public int silverPar = 35;
    public int bronzePar = 40;

	private int numMoves;
	private int totalTiles;
	private int paintedTiles;
	private float percentage;
	private int bestMoves;
    private int currentMoves;
    private string medal;

    // Start is called before the first frame update
    void Start()
    {
    	// get all tiles into array
    	tiles = GameObject.FindGameObjectsWithTag("FloorTile");

    	// calculate total
    	totalTiles = tiles.Length;

    	// print UI elements
    	showNumMoves();
    	showNumTilesLeft();
    }

    // Update is called once per frame
    void Update()
    {
    	// print UI elements
    	showNumMoves();
		showNumTilesLeft();

        if (player.GetComponent<RookScript>().completeLevelUI.activeSelf)
        {
            // print results
            updateResults();
        }
    }

    void showNumMoves()
    {
    	// get number of moves from player object
    	numMoves = player.GetComponent<RookScript>().numMoves;

    	// print results to UI
    	movesText.text = "Moves: " + numMoves;
    }

    void showNumTilesLeft()
    {
    	// initialize variables
    	paintedTiles = 0;

    	// check all tiles to see if they are painted
    	foreach (GameObject tile in tiles)
    	{
    		if (tile.GetComponent<Renderer>().material.color != Color.white)
    		{
    			paintedTiles += 1;
    		}
    	}

    	percentage = ((paintedTiles * 1.0f) / (totalTiles * 1.0f)) * 100f;

        roundedPercentage = Mathf.RoundToInt(percentage);

    	// print results to UI
    	tilesLeft.text = "Tiles Left: " + paintedTiles + "/" + totalTiles + "\n" + roundedPercentage + "%";
    }

    string getMedal()
    {
        return player.GetComponent<RookScript>().medalReceived;
    }

    int getBestMoves()
    {
        return player.GetComponent<RookScript>().numMovesBest;
    }

    int getCurrentMoves()
    {
        return player.GetComponent<RookScript>().numMovesFinal;
    }

    // reload current level
    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // go to level select screen
    public void gotoLevels()
    {
        SceneManager.LoadScene("Level Select");
    }

    // get data for and update results upon level completion
    void updateResults()
    {
        bestMoves = getBestMoves();
        currentMoves = getCurrentMoves();
        medal = getMedal();

        resultsText.text = "LEVEL COMPLETED\n\n" + "Best score: " + bestMoves + "\nCurrent score: " + currentMoves + "\nYou received " + medal;
    }
}
