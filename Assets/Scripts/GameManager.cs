using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int LayerId;
    public string LayerName;
    public MaskManager[] MasksPieces;
    public bool PieceSelected;

    public float animationScale = 81.1f;

    public GameObject complete;

    public GameObject next;

    GameObject c;

    public List<Sprite> patterns;

    [HideInInspector]
    public List<Bounds> g_pieces;

    float vel = 2f;
    

    public float gridPadding = 0.1f;

    // Start is called before the first frame update
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);


    }
    void Start()
    {
        MasksPieces = FindObjectsOfType<MaskManager>();
        foreach (MaskManager MasksPiece in MasksPieces)
        {
            Bounds b = MasksPiece.GetComponent<SpriteMask>().bounds;
            b.extents += new Vector3(gridPadding,gridPadding,0);
            g_pieces.Add(b);
        }
    }
    // Update is called once per frame
    void Update()
    {

    if(c!=null){
    float n = Mathf.SmoothDamp(c.transform.position.y, 5,  ref vel , 2.0f);
    c.transform.position = new Vector3(c.transform.position.x, n, c.transform.position.z);
    }




    }

    IEnumerator blur()
    {

        yield return new  WaitForSeconds(0.05f);
        Camera.main.GetComponent<SuperBlur.SuperBlurFast>().iterations = 2;
        yield return new  WaitForSeconds(0.05f);
        Camera.main.GetComponent<SuperBlur.SuperBlurFast>().iterations = 3;
    }

    public void CheckWin()
    {
        if (AllPatternsRight())
        {
            Debug.Log("Right");

            //c = Instantiate(complete,new Vector3(0,13,50),Quaternion.identity);
            Camera.main.GetComponent<SuperBlur.SuperBlurFast>().enabled = true;
            StartCoroutine(blur());
            LevelCanvesManager lm = FindObjectOfType<LevelCanvesManager>();
            lm.Won();
            
            
            //CompleteLevel();
        }
        else
        {
          //  Debug.Log("wrong");
        }
    }
    bool AllPatternsRight()
    {
        for (int i = 0; i < MasksPieces.Length; ++i)
        {
            if (MasksPieces[i].IsPatternRight() == false)
            {
                return false;
            }
        }

        return true;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
