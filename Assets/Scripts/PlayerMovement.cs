using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;
    private bool m_FacingRight = true;

    private float absSpeed = 0;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Animator animator;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    private float horizontalMovement;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isJumping = true;
            animator.SetBool("Jump",true);
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
}