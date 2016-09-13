using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Button startButton;
    public GameObject winLevelUI;

    int currentSceneIndex;
    int numPersonsInCurScene;
    int numInfectedPersonsInCurScene;
    static GameManager instance;

    void Start() {
        currentSceneIndex = 0;
        numPersonsInCurScene = 0;
        numInfectedPersonsInCurScene = 0;
        instance = this;

        startButton.onClick.AddListener(() => LoadNextScene());
    }

    void WinLevel() {
        Instantiate(winLevelUI);
        numPersonsInCurScene = 0;
        numInfectedPersonsInCurScene = 0;
    }

    public static void LoadNextScene() {
        instance.currentSceneIndex++;
        SceneManager.LoadScene(instance.currentSceneIndex);
    }

    public static void AddPerson() {
        instance.numPersonsInCurScene++;
    }

    public static void AddInfectedPerson() {
        instance.numInfectedPersonsInCurScene++;
        if(instance.numInfectedPersonsInCurScene == instance.numPersonsInCurScene) {
            instance.WinLevel();
        }
    }

    public static void RemoveInfectedPerson() {
        instance.numInfectedPersonsInCurScene--;
    }
}
