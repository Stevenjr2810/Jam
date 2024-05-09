using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    private Camera myCam;
    private Vector3 screenPoint;
    private float angleOffset;
    private Collider2D col;
    private float previousZRotation; // Almacena la rotaci�n en Z del frame anterior
    private float totalRotation = 0f; // Acumulador de la rotaci�n total
    private bool isDragging = false; // A�adido: flag para controlar si se est� arrastrando el objeto

    private void Start()
    {
        myCam = Camera.main;
        col = GetComponent<Collider2D>();
        previousZRotation = transform.eulerAngles.z; // Inicializa con la rotaci�n actual en Z
    }

    private void Update()
    {
        Vector3 objectPos = Input.mousePosition;
        objectPos.z = myCam.WorldToScreenPoint(transform.position).z;
        Vector3 mousePos = myCam.ScreenToWorldPoint(objectPos);

        if (Input.GetMouseButtonDown(0))
        {
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                isDragging = true; // Se comienza a arrastrar el objeto
                screenPoint = myCam.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Input.mousePosition - screenPoint;
                angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
                previousZRotation = transform.eulerAngles.z;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false; // Se deja de arrastrar el objeto
        }

        if (isDragging)
        {
            // Esta l�gica ahora solo se ejecuta si isDragging es true
            Vector3 vec3 = Input.mousePosition - screenPoint;
            float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle + angleOffset);

            // Calcula la diferencia de rotaci�n respecto al frame anterior
            float currentZRotation = transform.eulerAngles.z;
            float rotationDifference = Mathf.Abs(Mathf.DeltaAngle(currentZRotation, previousZRotation));

            // Acumula la rotaci�n total
            totalRotation += rotationDifference;

            Debug.Log($"Total Rotation: {totalRotation}");
            // Actualiza previousZRotation para el pr�ximo frame
            previousZRotation = currentZRotation;
        }
    }
}
