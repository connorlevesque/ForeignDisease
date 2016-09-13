using UnityEngine;
using System.Collections;

public class SonicAnimation : MonoBehaviour {

    public AudioClip feverSong;
    public float volume;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;

	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AudioSource.PlayClipAtPoint(feverSong, transform.position, volume);
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
            yield return new WaitForSeconds(0.1f); 
        }
    }
}
