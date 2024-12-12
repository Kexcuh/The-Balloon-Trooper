using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{

    private void Awake()
    {
        
        // Check if another instance already exists
        if (FindObjectsOfType<DoNotDestroy>().Length > 1)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
        
        // Make this object persistent
        DontDestroyOnLoad(gameObject);
    }
}