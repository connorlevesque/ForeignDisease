using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Button startButton;

    int currentSceneIndex;
    static GameManager instance;

    void Start() {
        currentSceneIndex = 0;
        instance = this;

        startButton.onClick.AddListener(() => LoadNextScene());
    }

    public static void LoadNextScene() {
        instance.currentSceneIndex++;
        SceneManager.LoadScene(instance.currentSceneIndex);
    }
}
