using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AppData", menuName = "Data", order = 0)]
public class AppData : ScriptableObject
{
    [SerializeField] int _row;
    [SerializeField] int _col;

    public int RowCount { get { return _row; } }
    public int ColCount { get { return _row; } }

    public List<CardData> flashCards = new List<CardData>();
}

