Shader "Transparent/Diffuse with Shadow" {
  Properties{
    _Color("Main Color", Color) = (1,1,1,1)
    _MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
  }

  SubShader{
    Tags { 
      "Queue" = "AlphaTest+50"
      "IgnoreProjector" = "True"
      "RenderType" = "TransparentCutout"
    }
    LOD 200
    CGPROGRAM

    #pragma surface surf Standard alpha:fade addshadow
    #pragma target 3.0

    sampler2D _MainTex;
    fixed4 _Color;

    struct Input {
        float2 uv_MainTex;
    };

    void surf(Input IN, inout SurfaceOutputStandard o) {
        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
        o.Albedo = c.rgb;
        o.Alpha = c.a;
#ifdef UNITY_PASS_SHADOWCASTER
        o.Alpha = 1.0;
#endif
    }

    ENDCG
  }
  Fallback "Legacy Shaders/Transparent/VertexLit"
}
