using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0); // Velocidad de rotación en grados por segundo

    void Update()
    {
        // Rota el objeto cada frame según la velocidad de rotación establecida
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}