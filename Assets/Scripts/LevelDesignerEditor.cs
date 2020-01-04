
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelDesigner))]
public class LevelDesignerEditor : Editor
{
    //public Transform transform;
    public override void OnInspectorGUI()
    {
        LevelDesigner Level = (LevelDesigner)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("[1] Spawn"))
        {
            Level.SpawmOne();
        }
        if (GUILayout.Button("[2] Spawn"))
        {
            Level.SpawmTwo();
        }
        if (GUILayout.Button("[3] Spawn"))
        {
            Level.SpawmThree();
        }
    }
}
