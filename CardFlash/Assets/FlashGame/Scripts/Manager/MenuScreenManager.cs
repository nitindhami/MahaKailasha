using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenManager : MonoBehaviour
{
    [SerializeField] GameObject _menuScreen;

    #region Unity_CallBacks
    
    private void Start()
    {
        GameController.s_Instance.onGameCompleted += ShowMenuScreen;
        _menuScreen.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

    }
    #endregion

    void ShowMenuScreen()
    {
        _menuScreen.SetActive(true);
        LeanTween.scale(_menuScreen, Vector3.one, 0.2f).setEase(LeanTweenType.easeInQuad) ;

    }
}
