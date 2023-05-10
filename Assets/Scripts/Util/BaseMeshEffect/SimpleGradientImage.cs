using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SimpleGradientImage : BaseMeshEffect
{
    public enum ColorType
    {
        TwoColor = 0,
        ThreeColor = 1,
        FourColor = 2,
        FiveColor = 3,
        SixColor = 4,
    }

    public ColorType colorType = ColorType.TwoColor;
    public Color topColor = Color.white;
    public Color centerColor1 = Color.white;
    public Color centerColor2 = Color.white;
    public Color centerColor3 = Color.white;
    public Color centerColor4 = Color.white;
    public Color bottomColor = Color.white;
    [SerializeField]
    [HideInInspector]
    public bool invertX;
    [SerializeField]
    [HideInInspector]
    public bool invertY;
    [Range(0.01f, 1f)]
    public float topPosition = 1f;
    [Range(0.01f, 0.99f)]
    public float centerPosition1 = 0.5f;
    [Range(0.01f, 0.99f)]
    public float centerPosition2 = 0.4f;
    [Range(0.01f, 0.99f)]
    public float centerPosition3 = 0.3f;
    [Range(0.01f, 0.99f)]
    public float centerPosition4 = 0.2f;
    [Range(0f, 0.99f)]
    public float bottomPosition = 0f;
    [Range(0f, 1f)]
    public float angle = 0.5f;
    [SerializeField]
    [HideInInspector]
    [Range(-1f, 1f)]
    public float italic = 0f;

    public float hue = 0f;
    public float saturation = 1f;
    public float value = 1f;

    public bool useStencil = false;
    public float stencilComp = 8;
    public float stencil = 0;
    public float stencilOp = 0;
    public float stencilWriteMask = 255;
    public float stencilReadMask = 255;

    protected List<UIVertex> _vertexList = new List<UIVertex>();
    protected Vector2 _minMaxX;
    protected Vector2 _minMaxY;
    Material mat;

    protected override void OnEnable()
    {
        var canvas = transform.root.GetComponent<Canvas>();
        if (canvas != null)
            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;

        mat = new Material(Shader.Find("Custom/SimpleVertexGradient"));
        Validate();
        this.GetComponent<Image>().material = mat;

        base.OnEnable();
    }

    protected override void OnDisable()
    {
        this.GetComponent<Image>().material = null;
        base.OnDisable();
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        Validate();

        base.OnValidate();
    }
#endif
    public void ForceValidate()
    {
#if UNITY_EDITOR
        OnValidate();
#else
        Validate();
#endif
    }

    void Validate()
    {
        if (mat == null) return;

        mat.SetFloat("_GradientType", (float)colorType);

        mat.SetColor("_TopColor", topColor);
        mat.SetColor("_CenterColor1", centerColor1);
        mat.SetColor("_CenterColor2", centerColor2);
        mat.SetColor("_CenterColor3", centerColor3);
        mat.SetColor("_CenterColor4", centerColor4);
        mat.SetColor("_BottomColor", bottomColor);

        mat.SetFloat("_TopPos", topPosition);
        mat.SetFloat("_CenterPos1", centerPosition1);
        mat.SetFloat("_CenterPos2", centerPosition2);
        mat.SetFloat("_CenterPos3", centerPosition3);
        mat.SetFloat("_CenterPos4", centerPosition4);
        mat.SetFloat("_BottomPos", bottomPosition);

        mat.SetFloat("_Angle", angle);
        mat.SetFloat("_Hue", hue);
        mat.SetFloat("_Sat", saturation);
        mat.SetFloat("_Val", value);

        mat.SetFloat("_StencilComp", stencilComp);
        mat.SetFloat("_Stencil", stencil);
        mat.SetFloat("_StencilOp", stencilOp);
        mat.SetFloat("_StencilWriteMask", stencilWriteMask);
        mat.SetFloat("_StencilReadMask", stencilReadMask);

        var toNotify = this.GetComponent<Image>() as IMaskable;
        if (toNotify != null)
            toNotify.RecalculateMasking();
    }

    public override void ModifyMesh(VertexHelper helper)
    {
        _vertexList.Clear();
        helper.GetUIVertexStream(_vertexList);

        if (_vertexList.Count <= 0) return;
        _minMaxX = new Vector2(
            _vertexList.Select(v => v.position[0]).Min(),
            _vertexList.Select(v => v.position[0]).Max()
        );

        _minMaxY = new Vector2(
            _vertexList.Select(v => v.position[1]).Min(),
            _vertexList.Select(v => v.position[1]).Max()
        );

        if (IsActive())
            SetVertexColor();

        helper.Clear();
        helper.AddUIVertexTriangleStream(_vertexList);
    }

    protected void SetVertexColor()
    {
        for (var i = 0; i < _vertexList.Count; i++)
        {
            var vertex = _vertexList[i];
            var x = (invertX) ? Mathf.InverseLerp(_minMaxX[1], _minMaxX[0], vertex.position[0]) : Mathf.InverseLerp(_minMaxX[0], _minMaxX[1], vertex.position[0]);
            var y = (invertY) ? Mathf.InverseLerp(_minMaxY[1], _minMaxY[0], vertex.position[1]) : Mathf.InverseLerp(_minMaxY[0], _minMaxY[1], vertex.position[1]);
            vertex.uv1 = new Vector2(x, y);
            var yStrange = Mathf.Abs(_minMaxY[0] - _minMaxY[1]);
            var offSetX = Mathf.InverseLerp(_minMaxY[0], _minMaxY[1], vertex.position[1]) - Mathf.InverseLerp(_minMaxY[0], _minMaxY[1], 0.5f);
            vertex.position = new Vector3(vertex.position.x + (offSetX * yStrange * italic), vertex.position.y, vertex.position.z);
            _vertexList[i] = vertex;
        }
    }
}
