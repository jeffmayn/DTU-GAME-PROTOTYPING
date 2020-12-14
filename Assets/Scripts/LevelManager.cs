using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] float loadingDelay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return new WaitForSecondsRealtime(loadingDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
