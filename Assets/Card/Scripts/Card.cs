using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Card : MonoBehaviour {

    [HideInInspector]
    public CardClickEvent OnCardClicked = new CardClickEvent();

    [SerializeField]
    private GameObject _background;

    public CardBundle.CardData CardData { get; set; }

    public void UpdateCard(CardBundle cardBundle) {
        if(cardBundle != null) {
            GetComponent<SpriteRenderer>().sprite = CardData.Sprite;
            transform.rotation = Quaternion.Euler(Vector3.forward * CardData.Rotation);
        }
    }

    public Vector2 GetSize() {
        return Background.GetComponent<SpriteRenderer>().bounds.size;
    }

    private void OnMouseOver() {
        if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) {
            OnCardClicked.Invoke(this);
        }
    }
    public GameObject Background => _background;

    public class CardClickEvent : UnityEvent<Card> {}

}