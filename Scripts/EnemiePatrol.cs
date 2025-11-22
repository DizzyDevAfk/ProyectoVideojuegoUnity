using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;         // Velocidad 
    
    [Header("Verificación de Giro")]
    public Transform detectionPoint; 
    public float checkDistance = 0.5f; 
    
    

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool movingRight = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        sr.flipX = !movingRight;
    }

    void Update()
    {
        // Movimiento del enemigo
        float direction = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        // Verificación de giro
        CheckForTurn(direction);
    }

    private void CheckForTurn(float direction)
    {
        Vector2 checkDirection = Vector2.right * direction;

        // --- Detección de Colisiones ---
        
        // Detección de Pared 
     
        RaycastHit2D wallHit = Physics2D.Raycast(detectionPoint.position, checkDirection, checkDistance);

        //  Detección de Borde 
        
        RaycastHit2D edgeHit = Physics2D.Raycast(detectionPoint.position, Vector2.down, 0.2f);
        
  
        
        bool turn = false;

        
        if (wallHit.collider != null && wallHit.collider.CompareTag("Ground"))
        {
            turn = true;
        }

      
        if (edgeHit.collider == null)
        {
            turn = true;
        }
        
   
        if (turn)
        {
            
            float pushBackDistance = 0.05f; 
            
            transform.position = new Vector3(
                transform.position.x + (-direction * pushBackDistance), 
                transform.position.y, 
                transform.position.z
            );

            // Actualizar el sprite
            movingRight = !movingRight;
            sr.flipX = movingRight;
        }
    }
    
 
    private void OnDrawGizmos()
    {
        if (Application.isPlaying && detectionPoint != null)
        {
            float direction = movingRight ? 1f : -1f;
            Vector2 checkDirection = Vector2.right * direction;

         
            Gizmos.color = Color.red;
            Gizmos.DrawRay(detectionPoint.position, checkDirection * checkDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(detectionPoint.position, Vector2.down * 0.2f);
        }
    }
}