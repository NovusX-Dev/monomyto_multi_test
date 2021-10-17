using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _score;

    private void OnEnable()
    {
        BreakableBox.OnBoxDestroyed += AddScore;
        EnemyHealth.OnEmenyKilledPoints += AddScore;
    }

    private void OnDisable()
    {
        BreakableBox.OnBoxDestroyed -= AddScore;
        EnemyHealth.OnEmenyKilledPoints -= AddScore;
    }

    void Start()
    {
        _score = 0;
        UIManager.Instance.UpdateScoreUI(_score);
    }

    public void AddScore(int score)
    {
        _score += score;
        UIManager.Instance.UpdateScoreUI(_score);
    }
}
