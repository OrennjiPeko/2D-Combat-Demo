using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AudioManager : Singletoon<AudioManager>
{
    public Sound[] sounds;
    private string currentScene;

    private bool IsPlaying = false;

    protected override void Awake()
    {
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        base.Awake();
    }
    public void Play(string name)
    {
        Sound s= Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
        
    }

    private void Start()
    {
        Debug.Log("Testing");
        currentScene = SceneManager.GetActiveScene().name;
        if(IsPlaying ==false)
        {
            StartZoneMusic();
        }

        
    }

    public void Stop(string name)
    {
        Sound s=Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
        IsPlaying = false;
    }
    // plays music after scene changes are finished
    public void PlayMusic()
    {
        
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Town" && IsPlaying == false)
        {
           Play("WoodsMusic");
           IsPlaying = true;
        }
        if (currentScene == "Scene_3"&&IsPlaying==false)
        {
            Play("WoodsMusic");
            IsPlaying = true;
        }

        if(currentScene=="Scene_4"&&IsPlaying == false)
        {
           Play("WoodsMusic");
           IsPlaying = true;
        }

        if(currentScene=="Ghost_Boss"&&IsPlaying==false)
        {
            Play("WoodsMusic");
            IsPlaying = true;
        }
        if(currentScene=="Scene_1"&&IsPlaying==false)
        {
            Play("WoodsMusic");
            IsPlaying = true;
        }
        if (currentScene == "Sceme_3" && IsPlaying == false)
        {
            Play("WoodsMusic");
            IsPlaying = true;
        }
        if (currentScene == "Scene_2" && IsPlaying == false)
        {
            Play("WoodsMusic");
            IsPlaying = true;
        }
       
        
    }
// stopping music between scenes 
    public void StopMusic()
    {
        if (IsPlaying == true)
        {
            currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Town")
            {
                Stop("Town");
            }
            if (currentScene == "Scene_3")
            {
                Stop("WoodsMusic");
                Stop("Town");
            }

            if (currentScene == "Scene_4")
            {
                Stop("WoodsMusic");
            }
            if (currentScene == "Ghost_Boss")
            {
                Stop("BossTheme");
                Stop("CaveMusic");
            }

            if (FindObjectOfType<PlayerHealth>().IsDead == true)
            {
                Stop("WoodsMusic");
                Stop("BossTheme");
                
            }
            
        }
        
    }

     private void StartZoneMusic()
    {
        if (currentScene == "Town")
        {
            Play("Town");
            IsPlaying=true;
        }
        if (currentScene == "Scene_4"||currentScene=="Scene_3"||currentScene=="Scene_2"||currentScene=="Scene_1")
        {
            Play("WoodsMusic");
            IsPlaying = true;
        }
        if (currentScene=="Ghost_Boss")
        {
            Play("BossTheme");
            IsPlaying = true;
        }
    }

}

