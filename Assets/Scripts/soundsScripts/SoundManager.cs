using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager instance;

    bool toDestory = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = this;
        }
        else 
        {   
            toDestory = true;
            return;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Start()
    {
        if(toDestory)
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds)
        {
            if(s.loop)
                s.source.Play();
        }
    }
    public static SoundManager getInstance()
    {
        return instance;
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
