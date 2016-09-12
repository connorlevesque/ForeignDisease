using UnityEngine;
using System.Collections;


public class Person : MonoBehaviour {

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
    SpriteRenderer spriteRenderer;
    Color originalColor;


    // Called by Unity when the scene is loading
    void Start() {
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
        // the direction the person should move in
        Vector3 direction = Vector3.right;

        // "animation" while loop
        while (true) {
            if (gridCount == 0) {
                // figrues out how many grid squares to traverse
                float.TryParse(movementMap[1, index], out gridCount);
                // figures out the direction to move in
                switch (movementMap[0, index]) {
                    case "up":
                        if (inReverse) direction = Vector3.down;
                        else direction = Vector3.up;
                        break;
                    case "down":
                        if (inReverse) direction = Vector3.up;
                        else direction = Vector3.down;
                        break;
                    case "right":
                        if (inReverse) direction = Vector3.left;
                        else direction = Vector3.right;
                        break;
                    case "left":
                        if (inReverse) direction = Vector3.right;
                        direction = Vector3.left;
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

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(getInfected());
        }
    }

    IEnumerator getInfected() {
        spriteRenderer.color = infectedColor;
        yield return new WaitForSeconds(time);
        spriteRenderer.color = originalColor;
    }
}