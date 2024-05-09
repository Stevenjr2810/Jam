using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Añade esto


public class Flash : MonoBehaviour
{
    public Light flashlight;
    public float batteryLife = 100f;
    public float consumptionRate = 5f;
    public float cooldownTime = 5f;

    public Text cooldownText; // Referencia al componente Text para mostrar el cooldown

    private bool isCoolingDown = false;

    void Update()
    {
        if (flashlight.enabled && !isCoolingDown)
        {
            batteryLife -= consumptionRate * Time.deltaTime;
            cooldownText.text = $"Battery: {batteryLife.ToString("F0")}%"; // Actualiza el texto
            if (batteryLife <= 0f)
            {
                StartCoroutine(Cooldown());
            }
        }
    }

    IEnumerator Cooldown()
    {
        isCoolingDown = true;
        flashlight.enabled = false;
        batteryLife = 0f;
        cooldownText.text = "Recharging..."; // Muestra mensaje de recarga

        yield return new WaitForSeconds(cooldownTime);

        batteryLife = 100f;
        flashlight.enabled = true;
        isCoolingDown = false;
        cooldownText.text = "Battery Full"; // Opcional, o puedes quitar el texto
    }
}
