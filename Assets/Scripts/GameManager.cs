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

    public List<Sprite> patterns;

    

    // Start is called before the first frame update
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);


    }
    void Start()
    {
        MasksPieces = FindObjectsOfType<MaskManager>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void CheckWin()
    {
        if (AllPatternsRight())
        {
           // Debug.Log("Right");
            
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


}
