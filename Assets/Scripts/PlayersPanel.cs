using TMPro;
using UnityEngine;

public class PlayersPanel : MonoBehaviour
{
    public TextMeshProUGUI player1Points;
    public TextMeshProUGUI player2Points;

    public TextMeshProUGUI player1BallType;
    public TextMeshProUGUI player2BallType;

    public void SetPlayer1Points(int point)
    {
        player1Points.SetText(point.ToString() + " p");
    }

    public void SetPlayer2Points(int point)
    {
        player2Points.SetText(point.ToString() + " p");
    }

    public void SetPlayer1BallType(string text)
    {
        player1BallType.SetText(text);
    }
    public void SetPlayer2BallType(string text)
    {
        player2BallType.SetText(text);
    }
}
