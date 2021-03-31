using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntrerEgout : MonoBehaviour
{
    private bool isInRange;
    public Text interactUI;
    public Animator transition;
    public float transitionTime = 1f;
    
    void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            ChangeScene();
            isInRange = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    void ChangeScene()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        interactUI.enabled = false;
        StartCoroutine(LoadLevel("Egout01"));
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
        interactUI.enabled = false;
    }
    
    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }
}



