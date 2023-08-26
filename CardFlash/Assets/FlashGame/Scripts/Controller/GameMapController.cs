using UnityEngine;
using UnityEngine.UI;

public class GameMapController : MonoBehaviour
{
    [SerializeField] Root rootMap = new Root();
    [SerializeField] Button saveMapButton;
    [SerializeField] Button LoadGameButton;
    [SerializeField] Text ErrorText;
    public Root GetRoot { get { return rootMap; } }

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
        GameController.s_Instance.onMatchFound += OnModifyMatchData;
        GameController.s_Instance.onGameCompleted += ClearGameMap;
        saveMapButton.onClick.AddListener(SaveGameMap);
        LoadGameButton.onClick.AddListener(LoadGameMap);
    }

    void OnModifyMatchData(int value)
    {

    }

    void ClearGameMap()
    {

        rootMap.cards.Clear();
    }

    void SaveGameMap()
    {
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
        GameController.s_Instance.LoadSavedGame();
    }

}

