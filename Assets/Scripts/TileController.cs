using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Sprite[] spritesWithGold;
    private SpriteRenderer spriteRenderer;

    private bool spriteChanged = false;

    protected void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerExit2D (Collider2D other) {
        if (other.tag == "Player" && !spriteChanged) {
            spriteRenderer.sprite = spritesWithGold[Random.Range(0, spritesWithGold.Length)];
            spriteChanged = true;
        }
    }

}
