using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController s_Instance;
    [SerializeField] AudioSource _dingSource;
    [SerializeField] AudioSource _wrongSource;
    [SerializeField] GameStatsManager GameStats;
    int turnCount=0;
    int ScoreCount=0;
    CardData previousCard = null;
    public Action onHideAllCards;
    public Action<int> onMatchFound;
    public Action onResetCards;
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
    }
    #endregion
    public void HideCards()
    {
        onHideAllCards?.Invoke();
    }
    public void FlashCardClicked(CardData card)
    {

         Debug.Log(card.cardObj.name);
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
            
            _dingSource.Play();
            Debug.Log("Match Found:");
            onMatchFound?.Invoke(card.CardCode);
            previousCard = null;
            ScoreCount++;
            turnCount++;

        }
        else
        {
            _wrongSource.Play();
            Debug.Log("Not Found:");
            onResetCards?.Invoke();
            previousCard = null;
            turnCount++;
        }
        GameStats.UpdateScore(ScoreCount.ToString());
        GameStats.UpdateTurns(turnCount.ToString());

    }
}
