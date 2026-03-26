using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameBtn : MonoBehaviour
{
    private void Awake()
    {
        var btn = GetComponent<Button>();

        btn.onClick.AddListener(LoadNextScene);

    }

    private void OnDestroy()
    {
        var btn = GetComponent<Button>();

        btn.onClick.RemoveListener(LoadNextScene);
    }

    private static void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        SceneManager.LoadScene(nextIndex);
    }
}