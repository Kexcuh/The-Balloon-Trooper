using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
     [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip shoot;
    //public AudioClip death;
    //public AudioClip checkpoint;
    //public AudioClip wallTouch;
    //public AudioClip portalIn;
    //public AudioClip portalOut;
    public AudioClip coin;
    public AudioClip jump;
    public AudioClip balloonPop;

    //Keep object persistent
    private void Awake()
    {
        
        // Check if another instance already exists
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
        
        // Make this object persistent
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
