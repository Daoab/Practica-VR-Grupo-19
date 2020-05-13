using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

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
    //En el juego solo hay una tubería pero el código está preparado por si se quisieran poner más en pantalla para aumentar la dificultad

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int maxScore = 10;
    int highScore = 0;
    public int score = 0;

    [Header("Menu UI")]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI uwuText;
    [SerializeField] RawImage runningBackground;
    [SerializeField] RawImage menuBackground;

    [Header("Doors")]
    [SerializeField] GameObject[] doors;

    [Header("Events")]
    [SerializeField] UnityEvent scoreUp;
    [SerializeField] UnityEvent onGameCompleted;

    //GAME STATE
    bool gameRunning = false;

    void Awake()
    {
        juanRB = juan.GetComponent<Rigidbody2D>();
        juanMovement = juan.GetComponent<JuanMovement>();
        juanOrigin = juan.transform.position;
        InitializePipes();

        ShowMenu();

        //Se activan las puertas que impiden al jugador acceder a la sala del pc al principio
        foreach (GameObject door in doors) door.SetActive(true);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A)) ProcessButtonInput();

        if (gameRunning)
        {
            if (juan.position.y < downLimit.position.y) //Si el jugador sale de la pantalla por debajo pierde
                GameOver();

            UpdateScore();
        }
    }

    //Configuración inicial de Flappy Juan
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
        highScoreText.text = "High Score: " + highScore.ToString() + "/" + maxScore.ToString();
        runningBackground.enabled = false;
        menuBackground.enabled = true;

        foreach (Pipe pipe in pipes) pipe.gameObject.SetActive(false);
        juan.gameObject.SetActive(false);
    }

    private void winGame()
    {
        //En caso de ganar se desactivan las puertas para que el jugador pueda ir a la sala del pc
        foreach (GameObject door in doors) door.SetActive(false);
        onGameCompleted.Invoke();
        GameOver();
    }

    public void GameOver()
    {
        gameRunning = false;
        highScore = score > highScore ? score : highScore;
        ShowMenu();

        juanRB.constraints = RigidbodyConstraints2D.FreezeAll;

        foreach (Pipe pipe in pipes) pipe.Freeze();
    }

    //Función que comprueba si Juan ha pasado una tubería para actualizar la puntuación
    private void UpdateScore()
    {
        foreach (Pipe pipe in pipes)
        {
            if (pipe.canAddScore && pipe.transform.position.x < juan.position.x)
            {
                score++;
                pipe.canAddScore = false;
                scoreUp.Invoke();
                if (score == maxScore) winGame();
            }
        }
        scoreText.text = score.ToString() + "/" + maxScore.ToString();
    }

    //Función que randomiza todas las tuberías
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
