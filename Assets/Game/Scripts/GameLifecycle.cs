using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameLifecycle : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _taskLabel;
    [SerializeField]
    private CanvasGroup _retryScreen;

    [HideInInspector]
    public UnityEventWrapper OnGameReloaded = new UnityEventWrapper();

    private bool _paused;
    public bool Paused {
        set {
            _paused = value;
        }
        get {
            return _paused;
        }
    }

    public void ShowRetryScreen() {
        _retryScreen.interactable = true;
        _retryScreen.alpha = 0;
        _retryScreen.DOFade(1, 0.5f);
    }

    public void HideRetryScreen() {
        _retryScreen.interactable = false;
        _retryScreen.alpha = 1;
        _retryScreen.DOFade(0, 0.5f);
    }

    public void ReloadGame() {
        HideRetryScreen();
        OnGameReloaded.Invoke();
    }

    public void SetTask(string id) {
        _taskLabel.text = "Find " + id.ToUpper();
    }

}