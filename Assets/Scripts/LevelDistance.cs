using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDistance : MonoBehaviour
{
    public GameObject disPlay;
    public int disRun;
    public bool addingDis = false;
    public float disDelay = 0.25f;

    private void Update()
    {
        if (addingDis == false)
        {
            addingDis = true;
            StartCoroutine(AddingDis());

        }
    }

    IEnumerator AddingDis()
    {
        disRun++;
        disPlay.GetComponent<TextMeshProUGUI>().text = "" + disRun;
        yield return new WaitForSeconds(disDelay);
        addingDis = false;
    }
}
