using UnityEngine;
using UnityEngine.UI;

public class openchest : MonoBehaviour
{
    private bool isInRange;
    private bool soundAnimator = true;
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
        playAudio();
        animator.SetTrigger("OpenChest");
        GetComponent<BoxCollider2D>().enabled = false;
        interactUI.enabled = false;
        soundAnimator = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
        interactUI.enabled = false;
    }

    void playAudio()
    {
        if (soundAnimator)
        {
            audioSource.PlayOneShot(sound);
        }
        
    }
}
