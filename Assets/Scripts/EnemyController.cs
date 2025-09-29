using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 1;
    int currentHealth;
    public float speed;
    public GameObject player;
    public float deathDuration;
    private bool isDead;
    public GameObject bullet;
    public Transform bulletPos;
    public float timer;
    private Animator animator;

    #region BOXCAST
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask playerLayer;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!isDead)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (isPlayerInRange())
            {
                if (timer > 10)
                {
                    timer = 0;
                    shoot();
                }
            }
        }

    }

    public void takeDamage(int damage, float deathTime)
    {
        currentHealth -= damage;
        deathDuration = deathTime; 

        if (currentHealth <= 0)
        {
            StartCoroutine(die());
        }
    }

    public IEnumerator die()
    {
        isDead = true;
        yield return new WaitForSeconds(deathDuration);
        Destroy(gameObject);
        Debug.Log("Enemy died.");
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.Log("Player touched object.");
    //        player.GetComponent<PlayerController>().doDamage(10);
    //    }
    //
    //    if (collision.CompareTag("Check"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void shoot()
    {
        animator.SetTrigger("Shoot");
        StartCoroutine(shooting());
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    public IEnumerator shooting()
    {
       yield return new WaitForSeconds(0.3f);
    }

    public bool isPlayerInRange()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.right, castDistance, playerLayer))
        {
            return true;
        }

        else
        {
            return false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.right * castDistance, boxSize);
    }
}
