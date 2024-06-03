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
    //public GameObject J_Panel;

    [Header("Buttons")]
    public Button J_QuitButton;

    [Header("Enemies")]
    public GameObject[] J_Enemies;
    
    // Game state stuff
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
        J_GameState = GameState.Start;
        //J_Panel.gameObject.SetActive(false);
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
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
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
        //J_Panel.gameObject.SetActive(true);

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
    private void GameStateGameOver()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
            OnNewGame();
        }
    }
    public void OnNewGame()
    {
        //J_Panel.gameObject.SetActive(false);
        J_AmmoCounterText.gameObject.SetActive(true);
        J_MessageText.text = "";

        J_GameState = GameState.Playing;

        for (int i = 0; i < J_Enemies.Length; i++)
        {
            J_Enemies[i].SetActive(true);
        }
    }
}
