using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardBundle", menuName = "Create Card Bundle", order = 0)]
public class CardBundle : ScriptableObject {

    [SerializeField]
    private List<CardData> _cardDataList = new List<CardData>();

    public List<CardData> CardDataList => _cardDataList;

    public CardData GetCardDataByID(string id) {
        foreach(CardData cardData in CardDataList) {
            if(cardData.ID == id) {
                return cardData;
            }
        }
        return null;
    }

    public CardData GetRandomCard() {
        return CardDataList[UnityEngine.Random.Range(0, _cardDataList.Count)];
    }

    [Serializable]
    public class CardData {

        [SerializeField]
        private string _id;

        [SerializeField]
        private Sprite _sprite;

        [SerializeField]
        private float rotation;

        public string ID => _id.ToLower();

        public Sprite Sprite => _sprite;

        public float Rotation => rotation;

    }

}
