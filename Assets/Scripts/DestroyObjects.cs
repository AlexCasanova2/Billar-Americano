using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    protected GameManager gameManager;
    protected PlayersPanel playersPanel;

    public GameObject player1BallType;
    public GameObject player2BallType;

    bool player1FirstBall;
    bool player2FirstBall;
    bool player1Smooth, player1Striped;
    bool player2Smooth, player2Striped;
    int count;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playersPanel = FindObjectOfType<PlayersPanel>();
        gameManager.isPlayer1Turn = true;
        player1FirstBall = true;
        player2FirstBall = true;
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (player1Smooth && player2Striped)
        {
            player1BallType.SetActive(true);
            player2BallType.SetActive(true);
            playersPanel.SetPlayer1BallType("Smooth");
            playersPanel.SetPlayer2BallType("Stripped");
        }
        if (player1Striped && player2Smooth)
        {
            player1BallType.SetActive(true);
            player2BallType.SetActive(true);
            playersPanel.SetPlayer1BallType("Stripped");
            playersPanel.SetPlayer2BallType("Smooth");
        }
    }

    //Logica del juego para contabilizar puntos mediante las normas del billar 
    private void OnTriggerEnter(Collider other)
    {
        gameManager.prueba = true;
        count++;

        //PLAYER1

        //Comprobamos si el Player 1 introduce las lisas por primera vez en la partida
        if (other.CompareTag("SmoothBall") && gameManager.isPlayer1Turn)
        {
            if (gameManager.player1Points <= 0 && player1FirstBall && !player1Smooth && !player1Striped)
            {
                gameManager.player1Points++;
                playersPanel.SetPlayer1Points(gameManager.player1Points);

                player1Smooth = true; player2Striped = true;
                player1FirstBall = false; player2FirstBall = false;
                
                gameManager.isPlayer1Turn = true;

                //El player 1 sigue tirando porque ha metido una bola
                gameManager.keepTurn = true;
                Debug.Log("El Jugador 1 ha metido una LISA por PRIMERA VEZ.");
            }
            other.gameObject.SetActive(false);
        }
        //Comprobamos si el Player 1 introduce una bola lisa despues de introducir la primera
        if (other.CompareTag("SmoothBall") && gameManager.isPlayer1Turn && !player1FirstBall && player1Smooth && count >=2)
        {
            gameManager.player1Points++;
            playersPanel.SetPlayer1Points(gameManager.player1Points);
            other.gameObject.SetActive(false);
            gameManager.isPlayer1Turn = true;

            //El player 1 sigue tirando porque ha metido una bola
            gameManager.keepTurn = true;
            Debug.Log("El Jugador 1 ha metido una LISA y va a LISAS.");
        }
        //Comprobamos si el Player 1 introduce una bola rayada si solo puede jugar las lisas
        if (other.CompareTag("StripedBalls") && gameManager.isPlayer1Turn && !player1FirstBall && player1Smooth && count >= 2)
        {
            gameManager.player2Points++;
            playersPanel.SetPlayer2Points(gameManager.player2Points);
            other.gameObject.SetActive(false);
            gameManager.isPlayer1Turn = !gameManager.isPlayer1Turn;

            //El player 1 NO sigue tirando porque ha metido una bola que no pertoca
            gameManager.keepTurn = false;
            Debug.Log("El Jugador 2 ha metido una RALLADA y va a LISAS.");            
        }

        //Comprobamos si el Player 1 introduce las rayadas por primera vez en la partida
        if (other.CompareTag("StripedBalls") && gameManager.isPlayer1Turn && !player1Smooth && !player1Striped)
        {
            if (gameManager.player1Points <= 0 && player1FirstBall)
            {
                gameManager.player1Points++;
                player1Striped = true;
                player2Smooth = true;
                player1FirstBall = false;
                player2FirstBall = false;
                gameManager.isPlayer1Turn = true;
                playersPanel.SetPlayer1Points(gameManager.player1Points);
                Debug.Log("El Jugador 1 ha metido una RALLADA por PRIMERA VEZ.");

                //El player 1 sigue tirando porque ha metido una bola
                gameManager.keepTurn = true;
            }
            other.gameObject.SetActive(false);
        }
        //Comprobamos si el Player 1 introduce una bola rayada despues de introducir la primera
        if (other.CompareTag("StripedBalls") && gameManager.isPlayer1Turn && !player1FirstBall && player1Striped && count >= 2)
        {
            gameManager.player1Points++;
            playersPanel.SetPlayer1Points(gameManager.player1Points);
            other.gameObject.SetActive(false);
            gameManager.isPlayer1Turn = true;
            Debug.Log("El Jugador 1 ha metido una RALLADA y va a RALLADAS.");

            //El player 1 sigue tirando porque ha metido una bola
            gameManager.keepTurn = true;
        }
        //Comprobamos si el Player 1 introduce una bola lisa si solo puede jugar las rayadas
        if (other.CompareTag("SmoothBall") && gameManager.isPlayer1Turn && !player1FirstBall && player1Striped && count >= 2)
        {
            gameManager.player2Points++;
            playersPanel.SetPlayer2Points(gameManager.player2Points);
            other.gameObject.SetActive(false);
            gameManager.isPlayer1Turn = !gameManager.isPlayer1Turn;
            Debug.Log("El Jugador 1 ha metido una LISA y va a RALLADAS.");

            //El player 1 NO sigue tirando porque ha metido una bola que no pertoca
            gameManager.keepTurn = false;
        }

        //PLAYER2



        //Comprobamos si el Player 2 introduce las lisas por primera vez en la partida
        if (other.CompareTag("SmoothBall") && !gameManager.isPlayer1Turn)
        {
            if (gameManager.player2Points <= 0 && player2FirstBall && !player2Smooth && !player2Striped)
            {
                gameManager.player2Points++;
                player2Smooth = true;
                player1Striped = true;
                player2FirstBall = false;
                player1FirstBall = false;
                gameManager.isPlayer1Turn = false;
                playersPanel.SetPlayer2Points(gameManager.player2Points);
                Debug.Log("El Jugador 2 ha metido una LISA por PRIMERA VEZ.");

                //El player 2 sigue tirando porque ha metido una bola
                gameManager.keepTurn = true;
            }
            other.gameObject.SetActive(false);
        }
        //Comprobamos si el Player 2 introduce una bola lisa despues de introducir la primera
        if (other.CompareTag("SmoothBall") && !gameManager.isPlayer1Turn && !player2FirstBall && player2Smooth && count >= 2)
        {
            gameManager.player2Points++;
            playersPanel.SetPlayer2Points(gameManager.player2Points);
            other.gameObject.SetActive(false);
            gameManager.isPlayer1Turn = false;
            Debug.Log("El Jugador 2 ha metido una LISA y va a LISAS.");

            //El player 2 sigue tirando porque ha metido una bola
            gameManager.keepTurn = true;
        }
        //Comprobamos si el Player 2 introduce una bola rayada si solo puede jugar las lisas
        if (other.CompareTag("StripedBalls") && !gameManager.isPlayer1Turn && !player2FirstBall && player2Smooth && count >= 2)
        {
            gameManager.player1Points++;
            playersPanel.SetPlayer1Points(gameManager.player1Points);
            other.gameObject.SetActive(false);
            Debug.Log("El Jugador 2 ha metido una RALLADA y va a LISAS.");
            Debug.Log("CAMBIO DE JUGADOR");
            gameManager.isPlayer1Turn = true;

            //El player 1 NO sigue tirando porque ha metido una bola que no pertoca
            gameManager.keepTurn = false;
        }


        //Comprobamos si el Player 2 introduce las rayadas por primera vez en la partida
        if (other.CompareTag("StripedBalls") && !gameManager.isPlayer1Turn)
        {
            if (gameManager.player2Points <= 0 && player2FirstBall && !player2Smooth && !player2Striped)
            {
                gameManager.player2Points++;
                player2Striped = true;
                player1Smooth = true;
                player2FirstBall = false;
                player1FirstBall = false;
                gameManager.isPlayer1Turn = false;
                playersPanel.SetPlayer2Points(gameManager.player2Points);
                Debug.Log("El Jugador 2 ha metido una RALLADA por PRIMERA VEZ.");

                //El player 2 sigue tirando porque ha metido una bola
                gameManager.keepTurn = true;
            }
            other.gameObject.SetActive(false);
        }
        //Comprobamos si el Player 2 introduce una bola rayada despues de introducir la primera
        if (other.CompareTag("StripedBalls") && !gameManager.isPlayer1Turn && !player2FirstBall && player2Striped && count >= 2)
        {
            gameManager.player2Points++;
            playersPanel.SetPlayer2Points(gameManager.player2Points);
            other.gameObject.SetActive(false);
            gameManager.isPlayer1Turn = false;
            Debug.Log("El Jugador 2 ha metido una RALLADA y va a RALLADAS.");

            //El player 2 sigue tirando porque ha metido una bola
            gameManager.keepTurn = true;
        }
        //Comprobamos si el Player 2 introduce una bola lisa si solo puede jugar las rayadas
        if (other.CompareTag("SmoothBall") && !gameManager.isPlayer1Turn && !player2FirstBall && player2Striped && count >= 2)
        {
            gameManager.player1Points++;
            playersPanel.SetPlayer1Points(gameManager.player1Points);
            other.gameObject.SetActive(false);
            gameManager.isPlayer1Turn = !gameManager.isPlayer1Turn;
            Debug.Log("El Jugador 2 ha metido una LISA y va a RALLADAS.");
            Debug.Log("CAMBIO DE JUGADOR");

            //El player 1 NO sigue tirando porque ha metido una bola que no pertoca
            gameManager.keepTurn = false;
        }


        //Comprobamos si entra la bola blanca
        if (other.CompareTag("WhiteBall"))
        {
            other.GetComponent<WhiteBall>().RespawnWhiteBall();
        }
        //Comprobamos si entra la bola negra
        if (other.CompareTag("BlackBall"))
        {
            if (gameManager.isPlayer1Turn && gameManager.player1Points >= 7)
            {
                gameManager.isGameFinished = true;
                gameManager.isPlayer1Winner = true;
                Time.timeScale = 0;
            }
            if (gameManager.isPlayer1Turn && gameManager.player1Points < 7)
            {
                gameManager.isGameFinished = true;
                gameManager.isPlayer2Winner = true;
                Time.timeScale = 0;
            }
            if (!gameManager.isPlayer1Turn && gameManager.player2Points >= 7)
            {
                gameManager.isGameFinished = true;
                gameManager.isPlayer2Winner = true;
                Time.timeScale = 0;
            }
            if (!gameManager.isPlayer1Turn && gameManager.player2Points < 7)
            {
                gameManager.isGameFinished = true;
                gameManager.isPlayer1Winner = true;
                Time.timeScale = 0;
            }

            other.gameObject.SetActive(false);
        }
        gameManager.prueba = false;
    }
    
}
