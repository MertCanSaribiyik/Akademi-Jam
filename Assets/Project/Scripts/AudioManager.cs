using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private List<MyDictionary<SoundEffectType, AudioClip>> soundEffectList;

    [SerializeField] private AudioSource musicEffectSource;

    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void PlayMusic(AudioClip audioClip) {
        musicEffectSource.clip = audioClip;
        musicEffectSource.Play();
    }

    public void PlayOneShot(SoundEffectType soundEffectType) {
        AudioClip clip = GetClipByType(soundEffectType);
        soundEffectSource.pitch = 1f;
        soundEffectSource.PlayOneShot(clip);
    }

    private AudioClip GetClipByType(SoundEffectType soundEffectType) {
        foreach (MyDictionary<SoundEffectType, AudioClip> list in soundEffectList) {
            if (list.key == soundEffectType) {
                return list.value;
            }
        }

        return null;
    }
}

public enum SoundEffectType
{
    PlayerAttack,
    PlayerJump,
    BoosDeath,
    Collectible,
    Button,
    PlayerDamage,
    FinishMusic
}

[Serializable]
public struct MyDictionary<TKey, TValue>
{
    public TKey key;
    public TValue value;
}