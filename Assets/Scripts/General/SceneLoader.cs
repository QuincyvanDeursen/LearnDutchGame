using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 0.5f;
    private int selectedSceneIndex;

    private WaitScript waitScript;

    void Start()
    {
        waitScript = gameObject.AddComponent<WaitScript>();
        waitScript.OnWaitCompleted += () => {
            SceneManager.LoadScene(selectedSceneIndex);
        };
    }

    public void SetSelectedScene(int sceneId)
    {
        selectedSceneIndex = sceneId;
    }

    public void LoadSelectedScene() {
        transition.SetTrigger("Start");
        waitScript.StartWait(transitionTime);
    }

    public void LoadScene(int sceneId)
    {
        selectedSceneIndex = sceneId;
        transition.SetTrigger("Start");
        waitScript.StartWait(transitionTime);
    }

}
