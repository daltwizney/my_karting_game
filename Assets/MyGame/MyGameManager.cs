using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public DeliveryZone[] deliveryZones;
    public PickupZone[] pickupZones;

    public PickupZone currentPickupZone;

    // Start is called before the first frame update
    void Start()
    {
        deliveryZones = GameObject.FindObjectsOfType<DeliveryZone>();
        pickupZones = GameObject.FindObjectsOfType<PickupZone>();

        selectRandomPickupZone();
    }

    PickupZone selectRandomPickupZone()
    {
        int index = Random.Range(0, pickupZones.Length - 1);

        return pickupZones[index];
    }

    DeliveryZone selectRandomDeliveryZone()
    {
        int index = Random.Range(0, deliveryZones.Length - 1);

        return deliveryZones[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
