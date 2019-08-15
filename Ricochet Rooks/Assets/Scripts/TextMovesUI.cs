using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMovesUI : MonoBehaviour
{
	Text movesText;
	public int numMoves;
	public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
    	// get text object
        movesText = gameObject.GetComponent<Text>();

        // get number of moves from player script
        numMoves = player.GetComponent<RookScript>().numMoves;

        // show initial number of moves (0)
        movesText.text = "Moves: " + numMoves;
    }

    // Update is called once per frame
    void Update()
    {
		// get number of moves from player script
        numMoves = player.GetComponent<RookScript>().numMoves;

    	// update the number of moves as it changes
        movesText.text = "Moves: " + numMoves;
    }
}
