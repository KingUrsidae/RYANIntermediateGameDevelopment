using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    [Header("Texts")]
    public TextMeshProUGUI J_AmmoCounterText;
    public TextMeshProUGUI J_MessageText;
    
    [Header("Panels")]
    public GameObject J_MenuPanel;
    public GameObject J_SettingsPanel;
    public GameObject J_UIPanel;
    public GameObject J_GameOverPanel;

    [Header("Buttons")]
    public Button J_QuitButton;
    public Button J_PlayButton;
    public Button J_SettingsButton;
    public Button J_RetryButton;

    [Header("Settings stuff")]
    public TextMeshProUGUI J_FOVSliderText;
    public Slider FOVSlider;
    public TextMeshProUGUI J_VolumeSliderText;
    public Slider VolumeSlider;
    public TextMeshProUGUI J_SensSliderText;
    public Slider SensSlider;

    [Header("Enemies")]
    public GameObject[] J_Enemies;

    [Header("Effects")]
    public PostProcessVolume postProcessVolume;
    public float J_newTempreture;
    private float originalTempreture;
    private float originalExposure;
    private ColorGrading colorGrading;

    [Header("Other")]
    public float J_gameTime;
    public ThirdPersonCamera thirdPersonCamera;
    public PlayerHealth playerHealth;
    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };
    private GameState J_GameState;
    public GameState State { get { return J_GameState; } }
    private void Awake()
    {
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            originalTempreture = colorGrading.temperature.value;
        }
    }
    private void Start()
    {
        
        for (int i = 0; i < J_Enemies.Length; i++)
        {
            J_Enemies[i].SetActive(false);
        }
    }
    private bool OneLeft()
    {
        int numEnemiesLeft = 0;
        for (int i = 0; i < J_Enemies.Length; i++)
        {
            if (J_Enemies[i].activeSelf == true)
            {
                numEnemiesLeft++;
            }
        }
        return numEnemiesLeft <= 1;
    }
    private bool IsPlayerDead()
    {
        for (int i = 0; i < J_Enemies.Length; i++)
        {
            if (J_Enemies[i].activeSelf == false)
            {
                if (J_Enemies[i].tag == "Player")
                    return true;
            }
        }
        return false;
    }
    private void Update()
    {
        SlidersValue();
        if (Input.GetKeyUp(KeyCode.R))
        {
            ReloadScene();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnQuit();
        }
        switch (J_GameState)
        {
            case GameState.Start:
                GameStateStart();
                break;
            case GameState.Playing:
                GameStatePlaying();
                break;
            case GameState.GameOver:
                GameStateGameOver();
                break;
        }
    }
    public void GameStateStart()
    {
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            OnNewGame();
        }
    }
    private void GameStatePlaying()
    {
        bool isGameOver = false;
        J_gameTime += Time.deltaTime;
        int seconds = Mathf.RoundToInt(J_gameTime);
        // panels 
        J_SettingsPanel.gameObject.SetActive(false);
        J_MenuPanel.gameObject.SetActive(false);
        J_UIPanel.gameObject.SetActive(true);
        J_GameOverPanel.gameObject.SetActive(false);
        // buttons
        J_QuitButton.gameObject.SetActive(false);
        J_PlayButton.gameObject.SetActive(false);
        J_SettingsButton.gameObject.SetActive(false);
        J_RetryButton.gameObject.SetActive(false);
        // other
        J_AmmoCounterText.gameObject.SetActive(true);
        if (IsPlayerDead() == true)
        {
            J_MessageText.text = "YOU DIED";
            isGameOver = true;
        }
        else if (OneLeft() == true)
        {
            J_MessageText.text = "YOU WON";
            isGameOver = true;
        }
        if (isGameOver == true)
        {
            J_GameState = GameState.GameOver;
        }
    }
    public void GameStateGameOver()
    {
        thirdPersonCamera.UnLockCursor();
        J_GameOverPanel.gameObject.SetActive(true);
        J_RetryButton.gameObject.SetActive(true);
    }
    public void OnNewGame()
    {
        J_gameTime = 0;
        // panels 
        J_SettingsPanel.gameObject.SetActive(true);
        J_MenuPanel.gameObject.SetActive(true);
        J_UIPanel.gameObject.SetActive(false);
        J_GameOverPanel.gameObject.SetActive(false);
        // buttons
        J_QuitButton.gameObject.SetActive(true);
        J_PlayButton.gameObject.SetActive(true);
        J_SettingsButton.gameObject.SetActive(true);
        J_RetryButton.gameObject.SetActive(false);
        // other
        J_AmmoCounterText.gameObject.SetActive(true);
        for (int i = 0; i < J_Enemies.Length; i++)
        {
            J_Enemies[i].SetActive(true);
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void startGame()
    {
        J_GameState = GameState.Playing;
    }
    public void OnQuit()
    {
        Application.Quit();
    }

    public void SlidersValue()
    {
        float J_FOVNum = FOVSlider.value; J_FOVSliderText.text = string.Format("FOV:{000}", J_FOVNum);
        float J_VolumeNum = VolumeSlider.value; J_VolumeSliderText.text = string.Format("Volume: {000}%", J_VolumeNum);
        float J_SensNum = SensSlider.value; J_SensSliderText.text = string.Format("Mouse Sensitivity: {000}%", J_SensNum);
    }

    public void ApplyLowHealth()
    {
        if (colorGrading != null)
        {
            colorGrading.temperature.value = J_newTempreture;
        }
    }
    public void RevertLowHealth()
    {
        if (colorGrading != null)
        {
            colorGrading.temperature.value = originalTempreture;
        }
    }
}
