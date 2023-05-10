using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItalicImage : BaseMeshEffect
{
    [Range(-1f, 1f)]
    public float italic = 0f;

    protected List<UIVertex> _vertexList = new List<UIVertex>();
    protected Vector2 _minMaxX;
    protected Vector2 _minMaxY;

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
            SetVertex();

        helper.Clear();
        helper.AddUIVertexTriangleStream(_vertexList);
    }

    protected void SetVertex()
    {
        for (var i = 0; i < _vertexList.Count; i++)
        {
            var vertex = _vertexList[i];
            var offSetX = Mathf.InverseLerp(_minMaxY[0], _minMaxY[1], vertex.position[1]) - Mathf.InverseLerp(_minMaxY[0], _minMaxY[1], 0.5f);
            var yStrange = Mathf.Abs(_minMaxY[0] - _minMaxY[1]);
            vertex.position = new Vector3(vertex.position.x + (offSetX * yStrange * italic), vertex.position.y, vertex.position.z);
            _vertexList[i] = vertex;
        }
    }
}
