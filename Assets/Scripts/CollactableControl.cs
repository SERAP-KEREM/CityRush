using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollactableControl : MonoBehaviour
{
    public static int coinCount;
    public GameObject coinCountDisplay;
    private void Update()
    {
        coinCountDisplay.GetComponent<TextMeshProUGUI>().text = ""+coinCount;
    }
}
