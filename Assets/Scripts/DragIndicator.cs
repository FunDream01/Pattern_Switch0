using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragIndicator : MonoBehaviour
{
    GameManager gameManager;
    Vector3 StartPos;
    Vector3 EndPos;
    Camera camera;
    
    LineRenderer line;
    Vector3 CameraOffset = new Vector3(0, 0, 10);
    [SerializeField]
    AnimationCurve Size;
    [SerializeField]
    Gradient color;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (line == null)
            {
                line = gameObject.AddComponent<LineRenderer>();
            }

            if (gameManager.PieceSelected)
            {
                line.enabled = true;
                line.positionCount = 2;
                StartPos = camera.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;
                line.SetPosition(0, StartPos);
                line.useWorldSpace = true;
                line.widthCurve = Size;
                line.numCapVertices = 10;
                line.colorGradient = color;
                line.sortingLayerName = "front";
                line.sortingOrder = 100;
            }

        }
        if (Input.GetMouseButton(0))
        {
            EndPos= camera.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;
            line.SetPosition(1, EndPos);

        }
        if (Input.GetMouseButtonUp(0))
        {
            line.enabled = false;
        }
    }
}
