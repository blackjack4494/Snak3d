using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    public List<Transform> bodyParts = new List<Transform>();

    public float minDist = 0.25f;

    public int startSize;

    public float speed = 1;
    public float rotationspeed = 50;

    public GameObject bodyprefab;

    private float dis;
    private Transform curBodyPart;
    private Transform prevBodyPart;

	// Use this for initialization
	void Start () {
		
        for(int i = 0; i < startSize - 1; i++)
        {
            addBodyPart();
        }

	}
	
	// Update is called once per frame
	void Update () {
        Move();

        if (Input.GetKey(KeyCode.Q))
            addBodyPart();


	}

    public void Move()
    {
        float curspeed = speed;

        if (Input.GetKey(KeyCode.W))
            curspeed *= 2;

        bodyParts[0].Translate(bodyParts[0].forward * curspeed * Time.smoothDeltaTime, Space.World);

        if (Input.GetAxis("Horizontal") != 0)
            bodyParts[0].Rotate(Vector3.up * rotationspeed * Time.deltaTime * Input.GetAxis("Horizontal"));

        for (int i = 1; i < bodyParts.Count; i++)
        {
            curBodyPart = bodyParts[i];
            prevBodyPart = bodyParts[i - 1];

            dis = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newPos = prevBodyPart.position;

            newPos.y = bodyParts[0].position.y;

            float T = Time.deltaTime * dis / minDist * curspeed;

            if (T > 0.5f)
                T = 0.5f;

            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, 1);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, 1);
        }

    }

    public void addBodyPart()
    {
        Transform newPart = (Instantiate(bodyprefab, bodyParts[bodyParts.Count-1].position, bodyParts[bodyParts.Count - 1].rotation) as GameObject).transform;

        newPart.SetParent(transform);

        bodyParts.Add(newPart);
    }

}
