using UnityEngine;
using UnityEngine.UI;

public class MenuScreenManager : MonoBehaviour
{
    [SerializeField] GameObject _menuScreen;
    [SerializeField] GameObject _gameScreen;
    [SerializeField] Button playButton;
    [SerializeField] Button _homeButton;

    #region Unity_CallBacks
    
    private void Start()
    {
        GameController.s_Instance.onGameCompleted += ShowMenuScreen;
        GameController.s_Instance.onLoadSavedGame += StartLoadGame;
        playButton.onClick.AddListener(StartGame);
        _homeButton.onClick.AddListener(GoToMenuScreen);

    }
    #endregion

    void ShowMenuScreen()
    {
        _menuScreen.SetActive(true);
        _gameScreen.SetActive(false);
        LeanTween.scale(_menuScreen, Vector3.one, 0.2f).setEase(LeanTweenType.easeInQuad) ;

    }

    void StartGame()
    {
        StartLoadGame();
        GameController.s_Instance.onStartGame?.Invoke();

    }
    void StartLoadGame()
    {
        _menuScreen.SetActive(false);
        _gameScreen.SetActive(true);
        _menuScreen.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

    }
    void GoToMenuScreen()
    {
        GameController.s_Instance.CompleteGame();
        ShowMenuScreen();

    }
}
