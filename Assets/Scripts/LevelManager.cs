using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public int LayerId;
    public string LayerName;
    public MiniImage[] MasksPieces;
    int LastLevel;
    public int level_index;

    // Start is called before the first frame update
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);

    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void CheckWin()
    {
        if (AllPatternsRight())
        {
            //Debug.Log("Right");
            CompleteLevel();
        }
        else
        {
           // Debug.Log("wrong");
        }
    }
    public void CompleteLevel()
    {
        //Invoke("NextLevel", NextLevelDelay);
        level_index++;
        PlayerPrefs.SetInt("Last_Level", level_index);
        NextScene();
    }
    bool AllPatternsRight()
    {
        for (int i = 0; i < MasksPieces.Length; ++i)
        {
        }

        return true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine("FindPieces");
    }
    public void NextScene()
    {
        if (level_index == 6)
        {
            SceneManager.LoadScene(0);
            PlayerPrefs.SetInt("Last_Level", 1);
            Destroy(this.gameObject);
        }
        else
        {
            SceneManager.LoadScene(level_index);
            StartCoroutine("FindPieces");
        }
    }
    IEnumerator FindPieces()
    {
        yield return new WaitForSeconds(0.1f);
        MasksPieces = FindObjectsOfType<MiniImage>();
    }
    public void PlayButton()
    {
        if (PlayerPrefs.GetInt("Last_Level") == 0 || PlayerPrefs.GetInt("Last_Level") == 3)
        {
            PlayerPrefs.SetInt("Last_Level", 1);
        }
        else
        {
            level_index = PlayerPrefs.GetInt("Last_Level");
        }
        SceneManager.LoadScene(level_index);
        StartCoroutine("FindPieces");

    }
}
