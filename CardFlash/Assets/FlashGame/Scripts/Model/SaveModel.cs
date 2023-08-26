using System;
using System.Collections.Generic;

[Serializable]
public class Root
{
    public List<CardInfo> cards = new List<CardInfo>();

}
[Serializable]
public class CardInfo
{
    public int State;
    public int IndexFromFlashCollection;

}