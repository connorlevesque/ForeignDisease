using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject winLevelUI;
    public Button nextLevelButton;
    public Button replayButton;
    public Button quitButton;
    
    int numInfectedPersonsInCurScene;
    List<Person> curPeopleInScene;

    static GameManager instance;

    void Awake() {
        winLevelUI.SetActive(false);
        curPeopleInScene = new List<Person>();
        numInfectedPersonsInCurScene = 0;
        instance = this;
    }

    void WinLevel() {
        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCount) {
            LoadNextScene();
        }
        else {
            winLevelUI.SetActive(true);
            nextLevelButton.onClick.AddListener(() => LoadNextScene());
            replayButton.onClick.AddListener(() => LoadSameScene());
            quitButton.onClick.AddListener(() => Application.Quit());
            foreach (Person person in curPeopleInScene) {
                person.StopMovingStayInfected();
            }
            numInfectedPersonsInCurScene = 0;
            curPeopleInScene = new List<Person>();
        }
    }

    public static void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadSameScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
