using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 2f;

    public IEnumerator StartSceneTransition(string SceneName)
    {
        transition.SetTrigger("Exit");

        yield return new WaitForSeconds(transitionTime);

        if (SceneName != null)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
