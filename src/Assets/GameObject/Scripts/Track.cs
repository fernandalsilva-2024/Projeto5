using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public GameObject[] obstaculos;
    public Vector2 numeroDeObstaculos;
    public GameObject fire;
    public Vector2 numeroDeFires;

    public List<GameObject> novosObstaculos;
    public List<GameObject> novosFires;

    void Start()
    {
        int novoNumeroDeObstaculos = (int)Random.Range(numeroDeObstaculos.x, numeroDeObstaculos.y);
        int novoNumeroDeFires = (int)Random.Range(numeroDeFires.x, numeroDeFires.y);

        for (int i = 0; i < novoNumeroDeObstaculos; i++)
        {
            novosObstaculos.Add(Instantiate(obstaculos[Random.Range(0, obstaculos.Length)], transform));
            novosObstaculos[i].SetActive(false);
        }

        for (int i = 0; i < novoNumeroDeObstaculos; i++)
        {
            novosFires.Add(Instantiate(fire, transform));
            novosFires[i].SetActive(false);
        }
        PosicionandoObstaculos();
        PosicionandoFires();
    }

    void PosicionandoObstaculos()
    {
        for (int i = 0; i < novosObstaculos.Count; i++)
        {
            float randomNumber = Random.Range(0f, 1f);
            float randomXvalue;
            if (randomNumber <= 0.5f)
            {
                randomXvalue = -6f;
            }
            else if (randomNumber <= 0.75f)
            {
                randomXvalue = 0f;
            }
            else
            {
                randomXvalue = 6f;
            }
            float posZmin = (300f / novosObstaculos.Count) + (300f / novosObstaculos.Count) * i;
            float posZmax = (300f / novosObstaculos.Count) + (300f / novosObstaculos.Count) * i + 1;
            novosObstaculos[i].transform.localPosition = new Vector3(randomXvalue, 0, Random.Range(posZmin, posZmax));
            novosObstaculos[i].transform.localPosition = new Vector3(randomXvalue, 0, Random.Range(posZmin, posZmax));
            novosObstaculos[i].SetActive(true);
        }
    }

    void PosicionandoFires()
    {
        float posZmin = 10f;
        for (int i = 0; i < novosFires.Count; i++)
        {
            float randomXPos = Random.Range(-6f, 7f);
            float posZmax = (300f / novosFires.Count) + (300f / novosFires.Count) * i + 1;
            float randomZPos = Random.Range(posZmin, posZmax);
            novosFires[i].transform.localPosition = new Vector3(randomXPos, transform.position.y, randomZPos);
            novosFires[i].SetActive(true);
            posZmin = randomZPos + 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().IncreaseSpeed();
            transform.position = new Vector3(20, 0, transform.position.z + 120f);
            PosicionandoObstaculos();
            PosicionandoFires();
        }
    }
}
