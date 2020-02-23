using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    public GameObject ArrowBase;
    public GameObject ArrowHead;
    Vector3 CameraOffset = new Vector3(0, 0, 10);
    Vector3 StartPos, EndPos;

    GameObject g,gs,a;

    GameManager gameManager;
    
     Camera cam;

    float origyscale;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        cam=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       
        bool inside = false;
        if (Input.GetMouseButton(0))
        {
            EndPos= cam.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;
            
            foreach(Bounds b in gameManager.g_pieces)
            {
                if(b.Contains(EndPos)){
                    inside=true;
                }
            }
           
        }

         if (Input.GetMouseButtonDown(0))
        {

            if (gameManager.PieceSelected && inside)
            {
                StartPos = cam.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;
                g = Instantiate(ArrowBase,StartPos,Quaternion.identity);
                origyscale = g.transform.localScale.y;
                a = Instantiate(ArrowHead,StartPos,Quaternion.identity);
                gs = Instantiate(ArrowHead,StartPos,Quaternion.identity);
            }

        }

        if (Input.GetMouseButtonUp(0) || !inside)
        {
            Destroy(g);
            Destroy(gs);
            Destroy(a);
            gameManager.PieceSelected = false;
            gameManager.LayerId = 0;
        }

        

        Vector3 diff = EndPos - StartPos;
        if(g!=null && a!=null && gs !=null)
        {
            float yscale = origyscale;
            if(StartPos.x > EndPos.x) yscale *=-1; 
            g.transform.localScale = new Vector3(diff.magnitude,yscale,g.transform.localScale.z);
            float angle = Vector2.Angle(new Vector2(1,0),diff);
            a.transform.position = EndPos;
            if(EndPos.y > StartPos.y) angle = -angle;
            g.transform.rotation = Quaternion.Euler(g.transform.rotation.eulerAngles.x,g.transform.rotation.eulerAngles.y,-angle);
        
        }
        
    }
}
