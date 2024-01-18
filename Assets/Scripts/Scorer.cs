using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    private int score = 0;
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Hit")  {
            score++;
            Debug.Log("You've bumped into a new obstacle!");
        }
        else {
            Debug.Log("Already bumped into this one!");
        }
        // Debug.Log($"You've bumped into a thing this many times: {++score}");
    }
}
