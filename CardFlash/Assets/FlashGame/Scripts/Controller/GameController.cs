using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController s_Instance;

    public Action onHideAllCards;
    public Action<int> onMatchFound;
    public Action onStartGame;
    public Action onLoadSavedGame;

    public Action onResetCards;
    public Action onCardClicked;
    public Action onGameCompleted;

    [SerializeField] GameStatsManager GameStats;

    CardData previousCard = null;
    int ScoreCount=0;
    int turnCount=0;

    #region Unity_CallBack

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else
            Destroy(gameObject);
    }
    private void OnDestroy()
    {
        onHideAllCards = null;
        onMatchFound = null;
        onResetCards = null;
        onGameCompleted = null;
        onCardClicked = null;
    }
    #endregion

    public void HideCards()
    {
        onHideAllCards?.Invoke();
    }
    public void FlashCardClicked(CardData card)
    {
        onCardClicked?.Invoke();
        if (previousCard == null)
        {
            previousCard = card;
            return;
        }
        StartCoroutine(EvaluateData(card));
    }

    IEnumerator EvaluateData(CardData card)
    {
        yield return new WaitForSeconds(0.18f);
        if (card.CardCode == previousCard.CardCode)
        {
            OnMatchFound(card);

        }
        else
          OnWrongMatch();       

        OnTurnCompleted();
    }

    private void OnWrongMatch()
    {
        onResetCards?.Invoke();
        previousCard = null;
        turnCount++;
    }

    private void OnMatchFound(CardData card)
    {
        onMatchFound?.Invoke(card.CardCode);
        previousCard = null;
        ScoreCount++;
        turnCount++;
    }

    private void OnTurnCompleted()
    {

        if (ScoreCount == AppDataController.s_Instance.GameCompletionValue)
        {
            Debug.Log("Game Has been completed");
            turnCount = 0;
            ScoreCount = 0;
            onGameCompleted?.Invoke();
        }
        GameStats.UpdateScore(ScoreCount.ToString());
        GameStats.UpdateTurns(turnCount.ToString());
    }
}
