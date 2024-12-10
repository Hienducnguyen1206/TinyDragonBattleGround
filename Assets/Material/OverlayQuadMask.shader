Shader "Custom/OverlayWithStencil"
{
    Properties
    {
        _OverlayColor ("Overlay Color", Color) = (0,0,0,0.5) // Màu của lớp phủ
    }
    SubShader
    {
        Tags { "Queue" = "Transparent+5" "RenderType"="Transparent" }
        Pass
        {
            // Chỉ vẽ bên ngoài Main Quad
            Stencil
            {
                Ref 1
                Comp NotEqual    // Chỉ vẽ khi giá trị stencil khác 1 (bên ngoài Main Quad)
            }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off          // Tắt ghi Z-Buffer
            ColorMask RGBA      // Cho phép vẽ tất cả các kênh màu

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _OverlayColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OverlayColor; // Áp dụng màu lớp phủ
            }
            ENDCG
        }
    }
    FallBack "Transparent/VertexLit"
}
