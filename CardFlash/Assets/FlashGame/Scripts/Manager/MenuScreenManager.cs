using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenManager : MonoBehaviour
{
    [SerializeField] GameObject _menuScreen;
    [SerializeField] GameObject _gameScreen;
    [SerializeField] Button playButton;
    #region Unity_CallBacks
    
    private void Start()
    {
        GameController.s_Instance.onGameCompleted += ShowMenuScreen;
        playButton.onClick.AddListener(StartGame);



    }
    #endregion

    void ShowMenuScreen()
    {
        _menuScreen.SetActive(true);
        LeanTween.scale(_menuScreen, Vector3.one, 0.2f).setEase(LeanTweenType.easeInQuad) ;

    }

    void StartGame()
    {
        _menuScreen.SetActive(false);
        _gameScreen.SetActive(true);
        _menuScreen.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        GameController.s_Instance.onStartGame?.Invoke();

    }
}
