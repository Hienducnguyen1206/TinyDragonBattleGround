Shader "Custom/CircularTransparentGlass"
{
    Properties
    {
        _Color ("Glass Color", Color) = (1,1,1,0.5) // Màu kính
        _MainTex ("Texture", 2D) = "white" {}      // Texture tùy chọn
        _Radius ("Circle Radius", float) = 0.5    // Bán kính hình tròn (0.0 - 0.5)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent+10" "RenderType"="Transparent" }
        Pass
        {
            // Ghi vào stencil buffer
            Stencil
            {
                Ref 1
                Comp Always      // Luôn ghi stencil
                Pass Replace     // Ghi giá trị stencil
            }

            // Blend để tạo hiệu ứng trong suốt
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off          // Tắt ghi Z-Buffer để tránh lỗi độ sâu
            ZTest LEqual        // Vẫn kiểm tra Z-Buffer để giữ thứ tự hiển thị

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Radius;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Kiểm tra khoảng cách từ pixel đến tâm
                float2 center = float2(0.5, 0.5);  // Tâm của hình tròn trong UV space
                float dist = distance(i.uv, center);

                // Loại bỏ pixel nếu nằm ngoài hình tròn
                if (dist > _Radius)
                    discard;

                // Lấy texture và áp dụng màu trong suốt
                fixed4 tex = tex2D(_MainTex, i.uv);
                return tex * _Color;
            }
            ENDCG
        }
    }
    FallBack "Transparent/VertexLit"
}
