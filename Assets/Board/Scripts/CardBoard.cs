using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardBoard : MonoBehaviour {

    public Card.CardClickEvent OnCardClicked = new Card.CardClickEvent();
    private List<Card> _cards = new List<Card>();

    [SerializeField]
    public int columnCount;
    [SerializeField]
    public int rowCount;
    [SerializeField]
    private GameObject _cardPrefab;

    [SerializeField]
    private Vector2 cellSize = new Vector2(3, 3);

    public void GenerateCards() {
        for(int i = 0; i < columnCount * rowCount; i++) {
            var instance = Instantiate(_cardPrefab);
            cellSize = instance.GetComponent<Card>().GetSize();
            AddCard(instance.GetComponent<Card>());
        }
        UpdateBoard();
    }

    public void AddCard(Card card) {
        _cards.Add(card);
        card.OnCardClicked.AddListener(CardClicked);
    }


    public void UpdateBoard() {
        for(int i = 0; i < columnCount * rowCount; i++) {
            if(i < _cards.Count) {
                cellSize = _cards[i].GetSize();
                _cards[i].transform.position = new Vector3(transform.position.x + (cellSize.x * (i % columnCount)), transform.position.y + (cellSize.y * (i / columnCount)));
            }
        }
        BoardCentering();
    }

    public Bounds GetBoardBounds() {
        Bounds result = new Bounds();
        foreach(Card card in _cards) {
            result.Encapsulate(card.transform.position);
        }
        return result;
    }

    public bool HasCardWithID(string id) {
        foreach(Card card in _cards) {
            if(card.CardData == null) {
                continue;
            }
            if(card.CardData.ID == id) {
                return true;
            }
        }

        return false;
    }

    public void BoardCentering() {
        transform.position = transform.position + (GetBoardBounds().size / 2);
        foreach(Card card in _cards) {
            card.transform.SetParent(transform);
        }
    }

    public void ClearBoard() {
        foreach(Card card in _cards) {
            Destroy(card.gameObject);
        }
        _cards.Clear();
    }

    private void CardClicked(Card cardComponent) {
        OnCardClicked.Invoke(cardComponent);
    }

    public List<Card> Cards => _cards;

}
