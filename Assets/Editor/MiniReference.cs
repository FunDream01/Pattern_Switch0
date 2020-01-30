
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MiniImage))]
public class MiniReference : Editor
{
    public override void OnInspectorGUI()
    {
        MiniImage image = (MiniImage)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Change Texture"))
        {
            image.ChangeTexture();
        }
        if (GUILayout.Button("Change Reference  Texture"))
        {
            image.ChangeReferenceTexture();
        }
    }
}
