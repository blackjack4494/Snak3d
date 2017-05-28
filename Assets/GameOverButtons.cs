using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButtons : MonoBehaviour {

    public Text scoreText;

	// Use this for initialization
	void Start () {
        displayScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void displayScore()
    {
        scoreText.text = "Your Score: " + FoodCollector.score;
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
