using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooperCapsule : MonoBehaviour
{
    [SerializeField] float _rotationStep = 1.0f;
    [SerializeField] float _thrustStep = 1.0f;

    Transform _looperOrigin;

    float _verticalOffset;

    // Start is called before the first frame update
    void Start()
    {
        _looperOrigin = transform.parent;
        _verticalOffset = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 relativePos = (_looperOrigin.position + new Vector3(0, _verticalOffset, 0)) - transform.position;
       Quaternion targetRotation = Quaternion.LookRotation(relativePos);

       transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationStep);
       transform.Translate(0, 0, Time.deltaTime * _thrustStep); 
    }
}
