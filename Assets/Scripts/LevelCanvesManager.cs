using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelCanvesManager : MonoBehaviour
{


    bool won = false;

    bool lost = false;

    AnalyticsController analyticsController;
    public GameObject facebookObj;

    public GameObject clearScreenUIElementWinning, clearScreenUIElementLosing;
    public GameObject textElement, levelCompleteAsset;
    FacebookController facebook;

    public TextMeshProUGUI levelText;

    public void NextButton()
    {

        SceneManager.LoadScene(Random.Range(4,21));


    }
    public void RestartButton()
    {
        analyticsController.LogLevelFailed(SceneManager.GetActiveScene().buildIndex);
        facebook.LogLevelFailure(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    void Start()
    {
        int lvl = PlayerPrefs.GetInt("level", 999);
        
        levelText.text = "level " + lvl;
        Debug.Log(lvl);
        analyticsController = new AnalyticsController();
        levelCompleteAsset.GetComponent<AspectRatioFitter>().enabled = true;
        Destroy(levelCompleteAsset.GetComponent<AspectRatioFitter>());
        levelCompleteAsset.transform.position += new Vector3(0, 1000, 0);
        analyticsController.LogLevelStarted(SceneManager.GetActiveScene().buildIndex);
        facebook = FindObjectOfType<FacebookController>();
        if (facebook == null)
        {
            Instantiate(facebookObj);
            facebook = FindObjectOfType<FacebookController>();
        }
        facebook.LogLevelStart(SceneManager.GetActiveScene().buildIndex);
        

    }


    public void Won()
    {
        won = true;
        clearScreenUIElementWinning.SetActive(true);
        analyticsController.LogLevelSucceeded(SceneManager.GetActiveScene().buildIndex);
        facebook.LogLevelSuccess(SceneManager.GetActiveScene().buildIndex);
        int lvl = PlayerPrefs.GetInt("level");
        PlayerPrefs.SetInt("level", lvl+1);
        PlayerPrefs.Save();

    }

    public void Lost(string colorName)
    {
        lost = true;
        won = false;
        textElement.GetComponent<TextMeshProUGUI>().text = "Color " + colorName + "\n is lost.";
        clearScreenUIElementLosing.SetActive(true);
        analyticsController.LogLevelFailed(SceneManager.GetActiveScene().buildIndex);
        facebook.LogLevelFailure(SceneManager.GetActiveScene().buildIndex);

    }
 IEnumerator blur()
    {
        Camera.main.GetComponent<SuperBlur.SuperBlurFast>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        Camera.main.GetComponent<SuperBlur.SuperBlurFast>().iterations = 2;
        yield return new WaitForSeconds(0.05f);
        Camera.main.GetComponent<SuperBlur.SuperBlurFast>().iterations = 3;
    }



    void Update()
    {
        /*
        if(levelText.text != "Level " + SceneManager.GetActiveScene().buildIndex)
        levelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        */
        string button_string = "";
        if (won)
        {
            button_string = "Next";
            Camera.main.GetComponent<Animator>().enabled = true;
        }
        if (lost)
        {
            button_string = "Replay";
        }
        if (won || lost) 
        {StartCoroutine(waitAnim(button_string)); won=false; lost=false;}
       
    }

    IEnumerator waitAnim(string button_string)
    {
        if(Camera.main.GetComponent<Animator>().enabled)
        while (Camera.main.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1){ yield return new WaitForEndOfFrame();
}
         for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).TryGetComponent<Animator>(out Animator t);
                if (t != null)
                { t.enabled = true; }
                if (transform.GetChild(i).gameObject.tag == "Replay")
                {
                    transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = button_string;
                }
            }

            StartCoroutine(blur());

    }
}
