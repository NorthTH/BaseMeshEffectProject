using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleGradientImage))]
public class SimpleGradientImageEditor : Editor
{
    private SimpleGradientImage _target;
    private SerializedProperty _invertXProperty;
    private SerializedProperty _invertYProperty;
    private SerializedProperty _italicProperty;

    private void OnEnable()
    {
        _target = target as SimpleGradientImage;

        // 各種Propertyを取得する
        _invertXProperty = serializedObject.FindProperty("invertX");
        _invertYProperty = serializedObject.FindProperty("invertY");
        _italicProperty = serializedObject.FindProperty("italic");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        // シリアライズされたPropertyを更新しておく
        serializedObject.Update();

        var image = target as SimpleGradientImage;
        image.colorType = (SimpleGradientImage.ColorType)EditorGUILayout.EnumPopup("Gradient Color Type", image.colorType);

        switch (image.colorType)
        {
            case SimpleGradientImage.ColorType.TwoColor:
                image.topColor = EditorGUILayout.ColorField("Top Color", image.topColor);
                image.bottomColor = EditorGUILayout.ColorField("Bottom Color", image.bottomColor);

                image.topPosition = EditorGUILayout.Slider("Top Position", image.topPosition, 0.01f, 1f);
                image.bottomPosition = EditorGUILayout.Slider("Bottom Position", image.bottomPosition, 0f, 0.99f);
                break;
            case SimpleGradientImage.ColorType.ThreeColor:
                image.topColor = EditorGUILayout.ColorField("Top Color", image.topColor);
                image.centerColor1 = EditorGUILayout.ColorField("Center Color", image.centerColor1);
                image.bottomColor = EditorGUILayout.ColorField("Bottom Color", image.bottomColor);

                image.topPosition = EditorGUILayout.Slider("Top Position", image.topPosition, 0.01f, 1f);
                image.centerPosition1 = EditorGUILayout.Slider("Center Position", image.centerPosition1, 0.01f, 0.99f);
                image.bottomPosition = EditorGUILayout.Slider("Bottom Position", image.bottomPosition, 0f, 0.99f);
                break;
            case SimpleGradientImage.ColorType.FourColor:
                image.topColor = EditorGUILayout.ColorField("Top Color", image.topColor);
                image.centerColor1 = EditorGUILayout.ColorField("Center Color 1", image.centerColor1);
                image.centerColor2 = EditorGUILayout.ColorField("Center Color 2", image.centerColor2);
                image.bottomColor = EditorGUILayout.ColorField("Bottom Color", image.bottomColor);

                image.topPosition = EditorGUILayout.Slider("Top Position", image.topPosition, 0.01f, 1f);
                image.centerPosition1 = EditorGUILayout.Slider("Center Position 1", image.centerPosition1, 0.01f, 0.99f);
                image.centerPosition2 = EditorGUILayout.Slider("Center Position 2", image.centerPosition2, 0.01f, 0.99f);
                image.bottomPosition = EditorGUILayout.Slider("Bottom Position", image.bottomPosition, 0f, 0.99f);
                break;
            case SimpleGradientImage.ColorType.FiveColor:
                image.topColor = EditorGUILayout.ColorField("Top Color", image.topColor);
                image.centerColor1 = EditorGUILayout.ColorField("Center Color 1", image.centerColor1);
                image.centerColor2 = EditorGUILayout.ColorField("Center Color 2", image.centerColor2);
                image.centerColor3 = EditorGUILayout.ColorField("Center Color 3", image.centerColor3);
                image.bottomColor = EditorGUILayout.ColorField("Bottom Color", image.bottomColor);

                image.topPosition = EditorGUILayout.Slider("Top Position", image.topPosition, 0.01f, 1f);
                image.centerPosition1 = EditorGUILayout.Slider("Center Position 1", image.centerPosition1, 0.01f, 0.99f);
                image.centerPosition2 = EditorGUILayout.Slider("Center Position 2", image.centerPosition2, 0.01f, 0.99f);
                image.centerPosition3 = EditorGUILayout.Slider("Center Position 3", image.centerPosition3, 0.01f, 0.99f);

                image.bottomPosition = EditorGUILayout.Slider("Bottom Position", image.bottomPosition, 0f, 0.99f);
                break;
            case SimpleGradientImage.ColorType.SixColor:
                image.topColor = EditorGUILayout.ColorField("Top Color", image.topColor);
                image.centerColor1 = EditorGUILayout.ColorField("Center Color 1", image.centerColor1);
                image.centerColor2 = EditorGUILayout.ColorField("Center Color 2", image.centerColor2);
                image.centerColor3 = EditorGUILayout.ColorField("Center Color 3", image.centerColor3);
                image.centerColor4 = EditorGUILayout.ColorField("Center Color 4", image.centerColor4);
                image.bottomColor = EditorGUILayout.ColorField("Bottom Color", image.bottomColor);

                image.topPosition = EditorGUILayout.Slider("Top Position", image.topPosition, 0.01f, 1f);
                image.centerPosition1 = EditorGUILayout.Slider("Center Position 1", image.centerPosition1, 0.01f, 0.99f);
                image.centerPosition2 = EditorGUILayout.Slider("Center Position 2", image.centerPosition2, 0.01f, 0.99f);
                image.centerPosition3 = EditorGUILayout.Slider("Center Position 3", image.centerPosition3, 0.01f, 0.99f);
                image.centerPosition4 = EditorGUILayout.Slider("Center Position 4", image.centerPosition4, 0.01f, 0.99f);
                image.bottomPosition = EditorGUILayout.Slider("Bottom Position", image.bottomPosition, 0f, 0.99f);
                break;
        }
        EditorGUILayout.PropertyField(_invertXProperty);
        EditorGUILayout.PropertyField(_invertYProperty);
        image.angle = EditorGUILayout.Slider("Angle", image.angle, 0f, 1f);
        EditorGUILayout.PropertyField(_italicProperty);

        image.hue = EditorGUILayout.FloatField("Hue", image.hue);
        image.saturation = EditorGUILayout.FloatField("Sat", image.saturation);
        image.value = EditorGUILayout.FloatField("Val", image.value);

        image.useStencil = EditorGUILayout.Toggle("Use Stencil", image.useStencil);
        if (image.useStencil)
        {
            image.stencilComp = EditorGUILayout.FloatField("StencilComp", image.stencilComp);
            image.stencil = EditorGUILayout.FloatField("Stencil", image.stencil);
            image.stencilOp = EditorGUILayout.FloatField("StencilOp", image.stencilOp);
            image.stencilWriteMask = EditorGUILayout.FloatField("StencilWriteMask", image.stencilWriteMask);
            image.stencilReadMask = EditorGUILayout.FloatField("StencilReadMask", image.stencilReadMask);
        }

        // 更新された値を適用する
        serializedObject.ApplyModifiedProperties();

        // 更新されたら内容を反映
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(image);
            image.ForceValidate();
        }
    }
}
