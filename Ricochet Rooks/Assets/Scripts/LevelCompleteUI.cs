using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCompleteUI : MonoBehaviour
{
	public GameObject player;

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


}
