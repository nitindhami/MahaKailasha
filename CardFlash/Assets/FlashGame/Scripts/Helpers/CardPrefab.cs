using System;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    public Action<CardData> onCardClicked;
    CardData data;
    [SerializeField] Image _cardImage;
    [SerializeField] GameObject hideCardObj;
    [SerializeField] Button button;

    #region Unity_CallBacks
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    #endregion

    public void Init(CardData cardData,Action<CardData> cardClicked)
    {
        hideCardObj.SetActive(false);
        data = cardData;
        cardData.cardObj = gameObject;
        _cardImage.sprite = cardData.cardImage;
        gameObject.name = cardData.name;
        onCardClicked = cardClicked;
        button.onClick.AddListener(OnCardClicked);
    }

    void OnCardClicked()
    {
        onCardClicked?.Invoke(data);
        RotateCard();
    }

    void RotateCard()
    {
        LeanTween.rotate(_cardImage.gameObject,new Vector3(0f,90f,0f),0.25f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(OnRotationCompleted);

    }

    void OnRotationCompleted()
    {
        _cardImage.gameObject.SetActive(false);
        hideCardObj.SetActive(true);

    }
}
