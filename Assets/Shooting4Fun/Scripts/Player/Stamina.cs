using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float maxStamina;
    public float staminaDecreaseSpeed;
    public float staminaIncreaseSpeed;

    private float currentStamina;

    public float StaminaPercentage { get { return currentStamina / maxStamina; } }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
    }

    public bool HasStamina()
    {
        return currentStamina > 0;
    }

    public void IncreaseStamina()
    {
        currentStamina += staminaIncreaseSpeed * Time.deltaTime;
        if (currentStamina > maxStamina){
            currentStamina = maxStamina;
        }
    }

    public void DecreaseStamina()
    {
        currentStamina -= staminaDecreaseSpeed * Time.deltaTime;
        if (currentStamina < 0){
            currentStamina = 0;
        }
    }

    public bool IsFull()
    {
        return currentStamina >= maxStamina;
    }

    public void AddStamina(float stamina)
    {
        currentStamina += stamina;
        if (stamina >= maxStamina){
            stamina = maxStamina;
        }
    }
}
