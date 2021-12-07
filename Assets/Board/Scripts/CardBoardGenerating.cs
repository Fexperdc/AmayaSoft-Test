using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardGenerating : MonoBehaviour {

    [SerializeField]
    private CardBoard _cardBoard;

    private List<CardBundle> _cardBundlesList = new List<CardBundle>();
    private int _rowCount;
    private int _columnCount;
    private CardBundle _currentCardBundle;
    private List<string> _pastTaskIDs = new List<string>();
    
    public void Generate() {
        _cardBoard.ClearBoard();
        _cardBoard.rowCount = _rowCount;
        _cardBoard.columnCount = _columnCount;
        _cardBoard.GenerateCards();
        _cardBoard.transform.position = Vector3.zero;

        _currentCardBundle = _cardBundlesList[Random.Range(0, _cardBundlesList.Count)];
        UpdateCards();
    }

    public void UpdateCards() {
        foreach(Card card in _cardBoard.Cards) {
            CardBundle.CardData cardData = _currentCardBundle.GetRandomCard();
            while(true) {
                cardData = _currentCardBundle.GetRandomCard();
                if(_cardBoard.HasCardWithID(cardData.ID) == false) {
                    break;
                }
            }
            card.CardData = cardData;
            card.UpdateCard(_currentCardBundle);
        }
    }

    public void SetBoardSize(int columnCount, int rowCount) {
        _columnCount = columnCount;
        _rowCount = rowCount;
    }

    public void SetCardBundlesList(List<CardBundle> cardBundlesList) {
        _cardBundlesList = cardBundlesList;
    }

    public void BlockID(string id) {
        _pastTaskIDs.Add(id);
    }

    public CardBoard CardBoard => _cardBoard;
    public List<CardBundle> CardBundlesList => _cardBundlesList;
    public CardBundle CurrentCardBundle => _currentCardBundle;
}
