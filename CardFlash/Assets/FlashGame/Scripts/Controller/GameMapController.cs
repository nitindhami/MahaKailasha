using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMapController : MonoBehaviour
{
    [SerializeField] Root rootMap = new Root();
    [SerializeField] Button saveMapButton;
    [SerializeField] Button LoadGameButton;
    [SerializeField] Text ErrorText;
    public Root GetRoot { get { return rootMap; } }
    public int[] skipIndex;
    public List<int> skipList = new List<int>();
    public static GameMapController s_Instance;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        GameController.s_Instance.onGameCompleted += ClearGameMap;
        saveMapButton.onClick.AddListener(SaveGameMap);
        LoadGameButton.onClick.AddListener(LoadGameMap);
    }
    void ClearGameMap()
    {
        rootMap.cards.Clear();
    }

    void SaveGameMap()
    {
        rootMap.DisabledMap = disableIndex;
        var map = JsonUtility.ToJson(GetRoot).ToString();
        Debug.Log(map);
        PlayerPrefs.SetString(Constants.SAVED_GAME_MAP, map);
    }

    void LoadGameMap()
    {
        
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(Constants.SAVED_GAME_MAP)))
        {
            ErrorText.text = Constants.NO_SAVED_MAP;
            return;

        }
        var text = PlayerPrefs.GetString(Constants.SAVED_GAME_MAP);
        rootMap = JsonUtility.FromJson<Root>(text);

        if (!string.IsNullOrEmpty(rootMap.DisabledMap))
        {
           rootMap.DisabledMap = rootMap.DisabledMap.TrimEnd('|');
            Debug.Log(rootMap.DisabledMap);
            var data = rootMap.DisabledMap.Split('|');
            skipIndex = new int[data.Length];

            for (int i = 0; i < skipIndex.Length; i++)
            {
                int.TryParse(data[i], out skipIndex[i]);
            }
            skipList.AddRange(skipIndex);
        }
        GameController.s_Instance.LoadSavedGame();
    }
    string disableIndex;
    public void AddToDisabledCardCollection(int index)
    {
        disableIndex += index+ "|";
        Debug.Log(disableIndex);

    }

    public void UpdateScoreAndTurnCount(int turn,int score)
    {
        rootMap.TurnCount = turn;
        rootMap.ScoreCount = score;

    }
}

