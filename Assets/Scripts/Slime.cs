using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slime : MonoBehaviour {

	public float speed;
	public GameManager gameManager;

	private bool sneezeReady = true;
	public float sneezeDelay;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		Vector3 inputV = new Vector3(inputX, inputY, 0);
		if (inputV != new Vector3 (0, 0, 0))
		{
			//float angle = Vector3.Angle(Vector3.up, inputV.normalized);
			//if (inputX > 0) angle *= -1;
			//transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.position += speed * inputV;
		}

		if (Input.GetKeyDown("space") && sneezeReady) {
			List<Person> people = GameManager.GetPeople();
			for (int i = 0; i < people.Count; i++)
			{
				people[i].Sneeze();
			}
		}
		StartCoroutine(SneezeCooldown());
	}

	IEnumerator SneezeCooldown() {
		sneezeReady = false;
		yield return new WaitForSeconds(sneezeDelay);
		sneezeReady = true;
	}
}
