using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDaCenaInicial;
    public void Jogar()
    {
        SceneManager.LoadScene(nomeDaCenaInicial);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
