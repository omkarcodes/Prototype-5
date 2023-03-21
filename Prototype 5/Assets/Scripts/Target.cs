using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minForce = 12;
    private float maxForce = 15;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = 2;


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(ApplyForce(), ForceMode.Impulse);
        targetRb.AddTorque(ApplyTorque(), ApplyTorque(), ApplyTorque());

        transform.position = SetPosition();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    Vector3 ApplyForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    float ApplyTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 SetPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), -ySpawnPos, 0);
    }
}
