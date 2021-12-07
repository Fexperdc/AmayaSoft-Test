using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameRules : MonoBehaviour {

    [SerializeField]
    private CardBoardGenerating _cardBoardGenerating;
    [SerializeField]
    private GameLifecycle _gameLifecycle;
    [SerializeField]
    private List<LevelData> _levelDatasList;

    [Header("FX")]
    [SerializeField]
    private FXPresets _fxPresets;

    private int _currentLevelDataIndex = 0;
    private string _taskID = "";

    private void Start() {
        _gameLifecycle.OnGameReloaded.AddListener(GameReloaded);
        StartGame(true);
    }

    public void StartGame(bool firstLaunch = false) {
        _cardBoardGenerating.CardBoard.OnCardClicked.AddListener(CardClicked);

        LoadCurrentLevel();
        _cardBoardGenerating.Generate();
        GiveRandomTaskID();
        if(firstLaunch == true) {
            foreach(Card card in _cardBoardGenerating.CardBoard.Cards) {
                card.transform.localScale = Vector3.zero;
                card.transform.DOScale(new Vector2(0.4f, 0.4f), 1).SetEase(Ease.OutBounce);
            }
        }
    }

    public void GiveRandomTaskID() {
        _taskID = _cardBoardGenerating.CardBoard.Cards[Random.Range(0, _cardBoardGenerating.CardBoard.Cards.Count)].CardData.ID;
        _gameLifecycle.SetTask(_taskID);
    }

    public void LoadCurrentLevel() {
        _cardBoardGenerating.SetCardBundlesList(CurrentLevelData.CardBundlesList);
        _cardBoardGenerating.SetBoardSize(CurrentLevelData.ColumnCount, CurrentLevelData.RowCount);
    }

    public void NextLevel() {
        if(_currentLevelDataIndex + 1 < _levelDatasList.Count) {
            _currentLevelDataIndex += 1;
            LoadCurrentLevel();
            StartGame();
        } else {
            _cardBoardGenerating.BlockID(_taskID);
            _cardBoardGenerating.CardBoard.ClearBoard();
            _gameLifecycle.ShowRetryScreen();
        }
    }

    private void CardClicked(Card cardComponent) {
        if(_gameLifecycle.Paused == false) {
            if(cardComponent.CardData.ID == _taskID) {
                NextLevel();
                _fxPresets.StarExplode(cardComponent.transform.position);
            } else {
                cardComponent.transform.DOKill(true);
                cardComponent.transform.DOShakePosition(0.2f, 0.3f, 20, fadeOut: false).From();
            }
        }
    }

    private void GameReloaded() {
        _currentLevelDataIndex = 0;
        StartGame(true);
    }

    public LevelData CurrentLevelData => _levelDatasList[_currentLevelDataIndex];

}
