Shader "Custom/RGB" {
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			uniform float _Magnitude;

			float4 frag (v2f_img i) : COLOR
			{
				float2 coords = i.uv.xy;

				float4 red = tex2D(_MainTex, coords.xy - .02f * _Magnitude);
				float4 green = tex2D(_MainTex, coords.xy);
				float4 blue = tex2D(_MainTex, coords.xy + .02f * _Magnitude);

				float4 col = float4(red.r, green.g, blue.b, 1.0f);
				return col;
			}
			ENDCG
		}
	}
}
