using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppDataController : MonoBehaviour
{
    [SerializeField] AppData _appData;

    public AppData GameData { get { return _appData; } }
    public Dictionary<int, CardData> GameMap = new Dictionary<int, CardData>();
    public Action onGamMapCreated;
    public static AppDataController s_Instance;

    #region Unity_CallBacks
    public void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else
            Destroy(gameObject);
       StartCoroutine( CreateFlashSequence());
    }

    private void OnDestroy()
    {
        onGamMapCreated = null;
    }

    #endregion


    public IEnumerator CreateFlashSequence()
    {

        int totalCount = _appData.ColCount * _appData.RowCount;
        Debug.Log(totalCount);
        for (int i = 0; i < totalCount; i++)
        {
            if (!GameMap.ContainsKey(i))
            {
                var val = UnityEngine.Random.Range(0,_appData.flashCards.Count);
                GameMap.Add(i, _appData.flashCards[val]);
                var randNext = UnityEngine.Random.Range(i+1,totalCount);

                while (GameMap.ContainsKey(randNext))
                {
                    randNext = UnityEngine.Random.Range(i + 1, totalCount);
                    yield return null;
                }
                GameMap.Add(randNext, _appData.flashCards[val]);
            }

        }

        foreach (var item in GameMap)
        {
            Debug.Log(item.Key+" : Value"+item.Value.CardCode);
        }
        onGamMapCreated?.Invoke();
    }
}
