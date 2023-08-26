
using UnityEngine;
using UnityEngine.UI;

public class GameStatsManager : MonoBehaviour
{
    [SerializeField] Text ScroreText;
    [SerializeField] Text TurnTextText;

    private void Start()
    {
        GameController.s_Instance.onLoadSavedGame += LoadPrevGame;
    }
    void LoadPrevGame()
    {

        UpdateScore(GameMapController.s_Instance.GetRoot.ScoreCount.ToString());
        UpdateTurns(GameMapController.s_Instance.GetRoot.TurnCount.ToString());
    }
    public void UpdateScore(string score)
    {
        ScroreText.text = "Score :" + "\n" + score;
    }

    public void UpdateTurns(string turn)
    {
        TurnTextText.text = "Turns :" + "\n" + turn;
    }
}

