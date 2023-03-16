using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        ApplyForce();
        ApplyTorque();
        SetPosition();
 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ApplyForce()
    {
        int speed = Random.Range(12, 18);
        targetRb.AddForce(Vector3.up * speed, ForceMode.Impulse);
    }

    void ApplyTorque()
    {
        int range = Random.Range(-10, 10);
        targetRb.AddTorque(range, range, range);
    }

    void SetPosition()
    {
        transform.position = new Vector3(Random.Range(-4, 4), -6, 0);
    }
}
