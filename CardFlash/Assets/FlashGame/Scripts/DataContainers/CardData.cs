using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "CardData", order = 0)]
public class CardData : ScriptableObject
{
    public GameObject cardObj;
    public CardPrefab Card;
    public Sprite cardImage;
    public int CardCode;

}
