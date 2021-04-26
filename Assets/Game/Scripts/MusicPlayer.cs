using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
    }

    public void ReduceSound()
    {
        _audioSource.volume = 0.1f;
    }

    public void RestoreSound()
    {
        _audioSource.volume = 1.0f;
    }
}
