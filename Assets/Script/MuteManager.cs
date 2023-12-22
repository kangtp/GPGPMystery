using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteManager : MonoBehaviour
{
    private AudioSource[] audioSources;

    public static MuteManager Instance;

    void Start()
    {
        Instance = this;
        audioSources = FindObjectsOfType<AudioSource>();
        // 현재 씬에 있는 모든 AudioSource를 찾아 배열에 저장
        int condition = PlayerPrefs.GetInt("SoundInfo");
        if(condition == 0)
        {
            MuteAllSounds();
        }
        else
        {
            UnmuteAllSounds();
        }
        
    }

    public void MuteAllSounds()
    {
        // 모든 AudioSource의 소리를 끕니다.
        foreach (var audioSource in audioSources)
        {
            audioSource.mute = true;
        }
    }

    public void UnmuteAllSounds()
    {
        // 모든 AudioSource의 소리를 켭니다.
        foreach (var audioSource in audioSources)
        {
            audioSource.mute = false;
        }
    }
}
