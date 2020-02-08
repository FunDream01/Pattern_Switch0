﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject completed;

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
       transform.GetChild(5).gameObject.SetActive(true);
    }

    public void Lost()
    {
        lost = true;
        won = false;

    }



    void Update()
    {

        if(won)
        {
            var a = transform.GetChild(1);
            var b = transform.GetChild(2);
            float n = Mathf.SmoothDamp(a.position.x, GetComponent<RectTransform>().rect.width/2+a.GetComponent<RectTransform>().rect.width ,  ref vel , 1.40f);
            a.position = new Vector3(n,a.position.y,a.position.z);
            b.position = new Vector3(n,b.position.y,b.position.z);
            
            
            var c = transform.GetChild(0);
            float m = Mathf.SmoothDamp(c.position.x, -GetComponent<RectTransform>().rect.width/2-c.GetComponent<RectTransform>().rect.width ,  ref vel2 , 1.40f);
            c.position = new Vector3(m,c.position.y,c.position.z);

            var i = transform.GetChild(3);
            float v = Mathf.SmoothDamp(i.position.y, 600,  ref vel3 , 0.6f);
            i.position = new Vector3(i.position.x,v,i.position.z);



            var f = transform.GetChild(4);
            float d = Mathf.SmoothDamp(f.position.y, 50,  ref vel4 , 0.6f);
            f.position = new Vector3(f.position.x,d,f.position.z);



        }

        if(lost)
        {
            var a = transform.GetChild(1);
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


            
        }


    }
}
