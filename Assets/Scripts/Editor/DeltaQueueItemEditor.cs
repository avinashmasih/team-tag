using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(DeltaQueue.DeltaQueueItem))]
class DeltaQueueItemEditor : Editor
{
    SerializedProperty time;
    SerializedProperty item;

    private void OnEnable()
    {
        time = serializedObject.FindProperty("time");
        item = serializedObject.FindProperty("item");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(time);
        EditorGUILayout.PropertyField(item);
        serializedObject.ApplyModifiedProperties();
    }
}

