using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelOneButton : MonoBehaviour
{
    [SerializeField] private Button levelOneButton;
    public int levelSceneIndex;

    void Start()
    {
        if (levelOneButton != null)
        {
            levelOneButton.onClick.AddListener(OnButtonClick);
        }
    }
    
    void OnButtonClick()
    {
        SceneManager.LoadSceneAsync(levelSceneIndex);
    }

    void OnDestroy()
    {
        if (levelOneButton != null)
        {
            levelOneButton.onClick.RemoveListener(OnButtonClick);
        }
    }
}
