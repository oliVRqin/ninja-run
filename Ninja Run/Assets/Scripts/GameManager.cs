using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { menu, getReady, oops, gameOver, success};

public class GameManager : MonoBehaviour
{
    // Singleton Declaration
    public static GameManager S;

    // Game State
    public GameState gameState;

    // UI variables
    public Text messageOverlay;
    public Text messageCount;
    public Text messageLives;
    public Text messageTotalCount;

    // Game variables
    private bool isAlive = true;
    private int currScore = 0;
    private static int finalScore = 0;
    private static int numLives = 5;

    // player variables
    public GameObject playerPrefab;

    private void Awake()
    {
        if (GameManager.S)
        {
            Destroy(this.gameObject);
        } else
        {
            S = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.menu;
        isAlive = true;
        messageCount.enabled = true;
        messageCount.text = "Score: " + currScore;
        messageLives.enabled = true;
        messageLives.text = "Lives: " + numLives;
        messageTotalCount.enabled = true;
        messageTotalCount.text = "Cumulative Score: " + finalScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.menu)
        {
            StartANewGame();
        } else if (gameState == GameState.gameOver)
        {
        } else if (gameState == GameState.success)
        {
        } else {
            if (playerPrefab.transform.position.y <= -5) 
            {
                SoundManager.S.MakePlayerDeathSound();
                PlayerDestroyed();
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void StartANewGame()
    {
        currScore = 0;
        messageCount.text = "Score: " + currScore;
        messageLives.text = "Lives: " + numLives;
        messageTotalCount.text = "Cumulative Score: " + finalScore;
        playerPrefab.transform.position = new Vector3(0,-0.6f,0);
        playerPrefab.gameObject.SetActive(true);
        ResetRound();
    }

    private void ResetRound()
    {
        gameState = GameState.getReady;
        StartCoroutine(GetReadyState());
    }

    public IEnumerator GetReadyState()
    {
        messageOverlay.enabled = true;
        messageOverlay.text = "Let's go!!!";
        yield return new WaitForSeconds(0.5f);
        messageOverlay.enabled = false;
    }

    public void EnemyDestroyed()
    {
        currScore += 10;
        messageCount.text = "Score: " + currScore;
    }

    public void PlayerDestroyed()
    {
        isAlive = false;
        playerPrefab.gameObject.SetActive(false);
        StartCoroutine(OopsState());
    }

    public void CoinCollected()
    {
        currScore++;
        messageCount.text = "Score: " + currScore;
    }
    bool playerWonHappened = false;
    public void PlayerWon()
    {
        if (playerWonHappened == false) {
            StartCoroutine(OopsState());
            playerWonHappened = true;
        }
    }
    
    public IEnumerator OopsState()
    {
        gameState = GameState.oops;
        if (isAlive == false){
            gameState = GameState.gameOver;
            numLives -= 1;
            messageLives.text = "Lives: " + numLives;
            if (numLives <= 0) {
                messageOverlay.enabled = true;
                finalScore += currScore;
                messageOverlay.text =  $"Better luck next time! \n Final Score: {finalScore}";
                yield return new WaitForSeconds(3.0f);
                SceneManager.LoadScene("MainMenu");
                numLives = 5;
                finalScore = 0;
            } else {
                messageOverlay.enabled = true;
                messageOverlay.text =  (numLives != 1) ? $"Ouch! You have {numLives} lives left" : $"Ouch! You have {numLives} life left";
                yield return new WaitForSeconds(3.0f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        } else {
            gameState = GameState.success;
            finalScore += currScore;
            if (SceneManager.GetActiveScene().name == "Level1") {
                messageOverlay.enabled = true;
                numLives++;
                messageOverlay.text =  " Hooray, you passed the first level! ";
                yield return new WaitForSeconds(2.0f);
                messageOverlay.text =  "Level 2 in 3...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Level 2 in 2...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Level 2 in 1...";
                yield return new WaitForSeconds(1.0f);
                playerWonHappened = false;
                SceneManager.LoadScene("Level2");
            } else if (SceneManager.GetActiveScene().name == "Level2") {
                messageOverlay.enabled = true;
                numLives++;
                messageOverlay.text =  $" Hooray, you passed the second level! ";
                yield return new WaitForSeconds(2.0f);
                messageOverlay.text =  "Level 3 in 3...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Level 3 in 2...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Level 3 in 1...";
                yield return new WaitForSeconds(1.0f);
                playerWonHappened = false;
                SceneManager.LoadScene("Level3");
            } else if (SceneManager.GetActiveScene().name == "Level3") {
                messageOverlay.enabled = true;
                numLives++;
                messageOverlay.text =  $" Hooray, you passed the third level! ";
                yield return new WaitForSeconds(2.0f);
                messageOverlay.text =  "Final Level in 3...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Final Level in 2...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Final Level in 1...";
                yield return new WaitForSeconds(1.0f);
                playerWonHappened = false;
                SceneManager.LoadScene("BossScene");
            } else if (SceneManager.GetActiveScene().name == "BossScene") {
                messageTotalCount.enabled = false;
                messageOverlay.enabled = true;
                messageOverlay.text =  $" Game Over: You Win! \n Final Score: {finalScore} ";
                yield return new WaitForSeconds(2.0f);
                messageOverlay.text =  "Main Menu in 3...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Main Menu in 2...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Main Menu in 1...";
                yield return new WaitForSeconds(1.0f);
                playerWonHappened = false;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
