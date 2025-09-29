using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayPause : MonoBehaviour
{
    public EnemyController[] controllers;
    public CoinCollectibleController[] coinController;
    public DashCollectibleController[] dashCollectibleController;
    public bool pause;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        controllers = GetComponentsInChildren<EnemyController>();
        coinController = GetComponentsInChildren<CoinCollectibleController>();
        dashCollectibleController = GetComponentsInChildren<DashCollectibleController>();
        if (pause)
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                controllers[i].enabled = false;
            }

            for (int i = 0; i <  coinController.Length; i++)
            {
                coinController[i].enabled = false;
            }

            for (int i = 0; i <  dashCollectibleController.Length; i++)
            {
                dashCollectibleController[i].enabled = false;
            }
        }
        else
        {
            for (int i=0; i < controllers.Length; i++)
            {
                controllers[i].enabled = true;
            }

            for (int i = 0; i < coinController.Length; i++)
            {
                coinController[i].enabled = true;
            }

            for (int i = 0; i < dashCollectibleController.Length; i++)
            {
                dashCollectibleController[i].enabled = true;
            }
        }
    }
}
