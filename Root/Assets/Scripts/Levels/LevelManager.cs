using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance {  get { return instance; } }
    public string[] levels;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(GetLevelStatus(levels[0]) == LevelStatus.locked)
        {
            SetLevelStatus(levels[0], LevelStatus.unlocked);
        }
    }

    public LevelStatus GetLevelStatus(string level)
    {
        LevelStatus levelstatus = (LevelStatus)PlayerPrefs.GetInt(level, 0);
        return levelstatus;
    }

    public void SetLevelStatus(string level, LevelStatus levelstatus)
    {
        PlayerPrefs.SetInt(level, (int)levelstatus);
        Debug.Log("Setting level " + level + " status as " + levelstatus + "!!!!");
    }

    public void MarkLevelComplete()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SetLevelStatus(currentScene.name, LevelStatus.completed);

        int currentSceneIndex = Array.FindIndex(levels, level => level == currentScene.name);
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex < levels.Length)
        {
            SetLevelStatus(levels[nextSceneIndex], LevelStatus.unlocked);
            
            SceneManager.LoadScene(levels[nextSceneIndex]);  
        }
    }
}
