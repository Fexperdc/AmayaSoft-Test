using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Create Level Data", order = 0)]
public class LevelData : ScriptableObject {

    [SerializeField]
    private List<CardBundle> _cardBundlesList;
    [SerializeField]
    private int _rowCount;
    [SerializeField]
    private int _columnCount;
    [SerializeField]

    public List<CardBundle> CardBundlesList => _cardBundlesList;

    public int RowCount => _rowCount;
    public int ColumnCount => _columnCount;

}