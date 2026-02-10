using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Gameplay,
        Pause,
        GameOver,
        LevelUp,
    }

    public GameState currentState;

    public GameState previousState;

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    public GameObject LevelUpScreen;

    [Header("Current Stats Display")]
    public Text currentHealthDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;

    [Header("Result Screen Display")]
    public Image chosenCharacterImage;
    public Text chosenCharacterName;
    public Text levelReached;
    public Text timeSurvivedDisplay;
    public List<Image> chosenWeaponsUI = new List<Image>(6);
    public List<Image> chosenItemsUI = new List<Image>(6);

    [Header("StopWatch")]
    public float timeLitmit;
    float stopwatchTime;
    public Text stopwachDisplay;

    public bool isGameOver = false; 

    public bool choosingLevelUp = false;

    public GameObject playerObject;

    public Player_Collector Player_Collector;

    void Awake () 
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Debug.Log("Extra " + this + " Deleted");
        }
        DisableScreen();


    }

    void Start()
    {
        Savemanager.Load();
    }
    void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                UpdateStopwach();
                break;

            case GameState.Pause:
                CheckForPauseAndResume();
                break;

            case GameState.GameOver:
                if (!isGameOver)
                {
                    Time.timeScale = 0f;
                    Player_Collector.SaveCoinsToStash(Player_Collector.GetCoins());
                    isGameOver = true;
                    DisplayResults();
                }
                break;
            case GameState.LevelUp:
                if (!choosingLevelUp) 
                {
                    choosingLevelUp = true;
                    Time.timeScale = 0f;
                    LevelUpScreen.SetActive(true);
                }
                break ;
            default:
                Debug.LogWarning("STATE DOES NOT EXIST");
                break;
        }
    }
    public void ChangeState(GameState newState) 
    {  
        currentState = newState; 
    }


    public void PauseGame() 
    {
        if (currentState != GameState.Pause)
        {
            previousState = currentState;
            ChangeState(GameState.Pause);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }

    }

    public void ResumaGame() 
    {
        if (currentState == GameState.Pause) 
        { 
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen?.SetActive(false);
        }

    }
    void CheckForPauseAndResume()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (currentState == GameState.Pause)
            {
                ResumaGame();
            }
            else 
            {
                PauseGame();
            }
        }
    }
    void DisableScreen()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
        LevelUpScreen.SetActive(false);
    }

    public void GameOver()
    {
        timeSurvivedDisplay.text = stopwachDisplay.text;
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(PlayerScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;

    }
    public void AssingLevelReachedUI(int levelReachedData)
    {
        levelReached.text = levelReachedData.ToString();
    }

    public void AssingChosenItemsAndWeaponsUI(List<Image> chosenWeaponsData, List<Image> chosenItemsData)
    {
        if (chosenWeaponsData.Count != chosenWeaponsUI.Count || chosenItemsData.Count != chosenItemsUI.Count)
        {
            Debug.Log("chosen item or weapon leght did not mach the data");
            return;
        }
        for (int i = 0; i < chosenWeaponsUI.Count; i++)
        {
            if (chosenWeaponsData[i].sprite)
            {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].sprite;
            }
            else
            {
                chosenWeaponsUI[i].enabled = false;
            }
        }
        for (int i = 0; i < chosenItemsUI.Count; i++)
        {
            if (chosenItemsData[i].sprite)
            {
                chosenItemsUI[i].enabled = true;
                chosenItemsUI[i].sprite = chosenItemsData[i].sprite;
            }
            else
            {
                chosenItemsUI[i].enabled = false;
            }
        }
    }

    void UpdateStopwach()
    {
        stopwatchTime += Time.deltaTime;

        UpdateStopwachDisplay();

        if (stopwatchTime >= timeLitmit) 
        {
            GameOver();
        }
    }

    void UpdateStopwachDisplay()
    {
        int minutes = Mathf.FloorToInt(stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(stopwatchTime % 60);

        stopwachDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp() 
    {
        choosingLevelUp = false;
        Time.timeScale = 1f;
        LevelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
 }
