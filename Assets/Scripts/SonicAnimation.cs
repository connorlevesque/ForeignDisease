using UnityEngine;
using System.Collections;

public class SonicAnimation : MonoBehaviour {

    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;

	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(startAnimation());
	}

    IEnumerator startAnimation() {
        int index = 0;
        while(true) {
            spriteRenderer.sprite = sprites[index];
            if(index == sprites.Length - 1) {
                index = 0;
            }else {
                index++;
            }
            yield return new WaitForSeconds(0.05f); 
        }
    }
}
