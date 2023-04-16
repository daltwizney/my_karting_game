using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPackage : MonoBehaviour
{
    public float lifetime = 5.0f;

    private float _timeSinceAwake = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceAwake += Time.deltaTime;

        if (_timeSinceAwake > lifetime)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
