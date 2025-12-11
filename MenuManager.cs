using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro; 

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject instructionsPanel;
    public GameObject mainMenuPanel;
    
    public GameObject highScoresPanel;
    public TextMeshProUGUI[] highScoreTextSlots;

    void Start()
    {
        settingsPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        highScoresPanel.SetActive(false); 
        mainMenuPanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1); 
    }

    public void OpenPanel(GameObject panelToOpen)
    {
        settingsPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        highScoresPanel.SetActive(false); 
        
        panelToOpen.SetActive(true);
    }
    
    public void GoToMainMenu()
    {
        OpenPanel(mainMenuPanel);
    }

    public void OpenHighScores()
    {
        OpenPanel(highScoresPanel);

        for (int i = 0; i < 5; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);
            
            if (i < highScoreTextSlots.Length)
            {
                highScoreTextSlots[i].text = (i + 1) + ". " + score;
            }
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}