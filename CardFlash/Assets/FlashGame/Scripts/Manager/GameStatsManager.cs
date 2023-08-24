
using UnityEngine;
using UnityEngine.UI;

public class GameStatsManager : MonoBehaviour
{
    [SerializeField] Text ScroreText;
    [SerializeField] Text TurnTextText;

    public void UpdateScore(string score)
    {
        ScroreText.text = "Score" + "\n" + score;
    }

    public void UpdateTurns(string turn)
    {
        TurnTextText.text = "Turns" + "\n" + turn;
    }
}

