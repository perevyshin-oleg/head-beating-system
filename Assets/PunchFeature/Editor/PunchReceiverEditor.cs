#if UNITY_EDITOR
using PunchFeature.Core;
using UnityEditor;

[CustomEditor(typeof(PunchReceiver))]
public class PunchReceiverEditor : Editor
{
    private SerializedProperty _rigidbody;
    private SerializedProperty _isChangesShape;
    private SerializedProperty _shapeCurve;
    private SerializedProperty _isChangeColor;
    private SerializedProperty _renderer;
    private SerializedProperty _colorCurve;
    private SerializedProperty _onHeatColor;
    private SerializedProperty _colorAnimationSpeed;
    private SerializedProperty _shapeAnimationSpeed;
    private SerializedProperty _isShowParticles;
    private SerializedProperty _particleSystem;

    private void OnEnable()
    {
        _rigidbody = serializedObject.FindProperty("_rigidbody");
        _isChangesShape = serializedObject.FindProperty("_isChangesShape");
        _colorAnimationSpeed = serializedObject.FindProperty("_colorAnimationSpeed");
        _shapeAnimationSpeed = serializedObject.FindProperty("_shapeAnimationSpeed");
        _shapeCurve = serializedObject.FindProperty("_shapeCurve");
        _isChangeColor = serializedObject.FindProperty("_isChangeColor");
        _renderer = serializedObject.FindProperty("_renderer");
        _colorCurve = serializedObject.FindProperty("_colorCurve");
        _onHeatColor = serializedObject.FindProperty("_onHeatColor");
        _isShowParticles = serializedObject.FindProperty("_isShowParticles");
        _particleSystem = serializedObject.FindProperty("_particleSystem");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_rigidbody);
        
        EditorGUILayout.PropertyField(_isChangesShape);
        if (_isChangesShape.boolValue)
        {
            EditorGUILayout.PropertyField(_shapeCurve);
            EditorGUILayout.PropertyField(_shapeAnimationSpeed);
        }
        
        EditorGUILayout.PropertyField(_isChangeColor);
        if (_isChangeColor.boolValue)
        {
            EditorGUILayout.PropertyField(_renderer);
            EditorGUILayout.PropertyField(_colorCurve);
            EditorGUILayout.PropertyField(_onHeatColor);
            EditorGUILayout.PropertyField(_colorAnimationSpeed);
        }
        
        EditorGUILayout.PropertyField(_isShowParticles);
        if (_isShowParticles.boolValue)
        {
            EditorGUILayout.PropertyField(_particleSystem);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif