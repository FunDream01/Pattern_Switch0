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

    float vel = 40.0f;
    float vel2 = 10.0f;

    float vel3 = 50f;
    float vel4 = 50f;

    float vel5 = 50f;

    public int level_index;

    GameObject img;
    public GameObject clearScreenUIElementWinning,clearScreenUIElementLosing;
    public GameObject textElement;

    public void HomeButton()
    {

    }

     public void NextButton()
    {

            
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void Start()
    {
    var x =transform.GetChild(3);
    x.GetComponent<AspectRatioFitter>().enabled=true;
    Destroy(x.GetComponent<AspectRatioFitter>());
    x.transform.position += new Vector3(0,600,0);
    }
    

    public void Won()
    {
       won = true;
       clearScreenUIElementWinning.SetActive(true);
    }

    public void Lost(string colorName)
    {
        lost = true;
        won = false;
        textElement.GetComponent<TextMeshProUGUI>().text = "Color " + colorName + "\n is lost." ;
        textElement.GetComponent<TextMeshProUGUI>().UpdateFontAsset();
       clearScreenUIElementLosing.SetActive(true);

    }



    void Update()
    {
        if(won || lost)
         for(int i = 0; i < transform.childCount; i++)
        {

            transform.GetChild(i).TryGetComponent<Animator>( out Animator t);
            if( t != null && (t.gameObject.name != "next_button_dont_change_name" && t.gameObject.name != "replay_button_dont_change_name" )) t.enabled = true; 
            
        }

        if(won)
        {
            transform.GetChild(0).GetComponent<Animator>().enabled = true;
        }

        if(lost)
        {
            transform.GetChild(1).GetComponent<Animator>().enabled = true;



            /* var a = transform.GetChild(1);
            var b = transform.GetChild(2);
            float n = Mathf.SmoothDamp(a.position.x, GetComponent<RectTransform>().rect.width/2+a.GetComponent<RectTransform>().rect.width ,  ref vel , 1.40f);
            a.position = new Vector3(n,a.position.y,a.position.z);
            b.position = new Vector3(n,b.position.y,b.position.z);
            
            
            var c = transform.GetChild(0);
            float m = Mathf.SmoothDamp(c.position.x, -GetComponent<RectTransform>().rect.width/2-c.GetComponent<RectTransform>().rect.width ,  ref vel2 , 1.40f);
            c.position = new Vector3(m,c.position.y,c.position.z);



            var f = transform.GetChild(6);
            float d = Mathf.SmoothDamp(f.position.y, 0,  ref vel5 , 0.6f);
            f.position = new Vector3(f.position.x,d,f.position.z);

 */
            
        }


    }
}
