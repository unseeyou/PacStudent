using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private int _homeSceneIndex = 0;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnClick);
        }
    }

    // Update is called once per frame
    void OnClick()
    {
        SceneManager.LoadSceneAsync(_homeSceneIndex);
    }

    private void OnDestroy()
    {
        if (exitButton != null)
        {
            exitButton.onClick.RemoveListener(OnClick);
        }
    }
}
