using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public RawImage[] life;

    public void UpdateLives(int lives)
    {
        for (int i = 0; i < life.Length; i++)
        {
            if(lives > i)
            {
                life[i].color = Color.white;
            }
            else
            {
                life[i].color = Color.black;
            }
        }
    }
}
