using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    [SerializeField] private ParticleSystem particulas;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private bool isGrounded = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
        
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

       
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y); 

        // Saltar si está en el suelo
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            particulas.Play();
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        
        // LÓGICA DE ANIMACIÓN
        anim.SetFloat("Speed", Mathf.Abs(move)); 
        
        // LÓGICA DE ANIMACIÓN
        anim.SetBool("IsJumping", !isGrounded);


        // Voltear el sprite
        if (move < 0) sr.flipX = false;
        else if (move > 0) sr.flipX = true;
    }
    
   
    public void Die()
    {
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TakeDamage();
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Chequeo de Suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
      
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Hazard"))
        {
           
            Die();
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}