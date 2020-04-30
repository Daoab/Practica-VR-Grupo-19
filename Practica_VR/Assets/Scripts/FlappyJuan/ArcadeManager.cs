using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArcadeManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform juan;
    JuanMovement juanMovement;
    private Vector3 juanOrigin;
    private Rigidbody2D juanRB;

    [Header("World Limits")]
    [SerializeField] Transform leftLimit;
    [SerializeField] Transform rightLimit;
    [SerializeField] Transform downLimit;
    [SerializeField] Transform upLimit;

    [Header("Pipes")]
    [SerializeField] Pipe[] pipes;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    float highScore = 2f;
    public float score = 0f;

    [Header("Menu UI")]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI uwuText;
    [SerializeField] RawImage runningBackground;
    [SerializeField] RawImage menuBackground;


    //GAME STATE
    bool gameRunning = false;

    void Awake()
    {
        juanRB = juan.GetComponent<Rigidbody2D>();
        juanMovement = juan.GetComponent<JuanMovement>();
        juanOrigin = juan.transform.position;
        InitializePipes();

        //LEER HIGHSCORE DE FICHERO

        ShowMenu();
    }

    void Update()
    {
        // LIMITE INFERIOR DEL MUNDO
        if (gameRunning)
        {
            if (juan.position.y < downLimit.position.y)
                GameOver();
            UpdateScore();
        }
    }

    public void StartGame()
    {
        Debug.Log("Start Game");

        foreach (Pipe pipe in pipes) pipe.gameObject.SetActive(true);
        juan.gameObject.SetActive(true);

        gameRunning = true;

        scoreText.enabled = true;
        uwuText.enabled = false;
        titleText.enabled = false;
        highScoreText.enabled = false;

        runningBackground.enabled = true;
        menuBackground.enabled = false;

        score = 0;

        juan.position = juanOrigin;
        juanRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        foreach (Pipe pipe in pipes) pipe.Release();
        ResetPipes();
    }

    public void ProcessButtonInput()
    {
        if (gameRunning)
            juanMovement.Jump();
        else
            StartGame();
    }

    private void ShowMenu()
    {
        scoreText.enabled = false;
        uwuText.enabled = true;
        titleText.enabled = true;
        highScoreText.enabled = true;
        highScoreText.text = "High Score: " + highScore.ToString();
        runningBackground.enabled = false;
        menuBackground.enabled = true;

        foreach (Pipe pipe in pipes) pipe.gameObject.SetActive(false);
        juan.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");

        gameRunning = false;
        highScore = score > highScore ? score : highScore;
        ShowMenu();

        juanRB.constraints = RigidbodyConstraints2D.FreezeAll;

        foreach (Pipe pipe in pipes) pipe.Freeze();

        
        //ESCRIBIR FICHERO
    }

    private void UpdateScore()
    {
        foreach (Pipe pipe in pipes)
        {
            if (pipe.canAddScore && pipe.transform.position.x < juan.position.x)
            {
                score++;
                pipe.canAddScore = false;
            }
        }
        scoreText.text = score.ToString();
    }

    private void ResetPipes()
    {
        foreach (Pipe pipe in pipes)
        {
            pipe.Randomize();
        }
    }

    private void InitializePipes()
    {
        foreach (Pipe pipe in pipes)
        {
            pipe.upLimit = upLimit;
            pipe.downLimit = downLimit;
            pipe.rightLimit = rightLimit;
            pipe.leftLimit = leftLimit;
            pipe.InitializePipe(this);
        }
    }
}
