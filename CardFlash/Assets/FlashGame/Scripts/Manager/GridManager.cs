using System.Collections;
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
       
    }

    public void CreateGrid()
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
        InitCardsWithValues();
    }
    void InitCardsWithValues()
    {

        for (int i = 0; i < data.ColCount* data.RowCount ; i++)
        {
            if(AppDataController.s_Instance.GameMap.ContainsKey(i))
            cardPrefabs[i].Init(AppDataController.s_Instance.GameMap[i]);
        }

    }

}
