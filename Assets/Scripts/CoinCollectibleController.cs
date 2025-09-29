using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectibleController : MonoBehaviour
{
    public float speed = 5.0f;
    public ScoreTracker scoreTracker;
    // Start is called before the first frame update
    void Start()
    {
        scoreTracker =  GameObject.Find("ScoreManager").GetComponent<ScoreTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scoreTracker.addScore(1);
            Debug.Log("Player touched object.");
            Destroy(gameObject);
        }
    }
}
