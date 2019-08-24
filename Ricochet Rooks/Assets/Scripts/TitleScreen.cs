using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
	public string levelsSceneName;
	public string settingsSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 
    public void levels()
    {
    	SceneManager.LoadScene(levelsSceneName);
    }

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
