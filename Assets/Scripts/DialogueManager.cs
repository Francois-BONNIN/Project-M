using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public static DialogueManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DialogueManager dans la scène. Sale beau gosse.");
            return;
        }

        instance = this;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
    }
}
