Shader "Custom/Outline"
{
    SubShader
    {
        Tags { "Queue" = "Overlay" }

        // �ƿ����� ������ Pass
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }

            ZWrite On
            ZTest LEqual
            Cull Front
            Blend SrcAlpha OneMinusSrcAlpha

            // �ƿ������� �׸��� ���� ó��
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex + v.normal * _OutlineWidth);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

        // ���� ��������Ʈ ������ Pass
        Pass
        {
            Tags { "LightMode" = "Always" }

            ZWrite On
            ZTest LEqual
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha

            // ��������Ʈ ������ ó��
            CGPROGRAM
            #include "UnityCG.cginc"
            ENDCG
        }
    }

    Fallback "Diffuse"
}
