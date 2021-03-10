using UnityEngine;
using UnityEngine.UI;

public class openchest : MonoBehaviour
{
    private bool isInRange;
    public Animator animator;
    public Text interactUI;

    public AudioSource audioSource;
    public AudioClip sound;

    void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            OpenChest();
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

    void OpenChest()
    {
        audioSource.PlayOneShot(sound);
        animator.SetTrigger("OpenChest");
        GetComponent<BoxCollider2D>().enabled = false;
        interactUI.enabled = false;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
        interactUI.enabled = false;
    }
}
