using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPlayPause : MonoBehaviour
{
    public BackgroundController[] controllers;
    public bool pause;
    // Start is called before the first frame update
    void Start()
    {
        controllers = GetComponentsInChildren<BackgroundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pause)
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                controllers[i].enabled = false;
            }
        }

        else
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                controllers[i].enabled = true;
            }
        }
    }
}
