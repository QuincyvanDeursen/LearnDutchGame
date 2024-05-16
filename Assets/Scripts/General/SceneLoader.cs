using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 0.5f;
    private int selectedSceneIndex;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneWithAnim(sceneId));
    }

    public void SetSelectedScene(int sceneId)
    {
        selectedSceneIndex = sceneId;
    }

    public void LoadSelectedScene()
    {
        StartCoroutine(LoadSceneWithAnim(selectedSceneIndex));
    }

    IEnumerator LoadSceneWithAnim(int sceneId)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneId);
    }
}
