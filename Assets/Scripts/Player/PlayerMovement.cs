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
    public CapsuleCollider2D capsuleCollider2D;

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
            capsuleCollider2D.size = new Vector2(1.01f, 0.0001f);
            capsuleCollider2D.offset = new Vector2(0, -0.56f);
            Debug.Log("Avant roulade size :" + capsuleCollider2D.size);
            Debug.Log("Avant roulade offset :" + capsuleCollider2D.offset);

            animator.SetTrigger("Roll");
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

    private void ResetCollider()
    {
        capsuleCollider2D.size = new Vector2(0.7903081f, 1.970276f);
        capsuleCollider2D.offset = new Vector2(-0.005380362f, -0.008248806f);
        Debug.Log("Après roulade size:" + capsuleCollider2D.size);
        Debug.Log("Après roulade offset:" + capsuleCollider2D.offset);
    }
}