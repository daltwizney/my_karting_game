using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupZone : MonoBehaviour
{
    public Action<PickupZone> OnPackagePickedUp;

    public bool hasPackage = false;

    public ParticleSystem activationFX;

    private bool _isActivated = false;

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
            if (this._isActivated)
            {
                this.OnPackagePickedUp(this);
            }
        }
    }

    public void Activate()
    {
        _isActivated = true;
        activationFX.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _isActivated = false;
        activationFX.gameObject.SetActive(false);
    }

}
