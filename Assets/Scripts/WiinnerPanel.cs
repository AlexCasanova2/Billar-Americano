using TMPro;
using UnityEngine;

public class WiinnerPanel : MonoBehaviour
{
    public GameObject highscoreTable;
    protected GameManager gameManager;
    public string input;
    public bool canSave;
    public string winnerName;
    public TextMeshProUGUI textWinner;



    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        highscoreTable = GameObject.Find("SaveManager");
    }

    public void Prueba()
    {
        Debug.Log("ey");
    }

    public void SetWinnerText()
    {
        if (gameManager.isPlayer1Winner)
        {
            textWinner.SetText("Congratulations Player 1");
        }
        if (gameManager.isPlayer2Winner)
        {
            textWinner.SetText("Congratulations Player 2");
        }
    }

    public void GetPlayerName(string s)
    {
        input = s;
        canSave = true;

        if (gameManager.isPlayer1Winner)
        {
            int totalPoints = gameManager.player1Points;
            int totalHits = gameManager.player1Hits;
            winnerName = input;
            /*Debug.Log("Points: " + totalPoints);
            Debug.Log("Hits: " + totalHits);
            Debug.Log("Name: " + winnerName);*/

            highscoreTable.GetComponent<HighscoreTable>().AddHighscoreEntry(totalPoints, totalHits, winnerName);

        }
        
        if (gameManager.isPlayer2Winner)
        {
            int totalPoints = gameManager.player2Points;
            int totalHits = gameManager.player2Hits;
            winnerName = input;
            /*Debug.Log("Points: " + totalPoints);
            Debug.Log("Hits: " + totalHits);
            Debug.Log("Name: " + winnerName);*/

            highscoreTable.GetComponent<HighscoreTable>().AddHighscoreEntry(totalPoints, totalHits, winnerName);

        }
                    
    }
}
