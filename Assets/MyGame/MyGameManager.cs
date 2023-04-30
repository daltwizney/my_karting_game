using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour
{
    enum State
    {
        Delivering,
        PickingUp,
        GamePaused
    }

    public MyGameUI gameUI;

    public MyPackageGun packageGun;

    public DeliveryZone[] deliveryZones;
    public PickupZone[] pickupZones;

    public PickupZone currentPickupZone = null;

    public DeliveryZone currentDeliveryZone = null;

    public float deliveryTimeout = 30.0f;
    public float pickupTimeout = 30.0f;

    public int deliveryPoints = 10;

    public int totalDeliveriesPerGame = 3;

    private State _currentState = State.PickingUp;

    private int _score = 0;
    private float _timer = 0.0f;

    private int _deliveriesAttempted = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

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

        this.gameUI.SetRemainingDeliveryAttempts(this.totalDeliveriesPerGame);

        this.gameUI.unpauseButton.onClick.AddListener(this.unpauseGame);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("MyKartGame");
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
            _score += deliveryPoints;

            deactivateAllZones();
            selectRandomPickupZone();
        }
    }

    void setState(State newState)
    {
        _currentState = newState;

        if (_currentState == State.Delivering)
        {
            gameUI.SetPackageGunEnabled(true);
            packageGun.enabled = true;
        }
        else if (_currentState == State.PickingUp)
        {
            gameUI.SetPackageGunEnabled(false);

            _deliveriesAttempted++;

            this.gameUI.SetRemainingDeliveryAttempts(this.totalDeliveriesPerGame - _deliveriesAttempted);

            if (_deliveriesAttempted > this.totalDeliveriesPerGame)
            {
                Debug.Log("Game Over - you won!");
                gameUI.ShowGameOverMenu();
                Time.timeScale = 0.0f;
            }

            packageGun.enabled = false;
        }
    }

    void selectRandomPickupZone()
    {
        int index = Random.Range(0, pickupZones.Length);

        currentPickupZone = pickupZones[index];

        currentPickupZone.Activate();

        this.setState(State.PickingUp);

        _timer = pickupTimeout;
    }

    void selectRandomDeliveryZone()
    {
        int index = Random.Range(0, deliveryZones.Length);

        currentDeliveryZone = deliveryZones[index];

        this.setState(State.Delivering);

        currentDeliveryZone.Activate();

        _timer = deliveryTimeout;
    }

    void deactivateAllZones()
    {
        deliveryZones.ToList().ForEach(zone => zone.Deactivate());
        pickupZones.ToList().ForEach(zone => zone.Deactivate());
    }

    void unpauseGame()
    {
        Time.timeScale = 1;
        this.gameUI.HidePauseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        // update timer
        _timer -= Time.deltaTime;

        // check if time ran out
        if (_timer < 0.0f)
        {
            _score -= deliveryPoints;

            deactivateAllZones();
            selectRandomPickupZone();
        }

        // update UI
        if (_currentState == State.Delivering)
        {
            gameUI.SetDeliveryClockTime(_timer);
        }
        else if (_currentState == State.PickingUp)
        {
            gameUI.SetPickupClockTime(_timer);
        }

        gameUI.SetGameScore(_score);

        // handle game controls
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            // pause game
            Time.timeScale = 0;
            this.gameUI.ShowPauseMenu();
        }
    }
}
