using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
    SpriteMask spriteMask;
    public int LayerId;
    GameManager gameManager;

    bool tempd = false, scaled = false;
    bool newerfront = false;
    public int FinalLayerId;
    BoxCollider2D Collider;
    GameObject ChildReffrence;
    public float AddToSize = 0.0f;
    GameObject temp;
    public Vector2 Size;

    float fix = 1.0f;


    Transform target;
    float smoothTime = 0.3f;
    float yVelocity = 5f, sVelocity = 3.0f;
    float scaler = 1.0f;
    float max_scale;

    float scale;
    enum Direction { LEFT, RIGHT, UP, DOWN }
    Direction direction;

    GameObject go,go2;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {

        camera = Camera.main;
        if (ChildReffrence == null)
        {
            ChildReffrence = gameObject.transform.GetChild(0).gameObject;
        }
        Collider = new BoxCollider2D();
        // add colllider 
        Collider = this.gameObject.AddComponent<BoxCollider2D>();
        //Size = new Vector2(Collider.size.x+(AddToSize/3), Collider.size.y + AddToSize);
        //Collider.size= Size;
        spriteMask = GetComponent<SpriteMask>();
        LayerId = spriteMask.frontSortingLayerID;
        gameManager = FindObjectOfType<GameManager>();
        max_scale = gameManager.animationScale;
    }
    // Update is called once per frame
    void Update()
    {

        if (tempd)
        {
            if (go == null)
            {
                Sprite newsprite;
                switch (SortingLayer.IDToName(LayerId))
                {
                    case "Green":
                        newsprite = gameManager.patterns[0];
                        break;
                    case "Pink":
                        newsprite = gameManager.patterns[1];
                        break;
                    case "Orange":
                        newsprite = gameManager.patterns[2];
                        break;
                    case "Blue":
                        newsprite = gameManager.patterns[3];
                        break;
                    case "Purple":
                        newsprite = gameManager.patterns[4];
                        break;
                    default:
                        newsprite = gameManager.patterns[0];
                        break;
                }

                go = new GameObject();
                go.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                go.transform.position = new Vector3(-1, 1, 0);
                SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = newsprite;
                sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                sr.sortingLayerID = SortingLayer.NameToID("frontfront");
                go.SetActive(true);
                go2 = Instantiate(go);
                go2.transform.position = new Vector3(5.14f,1f,0);
                temp.GetComponent<SpriteMask>().frontSortingLayerID = sr.sortingLayerID;
            }

            if(max_scale <= 1.005f) {scaler = 1.0f;scaled = true;}


            if (direction == Direction.LEFT)
            {
                if (!scaled)
                {
                    scaler = Mathf.SmoothDamp(scaler, max_scale, ref sVelocity, smoothTime);
                    if (scaler > (max_scale - 0.03f)) scaled = true;
                }
                else
                {
                    scaler = Mathf.SmoothDamp(scaler, 1.0f, ref sVelocity, smoothTime);
                }
                scale = Mathf.SmoothDamp(scale, 1.0f, ref yVelocity, smoothTime);
                temp.transform.localScale = new Vector3(scale, 1.0f,1.0f);
                temp.transform.localPosition = new Vector3((scale-1)*fix,0,0);
                temp.transform.localScale *= scaler;

            }
            else if (direction == Direction.RIGHT)
            {

                if (!scaled)
                {
                    scaler = Mathf.SmoothDamp(scaler, max_scale, ref sVelocity, smoothTime);
                    if (scaler > (max_scale - 0.03f)) scaled = true;
                }
                else
                {
                    scaler = Mathf.SmoothDamp(scaler, 1.0f, ref sVelocity, smoothTime);
                }

                scale = Mathf.SmoothDamp(scale, 1.0f, ref yVelocity, smoothTime);
                temp.transform.localScale = new Vector3(scale, 1.0f,1.0f);
                temp.transform.localPosition = new Vector3((1-scale)*fix,0,0);
                temp.transform.localScale *= scaler;
            }
            else if (direction == Direction.UP)
            {
                if (!scaled)
                {
                    scaler = Mathf.SmoothDamp(scaler, max_scale, ref sVelocity, smoothTime);
                    if (scaler > (max_scale - 0.03f)) scaled = true;
                }
                else
                {
                    scaler = Mathf.SmoothDamp(scaler, 1.0f, ref sVelocity, smoothTime);
                }

                scale = Mathf.SmoothDamp(scale, 1.0f, ref yVelocity, smoothTime);
                temp.transform.localScale = new Vector3(1.0f, scale, 1.0f);
                temp.transform.localPosition = new Vector3(0,-scale+1,0);
               temp.transform.localScale *= scaler;

            }
            else if (direction == Direction.DOWN)
            {
                if (!scaled)
                {
                    scaler = Mathf.SmoothDamp(scaler, max_scale, ref sVelocity, smoothTime);
                    if (scaler > (max_scale - 0.03f)) scaled = true;
                }
                else
                {
                    scaler = Mathf.SmoothDamp(scaler, 1.0f, ref sVelocity, smoothTime);
                }

                scale = Mathf.SmoothDamp(scale, 1.0f, ref yVelocity, smoothTime);
                temp.transform.localScale = new Vector3(1.0f, scale, 1.0f);
                temp.transform.localPosition = new Vector3(0,scale-1,0);
                temp.transform.localScale *= scaler;

            }
            /*else if (direction == Direction.DOWN)
            {
                scale = Mathf.SmoothDamp(temp.transform.localScale.y, transform.localScale.y, ref yVelocity, smoothTime);
                temp.transform.localScale = new Vector3(transform.localScale.x,scale, transform.localScale.z);
                temp.transform.position = (transform.position) + new Vector3(0,-(transform.localScale.y - scale), 0);
            }*/
            if(scaler <= 1.0005f && scaled) {scaler = 1.0f;scaled = true;}

            if ((new Vector3(1f,1f,1f) -temp.transform.localScale).magnitude <= 0.01 && scaler <= 1.01f && scaled)
            { spriteMask.frontSortingLayerID = LayerId; Destroy(temp); tempd = false; if (go != null) Destroy(go); if(go2!=null) Destroy(go2); scaled = false; scaler = 1.0f; }

            //  Debug.Log(newerfront);

        }


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
        if (gameManager.LayerId != 0 && gameManager.PieceSelected != false && gameManager.LayerId != LayerId)
        {
            if (!tempd)
            {
                temp = new GameObject("temp");
                temp.transform.parent = transform.parent;
                temp.transform.position = Vector3.zero;
                temp.transform.SetParent(transform,false);
                var s = temp.AddComponent<SpriteMask>();
                s.isCustomRangeActive = true;
                s.sprite = spriteMask.sprite;
                if(s.sprite.name=="32x128")fix=2.0f; else fix=1.0f;
                s.frontSortingOrder = spriteMask.frontSortingOrder + 1;
                s.frontSortingLayerID = gameManager.LayerId;
                s.alphaCutoff = 0.2f;
                tempd = true;
                if (SortingLayer.GetLayerValueFromID(gameManager.LayerId) > SortingLayer.GetLayerValueFromID(spriteMask.frontSortingLayerID)) newerfront = true; else newerfront = false;
                Vector3 diff = ((camera.ScreenToWorldPoint(Input.mousePosition) - transform.position));
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

                direction = (Direction)smallest_i;

                temp.transform.localScale = Vector3.zero;
                LayerId = gameManager.LayerId;
            }
            /*if(newerfront)*/
            /*else {
                temp.GetComponent<SpriteMask>().frontSortingLayerID = spriteMask.frontSortingLayerID;
                temp.transform.localScale = transform.localScale;
                spriteMask.frontSortingLayerID = LayerId;
                }
*/

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


        if (this.LayerId == ChildReffrence.GetComponent<SpriteMask>().frontSortingLayerID)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
