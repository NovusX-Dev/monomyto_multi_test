using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _healthText, _gunAmmoText;


    public void UpdateScoreUI(int score)
    {
        _scoreText.text = $"Score: { score}";
    }

    public void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        _healthText.text = $"Health: {currentHealth} / {maxHealth}";
    }

    public void UpdateGunAmmoUI(Weapon equipedWeapon, int ammo)
    {
        _gunAmmoText.text = $"{equipedWeapon.name} \n {ammo}";
    }
}
