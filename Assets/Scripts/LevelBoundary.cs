using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public static float leftSide=-3.5f;
    public static float rightSide=3.5f;
    public  float internalLeft;
    public float internalRight;


   

    private void Update()
    {
        internalLeft = leftSide;
        internalRight = rightSide;


    }
}
