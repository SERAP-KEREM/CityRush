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
    bool isStart = true;

    private void Update()
    {
        if (addingDis == false )
        {
            addingDis = true;
            StartCoroutine(AddingDis());

        }
    }

    IEnumerator AddingDis()
    {
        if(isStart)
        {
            isStart = false;    
            yield return new WaitForSeconds(4f);
        }
       
        disRun++;
        disPlay.GetComponent<TextMeshProUGUI>().text = "" + disRun;
        yield return new WaitForSeconds(disDelay);
        addingDis = false;
    }
}
