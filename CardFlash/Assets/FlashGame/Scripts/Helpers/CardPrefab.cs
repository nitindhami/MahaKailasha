using System;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    public Action<CardData> onCardClicked;
    CardData data;
    [SerializeField] Image _cardImage;
    [SerializeField] GameObject hideCardObj;
    [SerializeField] Button _closeCardButton;
    [SerializeField] Button _openCardButton;

    #region Unity_CallBacks
    private void Start()
    {
        GameController.s_Instance.onHideAllCards += HideCard;
    }
    private void OnDestroy()
    {
        _openCardButton.onClick.RemoveAllListeners();
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
        _openCardButton.onClick.AddListener(OnCardClicked);
    }

    void OnCardClicked()
    {
        onCardClicked?.Invoke(data);
        ShowCard();
    }

    void ShowCard()
    {
        LeanTween.rotate(hideCardObj.gameObject, new Vector3(0f, 90f, 0f), 0.25f)
    .setEase(LeanTweenType.easeOutCubic)
    .setOnComplete(OnShowAnimationCompleted);

    }

    void OnShowAnimationCompleted()
    {

        _cardImage.gameObject.SetActive(true);
        hideCardObj.gameObject.SetActive(false);
        hideCardObj.transform.rotation = Quaternion.Euler(0, 0, 0);
        
    }
    void HideCard()
    {
        LeanTween.rotate(_cardImage.gameObject, new Vector3(0f, 90f, 0f), 0.25f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(OnHidingCompleted);

    }
    void OnHidingCompleted()
    {
        _cardImage.gameObject.SetActive(false);
        _cardImage.transform.rotation = Quaternion.Euler(0,0,0);
        hideCardObj.SetActive(true);

    }
}
