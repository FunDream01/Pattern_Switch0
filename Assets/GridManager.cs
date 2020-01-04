using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    GameManager gameManager;
    BoxCollider2D Collider;
    BoxCollider2D OutCollider;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //Collider = this.gameObject.AddComponent<BoxCollider2D>();
        //GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseEnter()
    {
        gameManager.PieceSelected = false;
    }
    private void OnMouseExit()
    {
        //gameManager.PieceSelected = false;
    }
}
