using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public Transform[] movementPoints; 
    public float speed = 5f;
    
    private int nextPoint = 1;      
    private bool movingAscending = true; 
    private const float MIN_DISTANCE = 0.01f; // Mínima distancia para cambiar de punto

    void Update()
    {
        // Detener el movimiento si el juego está en pausa
        if (Time.timeScale == 0f) return; 

        // Mover la plataforma
        transform.position = Vector2.MoveTowards(
            transform.position, 
            movementPoints[nextPoint].position, 
            speed * Time.deltaTime
        );
        
        // Chequeo de Llegada
        if (Vector2.Distance(transform.position, movementPoints[nextPoint].position) < MIN_DISTANCE)
        {
            if (movingAscending)
            {
                nextPoint++;
                
                
                if (nextPoint >= movementPoints.Length) 
                {
                    movingAscending = false;
                    nextPoint = movementPoints.Length - 1; 
                }
            }
            else 
            {
                nextPoint--;
                
               
                if (nextPoint < 0)
                {
                    movingAscending = true;
                    nextPoint = 0; 
                }
            }
        }
    }

    // Solución para que el jugador se mueva con la plataforma
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform); 
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}