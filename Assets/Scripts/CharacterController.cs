using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 3;
    public float leftRightSpeed=4;
    void Update()
    {
        transform.Translate(Vector3.forward*Time.deltaTime*moveSpeed,Space.World);

        if(Input.GetKeyDown(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
            }
        }

        if(Input.GetKeyDown(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed * -1);

            }
        }
        
        
    }
}
