using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0); // Velocidad de rotaci�n en grados por segundo

    void Update()
    {
        // Rota el objeto cada frame seg�n la velocidad de rotaci�n establecida
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}