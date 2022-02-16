using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed;

    public float distance;

    //public Animator enemyAnimator;

    private bool movingRight = true;

    public Transform groundDetection;

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponent<PlayerController>() != null)
    //    {
    //        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
    //        enemyAnimator.SetBool("PlayerHit", true);
    //        playerController.KillPlayer();
    //    }

    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.KillPlayer();
        }
    }

    //public void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponent<PlayerController>() != null)
    //    {
    //        enemyAnimator.SetBool("PlayerHit", false);
    //    }
    //}

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //Enemy ground check for turning direction     

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if (!groundInfo.collider)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

    }

}

