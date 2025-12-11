using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int health = 100;

    [Header("UI References")]
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI healthText;      
    public TextMeshProUGUI finalScoreText;  
    public GameObject gameOverPanel;

    [Header("Background Images")]
    public GameObject grimReaperObject; 
    public GameObject thumbsUpObject;   

    [Header("Audio")]
    public AudioClip grimReaperSound;
    public AudioClip thumbsUpSound;
    public AudioClip gameOverSound;
    private AudioSource audioSource;
    
    private bool playedGrimSound = false;
    private bool playedThumbsUpSound = false;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        Time.timeScale = 1; 
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        if(gameOverPanel != null) gameOverPanel.SetActive(false);
        if(grimReaperObject != null) grimReaperObject.SetActive(false);
        if(thumbsUpObject != null) thumbsUpObject.SetActive(false);

        UpdateHealthUI();
    }

    public void AddPoint()
    {
        score++; 
        scoreText.text = "Score: " + score; 

        if (score >= 10)
        {
            if (thumbsUpObject != null) thumbsUpObject.SetActive(true);
            
            if (thumbsUpSound != null && !playedThumbsUpSound)
            {
                audioSource.PlayOneShot(thumbsUpSound);
                playedThumbsUpSound = true; 
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        UpdateHealthUI();

        if (health <= 50)
        {
            if (grimReaperObject != null) grimReaperObject.SetActive(true);

            if (grimReaperSound != null && !playedGrimSound)
            {
                audioSource.PlayOneShot(grimReaperSound);
                playedGrimSound = true;
            }
        }

        if (health <= 0)
        {
            EndGame();
        }
    }

    void UpdateHealthUI()
    {
        if(healthText != null)
            healthText.text = "Health: " + health;
    }

    void EndGame()
    {
        Time.timeScale = 0;
        
        if (gameOverSound != null) AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);

        CheckAndSaveHighScores(score);

        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            finalScoreText.text = "Final Score: " + score;
        }
    }

    void CheckAndSaveHighScores(int newScore)
    {
        List<int> highScores = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            highScores.Add(PlayerPrefs.GetInt("HighScore" + i, 0));
        }

        highScores.Add(newScore);
        highScores.Sort((a, b) => b.CompareTo(a));

        if (highScores.Count > 5) highScores.RemoveAt(5);

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }
        PlayerPrefs.Save();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0); 
    }
}