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
    Camera camera;

    float origyscale;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (gameManager.PieceSelected)
            {
                StartPos = camera.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;
                g = Instantiate(ArrowBase,StartPos,Quaternion.identity);
                origyscale = g.transform.localScale.y;
                a = Instantiate(ArrowHead,StartPos,Quaternion.identity);
                gs = Instantiate(ArrowHead,StartPos,Quaternion.identity);

              //  g.transform.GetChild(0).GetComponent<SpriteMask>().frontSortingLayerID = gameManager.LayerId;
             /*   gs = new GameObject("shadow");
                gs.transform.parent = g.transform;
                SpriteMask sr = gs.AddComponent<SpriteMask>();
                sr.sortingOrder = g.transform.GetChild(0).GetComponent<SpriteMask>().sortingOrder -1;
                sr.sprite = ss;
                sr.transform.localScale = new Vector3(5,5,5);
            */
            }

        }
        if (Input.GetMouseButton(0))
        {
            EndPos= camera.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;

        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(g);
            Destroy(gs);
            Destroy(a);
            //line.enabled = false;
        }

        Vector3 diff = EndPos - StartPos;
        if(g!=null && a!=null && gs !=null)
        {

            //Transform gm = g.transform.GetChild(0);
            //gm.localScale = new Vector3(diff.magnitude,gm.transform.localScale.y,gm.transform.localScale.z);
            float yscale = origyscale;
            if(StartPos.x > EndPos.x) yscale *=-1; 
            g.transform.localScale = new Vector3(diff.magnitude,yscale,g.transform.localScale.z);
            float angle = Vector2.Angle(new Vector2(1,0),diff);
            a.transform.position = EndPos;
            if(EndPos.y > StartPos.y) angle = -angle;
         //  a.transform.rotation = Quaternion.Euler(a.transform.rotation.eulerAngles.x,a.transform.rotation.eulerAngles.y,-angle);
            g.transform.rotation = Quaternion.Euler(g.transform.rotation.eulerAngles.x,g.transform.rotation.eulerAngles.y,-angle);
        
        }
        
    }
}
