using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
    SpriteMask spriteMask;
    public int LayerId, oldLayerID;
    GameManager gameManager;

    bool tempd = false, scaled = false;
    BoxCollider2D Collider;
    [HideInInspector]
    public GameObject ChildReffrence;
    public float AddToSize = 0.0f;
    GameObject temp;

    float fix = 1.0f;


    Transform target;
    float smoothTime = 0.2f;
    float yVelocity = 15f, sVelocity = 5.0f;
    float scaler = 1.0f;

    float scale;
    enum Direction { LEFT, RIGHT, UP, DOWN }
    Direction direction;

    GameObject go, go2;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;
        if (ChildReffrence == null)
        {
            ChildReffrence = gameObject.transform.GetChild(0).gameObject;
        }
        Collider = new BoxCollider2D();
        Collider = this.gameObject.AddComponent<BoxCollider2D>();
        
        spriteMask = GetComponent<SpriteMask>();
        LayerId = spriteMask.frontSortingLayerID;
        oldLayerID = LayerId;

        gameManager = FindObjectOfType<GameManager>();
    }


    struct Layer_Selection
    {
        public string go_layer;
        public string back_layer;
        public Sprite newsprite;

    }


    Layer_Selection SelectLayer(int id)
    {
        Layer_Selection l = new Layer_Selection();
        switch (SortingLayer.IDToName(id))
        {
            case "Green":
                l.newsprite = gameManager.patterns[0];
                l.go_layer = "green_front";
                l.back_layer = "Default";
                break;
            case "Pink":
                l.newsprite = gameManager.patterns[1];
                l.go_layer = "pink_front";
                l.back_layer = "green_front";

                break;
            case "Orange":
                l.newsprite = gameManager.patterns[2];
                l.back_layer = "pink_front";
                l.go_layer = "orange_front";

                break;
            case "Blue":
                l.newsprite = gameManager.patterns[3];
                l.back_layer = "orange_front";
                l.go_layer = "blue_front";

                break;
            case "Purple":
                l.newsprite = gameManager.patterns[4];
                l.back_layer = "blue_front";
                l.go_layer = "purple_front";

                break;
            default:
                l.newsprite = gameManager.patterns[0];
                l.back_layer = "Default";
                l.go_layer = "green_front";

                break;
        }
        return l;
    }

    void Update()
    {
        if (tempd)
        {
            if (go == null)
            {

                var l = SelectLayer(LayerId);
                go = new GameObject();
                go.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                go.transform.position = new Vector3(-1, 1, 0);
                SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = l.newsprite;
                sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                sr.sortingLayerID = SortingLayer.NameToID(l.go_layer);
                go.SetActive(true);
                go2 = Instantiate(go);
                go2.transform.position = new Vector3(5.14f, 1f, 0);
                temp.GetComponent<SpriteMask>().frontSortingLayerID = sr.sortingLayerID;
                temp.GetComponent<SpriteMask>().backSortingLayerID = SortingLayer.NameToID(l.back_layer);
            }

            if (gameManager.animationScale <= 1.005f) { scaler = 1.0f; scaled = true; }

            if (!scaled)
            {
                    scaler = Mathf.SmoothDamp(scaler, gameManager.animationScale, ref sVelocity, smoothTime);
                    if (scaler > (gameManager.animationScale - 0.03f)) scaled = true;
            }
            else
            {
                scaler = Mathf.SmoothDamp(scaler, 1.0f, ref sVelocity, smoothTime);
            }
            scale = Mathf.SmoothDamp(scale, 1.0f, ref yVelocity, smoothTime);


            if (direction == Direction.LEFT)
            {
                temp.transform.localScale = new Vector3(scale, 1.0f, 1.0f);
                temp.transform.localPosition = new Vector3((scale - 1) * fix, 0, 0);

            }
            else if (direction == Direction.RIGHT)
            {
                temp.transform.localScale = new Vector3(scale, 1.0f, 1.0f);
                temp.transform.localPosition = new Vector3((1 - scale) * fix, 0, 0);
            }
            else if (direction == Direction.UP)
            {
                temp.transform.localScale = new Vector3(1.0f, scale, 1.0f);
                temp.transform.localPosition = new Vector3(0, -scale + 1, 0);

            }
            else if (direction == Direction.DOWN)
            {
                temp.transform.localScale = new Vector3(1.0f, scale, 1.0f);
                temp.transform.localPosition = new Vector3(0, scale - 1, 0);
            }
            
            
            temp.transform.localScale *= scaler;
            if (scaler <= 1.0005f && scaled) { scaler = 1.0f; scaled = true; }

            if ((new Vector3(1f, 1f, 1f) - temp.transform.localScale).magnitude <= 0.01 && scaler <= 1.01f && scaled)
            { yVelocity = 15f; sVelocity = 5.0f; spriteMask.frontSortingLayerID = LayerId; Destroy(temp); tempd = false; if (go != null) Destroy(go); if (go2 != null) Destroy(go2); scaled = false; scaler = 1.0f; scale = 0.0f; }


        }


    }
    void OnMouseDown()
    {
        gameManager.LayerId = LayerId;
        gameManager.LayerName = SortingLayer.IDToName(LayerId);
        gameManager.PieceSelected = true;
        gameManager.road = new LinkedList<MaskManager>();
        gameManager.road.AddFirst(new LinkedListNode<MaskManager>(this));
        
    }


    
    

    public void transition(int layer)
    {
        

      //  if(spriteMask.frontSortingLayerID == layer) return;
        if(layer == LayerId) return;

        if (layer != 0)
        {
            
                if(temp!=null)
                 Destroy(temp);
                tempd = false;
                 if (go != null) 
                 Destroy(go); 
                 if (go2 != null) 
                 Destroy(go2); 
                 scaled = false; 
                 scaler = 1.0f; 
                 scale = 0.0f;
               
               
                /*
                if (go != null)
                {
                    Debug.Log("changed go");
                    var s1 = go.GetComponent<SpriteRenderer>();
                    s1.sprite = l.newsprite;
                    s1.sortingLayerID = SortingLayer.NameToID(l.go_layer);

                    var s2 = go2.GetComponent<SpriteRenderer>();
                    s2.sprite = l.newsprite;
                    s2.sortingLayerID = SortingLayer.NameToID(l.go_layer);
                }
                */

                yVelocity = 15f; 
                sVelocity = 5.0f;

                temp = new GameObject("Animation Object");
                temp.transform.parent = transform.parent;
                temp.transform.position = Vector3.zero;
                temp.transform.SetParent(transform, false);

                var s = temp.AddComponent<SpriteMask>();
                s.isCustomRangeActive = true;
                s.sprite = spriteMask.sprite;
                Debug.Log(GetComponent<Collider2D>().bounds.size);
                if (s.sprite.name == "32x128") fix = 2.0f; else fix = 1.0f;
                s.alphaCutoff = 0.2f;
                tempd = true;
                temp.transform.localScale = Vector3.zero;
            

                oldLayerID = LayerId;
                LayerId = layer;
                var l = SelectLayer(LayerId);
                temp.GetComponent<SpriteMask>().frontSortingLayerID = SortingLayer.NameToID(l.go_layer);
                temp.GetComponent<SpriteMask>().backSortingLayerID = SortingLayer.NameToID(l.back_layer);


           

        }
    }

    public void revertDirection()
    { switch(direction)
    {
        case Direction.LEFT:
        direction = Direction.RIGHT;
        break;

        case Direction.RIGHT:
        direction = Direction.LEFT;
        break;

        case Direction.UP:
        direction = Direction.DOWN;
        break;

        case Direction.DOWN:
        direction = Direction.UP;
        break;
    }
    }

    private void OnMouseEnter()
    {


        LinkedListNode<MaskManager> transitionFrom = gameManager.road.Last;
        bool isUndoAction = false;
        if (gameManager.LayerId != 0 && transitionFrom != null && transitionFrom.Previous != null && transitionFrom.Previous.Value == this) 
        {
            transitionFrom.Value.revertDirection();
            transitionFrom.Value.transition(transitionFrom.Value.oldLayerID);
            gameManager.road.RemoveLast();
            gameManager.road.RemoveLast();
            isUndoAction = true;
            
        }

        else if (gameManager.LayerId != 0 && gameManager.PieceSelected)
            
        {
            direction = relativeMousePosition();
            transition(gameManager.LayerId);
        }

        if(!isUndoAction && gameManager.road.Contains(this))
        {
            gameManager.road = new LinkedList<MaskManager>();
        }

        gameManager.road.AddLast(new LinkedListNode<MaskManager>(this));

    }


    Direction relativeMousePosition()
    {
         Vector3 diff = ((cam.ScreenToWorldPoint(Input.mousePosition) - transform.position));
        diff.x += transform.localScale.x;
        diff.y -= transform.localScale.y;
        diff.y *= -1;
        float[] diffs = new float[4];
        diffs[0] = diff.x; //LEFT
        diffs[1] = transform.localScale.x * 2 - diff.x; //RIGHT
        diffs[2] = diff.y; //UP
        diffs[3] = transform.localScale.y * 2 - diff.y; //DOWN
        float smallest = diffs[0];
        int smallest_i = 0;
        for (int i = 1; i < 4; i++)
        {
            if (diffs[i] < smallest)
            { smallest = diffs[i]; smallest_i = i; }
        }
        return (Direction) smallest_i;
    }
    private void OnMouseUp()
    {
        gameManager.LayerId = 0;
        gameManager.road = new LinkedList<MaskManager>();
        gameManager.CheckWin();
        foreach (MaskManager mm in FindObjectsOfType<MaskManager>())
        {mm.oldLayerID = mm.LayerId;}

    }

    int q = 0;
    private void OnMouseExit()
    {
    }

    public bool IsPatternRight()
    {
        if (this.LayerId == ChildReffrence.GetComponent<SpriteMask>().frontSortingLayerID)
        {
            return true;
        }
        return false;
    }
}
