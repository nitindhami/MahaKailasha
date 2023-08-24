using System;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    public Action onCardClicked;
    CardData data;
    [SerializeField] Image _cardImage;
    [SerializeField] Button button;

    #region Unity_CallBacks
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    #endregion

    public void Init(CardData cardData)
    {
         data = cardData;
        _cardImage.sprite = cardData.cardImage;
        button.onClick.AddListener(OnCardClicked);
    }

    void OnCardClicked()
    {
        Debug.Log("OnCardClicked ##");
        onCardClicked?.Invoke();
    }

}
