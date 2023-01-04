using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Hud : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Image bossHealthBar;
    [SerializeField]
    TextMeshProUGUI enemyCounter;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    GameObject restartButton;
    [SerializeField]
    TextMeshProUGUI finishedText;
    const string ScorePrefix = "Score: ";
    const string HealthPrefix = "Health: ";
    const string EnemyPrefix = "Enemy Ships: ";
    int enemy;
    int score=0;
    int healthPoint = 100;
    void Start()
    {
        /// initialize game texts 
        scoreText.text = ScorePrefix + score.ToString();
        healthText.text = HealthPrefix + healthPoint.ToString();
        enemyCounter.text = EnemyPrefix + enemy.ToString();
        
    }

    /// <summary>
    /// for adding points when kill enemy ships
    /// </summary>
    /// <param name="points"></param>
    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = ScorePrefix + score.ToString();
    }
    /// <summary>
    /// for decrease health when our ship hit
    /// </summary>
    /// <param name="health"></param>
    public void RemoveHealth(int health)
    {
        if(healthPoint-health<0)
        {
            healthPoint = 0;
        }
        else
        {
            healthPoint -= health;
        }
        healthText.text = HealthPrefix + healthPoint.ToString();    
    }
    public void EnemyCounter(int enemy)
    {
        this.enemy = enemy;
        enemyCounter.text = EnemyPrefix + enemy.ToString();
    }
    public void ActivateRestartButton(bool isWin)
    {
        restartButton.SetActive(true);
        Time.timeScale = 0;
        if (isWin)
        {
            finishedText.text = "you win";
        }
        else
            finishedText.text = "you lose";

            
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void UpdateHealthBar(float health,float maxHealth)
    {
        bossHealthBar.fillAmount = Mathf.Clamp(health/maxHealth,0,1f);
    }
    public void ActivateHealthBar()
    {
        bossHealthBar.gameObject.SetActive(true);    
    }

}
