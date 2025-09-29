using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayPause : MonoBehaviour
{
    public PlatformGenerator platformGenerator;
    public PlatformController platformController;
    public bool pause;
    // Start is called before the first frame update
    void Start()
    {
        platformGenerator = GetComponentInChildren<PlatformGenerator>();
        platformController = GetComponentInChildren<PlatformController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pause)
        {
            platformGenerator.enabled = false;
            platformController.enabled = false;
        }

        else
        {
            platformGenerator.enabled = true;
            platformController.enabled = true;
        }
    }
}
