using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 1.0f;
    public float chaseRange = 30.0f;
    public float collisionAvoidanceRange = 5.0f;
    public Transform lookTarget;

    private void Update()
    {
        if (player == null || lookTarget == null) return;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer < chaseRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Verificar si hay obstáculos entre el enemigo y el jugador
            if (!IsPathBlocked(directionToPlayer))
            {
                transform.position += directionToPlayer * speed * Time.deltaTime;

                // Hacer que el objeto de orientación mire al jugador
                lookTarget.LookAt(player);
                // Ajustar la rotación del enemigo para que coincida con la del objeto de orientación
                transform.rotation = Quaternion.LookRotation(lookTarget.forward);
            }
        }
    }

    bool IsPathBlocked(Vector3 directionToPlayer)
    {
        RaycastHit hit;
        // Lanzar un rayo desde la posición del enemigo hacia el jugador
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, collisionAvoidanceRange))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return true;  // Camino bloqueado
            }
        }
        return false;  // Camino despejado
    }

    void Start () 
    {   //Para que siga a lo que tiene el tag de player
        if (player == null) {
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
            if (playerGameObject != null) {
                player = playerGameObject.transform;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RestartGame();
        }
    }
        
    void RestartGame()
    {
        // Usando SceneManager para cargar la escena actualmente activa nuevamente, lo que reinicia el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
