using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {

	public float speed;
	private Vector3 direction = Vector3.zero;

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {

		if (Input.GetKey("up")) {
			direction = Vector3.up;
		} else if (Input.GetKey("left")) {
			direction = Vector3.left;
		} else if (Input.GetKey("right")) {
			direction = Vector3.right;
		} else if (Input.GetKey("down")) {
			direction = Vector3.down;
		} else {
			direction = Vector3.zero;
		}

		transform.position += speed * direction;
	}
}
