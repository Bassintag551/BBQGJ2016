Shader "Custom/Mask" {

	Properties
	{
		_MainTex("Base (RGBA)", 2D) = "white" {}
		_Color("Color", COLOR) = (1, 1, 0, 1)
	}

		SubShader{
			Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }

			ZWrite Off

			Blend srcAlpha OneMinusSrcAlpha
			ColorMask RGB

			Pass
			{
				
			}
	}
}
