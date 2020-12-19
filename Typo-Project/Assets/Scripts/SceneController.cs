using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public float loadDelay = 1f;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadScene_(sceneName));
    }
    public void LoadSceneNoDelay(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private IEnumerator LoadScene_(string sceneName)
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneName);
    }
}
