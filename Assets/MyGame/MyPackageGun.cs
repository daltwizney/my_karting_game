using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPackageGun : MonoBehaviour
{
    public GameObject packagePrefab;

    public float forceStrength = 100.0f;

    public Rigidbody kartRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var package = GameObject.Instantiate(packagePrefab);
            package.transform.position = this.transform.position;

            var packageRB = package.GetComponent<Rigidbody>();
            packageRB.velocity = kartRigidbody.velocity;

            var force = (this.transform.forward + this.transform.up) * forceStrength;

            packageRB.AddRelativeForce(force, ForceMode.Acceleration);
        }
    }
}
