using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour {
    
	void Start() {
        Button start = GetComponent<Button>();
        start.onClick.AddListener(() => {
            Debug.Log("hello?");
            GameManager.LoadNextScene();
        });
    }
}
