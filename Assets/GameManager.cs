using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int lives = 2;


    // singleton: if already one session exists, any time a new session is instantiated it destroyds itself

    private void Awake()
    {
        int gameSessions = FindObjectsOfType<GameManager>().Length;
        if(gameSessions > 1)
        {
            Destroy(gameObject);
        } else
        {
            // when scene is restartet, this makes sure the instance continues to exist
            DontDestroyOnLoad(gameObject);
        }
    }

    public void playerDeath()
    {
        if(lives > 1)
        {
            DecrementLife();

        } else
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }


    public void DecrementLife()
    {
        lives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
