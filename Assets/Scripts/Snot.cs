using UnityEngine;
using System.Collections;

public class Snot : MonoBehaviour {

	public float speed;
	public Vector3 direction;

	// Use this for initialization
	void Start(){

	}

	// Update is called once per frame
	void Update() {
		transform.position += speed * direction;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Destroy(gameObject);
	}

}
