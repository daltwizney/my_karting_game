using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public Action<DeliveryZone> OnPackageReceived;

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

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MyPackage>() != null)
        {
            if (this._isActivated)
            {
                this.OnPackageReceived(this);
            }
        }
    }
}
