using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    public Material[] Materials;
    public float volumeStep = 0.1f; 
    public int x;
    private Renderer _renderer;
    public Animator[] animators;
    public AudioSource _audioSource;
   // public string[] animationStateNames;
   private float animationSpeed;
    void Start()
    {
        x = 0;
        _renderer = _audioSource.GetComponent<Renderer>();
        _renderer.enabled = true;
        _renderer.sharedMaterial = Materials[x];
        _audioSource.volume = 0.5f;
        animationSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.sharedMaterial = Materials[x];

        for (int i = 0; i < animators.Length; i++)
        {
            if (animators[i] != null)
            {
                animators[i].speed = animationSpeed;
            }
        }

    }
    
  

    public void NextColor()
    {
        x = (x+1) % 2;
        if (_audioSource == null) return;
        
       
        if (x % 2 == 1)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
                for (int i = 0; i < animators.Length; i++)
                {
                    animationSpeed = 1.0f;
                    animators[i].speed = animationSpeed; 
                }
            }
        }
        else
        {
            _audioSource.Stop(); 
            animationSpeed = 0.0f;
        }
        
    }
    
    public void IncreaseVolume()
    {
        if (_audioSource != null)
        {
            _audioSource.volume = Mathf.Clamp(_audioSource.volume + volumeStep, 0.0f, 1.0f);
            animationSpeed += 0.2f;
        }
    }

    public void DecreaseVolume()
    {
        if (_audioSource != null)
        {
            _audioSource.volume = Mathf.Clamp(_audioSource.volume - volumeStep, 0.0f, 1.0f);
            animationSpeed -= 0.2f;
        }
    }
}
