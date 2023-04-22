using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class MyGameManager : MonoBehaviour
{
    enum State
    {
        Delivering,
        PickingUp
    }

    public MyGameUI _gameUI;

    public DeliveryZone[] deliveryZones;
    public PickupZone[] pickupZones;

    public PickupZone currentPickupZone = null;

    public DeliveryZone currentDeliveryZone = null;

    public float deliveryTimeout = 30.0f;
    public float pickupTimeout = 30.0f;

    private State _currentState = State.PickingUp;

    private float _timer = 0.0f;

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
            deactivateAllZones();
            selectRandomDeliveryZone();
        }
    }

    void handlePackageDelivered(DeliveryZone zone)
    {
        if (zone == currentDeliveryZone)
        {
            deactivateAllZones();
            selectRandomPickupZone();
        }
    }

    void selectRandomPickupZone()
    {
        int index = Random.Range(0, pickupZones.Length - 1);

        currentPickupZone = pickupZones[index];

        currentPickupZone.Activate();

        _currentState = State.PickingUp;

        _timer = pickupTimeout;
    }

    void selectRandomDeliveryZone()
    {
        int index = Random.Range(0, deliveryZones.Length - 1);

        currentDeliveryZone = deliveryZones[index];

        _currentState = State.Delivering;

        currentDeliveryZone.Activate();

        _timer = deliveryTimeout;
    }

    void deactivateAllZones()
    {
        deliveryZones.ToList().ForEach(zone => zone.Deactivate());
        pickupZones.ToList().ForEach(zone => zone.Deactivate());
    }

    // Update is called once per frame
    void Update()
    {
        // update timer
        _timer -= Time.deltaTime;

        // check if time ran out
        if (_timer < 0.0f)
        {
            deactivateAllZones();
            selectRandomPickupZone();
        }

        // update UI
        if (_currentState == State.Delivering)
        {
            _gameUI.SetDeliveryClockTime(_timer);
        }
        else if (_currentState == State.PickingUp)
        {
            _gameUI.SetPickupClockTime(_timer);
        }
    }
}
