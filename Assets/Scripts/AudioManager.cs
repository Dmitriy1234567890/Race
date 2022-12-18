using System;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    public static AudioManager Instance;

    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (var s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.clip;
            s.Source.volume = s.volume;
            s.Source.pitch = s.pitch;
            s.Source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("MainTheme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError($"No sound with name \"name\"");
            return;
        }
        s.Source.Play();
    }

    //public void SetActiveMainTheme(bool active)
    //{
    //    AudioSource audio = Array.Find(FindObjectsOfType<AudioSource>(), sound => sound.clip.name == "MainTheme");
    //    audio.volume = active ? 1 : 0;
    //}
    
    //public void SetActiveSoundEffects(bool active)
    //{
        
    //    foreach (var audio in FindObjectsOfType<AudioSource>())
    //    {
    //        if (audio.clip.name == "MainTheme") return;
    //        audio.volume = active ? 1 : 0;
    //    }
    //}
}

