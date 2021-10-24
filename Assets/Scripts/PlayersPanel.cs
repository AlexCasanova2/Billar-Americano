using TMPro;
using UnityEngine;

public class PlayersPanel : MonoBehaviour
{
    public TextMeshProUGUI player1Points;
    public TextMeshProUGUI player2Points;

    public void SetPlayer1Points(int point)
    {
        player1Points.SetText(point.ToString() + " p");
    }

    public void SetPlayer2Points(int point)
    {
        player2Points.SetText(point.ToString() + " p");
    }
}
