using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppDataController : MonoBehaviour
{

    public AppData GameData { get { return _appData; } }
    public Dictionary<int, CardData> GameMap = new Dictionary<int, CardData>();
    public Action onGamMapCreated;
    public int GameCompletionValue { get{ return (_appData.ColCount * _appData.RowCount)/2; } } 
    public static AppDataController s_Instance;

    [SerializeField] AppData _appData; 

    #region Unity_CallBacks
    public void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else
            Destroy(gameObject);

    }
    private void Start()
    {
        GameController.s_Instance.onStartGame += StartGame;
        GameController.s_Instance.onGameCompleted += ResetAppData;
    }

    void ResetAppData()
    {
        map = string.Empty;
        GameMap.Clear();
        PlayerPrefs.DeleteAll();

    }
    void StartGame()
    {
        StartCoroutine(CreateFlashSequence());

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
                map+= randNext+":"+val+"|";
            }

        }
        Debug.Log(map);
        PlayerPrefs.SetString(Constants.SAVED_GAME_MAP,map);
        onGamMapCreated?.Invoke();
    }
    string map;
}
