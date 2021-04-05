using UnityEngine;
using UnityEngine.UI;

public class openchest : MonoBehaviour
{
    private bool isInRange;
    private bool soundAnimator = true;
    private bool chest = true;

    public int shieldValue = 10;
    public bool shield = true;

    public int healthValue = 10;
    public bool health = true;
    public Animator animator;
    public Text interactUI;

    public PlayerHealth playerHealth;

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
        getShield();
        interactUI.enabled = false;
        soundAnimator = false;
        chest = false;
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

    void getShield()
    {
        if (chest)
        {
            if (shield && !health)
            {
                playerHealth.TakeShield(shieldValue);
            }
            if(!shield && health)
            {
                playerHealth.TakeHealth(healthValue);
            }
            if (shield && health)
            {
                playerHealth.TakeShield(shieldValue);
                playerHealth.TakeHealth(healthValue);
            }

        }
    }
}
