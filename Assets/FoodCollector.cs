using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCollector : MonoBehaviour {

    public Text scoreText;
    public static int score = 0;
    public int scriptScore = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Food"))
        {
            score++;
            scriptScore++;
            Destroy(other.gameObject);
            Movement.ate = true;
            scoreText.text = "Score: " + score;
        }
        Debug.Log("Food collected");
        if (other.CompareTag("specialFood"))
        {
            score += 10;
            scriptScore += 10;
            Destroy(other.gameObject);
            scoreText.text = "Score: " + score;
            Movement mv = GetComponent<Movement>();
            mv.speedUp(0.6f, 10);
        }
    }
}
