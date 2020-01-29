
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShadowBaker))]
public class ShadowBakerEditor : Editor
{
    //public Transform transform;
    float slider;
    bool autoTile=true;

    public override void OnInspectorGUI()
    {
        ShadowBaker baker = (ShadowBaker)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Bake 'EM"))
        {
            baker.bake();
        }
        autoTile = GUILayout.Toggle(autoTile,"Auto Tile Shadows");
        if(autoTile) {baker.tiling = 1.0f;}
        else {baker.tiling = EditorGUILayout.Slider("Tiling Spacing",baker.tiling,0.1f,1.5f);}
        EditorGUILayout.LabelField("Note: Shadow Scaler works in reverse!");


    }
}
