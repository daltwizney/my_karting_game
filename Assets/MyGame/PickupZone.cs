using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupZone : MonoBehaviour
{
    public Action OnPackagePickedUp;

    public bool hasPackage = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player ran through me!");
        }
    }

    public void Activate()
    {

    }

    public void Deactivate()
    {

    }
}
