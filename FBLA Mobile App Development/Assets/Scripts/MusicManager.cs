using UnityEngine.Audio;
using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Array containing audio sources
    public Sound[] sounds;

    // Music manager instance
    public static MusicManager instance;

    // When scene loads
    private void Awake()
    {
        // Make sure only one music controller exists
        if (instance == null)
            // If there's no music managers, use this music manager
            instance = this;
        else
        {
            // If there's a already a music manager, destroy this music manager
            Destroy(gameObject);
            return;
        }

        // Make sure music doesn't stop on scene switch
        DontDestroyOnLoad(gameObject);

        // Load all sounds
        foreach (Sound s in sounds)
        {   
            // Load each component
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
        }
    }

    // Called to play a sound
    public void Play (string name)
    {
        // Look for the song
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Play sound if there is one matching the name
        if (s != null)
            s.source.Play();
    }

    // Called to stop a sound
    public void Stop(string name)
    {
        // Look for the song
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Stop sound if there is one matching the name
        if (s != null)
            s.source.Stop();
    }
}
