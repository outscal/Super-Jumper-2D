using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{
    [SerializeField]
    GameObject LevelComplete;
    [SerializeField]
    AudioSource Door_open;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Door_open.Play();
            StartCoroutine("DisplayUI");
        }
    }

    IEnumerator DisplayUI()
    {
        LevelComplete.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        LevelManager.Instance.MarkLevelComplete();
    }
}
