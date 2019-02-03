using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text resultsText;

    private void OnEnable()
    {
        resultsText.text = "You killed " + EnemyManager.Instance.EnemyKills + " enemies.";
    }
}
