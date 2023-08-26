using System;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    public Action<CardData> onCardClicked;
    [SerializeField] Image _cardImage;
    [SerializeField] GameObject hideCardObj;
    [SerializeField] Button _closeCardButton;
    [SerializeField] Button _openCardButton;
    int myCardIndex;
    bool? isOpen = false;
    CardData data;
    int myCardCode;

    #region Unity_CallBacks
    private void Start()
    {
        GameController.s_Instance.onHideAllCards += HideCard;
        GameController.s_Instance.onMatchFound += OnMatchFound;
        GameController.s_Instance.onResetCards += ResetCards;
        GameController.s_Instance.onGameCompleted += Destruction;
    }
    private void OnDestroy()
    {
        GameController.s_Instance.onHideAllCards -= HideCard;
        GameController.s_Instance.onMatchFound -= OnMatchFound;
        GameController.s_Instance.onResetCards -= ResetCards;
        GameController.s_Instance.onGameCompleted -= Destruction;
        _openCardButton.onClick.RemoveAllListeners();
    }

    #endregion

    public void Init(CardData cardData,Action<CardData> cardClicked,int myInd)
    {
        hideCardObj.SetActive(false);
        data = cardData;
        myCardCode = data.CardCode;
        _cardImage.sprite = cardData.cardImage;
        _cardImage.preserveAspect = true;
        myCardIndex = myInd;
        gameObject.name = cardData.name;
        onCardClicked = cardClicked;
        _openCardButton.onClick.AddListener(OnCardClicked);
    }
    // This can be further optimized
    // But for the time being lets use this.
    void Destruction()
    {
        Destroy(gameObject);

    }
    void OnCardClicked()
    {
        isOpen = true;
        onCardClicked?.Invoke(data);
        ShowCard();
    }

    void ShowCard()
    {
        LeanTween.rotate(hideCardObj.gameObject, new Vector3(0f, 90f, 0f), 0.15f)
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
        LeanTween.rotate(_cardImage.gameObject, new Vector3(0f, 90f, 0f), 0.15f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(OnHidingCompleted);
        isOpen = false;
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
            Debug.Log("Disabled Card : " + myCardIndex);
            GameMapController.s_Instance.AddToDisabledCardCollection(myCardIndex);
        }

    }
    public void DisableCard()
    {
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
