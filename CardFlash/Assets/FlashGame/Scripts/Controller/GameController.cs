using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController s_Instance;
    [SerializeField] AudioSource _dingSource;
    int turnCount=0;
    int ScoreCount=0;
    CardData previousCard = null;
    CardData currentCard = null;
    public Action onHideAllCards;
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
    }
    #endregion
    public void HideCards()
    {
        onHideAllCards?.Invoke();
    }
    public void FlashCardClicked(CardData card)
    {

        _dingSource.Play();
        Debug.Log(card.cardObj.name);
        if (previousCard == null && currentCard == null)
        {
            previousCard = card;
            return;
        }

        if (card.CardCode == previousCard.CardCode)
        {
            //disable Cards.
            //increment Score.
            ScoreCount++;
            turnCount++;
        }
        else
        {
            // Hide Card Content again
            previousCard = currentCard = null;
            turnCount++;
        }
    }
}
