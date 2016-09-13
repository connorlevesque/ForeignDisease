using UnityEngine;
using System.Collections;


public class Person : MonoBehaviour {

	public Vector3 direction;
	// the inputted string that contains the desired path
	public string[] path;
	// indicates how the path should be repeated
	public bool reverse;
	// how fast the person should move
	public float speed;
	// how long the person stays infected
	public float time;
	// the color of the infected person
	public Color infectedColor;

	// conatins the parsed path
	string[,] movementMap;

	bool infected = false;
	int numTimesInfected;
	SpriteRenderer spriteRenderer;
	Color originalColor;

	public Snot snotPrefab;

	// directional sprites
	public Sprite spriteDown;
	public Sprite spriteLeft;
	public Sprite spriteRight;
	public Sprite spriteUp;

    // Called by Unity when the scene is loading
    void Start() {
        numTimesInfected = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        // sets the alpha for the public color value
        infectedColor.a = 255;
        // parses the path
        movementMap = new string[2, path.Length];
        for (int i = 0; i < path.Length; i++) {
            string[] parsedString = path[i].Split(' ');
            movementMap[0, i] = parsedString[0];
            movementMap[1, i] = parsedString[1];
        }
        GameManager.AddPerson(this);
        // starts to move the person
        StartCoroutine(MovePerson());
    }


    // Moves the person
    IEnumerator MovePerson() {
        // indexes the path
        int index = 0;
        // checks when the person has moved the alloted distance
        float gridCount = 0;
        // tracks whether or not currently reversing the path
        bool inReverse = false;

        // "animation" while loop
        while (true) {
            if (gridCount == 0) {
                // figrues out how many grid squares to traverse
                float.TryParse(movementMap[1, index], out gridCount);
                // figures out the direction to move in
                switch (movementMap[0, index]) {
                    case "up":
                        if (inReverse) SetDirection(Vector3.down);
                        else SetDirection(Vector3.up);
                        break;
                    case "down":
                        if (inReverse) SetDirection(Vector3.up);
                        else SetDirection(Vector3.down);
                        break;
                    case "right":
                        if (inReverse) SetDirection(Vector3.left);
                        else SetDirection(Vector3.right);
                        break;
                    case "left":
                        if (inReverse) SetDirection(Vector3.right);
                        else SetDirection(Vector3.left);
                        break;
                    default:
                        Debug.Log("Incorrectly inputted person movement");
                        break;
                }

                // adjusts the index based
                if (!inReverse) {
                    index++;
                }
                else {
                    if (index == 0) {
                        inReverse = false;
                    }
                    else {
                        index--;
                    }
                }
                if (index == path.Length) {
                    if (reverse) {
                        inReverse = true;
                        index--;
                    }
                    else {
                        index = 0;
                    }
                }
            }

            // moves the person
            if (gridCount - speed < 0) {
                transform.position += direction * gridCount;
                gridCount = 0;
            }
            else {
                transform.position += direction * speed;
                gridCount -= speed;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    // called by Unity when there's a collison
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Slime" || other.gameObject.tag == "Sneeze") {
            StartCoroutine(Infect());
        }
    }

    // "infects" the person for a given amount of time
    IEnumerator Infect() {
        infected = true;
        if (numTimesInfected == 0) GameManager.AddInfectedPerson();
        numTimesInfected++;
        spriteRenderer.color = infectedColor;
        yield return new WaitForSeconds(time);
        numTimesInfected--;
        if (numTimesInfected == 0) {
            infected = false;
            GameManager.RemoveInfectedPerson();
            spriteRenderer.color = originalColor;
        }
    }

	void SetDirection(Vector3 newDirection) {
		direction = newDirection;
		if (direction == Vector3.right) {
			spriteRenderer.sprite = spriteRight;
		} else if (direction == Vector3.up) {
			spriteRenderer.sprite = spriteUp;
		} else if (direction == Vector3.left) {
			spriteRenderer.sprite = spriteLeft;
		} else if (direction == Vector3.down) {
			spriteRenderer.sprite = spriteDown;
		}
	}

	public void Sneeze() {
		if (infected) {
			float spawnRadius = .4f;
			Vector3 spawnPosition = transform.position + direction * spawnRadius;
			Snot snot = (Snot)Instantiate(snotPrefab, spawnPosition, transform.rotation);
			snot.direction = direction;
			if (direction == Vector3.right) {
				snot.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
			} else if (direction == Vector3.up) {
				snot.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
			} else if (direction == Vector3.left) {
				snot.transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
			} else if (direction == Vector3.down) {
				snot.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
			}
		}
	}
}
