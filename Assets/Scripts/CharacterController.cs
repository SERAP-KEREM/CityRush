using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float leftRightSpeed = 4f;
    public bool isStart;
    public TextMeshProUGUI playgameText;


    public bool isJumping=false;
    public bool comingDown=false;
    public GameObject playerObject;
    public Animator playerAnimator;
    private Rigidbody rb;

    public float jumpForce = 5f;
    public float slideSpeed = 5f;
    private bool isGrounded = true;
    private bool isSliding = false;
    private void Start()
    {
        StartCoroutine(PlayGame());
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }



    void Update()
    {
        if (isStart)
        {
            playerAnimator.SetBool("isRunning",true);
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed, Space.World);
                }
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed, Space.World);


                }
            }  
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                if (isJumping==false)
                {

                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset y velocity to zero
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    isGrounded = false;
                    playerAnimator.SetTrigger("isJump");

                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Slide();
            }

        }
    }
    void Slide()
    {
        if (!isSliding)
        {
            isSliding = true;
            playerAnimator.SetBool("isSlide", true);
            StartCoroutine(StopSliding());
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        //    playerAnimator.SetBool("Grounded", true);
        }
    }
    IEnumerator StopSliding()
    {
        yield return new WaitForSeconds(1f); // Adjust slide duration if needed
        isSliding = false;
        playerAnimator.SetBool("isSlide", false);
    }
    IEnumerator PlayGame()
    {

        playgameText.text = "3";
        yield return new WaitForSeconds(1f);
        playgameText.text = "2";
        yield return new WaitForSeconds(1f);
        playgameText.text = "1";
        yield return new WaitForSeconds(1f);
        playgameText.text = "GO!";
        yield return new WaitForSeconds(1f);
        playgameText.text = "";
        isStart = true;


    }

}
