using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuContorller : MonoBehaviour
{
    private UIDocument _document;
    private Button _playButton;
    private Button _exitButton;

    void Awake()
    {
        _document = GetComponent<UIDocument>();
        if (_document == null)
        {
            Debug.LogError("UIDocument component not found!");
            return;
        }

        var buttonBox = _document.rootVisualElement.Q<VisualElement>("ButtonBox");
        if (buttonBox != null)
        {
            _playButton = buttonBox.Q<Button>("PlayButton");
            _exitButton = buttonBox.Q<Button>("ExitButton");
        }
        else
        {
            Debug.LogError("ButtonBox not found! Check the UI hierarchy.");
            return;
        }

        if (_playButton != null)
        {
            _playButton.RegisterCallback<ClickEvent>(PlayButtonClicked);
            Debug.Log("PlayButton successfully found and event registered.");
        }
        else
        {
            Debug.LogError("PlayButton not found! Check the UI hierarchy and name.");
        }

        if (_exitButton != null)
        {
            _exitButton.RegisterCallback<ClickEvent>(ExitButtonClicked);
            Debug.Log("ExitButton successfully found and event registered.");
        }
        else
        {
            Debug.LogError("ExitButton not found! Check the UI hierarchy and name.");
        }
    }

    private void OnDisable()
    {
        _playButton.UnregisterCallback<ClickEvent>(PlayButtonClicked);
    }

    private void PlayButtonClicked(ClickEvent evt)
    {
        Debug.Log("Play button clicked");
        SceneManager.LoadScene("Level_Tutorial");
    }

    private void ExitButtonClicked(ClickEvent evt)
    {
        Application.Quit();
    }
}
