using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource cardFlipSource;
    [SerializeField] AudioSource _dingSource;
    [SerializeField] AudioSource _wrongSource;
    #region UnityCallBacks

    private void Start()
    {
        GameController.s_Instance.onCardClicked += PlayCardFlipSound;
        GameController.s_Instance.onMatchFound += OnMatchFound;
        GameController.s_Instance.onResetCards += OnNoMatchFound;
    }
    #endregion

    #region CardSounds

    void PlayCardFlipSound()
    {
        cardFlipSource.Play();

    }

    void OnMatchFound(int val)
    {
        _dingSource.Play();

    }

    void OnNoMatchFound()
    {

        _wrongSource.Play();

    }

    #endregion
}
