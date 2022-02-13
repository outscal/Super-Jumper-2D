using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject LevelSelection_Box;
    [SerializeField]
    AudioSource Press_Cross;
    [SerializeField]
    AudioSource Press_Start;

    public void StartGame()
    {
        //SceneManager.LoadScene("Level_1");
        Press_Start.Play();
        LevelSelection_Box.SetActive(true);
    }

    public void ExitLevelSelection()
    {
        Press_Cross.Play();
        LevelSelection_Box.SetActive(false);
    }

    public void QuitGame()
    {
        Press_Start.Play();
        Application.Quit();
    }
}
