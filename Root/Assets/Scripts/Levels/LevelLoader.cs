using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    public string LevelName;
    Button LevelLoad_btn;
    [SerializeField]
    AudioSource Start_Game;

    void Awake()
    {
        LevelLoad_btn = GetComponent<Button>();
        LevelLoad_btn.onClick.AddListener(LoadLevel);
    }
    public void LoadLevel()
    {
        Start_Game.Play();
        LevelStatus levelstatus = LevelManager.Instance.GetLevelStatus(LevelName);
        switch(levelstatus)
        {
            case LevelStatus.locked:
                Debug.Log("Can't play level it's locked...");
                break;

            case LevelStatus.unlocked:
                SceneManager.LoadScene(LevelName);
                break;

            case LevelStatus.completed:
                SceneManager.LoadScene(LevelName);
                break;
        }
    }
}
