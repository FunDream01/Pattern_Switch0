using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniImage : MonoBehaviour
{
    int SpritesLayer;
    [HideInInspector]
    public int index = 0;
    [HideInInspector]
    public int Reffrenceindex = 0;
    GameObject ChildReffrence;
    MaskManager maskManager;
    private void Start()
    {
        maskManager= this.gameObject.AddComponent<MaskManager>();
    }
    public void ChangeTexture()
    {
        if (index == 0)
        {
            SpritesLayer = SortingLayer.NameToID("Green");
        }
        else if (index == 1)
        {
            SpritesLayer = SortingLayer.NameToID("Pink");
        }
        else if (index == 2)
        {
            SpritesLayer = SortingLayer.NameToID("Orange");
        }
        else if (index == 3)
        {
            SpritesLayer = SortingLayer.NameToID("Blue");
        }
        else if (index == 4)
        {
            SpritesLayer = SortingLayer.NameToID("Purple");
           
        }
        index++;
        GetComponent<SpriteMask>().frontSortingLayerID = SpritesLayer;


        if (index == 5)
        {
            index = 0;

        }
    }
    public void ChangeReferenceTexture()
    {
        if (Reffrenceindex == 0)
        {
            SpritesLayer = SortingLayer.NameToID("Green");
        }
        else if (Reffrenceindex == 1)
        {
            SpritesLayer = SortingLayer.NameToID("Pink");
        }
        else if (Reffrenceindex == 2)
        {
            SpritesLayer = SortingLayer.NameToID("Orange");
        }
        else if (Reffrenceindex == 3)
        {
            SpritesLayer = SortingLayer.NameToID("Blue");
        }
        else if (Reffrenceindex == 4)
        {
            SpritesLayer = SortingLayer.NameToID("Purple");
        }
        Reffrenceindex++;
        if (ChildReffrence == null)
        {
            ChildReffrence = gameObject.transform.GetChild(0).gameObject;
        }
        ChildReffrence.GetComponent<SpriteMask>().frontSortingLayerID = SpritesLayer;

        if (Reffrenceindex == 5)
        {
            Reffrenceindex = 0;

        }
    }
}
