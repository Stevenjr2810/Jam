using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con el componente Text de UI

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 180; // 3 minutos expresados en segundos
    public bool timerIsRunning = false;

    public Text timeText; // Referencia al componente de texto donde mostrarás el tiempo
    public GameObject loseTextGameObject; // Referencia al GameObject que contiene el texto de "Perdiste"

    private void Start()
    {
        // Inicia el temporizador al comenzar el juego
        timerIsRunning = true;
        loseTextGameObject.SetActive(false); // Asegúrate de que el texto de "Perdiste" está oculto al inicio
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                loseTextGameObject.SetActive(true); // Muestra el texto de "Perdiste"
                Time.timeScale = 0; // Detiene el juego
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}