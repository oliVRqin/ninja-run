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

    // Game variables
    private bool isAlive = true;
    private int currScore = 0;

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
        messageCount.text = "Count: " + currScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.menu)
        {
            StartANewGame();
        } else if (gameState == GameState.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartLevel();
                StartANewGame();
            }
        } else if (gameState == GameState.success)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartLevel();
                StartANewGame();
            }
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
        // reload this scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void StartANewGame()
    {
        currScore = 0;
        messageCount.text = "Count: " + currScore;
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
        messageOverlay.text = "Get Ready!!!";

        yield return new WaitForSeconds(2.0f);

        messageOverlay.enabled = false;
    }

    public void EnemyDestroyed()
    {
        currScore += 10;
        messageCount.text = "Count: " + currScore;
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
        messageCount.text = "Count: " + currScore;
    }

    public void PlayerWon()
    {
        StartCoroutine(OopsState());
    }

    public IEnumerator OopsState()
    {
        gameState = GameState.oops;

        if (isAlive == false){
            gameState = GameState.gameOver;
            messageOverlay.enabled = true;
            messageOverlay.text =  " Game Over: You Lose ";
            yield return new WaitForSeconds(2.0f);
            messageOverlay.text =  " Press 'R' to Restart ";
        } else {
            gameState = GameState.success;
            if (SceneManager.GetActiveScene().name == "Level1") {
                messageOverlay.enabled = true;
                messageOverlay.text =  " Game Over: You Win ";
                yield return new WaitForSeconds(2.0f);
                messageOverlay.text =  "Level 2 in 3...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Level 2 in 2...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Level 2 in 1...";
                yield return new WaitForSeconds(1.0f);
                SceneManager.LoadScene("Level2");
            } else if (SceneManager.GetActiveScene().name == "Level2") {
                messageOverlay.enabled = true;
                messageOverlay.text =  " Game Over: You Win ";
                yield return new WaitForSeconds(2.0f);
                messageOverlay.text =  "Level 3 in 3...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Level 3 in 2...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Level 3 in 1...";
                yield return new WaitForSeconds(1.0f);
                SceneManager.LoadScene("Level3");
            } else if (SceneManager.GetActiveScene().name == "Level3") {
                messageOverlay.enabled = true;
                messageOverlay.text =  " Game Over: You Win ";
                yield return new WaitForSeconds(2.0f);
                messageOverlay.text =  "Final Level in 3...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Final Level in 2...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Final Level in 1...";
                yield return new WaitForSeconds(1.0f);
                SceneManager.LoadScene("BossScene");
            } else if (SceneManager.GetActiveScene().name == "BossScene") {
                messageOverlay.enabled = true;
                messageOverlay.text =  " Game Over: You Win! ";
                yield return new WaitForSeconds(2.0f);
                messageOverlay.text =  "Main Menu in 3...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Main Menu in 2...";
                yield return new WaitForSeconds(1.0f);
                messageOverlay.text =  "Main Menu in 1...";
                yield return new WaitForSeconds(1.0f);
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
