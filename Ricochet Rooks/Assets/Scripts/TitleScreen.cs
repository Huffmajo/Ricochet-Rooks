using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
	public string levelsSceneName;
	public string settingsSceneName;

    // 
    public void levels()
    {
    	SceneManager.LoadScene(levelsSceneName);
    }

    // change to settings menu
	public void settings()
    {
    	SceneManager.LoadScene(settingsSceneName);
    }

    // close out application
    public void quitGame()
    {
    	Application.Quit();
    }
}
