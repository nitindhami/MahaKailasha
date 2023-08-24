using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardData", menuName = "CardData", order = 0)]
public class CardData : ScriptableObject
{
    public Sprite cardImage;
    public int CardCode;

}
