using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int LayerId;
    public string LayerName;
    [HideInInspector]
    public MaskManager[] MasksPieces;


    

    [HideInInspector]
    public LinkedList<MaskManager> road;
    public bool PieceSelected;



    public float animationScale;

    GameObject c;

    public List<Sprite> patterns;

    [HideInInspector]
    public List<Bounds> g_pieces;

    float vel = 2f;

    List<int> refs;
    public float gridPadding = 0.1f;

    // Start is called before the first frame update
   
    void Start()
    {
        road = new LinkedList<MaskManager>();
        MasksPieces = FindObjectsOfType<MaskManager>();
        foreach (MaskManager MasksPiece in MasksPieces)
        {
            Bounds b = MasksPiece.GetComponent<SpriteMask>().bounds;
            b.extents += new Vector3(gridPadding, gridPadding, 0);
            g_pieces.Add(b);
        }


        refs = new List<int>();


    }
    // Update is called once per frame
    void Update()
    {
        if(refs.Count == 0)
        {
        foreach (var MaskPiece in MasksPieces)
        {
            bool has_it = false;
            var l_id = MaskPiece.ChildReffrence.GetComponent<SpriteMask>().frontSortingLayerID;
            foreach (var reff in refs) if(reff == l_id) has_it = true;
            if(!has_it) refs.Add(l_id);
        }
       }

        if (c != null)
        {
            float n = Mathf.SmoothDamp(c.transform.position.y, 5, ref vel, 2.0f);
            c.transform.position = new Vector3(c.transform.position.x, n, c.transform.position.z);
        }
    }

   

    

    public void CheckWin()
    {
        if (AllPatternsRight())
        {
            LevelCanvesManager lm = FindObjectOfType<LevelCanvesManager>();
            lm.Won();
        }
        if(lostColor() != "")
        {
            LevelCanvesManager lm = FindObjectOfType<LevelCanvesManager>();
            lm.Lost(lostColor());

        }

    }

    string lostColor()
    {
        string res  = "";
        List<bool> ref_available = new List<bool>(refs.Count);
        foreach (var r in refs) ref_available.Add(false);
    
        foreach (var MaskPiece in MasksPieces)
        {
            if(refs.Contains(MaskPiece.LayerId)) ref_available[refs.IndexOf(MaskPiece.LayerId)] = true;
        }
        if(ref_available.Contains(false)) res = SortingLayer.IDToName(refs[ref_available.IndexOf(false)]); 

        return res;
    }

    bool AllPatternsRight()
    {
        for (int i = 0; i < MasksPieces.Length; ++i)
        {
            if (!MasksPieces[i].IsPatternRight())
            {
                return false;
            }
        }

        return true;
    }

    public void PlayButton()
    {
        int lvl = PlayerPrefs.GetInt("level", 999);
        if (lvl == 999)
        {
            PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex +1 );
            PlayerPrefs.Save();
        }else{
            SceneManager.LoadScene(lvl);
        }
    }


}
