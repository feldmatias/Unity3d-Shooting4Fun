using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Health target;
    public Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponentInParent<Health>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var health = target.HealthPercentage;
        healthbar.fillAmount = health;

        transform.LookAt(Camera.main.transform.position);
    }
}
