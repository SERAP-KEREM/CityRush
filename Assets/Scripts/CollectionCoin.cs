using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionCoin : MonoBehaviour
{
    public AudioSource coinFX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        CollactableControl.coinCount++;
        coinFX.Play();
        this.gameObject.SetActive(false);   
    }
}
