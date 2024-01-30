using UnityEngine;
using UnityEngine.SceneManagement;


public class DebugGame : MonoBehaviour {

    int currSceneIdx;
    int sceneCount;

    void Start() {
        
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        sceneCount = SceneManager.sceneCountInBuildSettings;

    }

    void Update() {

        // Key L (next level)
        bool LKeyIsPressed = Input.GetKey(KeyCode.L);

        if (LKeyIsPressed) {

            Debug.Log(currSceneIdx);
            Debug.Log(sceneCount);

            if (currSceneIdx + 1 == sceneCount) {
                SceneManager.LoadScene(Constants.FirstSceneIdx);
                currSceneIdx = Constants.FirstSceneIdx;
            }
            else {
                SceneManager.LoadScene(++currSceneIdx);
            }
        }

    }
}