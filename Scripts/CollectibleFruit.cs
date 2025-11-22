using UnityEngine;

public class CollectibleFruit : MonoBehaviour
{
    public int scoreValue = 1; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que tocó la fruta es el Jugador
        if (other.CompareTag("Player"))
        {
           
            Collect();
        }
    }

    void Collect()
    {
        // Llamar al GameManager 
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }
        
      
        Destroy(gameObject); 
    }
}