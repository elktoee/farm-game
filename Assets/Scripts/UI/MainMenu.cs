using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private int index;

    [SerializeField]
    private GameObject menu;

    public void PlayGame(){
        SceneManager.LoadScene(index);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void ShowMenu(){
        menu.SetActive(true);
    }

    public void HideMenu(){
        menu.SetActive(false);
    }

}
