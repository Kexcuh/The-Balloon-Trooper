using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public int currentSceneIndex; // Index of current scene
    [SerializeField] int score = 0; // How much score player has
    const int DEFAULT_POINTS = 1; //Default score for player
    public Text scoreText; //Display text on HUD for score amount
    public Text playerLivesText;
    public int playerLives;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = 1;
        playerLives = 3;
        playerLivesText.text = playerLives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        HandleLevelSwitch();
    }

    private void Awake()
    {
        
        // Check if another instance already exists
        if (FindObjectsOfType<ScoreKeeper>().Length > 1)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
        
        // Make this object persistent
        DontDestroyOnLoad(gameObject);
    }

    public void HandleLevelSwitch()
    {

    }

[ContextMenu("Increase Score")]
    public void AddPoints()
    {
        AddPoints(DEFAULT_POINTS);
    }
    
    [ContextMenu("Increase Score")]
    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
        Debug.Log("score: " + score);
    }

     [ContextMenu("Restart Level")]
    public void restartLevel()
    {
        SceneManager.LoadScene(currentSceneIndex); 
        
    }

    [ContextMenu("Next Level")]
    public void nextLevel()
    {
        currentSceneIndex++;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void resetPlayerLives()
    {
        playerLives = 3;
        playerLivesText.text = playerLives.ToString();
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Level 1");
        score = 0;
        scoreText.text = score.ToString();
        playerLives = 3;
        playerLivesText.text = playerLives.ToString();
        currentSceneIndex = 1;
    }
    public void loadScore()
    {
        SceneManager.LoadScene("Score Table");
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void incSceneIndex()
    {
        currentSceneIndex++;
    }

    public void loseALife()
    {
        playerLives-=1;
        playerLivesText.text = playerLives.ToString();
    }

    [ContextMenu("GameOver")]
    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    [ContextMenu("Start Game From Beginning")]
    public void StartGameFromBeginning()
    {
        restartGame();
    }

    [ContextMenu("SetPlayerResults")]
    public void SetPlayerResults(string playerName, int playerScore)
    {currentSceneIndex = 1;
        // Set the player's name and score in PersistentData
        //PersistentData.Instance.setName(playerName);
        //PersistentData.Instance.setScore(playerScore);

        Debug.Log($"Player name set to: {playerName}");
        Debug.Log($"Player score set to: {playerScore}");
    }

    void EndGame()
    {
        string playerName = "Player1";
        int playerScore = 150;         // Example score (could come from your game logic)

        SetPlayerResults(playerName, playerScore);

        // Load the scoreboard scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("ScoreTable");
    }

}
