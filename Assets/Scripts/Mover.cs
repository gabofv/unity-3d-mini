using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // [SerializeField] float xVal = 0.02f;
    [SerializeField] float yVal = 0f;
    // [SerializeField] float zVal = 0f;

    [SerializeField] float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        PrintInstruction();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();        
    }

    void PrintInstruction() {
        Debug.Log("Holaaaaa!");
    }

    void MovePlayer() {

        float xVal = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zVal = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Translate(xVal, yVal, zVal);
    }
}
