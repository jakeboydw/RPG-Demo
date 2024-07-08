using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool isAlive = true;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject gameOverScene;
    private int currentHealth;
    private Vector3 input;
    private Animator animator;
    private Rigidbody2D mRigidbody;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isAlive)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");    //get input
        }

        if (input.x != 0) input.y = 0;    //prevent moving diagonally

        if (input != Vector3.zero)
        {
            moveCharacter(input);

            animator.SetFloat("moveX", input.x);     //setFloat here to assure that everytime moveX and moveY are not 0,
            animator.SetFloat("moveY", input.y);     //otherwise player will possibly stay in WalkDown animation.
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void moveCharacter(Vector3 input)
    {
        mRigidbody.MovePosition(transform.position + input * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Die()
    {
        isAlive = false;
        input = Vector3.zero;
        gameOverScene.SetActive(true);
    }
}
