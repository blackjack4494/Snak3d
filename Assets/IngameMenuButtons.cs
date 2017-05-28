using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void resumeGame()
    {
        Movement mv = GameObject.Find("Head").GetComponent<Movement>();
        mv.isPaused = false;
        mv.pauseState();
        SceneManager.UnloadSceneAsync("IngameMenu");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("snak3d");
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
