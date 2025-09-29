using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeCounter : MonoBehaviour
{
    public ScoreTracker scoreTracker;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        scoreTracker = GetComponentInParent<ScoreTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            scoreTracker.addScore(1);
            Destroy(collision.gameObject);
        }

        else
        {
            Destroy(collision.gameObject);
        }
    }
}
