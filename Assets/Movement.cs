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

    public bool isPaused = false;

    public float speed;

    //private Vector3 pos;
    private Vector3 dir;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();

        //pos = transform.position;

        dir = new Vector3(0, 0, 1);

        isPaused = false;
        pauseState();

        InvokeRepeating("placeHolder", 1f, 1f / speed);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isPaused = true;
            pauseState();
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }

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

    public void gameOver()
    {
        isPaused = true;
        pauseState();
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    void placeHolder ()
    {

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
        else if (tail.Count > 0)
        {
            tail.Last().position = prevPos;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
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
