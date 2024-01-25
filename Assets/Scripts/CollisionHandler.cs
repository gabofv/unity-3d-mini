using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

static class Constants {
    public const int FirstSceneIdx = 0;
}

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashDelay = 1f;
    [SerializeField] float successDelay = 1f;
    [SerializeField] AudioClip explosionAudio;
    [SerializeField] AudioClip successAudio;

    AudioSource audioSource;

    // Create a new class in charge of loading scenes!
    int currSceneIdx;
    int sceneCount;

    bool isGameLive = true;

    void Start() {

        currSceneIdx = SceneManager.GetActiveScene().buildIndex;

        // Should it be included only in the method used?
        sceneCount = SceneManager.sceneCountInBuildSettings;

        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) {

        if (!isGameLive) {
            return;
        }

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

        isGameLive = false;

        GetComponent<Movement>().enabled = false;

        audioSource.Stop();

        audioSource.PlayOneShot(explosionAudio);

        Invoke(nameof(ReloadLevel), crashDelay);

    }

    void StartSuccessSequence() {

        // todo add SFX upon success
        // todo add animation upon success

        isGameLive = false;

        GetComponent<Movement>().enabled = false;

        audioSource.Stop();

        audioSource.PlayOneShot(successAudio);

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
