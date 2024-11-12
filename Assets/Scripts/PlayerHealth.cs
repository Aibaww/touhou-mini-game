using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // health bar variables
    public Image heartImage; 
    private Sprite ThreeHearts; 
    private Sprite TwoHearts;
    private Sprite OneHeart;
    private Sprite NoHearts;

    // game over screen
    public Image gameOverImage;
    private Sprite gameOverSprite;

    // player hitsound vars
    private AudioSource audioSource;
    public AudioClip hitSound;

    private void Start()
    {
        ThreeHearts = Resources.Load<Sprite>("3Heart");
        TwoHearts = Resources.Load<Sprite>("2Heart");
        OneHeart = Resources.Load<Sprite>("1Heart");
        NoHearts = Resources.Load<Sprite>("empty");
        gameOverSprite = Resources.Load<Sprite>("GameOver");
        heartImage.sprite = ThreeHearts;

        audioSource = GetComponent<AudioSource>();
    }

    // Call this function when the player gets hit
    public void OnPlayerHit()
    {
        audioSource.PlayOneShot(hitSound);

        if (heartImage.sprite == ThreeHearts) {
            heartImage.sprite = TwoHearts;
        } else if (heartImage.sprite == TwoHearts) {
            heartImage.sprite = OneHeart;
        } else {
            heartImage.sprite = NoHearts;
            GameOver();
        }
    }

    private void GameOver() {
        gameOverImage.sprite = gameOverSprite;
        gameOverImage.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }
}
