using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    private Sound currentSound;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0;i<sounds.Length;i++)
        {
            AudioSource source = sounds[i].parent.gameObject.AddComponent<AudioSource>();
            sounds[i].source = source;

            source.clip = sounds[i].clip;
            source.volume = sounds[i].volume;
            source.pitch = sounds[i].pitch;
            source.spatialBlend = sounds[i].spatialBlender;
            source.playOnAwake = sounds[i].playOnAwake;
            source.loop = sounds[i].looping;
        }

        PlaySound("Music");
    }

    public void PlaySound(string name)
    {
        foreach(Sound sound in sounds)
        {
            if (sound.name == name)
                if (!sound.source.isPlaying)
                {
                    sound.source.Play();
                    break;
                }
        }
    }

    public void StopSound(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
                if (sound.source.isPlaying)
                {
                    sound.source.Stop();
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public struct Sound
    {
        public string name;
        public AudioSource source;
        public AudioClip clip;
        public float volume;
        public float pitch;
        public float spatialBlender;
        public bool playOnAwake;
        public bool looping;
        public Transform parent;
    }
}
