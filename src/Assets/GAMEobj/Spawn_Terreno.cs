using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Terreno : MonoBehaviour
{
    
    public List<GameObject> plataforms = new List<GameObject>();
    public List<Transform> currentPlatforms = new List<Transform>();
    
    public int offset;

    private Transform player;
    private Transform currentPlatformPoint;
    private int platformIndex;


    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
        
        for (int i = 0; i < plataforms.Count; i++)
        {
            Transform p = Instantiate(plataforms[i], new Vector3(-20,0,i * 60), transform.rotation).transform;
            currentPlatforms.Add(p);
            offset += 60;
        }

        currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<plataforma>().point;
    }
  
    // Update is called once per frame
    void Update()
    {
      
        float distance = player.position.z - currentPlatformPoint.position.z;

        if(distance >= 60)
        {
            Recycle(currentPlatforms[platformIndex].gameObject);
            platformIndex++;

            if(platformIndex > currentPlatforms.Count - 1)
            {
                platformIndex = 0;
            }

            currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<plataforma>().point;
        }



    }

    public void Recycle(GameObject plataform)
    {
        plataform.transform.position = new Vector3 (-20,0,offset);
        offset += 60;
    }
}
