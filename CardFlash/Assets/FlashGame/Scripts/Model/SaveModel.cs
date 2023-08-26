using System;
using System.Collections.Generic;

[Serializable]
public class Root
{
    public List<CardInfo> cards = new List<CardInfo>();
    public string DisabledMap;
    public int ScoreCount;
    public int TurnCount;
}
[Serializable]
public class CardInfo
{
    public int State;
    public int IndexFromFlashCollection;
 
}