using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;

    //[SerializeField] [Range(0, 1)] 
    float movementFactor;

    [SerializeField] [Min(0.1f)] float period = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
    }

    // Update is called once per frame
    void Update()
    {

        // Protect against NaN (div by zero)
        if (period <= Mathf.Epsilon)
            return;

        float sineCycles = Time.time / period;

        const float tau = Mathf.PI * 2;

        float rawSineWave = Mathf.Sin(sineCycles * tau);

        // Instead of range [-1, 1] -> [0, 1]
        movementFactor = (rawSineWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
        
    }
}
