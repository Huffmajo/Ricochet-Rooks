using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    //  
    public void level_3_1()
    {
    	SceneManager.LoadScene("3-1");
    }

    public void level_5_1()
    {
    	SceneManager.LoadScene("5-1");
    }

    public void level_9_1()
    {
    	SceneManager.LoadScene("9-1");
    }

    // change to settings menu
	public void gotoMainMenu()
    {
    	SceneManager.LoadScene("Title Scene");
    }

}
