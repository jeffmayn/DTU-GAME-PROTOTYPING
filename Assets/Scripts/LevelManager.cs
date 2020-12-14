using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameObject boss;
    bool alive = true;
   [SerializeField] float loadingDelay = 2f;

    private void Start()
    {
        boss = GameObject.FindWithTag("Boss");
    }

 

    private void OnTriggerEnter(Collider other)
    {

        alive = boss.GetComponent<EnemyController>().isEnemyAlive();
       
        if (!alive)
        {
            StartCoroutine(Load());
        }
        
    }

    IEnumerator Load()
    {
        yield return new WaitForSecondsRealtime(loadingDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
