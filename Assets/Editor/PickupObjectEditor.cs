using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(PickupObject))]
public class PickupObjectEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed)
        {
            (target as PickupObject).RecreateModel();
        }
    }

}