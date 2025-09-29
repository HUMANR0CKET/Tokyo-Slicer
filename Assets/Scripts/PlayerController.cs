using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region COMPONENTS
    public Rigidbody2D rb;
    public PlayerAnimator animHandler;
    public AudioSource audioSource;
    public GameObject background;
    public BackgroundPlayPause backgroundPlayPause;
    public GameObject mainCam;
    public CameraController cameraController;
    public LayerMask enemyLayer;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public GameObject scoreManager;
    public ScoreTracker scoreTracker;
    public healthBarController healthBarController;
    public DashBarController dashBarController;
    public PlatformPlayPause platformPlayPause;
    public GameObject platforms;
    public GameObject enemies;
    public EnemyPlayPause enemyPlayPause;
    public GameObject gameManager;
    public GameManager gameManageScript;
    #endregion

    #region INPUTS
    private Vector2 moveInput;
    #endregion

    #region VARIABLES
    public float speed = 3f;
    public float jumpForce;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    public float pauseDuration;
    public int maxhealth = 10;
    public int health = 10;
    public int dashPoints;
    public int jumps;
    #endregion

    #region CHECKS
    public bool canAttack = true;
    public bool isDashing;
    public bool gameOverTrigger;
    // public bool canDash = true;
    #endregion

    #region BOXCAST
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animHandler = GetComponent<PlayerAnimator>();
        audioSource = GetComponent<AudioSource>();
        background = GameObject.FindGameObjectWithTag("Background");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        scoreManager = GameObject.Find("ScoreManager");
        platforms = GameObject.FindGameObjectWithTag("Platforms");
        enemies = GameObject.Find("Enemies");
        gameManager = GameObject.Find("GameManager");
    }
    // Start is called before the first frame update
    void Start()
    {
        backgroundPlayPause = background.GetComponent<BackgroundPlayPause>();
        cameraController = mainCam.GetComponent<CameraController>();
        scoreTracker = scoreManager.GetComponent<ScoreTracker>();
        healthBarController.setMaxHealth(maxhealth);
        dashBarController.setMaxDash(10);
        platformPlayPause = platforms.GetComponent<PlatformPlayPause>();
        enemyPlayPause = enemies.GetComponent<EnemyPlayPause>();
        gameManageScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead() && !gameOverTrigger)
        {
            animHandler.isDead = true;
            gameManageScript.gameOver();
            gameOverTrigger = true;
        }

        else
        {
            dashCheck();
            #region CHECKS
            if (!canAttack)
            {
                if (isGrounded())
                {
                    canAttack = true;
                }
            }

            if (isGrounded())
            {
                jumps = 0;
            }
            #endregion
            #region INPUT HANDLER
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            
            if (Input.GetKeyDown(KeyCode.W) && (isGrounded() || canDoubleJump()))
            {
                jump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                attack();
            }

            if (Input.GetKeyDown(KeyCode.Space) && canDash())
            {
                StartCoroutine(dash());
            }

        }
        #endregion
    }

    private void FixedUpdate()
    {
        dashCheck();
    }

    private void jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animHandler.isJumping = true;
        jumps++;
    }

    private void attack()
    {
        if (canAttack)
        {
            animHandler.isAttacking = true;
            StartCoroutine(attacking());
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach(Collider2D enemy in hitEnemy)
            {
                enemy.GetComponent<EnemyController>().takeDamage(1, pauseDuration);
                scoreTracker.addScore(2);
            }
            canAttack = false;
        }
    }

    public IEnumerator attacking()
    {
        pauseDuration = 1;
        StartCoroutine(pause());
        rb.simulated = false;
        for (int i=0; i<2; i++)
        {
            audioSource.Play();
            yield return new WaitForSeconds(0.5f);
        }
        rb.simulated = true;
    }

    public IEnumerator dash()
    {
        // canDash = false;
        isDashing = true;
        pauseDuration = dashDuration*4;
        StartCoroutine(pause());
        rb.velocity = new Vector2(Vector2.right.x * dashSpeed, transform.position.y);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        cameraController.isDashing = true;
        dashPoints = 0;
        dashBarController.removeDashPoints(10);
        // yield return new WaitForSeconds(dashCooldown);
        // canDash = true;
    }

    public void doDamage(int damage)
    {
        health -= damage;
        healthBarController.setHealth(health);
        animHandler.isAttacked = true;
    }

    private void dashCheck()
    {
        if (isDashing)
        {
            return;
        }
    }
    
    public IEnumerator pause()
    {
        backgroundPlayPause.pause = true;
        platformPlayPause.pause = true;
        enemyPlayPause.pause = true;
        yield return new WaitForSeconds(pauseDuration);
        backgroundPlayPause.pause = false;
        platformPlayPause.pause = false;
        enemyPlayPause.pause = false;
    }
    public bool isDead()
    {
        if (health <= 0)
        {
            pauseDuration = 2f;
            StartCoroutine(pause());
            return true;

        }
        else
        {
            return false;
        }
    }
    public bool canDash()
    {
        if (dashPoints >= 10)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public bool canDoubleJump()
    {
        if (jumps < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void addDashPoints(int addPoints)
    {
        dashPoints += addPoints;
        dashBarController.addDashPoints(addPoints);
    }
    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
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
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);

        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
