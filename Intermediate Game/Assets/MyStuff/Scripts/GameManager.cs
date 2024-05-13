using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI J_AmmoCounterText;
    // Panels
    // public GameObject J_Panel;
    // Buttons
    public Button J_QuitButton;
    
    // More things
    public GameObject[] J_Enemies;
    private float m_gameTime = 0;
    // Game state stuff
    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };
    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }
    private void Awake()
    {
        m_GameState = GameState.Start;
    }
    private void Start()
    {
        for (int i = 0; i < J_Enemies.Length; i++)
        {
            J_Enemies[i].SetActive(false);
        }
                
        J_AmmoCounterText.gameObject.SetActive(false);
    }
    private bool OneLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < J_Enemies.Length; i++)
        {
            if (J_Enemies[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        return numTanksLeft <= 1;
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
    public float GameTime { get { return m_gameTime; } }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        switch (m_GameState)
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
    private void GameStateStart()
    {
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            OnNewGame();
        }
    }
    private void GameStatePlaying()
    {
        bool isGameOver = false;

        m_gameTime += Time.deltaTime;
        int seconds = Mathf.RoundToInt(m_gameTime);

        if (IsPlayerDead() == true)
        {
            //J_MessageText.text = "YOU DIED";
            isGameOver = true;
        }
        else if (OneLeft() == true)
        {
            //J_MessageText.text = "YOU WON";
            isGameOver = true;
            
        }
        if (isGameOver == true)
        {
            m_GameState = GameState.GameOver;
        }
    }
    private void GameStateGameOver()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
            OnNewGame();
        }
    }
    public void OnNewGame()
    {
        //Panel.gameObject.SetActive(false);
        J_AmmoCounterText.gameObject.SetActive(true);
        //m_MessageText.text = "";

        m_gameTime = 0;
        m_GameState = GameState.Playing;

        for (int i = 0; i < J_Enemies.Length; i++)
        {
            J_Enemies[i].SetActive(true);
        }
    }
}
