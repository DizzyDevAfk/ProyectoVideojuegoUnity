using UnityEngine;
using TMPro; // NECESARIO para usar TextMeshPro
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    
    private int score;
    private int lives; 
    private bool isGameOver = false; 
    
   
    [Header("Configuración de Tiempo")]
    public float levelTime = 60f; // Tiempo inicial 
    private float currentTime;    // Contador 

    [Header("Elementos de UI")]
    public TextMeshProUGUI scoreText; 
   
    public TextMeshProUGUI timerText; 
    public GameObject[] lifeIcons; 
    public GameObject gameOverPanel; 
    
    [Header("Referencias de Juego")]
    public PlayerMovement player; 
    
    private Vector3 respawnPosition; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Time.timeScale = 1f; 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isGameOver = false;
        
        score = 0;
        lives = 3; 
        // Inicializar el contador
        currentTime = levelTime; 
        
        if (player != null)
        {
            respawnPosition = player.transform.position;
        }

        if (gameOverPanel != null) 
        {
            gameOverPanel.SetActive(false); 
        }

        UpdateScoreDisplay();
        UpdateLivesUI(); 
        UpdateTimerDisplay(); // Mostrar el tiempo inicial
    }
    
    void Update()
    {
       
        if (!isGameOver)
        {
            
            currentTime -= Time.deltaTime;
            
            // Asegurarse de que el tiempo no baje de cero
            if (currentTime <= 0)
            {
                currentTime = 0;
                GameOverByTimeOut(); // Función específica para la derrota por tiempo
            }
            
            UpdateTimerDisplay();
        }

        // Si el juego ha terminado y el jugador presiona ESPACIO
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

   
    
    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
           
            timerText.text = "Time: " + Mathf.FloorToInt(currentTime).ToString(); 
        }
    }
    
    void GameOverByTimeOut()
    {
       
        
        GameOver(); // Llama a la función de Game Over existente
    }

   
    
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString(); 
        }
    }
    
    public void TakeDamage()
    {
        if (isGameOver) return; 

        lives--;
        UpdateLivesUI();

        if (lives > 0)
        {
            RespawnPlayer();
        }
        else
        {
            GameOver();
        }
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].SetActive(i < lives);
        }
    }
    
    void RespawnPlayer()
    {
        if (player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                
                rb.linearVelocity = Vector2.zero; 
            }
            
            player.transform.position = respawnPosition; 
        }
    }
    
    void GameOver()
    {
        Debug.Log("¡JUEGO TERMINADO! Presiona Espacio para Reiniciar.");
        
        // Pausar el juego
        Time.timeScale = 0f;
        isGameOver = true;
        
        // Mostrar el panel de Game Over
        if (gameOverPanel != null) 
        {
            gameOverPanel.SetActive(true); 
        }
    }
    
    void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}