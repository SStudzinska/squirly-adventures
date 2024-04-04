using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnWin;
    public float health, maxHealth;
    public Image winning;
    private float timeElapsed;

    private void Start()
    {
        health = maxHealth;
        Time.timeScale = 1;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        OnPlayerDamaged?.Invoke();

        if(health <= 0)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WIN"))
        {

            PlayerBar playerbar = PlayerBar.instance;
            int nutsLeft = GameObject.FindGameObjectsWithTag("Nut").Length;
            string nutCount = playerbar.currentNuts + "/" + (nutsLeft + playerbar.currentNuts);

            int minutes = Mathf.FloorToInt(timeElapsed)/60;
            int seconds = Mathf.FloorToInt(timeElapsed)%60;
            string timeText;
            if (minutes < 10 && seconds < 10)
            {
                timeText = $"0{minutes}:0{seconds}";
            }
            else if (minutes > 10 && seconds < 10)
            {
                timeText = $"{minutes}:0{seconds}";
            }
            else if (minutes < 10 && seconds > 10)
            {
                timeText = $"0{minutes}:{seconds}";
            }
            else
            {
                timeText = $"{minutes}:{seconds}";
            }
            
            int score = playerbar.currentNuts * 500 + Mathf.FloorToInt(health) * 200 +  Mathf.FloorToInt(1f/Mathf.FloorToInt(timeElapsed)*10000f);
           
            string currentScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("timeCount", timeText);
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetString("Nuts", nutCount);
            PlayerPrefs.SetString("Level", currentScene);
            if (currentScene == "LevelOne")
            {
                OnWin?.Invoke();
                winning.gameObject.SetActive(true);
                
                Time.timeScale = 0;
            }
            else
            {
                SceneManager.LoadScene("Win");
            }
            
        }
    }
       

    private void GameOver()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("Level", currentScene);
        SceneManager.LoadScene("GameOver");
    }
}
