using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class WeaponImage
{
    public string weaponName;
    public RawImage image;
}

public class HudManager : MonoBehaviour
{

    [Header("Info")]
    public Player player;
    private Health playerHealth;
    private Stamina playerStamina;

    [Header ("Visuals")]
    public WeaponImage[] weapons;
    public Text ammo;
    public Image crosshair;
    public Text enemiesCount;
    public Image healthBar;
    public Image staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<Health>();
        playerStamina = player.GetComponent<Stamina>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWeaponHud();
        UpdateCrosshair();
        UpdateEnemiesCount();
        UpdateHealthBar();
        UpdateStaminaBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = playerHealth.HealthPercentage;
    }

    private void UpdateStaminaBar()
    {
        staminaBar.fillAmount = playerStamina.StaminaPercentage;
    }

    private void UpdateEnemiesCount()
    {
        enemiesCount.text = EnemyManager.Instance.EnemyKills.ToString();
    }

    private void UpdateCrosshair()
    {
        crosshair.gameObject.SetActive(player.IsAiming);
    }

    private void UpdateWeaponHud()
    {
        var selectedWeapon = player.SelectedWeapon;

        foreach (var weapon in weapons){
            weapon.image.gameObject.SetActive(selectedWeapon.weaponName == weapon.weaponName);
        }

        ammo.text = selectedWeapon.CurrentAmmo + " / " + selectedWeapon.munition;
    }
}
