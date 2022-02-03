using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreOverlay : OverlayUI
{
    public delegate void CallRetry();
    public event CallRetry OnRetry, OnQuit;

    [SerializeField] private Image _medalIcon;
    [SerializeField] private Sprite[] _medals;
    [SerializeField] private GameObject _newTag;
    [SerializeField] private TextMeshProUGUI _scoreText, _bestText;

    public void OnMedalsReturn(Scores scores, bool newScore, int medalIndex, bool record)
    {
        _scoreText.text = $"{scores.lastScore}";
        _bestText.text = $"{scores.gold}";
        _medalIcon.sprite = _medals[medalIndex];
        _medalIcon.enabled = newScore;
        _newTag.SetActive(record);
    }
    public void BtnRestart()
    {
        hudController.InvokePressButton();
        OnRetry?.Invoke();
    }
    public void BtnQuit()
    {
        hudController.InvokePressButton();
        OnQuit?.Invoke();
    }
}
