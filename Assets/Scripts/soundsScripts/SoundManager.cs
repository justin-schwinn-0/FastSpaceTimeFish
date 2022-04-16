using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager instnace;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(instnace == null)
        {
            instnace = this;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    public static SoundManager getInstance()
    {
        return instnace;
    }

    public void Play(string name)
    {
        foreach(Sound s in sounds)
        {
            if(s.name.CompareTo(name) == 0)
            {
                s.source.Play();
                return;
            }
        }
    }
}
