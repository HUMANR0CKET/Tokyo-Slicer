using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollectibleController : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
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
            playerController.addDashPoints(1);
            Debug.Log("Player touched object.");
            Destroy(gameObject);
        }
    }
}
