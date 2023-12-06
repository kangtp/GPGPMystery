using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    GameObject BackgroundMusic;
   private AudioSource audioSource;

   private void Awake() {
    BackgroundMusic = GameObject.Find("MusicManager");
    audioSource = BackgroundMusic.GetComponent<AudioSource>();
    if(audioSource.isPlaying) return;
    else
    {
        audioSource.Play();
        DontDestroyOnLoad(BackgroundMusic);
    }
   }
}
