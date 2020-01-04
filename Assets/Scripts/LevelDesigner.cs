using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesigner : MonoBehaviour
{
    [Header("Prefaps ")]
    public GameObject[] Prefaps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawmOne()
    {
        Instantiate(Prefaps[0], transform);
    }
    public void SpawmTwo()
    {
        Instantiate(Prefaps[1], transform);
    }
    public void SpawmThree()
    {
        Instantiate(Prefaps[2], transform);
    }
}
