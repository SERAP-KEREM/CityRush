using System.Collections;
using System.Collections.Generic;
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

        
        if (SwipeLeft && !inRoll)
        {
            if (midSide == SIDE.Mid)
            {
                newXpos = -XValue;
                midSide=SIDE.Left;
                playerAnimator.Play("Left");
            }
            else if (midSide == SIDE.Right)
            {
                newXpos = 0;
                midSide = SIDE.Mid;
                playerAnimator.Play("Left");
            }
        }
        else if(SwipeRight&& !inRoll) 
        {

            if (midSide == SIDE.Mid)
            {
                newXpos = XValue;
                midSide=SIDE.Right;
                playerAnimator.Play("Right");

            }
            else if (midSide == SIDE.Left)
            {
                newXpos = 0;
                midSide = SIDE.Mid;
                playerAnimator.Play("Right");

            }
        }
        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, fwdSpeed * Time.deltaTime);
        x=Mathf.Lerp(x,newXpos,Time.deltaTime* speedDodge);
      //  characterController.Move(moveVector);
        Jump();
        Roll();

    }
    public void Jump()
    {
        if(characterController.isGrounded )
        {
            if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                playerAnimator.Play("Landing");
                inJump = false;
            }
            if(SwipeUp) {
                y = jumpPower;
               // playerAnimator.CrossFadeInFixedTime("Jump", 0.1f);
                inJump = true;

            }
        }
        else
        {
            y-=jumpPower*2*Time.deltaTime;
            if ((characterController.velocity.y<-0.1f))
            {
                
            }
           // playerAnimator.Play("Falling");
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
            playerAnimator.CrossFadeInFixedTime("Roll", 0.1f);
            inRoll= true;
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

}
