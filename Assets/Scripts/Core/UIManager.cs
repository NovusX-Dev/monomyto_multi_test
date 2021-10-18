using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _healthText, _gunAmmoText;

    [Header("Game Over Panel")]
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] TextMeshProUGUI _GOScoreText;


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

    public void GameOverPanel()
    {
        _gameOverPanel.SetActive(true);
        _GOScoreText.text = $"Final Score: {ScoreManager.Instance.Score}";
    }

    public void RestartButton()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void QuitButton()
    {
        LevelManager.Instance.QuitGame();
    }
}
