using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Texts")]
    public TextMeshProUGUI J_AmmoCounterText;
    public TextMeshProUGUI J_MessageText;
    
    [Header("Panels")]
    public GameObject J_MenuPanel;
    public GameObject J_SettingsPanel;
    public GameObject J_UIPanel;

    [Header("Buttons")]
    public Button J_QuitButton;
    public Button J_PlayButton;
    public Button J_SettingsButton;

    [Header("Settings stuff")]

    [Header("Enemies")]
    public GameObject[] J_Enemies;
    
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
        
    }
    private void Start()
    {
        for (int i = 0; i < J_Enemies.Length; i++)
        {
            J_Enemies[i].SetActive(true);
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
        // panels 
        J_SettingsPanel.gameObject.SetActive(false);
        J_MenuPanel.gameObject.SetActive(false);
        J_UIPanel.gameObject.SetActive(true);
        // buttons
        J_QuitButton.gameObject.SetActive(false);
        J_PlayButton.gameObject.SetActive(false);
        J_SettingsButton.gameObject.SetActive(false);
        // other
        J_AmmoCounterText.gameObject.SetActive(true);
        bool isGameOver = false;
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
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            OnNewGame();
        }
    }
    public void OnNewGame()
    {
        // panels 
        J_SettingsPanel.gameObject.SetActive(true);
        J_MenuPanel.gameObject.SetActive(true);
        J_UIPanel.gameObject.SetActive(false);
        // buttons
        J_QuitButton.gameObject.SetActive(true);
        J_PlayButton.gameObject.SetActive(true);
        J_SettingsButton.gameObject.SetActive(true);
        // other
        J_AmmoCounterText.gameObject.SetActive(false);
    }
    public void startGame()
    {
        J_GameState = GameState.Playing;
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
