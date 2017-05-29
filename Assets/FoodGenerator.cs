using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour {

    public int width;
    public int height;

    public GameObject foodPrefab;

    public Vector3 curPos;

    public GameObject curFood;

    public GameObject specialFoodPrefab;
    public GameObject curSpecialFood;

	// Use this for initialization
	void Start () {
        width = 15;
        height = 8;

        spawnFood();
        InvokeRepeating("spawnSpecial",3f,5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (!curFood)
        {
            spawnFood();
        }
	}

    public void spawnSpecial()
    {
        //randomPosition();
        curSpecialFood = GameObject.Instantiate(specialFoodPrefab, randomPosition(), Quaternion.identity) as GameObject;
        Debug.Log("SpecialFood spawned");
    }

    public void spawnFood()
    {
        //randomPosition();
        curFood = GameObject.Instantiate(foodPrefab, randomPosition(), Quaternion.identity) as GameObject;
        Debug.Log("Food spawned");
    }

    public Vector3 randomPosition()
    {
        return new Vector3(Random.Range(-width, width), 0.5f, Random.Range(-height, height));
    }
}
