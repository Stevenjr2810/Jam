using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Text coinText; // UI Text que muestra el conteo de monedas
    public GameObject winText; // Objeto de texto que dice "Ganaste"
    private int totalCoins = 0;
    private int collectedCoins = 0;
    private bool isGameActive = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        winText.SetActive(false); // Asegura que el texto de "Ganaste" está oculto al inicio
        UpdateCoinText();
    }

    public void SetTotalCoins(int amount)
    {
        totalCoins = amount;
        UpdateCoinText();
    }

    public void CollectCoin()
    {
        if (!isGameActive) return; // No hace nada si el juego ya no está activo

        collectedCoins++;
        UpdateCoinText();

        // Verifica si todas las monedas han sido recolectadas
        if (collectedCoins >= totalCoins)
        {
            WinGame();
        }
    }

    void UpdateCoinText()
    {
        coinText.text = "Koalas: " + collectedCoins + " / " + totalCoins;
    }

    void WinGame()
    {
        winText.SetActive(true); // Muestra el texto de "Ganaste"
        isGameActive = false; // Marca el juego como no activo
                              // Aquí puedes añadir más lógica para cuando el jugador gana, como detener el tiempo, etc.

        Time.timeScale = 0;

    }
}
