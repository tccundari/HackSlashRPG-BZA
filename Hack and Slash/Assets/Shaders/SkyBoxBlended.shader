Shader "RenderFX/Skybox Blended" {
	Properties {
		_Tint ("Tint Color", Color) = (.5, .5, .5, .5)
		_Blend ("Blend", Range(0.0,1.0)) = 0.5
		_FrontTex ("Front (+z)", 2D) = "white" {}
		_BackTex ("Back (-z)", 2D) = "white" {}
		_LeftTex ("Left (+x)", 2D) = "white" {}
		_RightTex ("Right (-x)", 2D) = "white" {}
		_UpTex ("Up (+y)", 2D) = "white" {}
		_DownTex ("Down (-y)", 2D) = "white" {}
		_FrontTex2 ("2 Front (+z)", 2D) = "white" {}
		_BackTex2 ("2 Back (-z)", 2D) = "white" {}
		_LeftTex2 ("2 Left (+x)", 2D) = "white" {}
		_RightTex2 ("2 Right (-x)", 2D) = "white" {}
		_UpTex2 ("2 Up (+y)", 2D) = "white" {}
		_DownTex2 ("2 Down (-y)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue" = "Background" }
		Cull Off
		Fog { Mode Off }
		Lighting Off
		Color [_Tint]
		Pass
		{
			SetTexture [_FrontTex] {combine texture }
			SetTexture [_FrontTex2] { ConstantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous}
			SetTexture [_FrontTex2] {combine previous +- primary, previous * primary}
		}
		Pass
		{
			SetTexture [_BackTex] {combine texture }
			SetTexture [_BackTex2] { ConstantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous}
			SetTexture [_BackTex2] {combine previous +- primary, previous * primary}
		}
		Pass
		{
			SetTexture [_LeftTex] {combine texture }
			SetTexture [_LeftTex2] { ConstantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous}
			SetTexture [_LeftTex2] {combine previous +- primary, previous * primary}
		}
		Pass
		{
			SetTexture [_RightTex] {combine texture }
			SetTexture [_RightTex2] { ConstantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous}
			SetTexture [_RightTex2] {combine previous +- primary, previous * primary}
		}
		Pass
		{
			SetTexture [_UpTex] {combine texture }
			SetTexture [_UpTex2] { ConstantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous}
			SetTexture [_UpTex2] {combine previous +- primary, previous * primary}
		}
		Pass
		{
			SetTexture [_DownTex] {combine texture }
			SetTexture [_DownTex2] { ConstantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous}
			SetTexture [_DownTex2] {combine previous +- primary, previous * primary}
		}
	} 
	
	Fallback "RenderFX/Skybox", 1
}
