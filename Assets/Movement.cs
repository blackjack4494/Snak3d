using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Movement : MonoBehaviour {

    private Rigidbody rb;

    public List<Transform> tail = new List<Transform>();
    public static bool ate = false;
    public GameObject tailPrefab;
    private Vector3 prevPos;
    private Vector3 startPos;
    public int startSize;

    public int speedUpDuration;

    public bool isPaused = false;

    public float startSpeed;
    public float speed;
    public int surprise = 30;

    private Vector3 dir;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();

        FoodCollector.score = 0;

        dir = new Vector3(0, 0, 1);
        startPos = transform.position;
        startSpeed = speed;
        initSnakeSize(startSize);
        isPaused = false;
        pauseState();

        //InvokeRepeating("snakeLogic", 1f, 1f / speed);
        snakeLogic();
	}
	
	// Update is called once per frame
	void Update () {

        pauseState();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!SceneManager.GetSceneByName("GameOver").isLoaded)
            {
                if (!SceneManager.GetSceneByName("IngameMenu").isLoaded)
                {
                    isPaused = true;
                    pauseState();
                    SceneManager.LoadScene("IngameMenu", LoadSceneMode.Additive);
                    //SceneManager.UnloadSceneAsync("IngameMenu");
                }
                else
                {
                    SceneManager.UnloadSceneAsync("IngameMenu");
                    isPaused = false;
                    pauseState();
                }
            }
        }

        
        if(rb.transform.position.x > 15.1 || rb.transform.position.x < -15.1 || rb.transform.position.z < -8.6 || rb.transform.position.z > 8.6)
        {
            //Destroy(gameObject);
            gameOver();
        }
        

        //if (Input.GetKeyDown(KeyCode.P))
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
            Debug.Log("Pause button pressed");
        }

        if (Input.GetButtonDown("Up") && dir != Vector3.back)
        {
            //dir = new Vector3.forward;
            dir = new Vector3(0, 0, 1);
            Debug.Log("Up button pressed");
        }
        if (Input.GetButtonDown("Down") && dir != Vector3.forward)
        {
            //dir = new Vector3.back;
            dir = new Vector3(0, 0, -1);
            Debug.Log("Down button pressed");
        }
        if (Input.GetButtonDown("Left") && dir != Vector3.right)
        {
            //dir = new Vector3.left;
            dir = new Vector3(-1, 0, 0);
            Debug.Log("Left button pressed");
        }
        if (Input.GetButtonDown("Right") && dir != Vector3.left)
        {
            //dir = new Vector3.right;
            dir = new Vector3(1, 0, 0);
            Debug.Log("Right button pressed");
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("bodyPart"))
        {
            gameOver();
            Debug.Log("Dang! You ate yourself.");
        }
    }

    public void speedUp(float speedIncrease, int sDuration)
    {
        if(speed < 6 || speedUpDuration < 60)
        {
            speed += speedIncrease;
            speedUpDuration += sDuration;
            Debug.Log("SpeedUp for " + sDuration + " with " + speedIncrease + " increased speed");
        }
    }

    public void gameOver()
    {
        if (!SceneManager.GetSceneByName("GameOver").isLoaded)
        {
            isPaused = true;
            pauseState();
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }
    }

    public void surpriseAttack()
    {
        FoodGenerator fg = GameObject.Find("Feeder").GetComponent<FoodGenerator>();
        for (int i = 0; i < 10; i++)
        {
            fg.spawnSpecial();
        }
    }

    void snakeLogic ()
    {

        if (speedUpDuration != 0)
        {
            speedUpDuration--;
        }
        else if(speedUpDuration == 0)
        {
            speed = startSpeed;
        }

        if(surprise != 0)
        {
            surprise--;
        }
        else if(surprise == 0)
        {
            surpriseAttack();
            /*
            FoodGenerator fg = GameObject.Find("Feeder").GetComponent<FoodGenerator>();
            for (int i = 0; i < 10; i++) {
                fg.spawnSpecial();
            }
            */
            //surprise = 30;
        }

        prevPos = transform.position;
        //rb.MovePosition(transform.position + dir);
        rb.position = transform.position + dir;
        //transform.position = Vector3.MoveTowards(transform.position, pos, 1);

        if (ate)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, prevPos, Quaternion.identity);

            tail.Insert(0, g.transform);
            ate = false;
        }
        else
        {
            tail.Last().position = prevPos;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }

        Invoke("snakeLogic", 1f / speed);
    }

    public void initSnakeSize(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, (startPos-Vector3.forward), Quaternion.identity);
            tail.Add(g.transform);
            startPos -= Vector3.forward;
        }
    }

    public void Pause()
    {
        if(isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void pauseState()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
