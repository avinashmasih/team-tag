using UnityEditor;

[CustomEditor(typeof(SprayCan))]
[CanEditMultipleObjects]
public class SprayCanEditor : Editor
{
    SerializedProperty color;
    SerializedProperty sprayRate;
    SerializedProperty maxCapacity;
    SerializedProperty currentFill;
    SerializedProperty spraying;
    SerializedProperty refill;

    private void OnEnable()
    {
        color = serializedObject.FindProperty("color");
        sprayRate = serializedObject.FindProperty("SprayRate");
        maxCapacity = serializedObject.FindProperty("MaxCapacity");
        currentFill = serializedObject.FindProperty("currentFill");
        spraying = serializedObject.FindProperty("spraying");
        refill = serializedObject.FindProperty("RefillRate");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(color);
        EditorGUILayout.PropertyField(sprayRate);
        EditorGUILayout.PropertyField(refill);
        EditorGUILayout.PropertyField(maxCapacity);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.LabelField($"Current Fill: {currentFill.floatValue} ({spraying.boolValue})");
    }
}
