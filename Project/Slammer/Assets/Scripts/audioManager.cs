using UnityEngine;
using System;
using UnityEngine.Audio;

    /*
    *   Code block taken from: 
    *   https://www.youtube.com/watch?v=6OT43pvUyfY
    */

public class audioManager : MonoBehaviour {
    public sound[] sounds;

    void Awake() {
        if (FindObjectsOfType<audioManager>().Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(this);
            foreach (sound s in sounds) {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
        
    }

    public void p (string name) {
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public static void play (string name) {
        FindObjectOfType<audioManager>().p(name);
    }
}
