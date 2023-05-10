Shader "Custom/SimpleVertexGradient"
{
    Properties
    {
        _GradientType ("Gradient Type", Float) = 0
        _MainTex ("Texture", 2D) = "white" {}

        _TopColor("Top Color", Color) = (1,1,1,1)
        _CenterColor1("Center 1 Color", Color) = (1,1,1,1)
        _CenterColor2("Center 2 Color", Color) = (1,1,1,1)
        _CenterColor3("Center 3 Color", Color) = (1,1,1,1)
        _CenterColor4("Center 4 Color", Color) = (1,1,1,1)
        _BottomColor("Bottom Color", Color) = (1,1,1,1)
        _TopPos("Top Position", Range(0.01,1)) = 1
        _CenterPos1("Center 1 Position", Range(0.01,0.99)) = 0.5
        _CenterPos2("Center 2 Position", Range(0.01,0.99)) = 0.4
        _CenterPos3("Center 3 Position", Range(0.01,0.99)) = 0.3
        _CenterPos4("Center 4 Position", Range(0.01,0.99)) = 0.2
        _BottomPos("Bottom Position", Range(0, 0.99)) = 0
        _Angle("Angle", Range(0, 1)) = 0.5
        
        _Hue ("Hue", Float) = 0
        _Sat ("Saturation", Float) = 1
        _Val ("Value", Float) = 1

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100
		Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Stencil
            {
                // ステンシルテストの基準値
                Ref [_Stencil]

                // 比較関数
                Comp [_StencilComp]

                // ステンシルテスト成功時の挙動
                Pass [_StencilOp]

                // バッファ読み込み時ビットマスク
                ReadMask [_StencilReadMask]

                // バッファ書き込み時ビットマスク
                WriteMask [_StencilWriteMask]
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float2 uv1    : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed2 uv1    : TEXCOORD1;
                float4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed3 shift_col(fixed3 RGB, half3 shift)
            {
                fixed3 RESULT = fixed3(RGB);
                float VSU = shift.z*shift.y*cos(shift.x*3.14159265/180);
                float VSW = shift.z*shift.y*sin(shift.x*3.14159265/180);

                 RESULT.x = (.299*shift.z+.701*VSU+.168*VSW)*RGB.x
                     + (.587*shift.z-.587*VSU+.330*VSW)*RGB.y
                     + (.114*shift.z-.114*VSU-.497*VSW)*RGB.z;

                 RESULT.y = (.299*shift.z-.299*VSU-.328*VSW)*RGB.x
                     + (.587*shift.z+.413*VSU+.035*VSW)*RGB.y
                     + (.114*shift.z-.114*VSU+.292*VSW)*RGB.z;

                 RESULT.z = (.299*shift.z-.3*VSU+1.25*VSW)*RGB.x
                     + (.587*shift.z-.588*VSU-1.05*VSW)*RGB.y
                     + (.114*shift.z+.886*VSU-.203*VSW)*RGB.z;

                return (RESULT);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv1 = v.uv1;
                o.color = v.color;
                return o;
            }

            float _Hue;
            float _Sat;
            float _Val;
            float _Angle;

            float _GradientType;

            fixed4 _TopColor;
            fixed4 _CenterColor1;
            fixed4 _CenterColor2;
            fixed4 _CenterColor3;
            fixed4 _CenterColor4;
            fixed4 _BottomColor;
            float _BottomPos;
            float _CenterPos1;
            float _CenterPos2;
            float _CenterPos3;
            float _CenterPos4;
            float _TopPos;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col_ori = tex2D(_MainTex, i.uv);
                float y = i.uv1.y * _Angle + i.uv1.x * (1 - _Angle);
                fixed4 col = col_ori;
                switch (_GradientType) {
                case 0:
                    col = lerp(_BottomColor, 
                            lerp(lerp(_BottomColor, _TopColor, (y - _BottomPos) / (_TopPos - _BottomPos)), 
                                    _TopColor
                                , step(_TopPos, y))
                            , step(_BottomPos, y) );
                    break;
                case 1:
                    col = lerp(_BottomColor, 
                            lerp(lerp(_BottomColor, _CenterColor1, (y - _BottomPos) / (_CenterPos1 - _BottomPos)), 
                                    lerp(lerp(_CenterColor1, _TopColor, (y - _CenterPos1) / (_TopPos - _CenterPos1)), 
                                        _TopColor
                                    , step(_TopPos, y))
                                , step(_CenterPos1, y))
                            , step(_BottomPos, y));
                    break;
                case 2:
                    col = lerp(_BottomColor, 
                            lerp(
                                lerp(_BottomColor, _CenterColor2, (y - _BottomPos) / (_CenterPos2 - _BottomPos)), 
                                    lerp(lerp(_CenterColor2, _CenterColor1, (y - _CenterPos2) / (_CenterPos1 - _CenterPos2)), 
                                        lerp(lerp(_CenterColor1, _TopColor, (y - _CenterPos1) / (_TopPos - _CenterPos1)), 
                                            _TopColor
                                        , step(_TopPos, y))
                                    , step(_CenterPos1, y))
                                , step(_CenterPos2, y))
                            , step(_BottomPos, y));
                    break;
                case 3:
                    col = lerp(_BottomColor, 
                            lerp(
                                lerp(_BottomColor, _CenterColor3, (y - _BottomPos) / (_CenterPos3 - _BottomPos)), 
                                    lerp(lerp(_CenterColor3, _CenterColor2, (y - _CenterPos3) / (_CenterPos2 - _CenterPos3)), 
                                        lerp(lerp(_CenterColor2, _CenterColor1, (y - _CenterPos2) / (_CenterPos1 - _CenterPos2)), 
                                            lerp(lerp(_CenterColor1, _TopColor, (y - _CenterPos1) / (_TopPos - _CenterPos1)), 
                                                _TopColor
                                            , step(_TopPos, y))
                                        , step(_CenterPos1, y))
                                    , step(_CenterPos2, y))
                                , step(_CenterPos3, y))
                            , step(_BottomPos, y));
                    break;
                case 4:
                    col = lerp(_BottomColor, 
                            lerp(
                                lerp(_BottomColor, _CenterColor4, (y - _BottomPos) / (_CenterPos4 - _BottomPos)), 
                                    lerp(lerp(_CenterColor4, _CenterColor3, (y - _CenterPos4) / (_CenterPos3 - _CenterPos4)), 
                                        lerp(lerp(_CenterColor3, _CenterColor2, (y - _CenterPos3) / (_CenterPos2 - _CenterPos3)), 
                                            lerp(lerp(_CenterColor2, _CenterColor1, (y - _CenterPos2) / (_CenterPos1 - _CenterPos2)), 
                                                lerp(lerp(_CenterColor1, _TopColor, (y - _CenterPos1) / (_TopPos - _CenterPos1)), 
                                                    _TopColor
                                                , step(_TopPos, y))
                                            , step(_CenterPos1, y))
                                        , step(_CenterPos2, y))
                                    , step(_CenterPos3, y))
                                , step(_CenterPos4, y))
                            , step(_BottomPos, y));
                    break;
                }

                col.rgb *= col_ori.rgb;
                col.a *= col_ori.a * i.color.a;
                half3 shift = half3(_Hue, _Sat, _Val);
                return fixed4(shift_col(col, shift), col.a);
            }
            ENDCG
        }
    }
    CustomEditor "SimpleVertexGradientInspector"
}