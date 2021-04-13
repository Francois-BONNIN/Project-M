using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
            int health = PlayerPrefs.GetInt("Health");
            int shield = PlayerPrefs.GetInt("Shield");

            PlayerPrefs.SetInt("saveHealth",health);
            PlayerPrefs.SetInt("saveShield",shield);

        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel("Egout01"));
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }
}
