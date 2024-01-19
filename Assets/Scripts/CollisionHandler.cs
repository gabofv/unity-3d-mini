using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

static class Constants {
    public const int FirstSceneIdx = 0;
}

public class CollisionHandler : MonoBehaviour
{

    // Create a new class in charge of loading scenes!
    int currSceneIdx;
    int sceneCount;
    [SerializeField] float crashDelay = 1f;
    [SerializeField] float successDelay = 1f;

    void Start() {

        currSceneIdx = SceneManager.GetActiveScene().buildIndex;

        // Should it be included only in the method used?
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    void OnCollisionEnter(Collision other) {

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have collided with a FRIENDLY object!");
                break;
            case "Finish":
                Debug.Log("You have reached the FINISH object!");
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("You have encountered FUEL!");
                break;
            default:
                Debug.Log("You blew up! :c");
                StartCrashSequence();
                break;
        }

    }

    void StartCrashSequence() {

        // todo add SFX upon crash
        // todo add crash particles upon crash
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().enabled = false;

        Invoke(nameof(ReloadLevel), crashDelay);

    }

    void StartSuccessSequence() {

        // todo add SFX upon success
        // todo add animation upon success
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().enabled = false;

        Invoke(nameof(LoadNextLevel), successDelay);
    }

    void ReloadLevel() {
        SceneManager.LoadScene(currSceneIdx);
    }

    void LoadNextLevel() {

        int nextSceneIdx = currSceneIdx + 1;

        if (nextSceneIdx == sceneCount) {
            ResetGame();
        }
        else {
            SceneManager.LoadScene(nextSceneIdx);
            currSceneIdx = nextSceneIdx;
        }
    }

    void ResetGame() {
        currSceneIdx = Constants.FirstSceneIdx;
        SceneManager.LoadScene(Constants.FirstSceneIdx);
    }
}
