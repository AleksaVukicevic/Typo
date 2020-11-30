using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundsScript : MonoBehaviour
{
    public static SoundsScript ss;
    [SerializeField] private AudioMixerGroup audioGroup;
    [Header("Sounds")]
    [SerializeField] private Sound[] Sounds;

    private void Awake()
    {
        if (ss != null)
        {
            Destroy(ss.gameObject);
            ss = this;
        }
        else
        {
            ss = this;
        }
        DontDestroyOnLoad(this);

        SetupSounds();
    }

    public void SetupSounds()
    {
        for (int s = 0; s < Sounds.Length; s++)
        {
            Sounds[s].source = gameObject.AddComponent<AudioSource>();
            Sounds[s].source.clip = Sounds[s].clip;
            Sounds[s].source.volume = Sounds[s].volume;
            Sounds[s].source.pitch = Sounds[s].pitch;
            Sounds[s].source.outputAudioMixerGroup = audioGroup;
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        if (s != null)
        {
            s.source.Play();
        }
    }
}
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0, 1)] public float volume = 1f;
    [Range(-3, 3)] public float pitch = 1f;

    [HideInInspector] public AudioSource source;
}

