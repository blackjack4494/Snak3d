using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCollector : MonoBehaviour {

    public Text scoreText;
    public static int score = 0;

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
            Destroy(other.gameObject);
            Movement.ate = true;
            scoreText.text = "Score: " + score;
        }

        Debug.Log("Food collected");
    }
}
