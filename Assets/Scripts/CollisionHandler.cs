using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have collided with a FRIENDLY object!");
                break;
            case "Finish":
                Debug.Log("You have reached the FINISH object!");
                break;
            case "Fuel":
                Debug.Log("You have encountered FUEL!");
                break;
            default:
                Debug.Log("You blew up! :(");
                ReloadLevel();
                break;
        }
    }

    private void ReloadLevel() {

        int currSceneIdx = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currSceneIdx);
    }
}
