using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image _roadPlayerGet;

    [SerializeField] private GameObject _idleUI;
    [SerializeField] private GameObject _levelUI;
    [SerializeField] private Text _finishFeedback;
    [SerializeField] private Button _finishButton;
    [SerializeField] private Button _restartButton;

    private string[] _goodFeedbacks = { "Great", "Perfect" };
    private string[] _badFeedbacks = { "Not Bad", "Try Again" };

    public void RoadPlayerGet(float filledValue)
    {
        _roadPlayerGet.fillAmount = filledValue;
    }

    public void CloseIdleUI()
    {
        _idleUI.SetActive(false);
    }

    public void OpenLevelUI()
    {
        _levelUI.SetActive(true);
    }

    public void CloseLevelUI()
    {
        _levelUI.SetActive(false);
    }

    public void OpenGoodFinishFeedback()
    {
        _finishFeedback.text = _goodFeedbacks[Random.Range(0, _goodFeedbacks.Length)];
        OpenFinishFeedback();
    }

    public void OpenBadFinishFeedback()
    {
        _finishFeedback.text = _badFeedbacks[Random.Range(0, _goodFeedbacks.Length)];
        OpenFinishFeedback();
    }

    private void OpenFinishFeedback()
    {
        _finishFeedback.gameObject.SetActive(true);
    }

    public void OpenFinishButton()
    {
        _finishButton.gameObject.SetActive(true);
    }

    public void CloseFinishButton()
    {
        _finishButton.gameObject.SetActive(false);
    }

    public void OpenRestartButton()
    {
        _restartButton.gameObject.SetActive(true);
    }
}
