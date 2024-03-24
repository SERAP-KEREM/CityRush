using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum SIDE { Left, Mid, Right };
public enum HitX { Left, Mid, Right,None };
public enum HitY { Up, Mid, Down,None };
public enum HitZ { Forward, Mid, Baxkward ,None };
public class Character : MonoBehaviour
{
    public SIDE midSide = SIDE.Mid;
    float newXpos = 0f;
    public bool SwipeUp;
    public bool SwipeDown;
    public bool SwipeLeft;
    public bool SwipeRight;
    public float XValue;
    private CharacterController characterController;

    public bool isStart;
    public TextMeshProUGUI playgameText;


    private float x;
    public float speedDodge;
    public float jumpPower=7f;
    private float y;
    public bool inJump;
    public bool inRoll;
    internal float rollCounter;
    public float fwdSpeed = 7f;

    public Animator playerAnimator;
    public float colliderHeight;
    public float colliderCenterY;


    public HitX hitX=HitX.None;
    private HitY hitY=HitY.None;
    private HitZ hitZ=HitZ.None;

    private void Start()
    {
        StartCoroutine(PlayGame());

        characterController = GetComponent<CharacterController>();
        colliderHeight = characterController.height;
        colliderCenterY = characterController.center.y;
        playerAnimator = GetComponent<Animator>();
        transform.position = new Vector3(0, -2.5f, -50f); 
    }

    private void Update()
    {
        SwipeLeft=Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        if (isStart)
        {

            playerAnimator.SetBool("isRunning",true);
            if (SwipeLeft && !inRoll)
            {
                if (midSide == SIDE.Mid)
                {
                    newXpos = -XValue;
                    midSide = SIDE.Left;
                    //
                    //
                    //playerAnimator.Play("leftStrafe");
                   playerAnimator.SetTrigger("leftStrafe");
                   // playerAnimator.SetBool("isRunning", false);
                }
                else if (midSide == SIDE.Right)
                {
                    newXpos = 0;
                    midSide = SIDE.Mid;
                    playerAnimator.SetTrigger("leftStrafe");
                    //playerAnimator.SetBool("isRunning", false);



                }
            }
            else if (SwipeRight && !inRoll)
            {
            
                if (midSide == SIDE.Mid)
                {
                    newXpos = XValue;
                    midSide = SIDE.Right;
                    playerAnimator.SetTrigger("rightStrafe");
                   // playerAnimator.SetBool("isRunning", false);



                }
                else if (midSide == SIDE.Left)
                {
                    newXpos = 0;
                    midSide = SIDE.Mid;
                    playerAnimator.SetTrigger("rightStrafe");
                  //  playerAnimator.SetBool("isRunning", false);



                }
            }
            Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, fwdSpeed * Time.deltaTime);
            x = Mathf.Lerp(x, newXpos, Time.deltaTime * speedDodge);
             characterController.Move(moveVector);
            Jump();
            Roll();
        }
    }
    public void Jump()
    {
        if (characterController.isGrounded)
        {
           
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            {
                playerAnimator.Play("Landing");
                inJump = false;
            }
            if (SwipeUp)
            {
                y = jumpPower;
                playerAnimator.SetBool("isRunning", false);
                playerAnimator.SetBool("isJump",true);
                inJump = true;
            }
        }
        else
        {
            y -= jumpPower * 2 * Time.deltaTime;
            if (characterController.velocity.y < -0.1f)
            {
                playerAnimator.Play("Fall");
            }
        }
    }


    public void Roll()
    {
        rollCounter-=Time.deltaTime;
        if (rollCounter <= 0)
        {
            rollCounter = 0f;
            characterController.center = new Vector3(0,colliderCenterY,0);
            characterController.height = colliderHeight;
            inRoll = false;

        }
        if (SwipeDown)
        {
            rollCounter = 0.2f;
            y -= 10f;
            characterController.center=new Vector3(0,colliderCenterY/2f,0);
            characterController.height = colliderHeight/2f;
            //playerAnimator.CrossFadeInFixedTime("isSlide", 0.1f);
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isSlide", true);
            inRoll = true;
            inJump=false;

        }
    }

    public void OnCharacterControllerHit(Collider collider)
    {
        hitX=GetHitX(collider);
    }
    public HitX GetHitX(Collider collider)
    {
        Bounds characterBounds = characterController.bounds;
        Bounds colliderBounds= collider.bounds;
        float minX = Mathf.Max(colliderBounds.min.x, characterBounds.min.x);
        float maxX = Mathf.Min(colliderBounds.max.x, characterBounds.max.x);
        float average=(minX+maxX)/2f-colliderBounds.min.x;
        HitX hit;
        if (average > colliderBounds.size.x - 0.33f)
            hit = HitX.Right;
        else if (average < 0.33f)
            hit = HitX.Left;
        else
            hit = HitX.Mid;
        return hit;

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
