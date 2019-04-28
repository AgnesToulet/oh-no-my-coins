using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource efxSource1;
    public AudioSource efxSource2;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    // Start is called before the first frame update
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle (AudioClip clip) {
        if (efxSource1.isPlaying) {
            efxSource2.clip = clip;
            efxSource2.Play();
        } else {
            efxSource1.clip = clip;
            efxSource1.Play();
        }
    }
}
