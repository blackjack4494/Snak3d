using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour {

    public int width;
    public int height;

    public GameObject foodPrefab;

    public Vector3 curPos;

    public GameObject curFood;

	// Use this for initialization
	void Start () {
        width = 15;
        height = 8;

        spawnFood();
	}
	
	// Update is called once per frame
	void Update () {
        if (!curFood)
        {
            spawnFood();
        }
	}

    public void spawnFood()
    {
        randomPosition();
        curFood = GameObject.Instantiate(foodPrefab, curPos, Quaternion.identity) as GameObject;
        Debug.Log("Food spawned");
    }

    public void randomPosition()
    {
        curPos = new Vector3(Random.Range(-width, width), 0.5f, Random.Range(-height, height));
    }
}
