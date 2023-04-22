using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public DeliveryZone[] deliveryZones;
    public PickupZone[] pickupZones;

    public PickupZone currentPickupZone = null;

    public DeliveryZone currentDeliveryZone = null;

    // Start is called before the first frame update
    void Start()
    {
        deliveryZones = GameObject.FindObjectsOfType<DeliveryZone>();
        pickupZones = GameObject.FindObjectsOfType<PickupZone>();

        for (int i = 0; i < deliveryZones.Length; ++i)
        {
            deliveryZones[i].OnPackageReceived += this.handlePackageDelivered;
        }

        for (int i = 0; i < pickupZones.Length; ++i)
        {
            pickupZones[i].OnPackagePickedUp += this.handlePackagePickup;
        }

        selectRandomPickupZone();
    }

    void handlePackagePickup(PickupZone zone)
    {
        if (zone == currentPickupZone)
        {
            currentPickupZone.Deactivate();
            selectRandomDeliveryZone();
        }
    }

    void handlePackageDelivered(DeliveryZone zone)
    {
        if (zone == currentDeliveryZone)
        {
            currentDeliveryZone.Deactivate();

            selectRandomPickupZone();
        }
    }

    void selectRandomPickupZone()
    {
        int index = Random.Range(0, pickupZones.Length - 1);

        currentPickupZone = pickupZones[index];

        currentPickupZone.Activate();
    }

    void selectRandomDeliveryZone()
    {
        int index = Random.Range(0, deliveryZones.Length - 1);

        currentDeliveryZone = deliveryZones[index];

        currentDeliveryZone.Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
