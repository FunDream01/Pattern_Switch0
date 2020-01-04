using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
    SpriteMask spriteMask;
    public int LayerId;
    GameManager gameManager;
    public int FinalLayerId;
    BoxCollider2D Collider;
    GameObject ChildReffrence;
    public float AddToSize=0.5f;
    public Vector2 Size;
    // Start is called before the first frame update
    void Start()
    {
        if (ChildReffrence == null)
        {
            ChildReffrence = gameObject.transform.GetChild(0).gameObject;
        }
        Collider = new BoxCollider2D();
        // add colllider 
        Collider= this.gameObject.AddComponent<BoxCollider2D>();
        //Size = new Vector2(Collider.size.x+(AddToSize/3), Collider.size.y + AddToSize);
        //Collider.size= Size;
        spriteMask = GetComponent<SpriteMask>();
        LayerId = spriteMask.frontSortingLayerID;
        gameManager = FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        gameManager.LayerId = LayerId;
        gameManager.LayerName = SortingLayer.IDToName(LayerId);
        gameManager.PieceSelected = true;
    }
    private void OnMouseEnter()
    {
        //gameManager.PieceSelected = true;
        if (gameManager.LayerId != 0 && gameManager.PieceSelected != false)
        {
            spriteMask.frontSortingLayerID = gameManager.LayerId;
            LayerId = gameManager.LayerId;
        }

    }
    private void OnMouseUp()
    {
        gameManager.LayerId = 0;
        gameManager.CheckWin();
    }
    private void OnMouseExit()
    {
        //gameManager.PieceSelected = false;
    }
    public bool IsPatternRight()
    {
        
        
        if (this.LayerId== ChildReffrence.GetComponent<SpriteMask>().frontSortingLayerID)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
