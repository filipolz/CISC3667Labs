using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Needed for the Slider component

public class MenuManager : MonoBehaviour
{
    // Public references to the panels, so we can connect them in the Inspector
    public GameObject settingsPanel;
    public GameObject instructionsPanel;
    public GameObject mainMenuPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        // Make sure the panels are hidden and the main menu is shown at the start
        settingsPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        
        // Set the volume slider to the current volume when the game starts
        // (You may want to set the initial Slider value here as well)
        // If you want volume control to persist, you should save the value 
        // using PlayerPrefs, but we will keep it simple for now.
    }

    // --- BUTTON FUNCTIONS ---

    // Function for the "Play Game" button
    public void StartGame()
    {
        // Loads the scene at index 1 (which should be your GameScene)
        SceneManager.LoadScene(1); 
    }

    // Function to open a panel and close the others
    public void OpenPanel(GameObject panelToOpen)
    {
        // Close all three panels
        settingsPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        
        // Open the requested panel
        panelToOpen.SetActive(true);
    }
    
    // Function to go back to the main menu
    public void GoToMainMenu()
    {
        OpenPanel(mainMenuPanel);
    }

    // --- SETTINGS FUNCTIONS ---

    // Function called by the UI Slider's 'On Value Changed' event
    public void SetVolume(float volume)
    {
        // The AudioListener controls the overall volume of all game sounds
        // The slider's value (0.0 to 1.0) is passed in as the 'volume' parameter
        AudioListener.volume = volume;
    }
}