using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator player_animator;
    [SerializeField]
    Rigidbody2D player_Rb;
    /*[SerializeField]
    ScoreController player_score;*/
    [SerializeField]
    GameObject life_one, life_two, life_three;
    [SerializeField]
    GameOverController player_gameover;
    /*[SerializeField]
    AudioSource player_jump;
    [SerializeField]
    AudioSource player_movement;
    [SerializeField]
    AudioSource player_land;
    [SerializeField]
    AudioSource player_keypicked;*/

    //Dash feature
    IEnumerator dashCoroutine;
    bool isDashing, canDash = true;
    float direction = 1;
    float horizontal;
    float runspeed = 5.0f;
    int player_health = 3;
    float jump_force = 2.0f;
   // [SerializeField]
    //float jumpspeed = 10.0f;
    bool crouch;
    [SerializeField]
    bool onGround;
    private int extraJump = 0;
    private bool isPickUp;
    private float currentTime;
    private bool finishTimer = true;
    [SerializeField] public Text countDownText;


    public GameObject powerUpObject;
    private void Awake()
    {
        player_Rb = gameObject.GetComponent<Rigidbody2D>();
        countDownText.gameObject.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("powerUp"))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        Debug.Log("Power up picked up!!");
        isPickUp = true;
        finishTimer = false;
        countDownText.gameObject.SetActive(true);
        /*Instantiate(pickUpEffect, transform.position, transform.rotation);*/
        Destroy(powerUpObject.gameObject);
        currentTime = 5f;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (currentTime == 0f)
        {
            finishTimer = true;
            //Debug.Log("timer true");
        }

        bool vertical = Input.GetKeyDown(KeyCode.Space);

        if (currentTime == 0f || (finishTimer == true))
        {
            
            Player_Jump(vertical);
            //Debug.Log("vertical");
        }


        if ((isPickUp == true) && (currentTime > 0f) && finishTimer==false)
        {
            currentTime -= 1 * Time.deltaTime;
            countDownText.text = currentTime.ToString("0");
            PlayerDoubleJump(vertical);
        }
        else if(currentTime <= 0f)
        {
            currentTime = 0f;
            countDownText.gameObject.SetActive(false);
            isPickUp = false;
        }

        //DASH
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(0.1f, 2);
            StartCoroutine(dashCoroutine);
        }
        if (horizontal != 0)
        {
            direction = horizontal;
        }

        /*   if (finishTimer == true)
           {
               currentTime = 5f;
           }
   */

        Player_Movement(horizontal);
        Player_Run(horizontal);

        if (Input.GetKeyDown(KeyCode.C))
        {
            Player_Crouch();
        } 
    }


    void Player_Crouch()
    {
        if(crouch == false)
        {
            crouch = true;
            player_animator.SetBool("Crouch", true);
        }
        else
        {
            crouch = false;
            player_animator.SetBool("Crouch", false);
        }
    }

    void Player_Jump(bool vertical)
    {
        /*if((vertical) && (onGround == true))
        {*/
        //player_animator.SetBool("Jump", true);
       /* player_Rb.AddForce(new Vector2(0.0f, 10f), ForceMode2D.Impulse);
        Debug.Log("2323Jumppppp!!");*/
        //player_jump.Play();
        //extraJump = extraJump - 1;
        //onGround = false;
        /*}*/
        /*  else if((vertical) && onGround == false)
          {
              //player_animator.SetBool("Jump", false);
          }*/

        if ((vertical) && (isPickUp != true) && (onGround == true))
        {
            //Debug.Log("jump");
            player_Rb.AddForce(new Vector2(0f, 7f) * jump_force, ForceMode2D.Impulse);
        }
    }
    private void PlayerDoubleJump(bool vertical)
    {
/*       // player_animator.SetBool("Jump", true);
        player_Rb.AddForce(new Vector2(0.0f, 10f), ForceMode2D.Impulse);
       // onGround = false;
        extraJump = extraJump - 1;
        Debug.Log("Jumppppp!!");
*/

        if (onGround == true)
        {
            extraJump = 1;
        }

        if ((vertical) && (extraJump > 0))
        {
            player_Rb.AddForce(new Vector2(0f, 7f) * jump_force, ForceMode2D.Impulse);
            extraJump--;
            Debug.Log("ExtraJump enabled");
        }
    }


    void Player_Movement(float horizontal)
    {
        player_animator.SetFloat("Speed", Mathf.Abs(horizontal));

        Vector3 scale = transform.localScale;

        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    void Player_Run(float horizontal)
    {
        Vector2 move_position = transform.position;
        move_position.x += horizontal * runspeed * Time.deltaTime;
        transform.position = move_position;
    }

    /*public void KeyCollected()
    {
        player_score.UpdateScore(10);
        //player_keypicked.Play();
    }*/

    public void KillPlayer()
    {   
        player_health--;
        if(player_health == 2)
        {
            player_animator.SetTrigger("Hurt");
            life_three.SetActive(false);
        }
        else if(player_health == 1)
        {
            player_animator.SetTrigger("Hurt");
            life_two.SetActive(false);
        }
        else
        {
            player_animator.SetTrigger("Death");
            life_one.SetActive(false);
            player_gameover.GameOver();
            this.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        //Dash physics
        if (isDashing)
        {
            player_Rb.AddForce(new Vector2(direction * 20, 0), ForceMode2D.Impulse);
        }
    }

    IEnumerator Dash(float dashDuration, float dashCooldown)
    {
        isDashing = true;
        canDash = false;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        player_Rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
