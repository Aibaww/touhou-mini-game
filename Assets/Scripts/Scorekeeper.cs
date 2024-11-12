using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]

public class Scorekeeper : MonoBehaviour
{
    public static Scorekeeper scorekeeper;

    public int score = 0;
    private TMP_Text text;

    private AudioSource audioSource;
    public AudioClip scoreSound;

    void Start()
    {
        scorekeeper = this;
        text = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ScorePoints(int pt) {
        audioSource.PlayOneShot(scoreSound);
        score += pt;
        text.text = "Score: " + score;
    }
}
