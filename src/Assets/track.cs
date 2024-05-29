using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class track : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector2 number0fObstacles;
    public List<GameObject> newObstacles;

    // Start is called before the first frame update
    void Start()
    {
        int newnumber0fObstacles = (int)Random.Range(number0fObstacles.x,number0fObstacles.y);
        for (int i = 0; i < newnumber0fObstacles; i++)
        {
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);

        }

        PositionateObstacles();


    }

    void PositionateObstacles()
    {
        for (int i = 0;i < newObstacles.Count;i++)
        {
            float posZmin = (300f / newObstacles.Count) + (300f / newObstacles.Count) * i;
            float posZmax = (300f / newObstacles.Count) + (300f / newObstacles.Count) * i + 1;
            newObstacles[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZmin, posZmax));
            newObstacles[i]. SetActive(true);

        }


    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
