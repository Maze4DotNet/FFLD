using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] _audioClips;
    Dictionary<string, AudioSource> _sounds = new Dictionary<string, AudioSource>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var audioClip in _audioClips)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            _sounds.Add(audioClip.name, audioSource);
        }
    }

    public void PlaySound(string name){
        _sounds[name].Play();
    }

    public void StopSound(string name){
        _sounds[name].Stop();
    }
}
