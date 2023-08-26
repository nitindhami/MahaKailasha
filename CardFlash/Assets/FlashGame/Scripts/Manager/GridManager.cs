using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] GameObject _rowPrefab;
    [SerializeField] CardPrefab _cardPrefab;
    [SerializeField] Transform _rowParent;
    [SerializeField] List<CardPrefab> cardPrefabs = new List<CardPrefab>();

    AppData data;
    private void Start()
    {
        data = AppDataController.s_Instance.GameData;
        AppDataController.s_Instance.onGamMapCreated += CreateGrid;
        GameController.s_Instance.onGameCompleted += GameCompleted;
        GameController.s_Instance.onLoadSavedGame += CreateGridFromSavedMap;

    }

    public void CreateGrid()
    {
        CreateGridPlaceHolders();
        InitCardsWithValues();
    }

    private void CreateGridPlaceHolders()
    {
        for (int i = 0; i < data.RowCount; i++)
        {
            var RowTrans = Instantiate(_rowPrefab, _rowParent).transform;
            for (int j = 0; j < data.ColCount; j++)
            {
                var cardObj = Instantiate(_cardPrefab, RowTrans);
                cardPrefabs.Add(cardObj);

            }

        }
    }

    void InitCardsWithValues()
    {

        for (int i = 0; i < data.ColCount* data.RowCount ; i++)
        {
            CardInfo card = new CardInfo();
            card.State = 0;

            if (AppDataController.s_Instance.GameMap.ContainsKey(i))
            {
                card.IndexFromFlashCollection =
                    AppDataController.s_Instance.GameData.flashCards.
                    IndexOf(AppDataController.s_Instance.GameMap[i]);
                GameMapController.s_Instance.GetRoot.cards.Add(card);

                cardPrefabs[i].Init(AppDataController.s_Instance.GameMap[i],
                    GameController.s_Instance.FlashCardClicked,i);
            }
        }

        Invoke("HideCards",Constants.HIDE_CARDS_DURATION);
    }

    void HideCards() {

        GameController.s_Instance.HideCards();
    }
    void GameCompleted()
    {
        cardPrefabs.Clear();
    }

    void CreateGridFromSavedMap()
    {
       var Cards = GameMapController.s_Instance.GetRoot.cards;
        for (int i = 0; i < Cards.Count; i++)
        {

        }

    }

}
