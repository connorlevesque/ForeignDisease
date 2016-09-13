using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject winLevelUI;
    public Button nextLevelButton;
    public Button replayButton;
    public Button quitButton;

    int currentSceneIndex;
    int numInfectedPersonsInCurScene;
    List<Person> curPeopleInScene;

    static GameManager instance;

    void Awake() {
        winLevelUI.SetActive(false);
        currentSceneIndex = 0;
        curPeopleInScene = new List<Person>();
        numInfectedPersonsInCurScene = 0;
        instance = this;
    }

    void WinLevel() {
        winLevelUI.SetActive(true);
        nextLevelButton.onClick.AddListener(() => LoadNextScene());
        replayButton.onClick.AddListener(() => LoadSameScene());
        quitButton.onClick.AddListener(() => Application.Quit());
        foreach(Person person in curPeopleInScene) {
            person.StopMovingStayInfected();
        }
        numInfectedPersonsInCurScene = 0;
        curPeopleInScene = new List<Person>();
    }

    public static void LoadNextScene() {
        instance.currentSceneIndex++;
        SceneManager.LoadScene(instance.currentSceneIndex);
    }

    public static void LoadSameScene() {
        SceneManager.LoadScene(instance.currentSceneIndex);
    }

    public static void AddPerson(Person person) {
        instance.curPeopleInScene.Add(person);
    }

    public static void AddInfectedPerson() {
        instance.numInfectedPersonsInCurScene++;
        if (instance.numInfectedPersonsInCurScene == instance.curPeopleInScene.Count) {
            instance.WinLevel();
        }
    }

    public static void RemoveInfectedPerson() {
        instance.numInfectedPersonsInCurScene--;
    }

    public static List<Person> GetPeople() {
        return instance.curPeopleInScene;
    }
}
