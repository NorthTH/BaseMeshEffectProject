using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.UI;

public class SimpleVertexGradientInspector : MaterialEditor
{
    enum GradientType
    {
        TwoColor = 0,
        ThreeColor = 1,
        FourColor = 2,
        FiveColor = 3,
        SixColor = 4,
    }

    // マテリアルへのアクセス
    Material material
    {
        get
        {
            return (Material)target;
        }
    }

    // Inspectorに表示される内容
    public override void OnInspectorGUI()
    {
        // マテリアルを閉じた時に非表示にする
        if (isVisible == false) { return; }

        // 入力内容が変更されたかチェック
        EditorGUI.BeginChangeCheck();

        var gradientType = (GradientType)EditorGUILayout.EnumPopup("Gradient Type", (GradientType)material.GetFloat("_GradientType"));


        // InspectorのGUIを定義
        Texture mainTex = EditorGUILayout.ObjectField("Main Texture", material.GetTexture("_MainTex"), typeof(Texture), false) as Texture;

        Color topColor = material.GetColor("_TopColor");
        Color centerColor1 = material.GetColor("_CenterColor1");
        Color centerColor2 = material.GetColor("_CenterColor2");
        Color centerColor3 = material.GetColor("_CenterColor3");
        Color centerColor4 = material.GetColor("_CenterColor4");
        Color bottomColor = material.GetColor("_BottomColor");

        var topPos = material.GetFloat("_TopPos");
        var centerPos1 = material.GetFloat("_CenterPos1");
        var centerPos2 = material.GetFloat("_CenterPos2");
        var centerPos3 = material.GetFloat("_CenterPos3");
        var centerPos4 = material.GetFloat("_CenterPos4");
        var bottomPos = material.GetFloat("_BottomPos");

        var angle = material.GetFloat("_Angle");

        var hue = material.GetFloat("_Hue");
        var sat = material.GetFloat("_Sat");
        var val = material.GetFloat("_Val");

        float stencilComp = material.GetFloat("_StencilComp");
        float stencil = material.GetFloat("_Stencil");
        float stencilOp = material.GetFloat("_StencilOp");
        float stencilWriteMask = material.GetFloat("_StencilWriteMask");
        float stencilReadMask = material.GetFloat("_StencilReadMask");

        switch (gradientType)
        {
            case GradientType.TwoColor:
                topColor = EditorGUILayout.ColorField("Top Color", topColor);
                bottomColor = EditorGUILayout.ColorField("Bottom Color", bottomColor);

                topPos = EditorGUILayout.Slider("Top Position", topPos, 0.01f, 1f);
                bottomPos = EditorGUILayout.Slider("Bottom Position", bottomPos, 0f, 0.99f);
                break;
            case GradientType.ThreeColor:
                topColor = EditorGUILayout.ColorField("Top Color", topColor);
                centerColor1 = EditorGUILayout.ColorField("Center Color", centerColor1);
                bottomColor = EditorGUILayout.ColorField("Bottom Color", bottomColor);

                topPos = EditorGUILayout.Slider("Top Position", topPos, 0.01f, 1f);
                centerPos1 = EditorGUILayout.Slider("Center Position", centerPos1, 0.01f, 0.99f);
                bottomPos = EditorGUILayout.Slider("Bottom Position", bottomPos, 0f, 0.99f);
                break;
            case GradientType.FourColor:
                topColor = EditorGUILayout.ColorField("Top Color", topColor);
                centerColor1 = EditorGUILayout.ColorField("Center Color 1", centerColor1);
                centerColor2 = EditorGUILayout.ColorField("Center Color 2", centerColor2);
                bottomColor = EditorGUILayout.ColorField("Bottom Color", bottomColor);

                topPos = EditorGUILayout.Slider("Top Position", topPos, 0.01f, 1f);
                centerPos1 = EditorGUILayout.Slider("Center Position 1", centerPos1, 0.01f, 0.99f);
                centerPos2 = EditorGUILayout.Slider("Center Position 2", centerPos2, 0.01f, 0.99f);
                bottomPos = EditorGUILayout.Slider("Bottom Position", bottomPos, 0f, 0.99f);
                break;
            case GradientType.FiveColor:
                topColor = EditorGUILayout.ColorField("Top Color", topColor);
                centerColor1 = EditorGUILayout.ColorField("Center Color 1", centerColor1);
                centerColor2 = EditorGUILayout.ColorField("Center Color 2", centerColor2);
                centerColor3 = EditorGUILayout.ColorField("Center Color 3", centerColor3);
                bottomColor = EditorGUILayout.ColorField("Bottom Color", bottomColor);

                topPos = EditorGUILayout.Slider("Top Position", topPos, 0.01f, 1f);
                centerPos1 = EditorGUILayout.Slider("Center Position 1", centerPos1, 0.01f, 0.99f);
                centerPos2 = EditorGUILayout.Slider("Center Position 2", centerPos2, 0.01f, 0.99f);
                centerPos3 = EditorGUILayout.Slider("Center Position 3", centerPos3, 0.01f, 0.99f);
                bottomPos = EditorGUILayout.Slider("Bottom Position", bottomPos, 0f, 0.99f);
                break;
            case GradientType.SixColor:
                topColor = EditorGUILayout.ColorField("Top Color", topColor);
                centerColor1 = EditorGUILayout.ColorField("Center Color 1", centerColor1);
                centerColor2 = EditorGUILayout.ColorField("Center Color 2", centerColor2);
                centerColor3 = EditorGUILayout.ColorField("Center Color 3", centerColor3);
                centerColor4 = EditorGUILayout.ColorField("Center Color 4", centerColor4);
                bottomColor = EditorGUILayout.ColorField("Bottom Color", bottomColor);

                topPos = EditorGUILayout.Slider("Top Position", topPos, 0.01f, 1f);
                centerPos1 = EditorGUILayout.Slider("Center Position 1", centerPos1, 0.01f, 0.99f);
                centerPos2 = EditorGUILayout.Slider("Center Position 2", centerPos2, 0.01f, 0.99f);
                centerPos3 = EditorGUILayout.Slider("Center Position 3", centerPos3, 0.01f, 0.99f);
                centerPos4 = EditorGUILayout.Slider("Center Position 4", centerPos4, 0.01f, 0.99f);
                bottomPos = EditorGUILayout.Slider("Bottom Position", bottomPos, 0f, 0.99f);
                break;
        }

        angle = EditorGUILayout.Slider("Angle", angle, 0f, 1f);

        hue = EditorGUILayout.FloatField("Hue", hue);
        sat = EditorGUILayout.FloatField("Sat", sat);
        val = EditorGUILayout.FloatField("Val", val);

        stencilComp = EditorGUILayout.FloatField("StencilComp", stencilComp);
        stencil = EditorGUILayout.FloatField("Stencil", stencil);
        stencilOp = EditorGUILayout.FloatField("StencilOp", stencilOp);
        stencilWriteMask = EditorGUILayout.FloatField("StencilWriteMask", stencilWriteMask);
        stencilReadMask = EditorGUILayout.FloatField("StencilReadMask", stencilReadMask);

        // 更新されたら内容を反映
        if (EditorGUI.EndChangeCheck())
        {
            material.SetFloat("_GradientType", (float)gradientType);
            material.SetTexture("_MainTex", mainTex);

            switch (gradientType)
            {
                case GradientType.TwoColor:
                    material.SetColor("_TopColor", topColor);
                    material.SetColor("_BottomColor", bottomColor);
                    material.SetFloat("_TopPos", topPos);
                    material.SetFloat("_BottomPos", bottomPos);
                    break;
                case GradientType.ThreeColor:
                    material.SetColor("_TopColor", topColor);
                    material.SetColor("_CenterColor1", centerColor1);
                    material.SetColor("_BottomColor", bottomColor);
                    material.SetFloat("_TopPos", topPos);
                    material.SetFloat("_CenterPos1", centerPos1);
                    material.SetFloat("_BottomPos", bottomPos);
                    break;
                case GradientType.FourColor:
                    material.SetColor("_TopColor", topColor);
                    material.SetColor("_CenterColor1", centerColor1);
                    material.SetColor("_CenterColor2", centerColor2);
                    material.SetColor("_BottomColor", bottomColor);
                    material.SetFloat("_TopPos", topPos);
                    material.SetFloat("_CenterPos1", centerPos1);
                    material.SetFloat("_CenterPos2", centerPos2);
                    material.SetFloat("_BottomPos", bottomPos);
                    break;
                case GradientType.FiveColor:
                    material.SetColor("_TopColor", topColor);
                    material.SetColor("_CenterColor1", centerColor1);
                    material.SetColor("_CenterColor2", centerColor2);
                    material.SetColor("_CenterColor3", centerColor3);
                    material.SetColor("_BottomColor", bottomColor);
                    material.SetFloat("_TopPos", topPos);
                    material.SetFloat("_CenterPos1", centerPos1);
                    material.SetFloat("_CenterPos2", centerPos2);
                    material.SetFloat("_CenterPos3", centerPos3);
                    material.SetFloat("_BottomPos", bottomPos);
                    break;
                case GradientType.SixColor:
                    material.SetColor("_TopColor", topColor);
                    material.SetColor("_CenterColor1", centerColor1);
                    material.SetColor("_CenterColor2", centerColor2);
                    material.SetColor("_CenterColor3", centerColor3);
                    material.SetColor("_CenterColor4", centerColor4);
                    material.SetColor("_BottomColor", bottomColor);
                    material.SetFloat("_TopPos", topPos);
                    material.SetFloat("_CenterPos1", centerPos1);
                    material.SetFloat("_CenterPos2", centerPos2);
                    material.SetFloat("_CenterPos3", centerPos3);
                    material.SetFloat("_CenterPos4", centerPos4);
                    material.SetFloat("_BottomPos", bottomPos);
                    break;
            }

            material.SetFloat("_Angle", angle);

            material.SetFloat("_Hue", hue);
            material.SetFloat("_Sat", sat);
            material.SetFloat("_Val", val);

            material.SetFloat("_StencilComp", stencilComp);
            material.SetFloat("_Stencil", stencil);
            material.SetFloat("_StencilOp", stencilOp);
            material.SetFloat("_StencilWriteMask", stencilWriteMask);
            material.SetFloat("_StencilReadMask", stencilReadMask);
        }
    }
}
