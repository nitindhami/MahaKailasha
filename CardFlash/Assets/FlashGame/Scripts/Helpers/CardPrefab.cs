using System;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    public Action<CardData> onCardClicked;
    CardData data;
    int myCardCode;
    bool? isOpen = false;
    [SerializeField] Image _cardImage;
    [SerializeField] GameObject hideCardObj;
    [SerializeField] Button _closeCardButton;
    [SerializeField] Button _openCardButton;

    #region Unity_CallBacks
    private void Start()
    {
        GameController.s_Instance.onHideAllCards += HideCard;
        GameController.s_Instance.onMatchFound += OnMatchFound;
        GameController.s_Instance.onResetCards += ResetCards;
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
        myCardCode = data.CardCode;
        data.Card = this;
        cardData.cardObj = gameObject;
        _cardImage.sprite = cardData.cardImage;
        _cardImage.preserveAspect = true;
        gameObject.name = cardData.name;
        onCardClicked = cardClicked;
        _openCardButton.onClick.AddListener(OnCardClicked);
    }

    void OnCardClicked()
    {
        isOpen = true;
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
   public void HideCard()
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
    void OnMatchFound(int cardCode)
    {

        if (cardCode == myCardCode && (isOpen == true))
        {
            DisableCard();
        }

    }
    public void DisableCard()
    {
        Debug.Log("DisableCard");
        _cardImage.gameObject.SetActive(false);
        hideCardObj.SetActive(false);
        isOpen = null;

    }

    void ResetCards()
    {
        if (isOpen == null)
            return;
        HideCard();
    }
}
