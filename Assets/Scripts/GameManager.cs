using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject winLevelUI;

    int currentSceneIndex;
    int numInfectedPersonsInCurScene;
    List<Person> curPeopleInScene;

    static GameManager instance;

    void Awake() {
        currentSceneIndex = 0;
        curPeopleInScene = new List<Person>();
        numInfectedPersonsInCurScene = 0;
        instance = this;
    }

    void WinLevel() {
        Instantiate(winLevelUI);
        numInfectedPersonsInCurScene = 0;
        curPeopleInScene = new List<Person>();
    }

    public static void LoadNextScene() {
        instance.currentSceneIndex++;
        SceneManager.LoadScene(instance.currentSceneIndex);
    }

    public static void AddPerson(Person person) {
        //Debug.LogFormat("instance = {0}, curPeople = {1}, person = {2}", instance, instance.curPeopleInScene, person);
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
