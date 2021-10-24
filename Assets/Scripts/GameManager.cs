using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected LineRenderer lineRenderer;
    protected WhiteBall whiteBall;
    public GameObject pauseMenu;
    public GameObject winnerMenu;
    public GameObject balls;
    public GameObject[] billiardBalls;
    protected PlayersPanel playersPanel;
    protected WiinnerPanel winnerPanel;

    private float currentDistance;

    //Variables 
    [Header("Player 1 Options")]
    public Color player1Color;
    public int player1Points, player1Hits = 0;

    [Header("Player 2 Options")]
    public Color player2Color;
    public int player2Points, player2Hits = 0;

    [Header("Game Options")]
    public bool isPlayer1Turn;
    public bool turnIsOver;
    public bool isGamePaused;

    public bool isGameFinished;
    public bool isPlayer1Winner;
    public bool isPlayer2Winner;
    public string winnerName;
    bool player1hittedBall;
    bool canHit;
    public bool canRespawn;
    [HideInInspector]
    public bool keepTurn;
    public bool prueba;

    private void Awake()
    {
        lineRenderer = FindObjectOfType<LineRenderer>();
        whiteBall = FindObjectOfType<WhiteBall>();
        playersPanel = FindObjectOfType<PlayersPanel>();
        winnerPanel = FindObjectOfType<WiinnerPanel>();
    }
    
    void Start()
    {
        isPlayer1Turn = true;
        isGamePaused = false;
        player1Points = 0;
        player2Points = 0;
        Time.timeScale = 1;
        canHit = false;
        prueba = false;
    }

    void Update()
    {

        Debug.Log("PRUEBA: " + prueba);
        HitBall();
        ChangePlayer();
        CheckWinner();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResume();
        }

        foreach (GameObject child in billiardBalls)
        {
            if (child.GetComponent<Rigidbody>().velocity.magnitude == 0f)
            {
                child.GetComponent<Rigidbody>().velocity = Vector3.zero;
                child.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                canRespawn = true;
                canHit = true;
            }
        }
    }

    //Funcion para cambiar de jugador y su color al Line Renderer
    public void ChangePlayer()
    {
        if (isPlayer1Turn)
        {
            lineRenderer.material.color = player1Color;
        }
        if (!isPlayer1Turn)
        {
            lineRenderer.material.color = player2Color;
        }
    }

    //Pausar o reanudar el juego
    public void PauseResume()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        isGamePaused = !isGamePaused;
    }

    //Comprobamos si algun jugador ha ganado
    public void CheckWinner()
    {
        if (isPlayer1Winner && isGameFinished)
        {
            winnerMenu.SetActive(true);
            winnerMenu.gameObject.GetComponent<WiinnerPanel>().SetWinnerText();
        }
        
        if (isPlayer2Winner && isGameFinished)
        {
            winnerMenu.SetActive(true);
            winnerMenu.gameObject.GetComponent<WiinnerPanel>().SetWinnerText();
        }
    }

    public void HitBall()
    {
        turnIsOver = false;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var direction = Vector3.zero;

        if (Physics.Raycast(ray, out hit))
        {
            var ballPos = new Vector3(whiteBall.transform.position.x, 0.1f, whiteBall.transform.position.z);
            var mousePos = new Vector3(hit.point.x, 0.1f, hit.point.z);

            //Calculamos la distancia entre nuestro Cursor y la bola blanca
            currentDistance = Vector3.Distance(whiteBall.transform.position, mousePos);
           

            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, ballPos);
            direction = (mousePos - ballPos).normalized;
        }

        if (Input.GetMouseButtonDown(0) && lineRenderer.gameObject.activeSelf)
        {

            lineRenderer.gameObject.SetActive(false);
            whiteBall.GetComponent<Rigidbody>().velocity = direction * currentDistance * 3.0f;
        }

        if (!lineRenderer.gameObject.activeSelf && whiteBall.GetComponent<Rigidbody>().velocity.magnitude == 0f && canHit)
        {
            keepTurn = false;

            whiteBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            whiteBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            if (isPlayer1Turn)
            {
                if (keepTurn)
                {
                    EndTurn();
                    isPlayer1Turn = true;
                    player1Hits++;
                    playersPanel.SetPlayer1Points(player1Points);
                }
                if (!keepTurn)
                {
                    EndTurn();
                    isPlayer1Turn = false;
                    player1Hits++;
                    playersPanel.SetPlayer1Points(player1Points);
                }
            }
            else
            {
                if (keepTurn)
                {
                    EndTurn();
                    isPlayer1Turn = false;
                    player2Hits++;
                    playersPanel.SetPlayer2Points(player2Points);
                }
                if (!keepTurn)
                {
                    EndTurn();
                    isPlayer1Turn = true;
                    player2Hits++;
                    playersPanel.SetPlayer2Points(player2Points);
                }
            }

            if (canHit)
            {
                EndTurn();
                lineRenderer.gameObject.SetActive(true);
            }
            turnIsOver = true;
        }
    }

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(5);
    }
}
