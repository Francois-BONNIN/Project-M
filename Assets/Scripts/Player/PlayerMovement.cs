using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;
    private bool m_FacingRight = true;
    private bool runningSound = true;
    
    public float transitionTime = 10f;

    private float absSpeed = 0;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Animator animator;
    public BoxCollider2D boxCollider2D;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public AudioSource audioSource;
    public AudioClip sound;

    private Vector3 velocity = Vector3.zero;

    private float horizontalMovement;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isJumping = true;
            animator.SetBool("Jump",true);
        }

        if (Input.GetButtonDown("Roll") && isGrounded == true)
        {
            boxCollider2D.size = new Vector2(1.10f, 1.23f);
            boxCollider2D.offset = new Vector2(-0.01747513f, -0.3818567f);
            Debug.Log("Avant roulade size :" + boxCollider2D.size);
            Debug.Log("Avant roulade offset :" + boxCollider2D.offset);

            animator.SetTrigger("Roll");
            
            StartCoroutine(ResetCollider(0.7f));
        }
        
        if (rb.velocity.x > 0.1f && !m_FacingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < -0.1f && m_FacingRight)
        {
            Flip();
        }
        animator.SetFloat("yVelocity",rb.velocity.y);
    }

    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        absSpeed = Mathf.Abs(horizontalMovement);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        animator.SetBool("Ground",isGrounded);

        MovePlayer(horizontalMovement);

        //Course du personnage
        animator.SetFloat("Speed", absSpeed);

      /*  if (absSpeed > 0.1 && isGrounded == true)
        {
            Debug.Log("Run One Time");
            if (runningSound)
            {
                audioSource.PlayOneShot(sound);
            }
            runningSound = false;
        }
        else
        {
            runningSound = true;
        }*/
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
            animator.SetBool("Jump",false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    void Flip()
    {
    	m_FacingRight = !m_FacingRight;

        transform.Rotate(0f,180f,0f);
    }
    IEnumerator ResetCollider(float temps)
    {
        yield return new WaitForSeconds(temps);
        
        boxCollider2D.size = new Vector2(0.664856f, 1.943346f);
        boxCollider2D.offset = new Vector2(0.03073883f, -0.02832699f);
        Debug.Log("Après roulade size:" + boxCollider2D.size);
        Debug.Log("Après roulade offset:" + boxCollider2D.offset);
    }
}