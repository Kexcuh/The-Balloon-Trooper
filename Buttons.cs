using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject[] pauseMode;
    public GameObject[] playMode;
    public bool isGamePaused;
    //public ScoreKeeper scorekeeper;

    void Start()
    {
       //scorekeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
       Time.timeScale = 1.0f;


       foreach (GameObject g in pauseMode)
       {
            g.SetActive(false);
       }
       isGamePaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused == false)
            {
                Pause();    
            }
            else
            {
                Resume();
            }
        }
    }

    private void Awake()
    {
        
        // Check if another instance already exists
        if (FindObjectsOfType<Buttons>().Length > 1)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
        
        // Make this object persistent
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlayGame()
    {
      SceneManager.LoadScene("Level 1");
      

    }

    public void LoadMenu()
    {
      //scorekeeper.loadMenu();
    }

    public void Pause()
    {
      isGamePaused = true;
      Time.timeScale = 0.0f;
      
        foreach(GameObject g in pauseMode)
            g.SetActive(true);

        foreach(GameObject g in playMode)
            g.SetActive(false);
    }

    public void Resume()
    {
      isGamePaused = false;
      Time.timeScale = 1.0f;

        foreach (GameObject g in pauseMode)
            g.SetActive(false);

        foreach (GameObject g in playMode)
            g.SetActive(true);
    }
    
}
