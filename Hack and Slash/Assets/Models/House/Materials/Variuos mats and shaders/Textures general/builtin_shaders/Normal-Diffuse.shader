// Upgrade NOTE: replaced 'PositionFog()' with multiply of UNITY_MATRIX_MVP by position
// Upgrade NOTE: replaced 'V2F_POS_FOG' with 'float4 pos : SV_POSITION'

Shader "Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
}

Category {
	Tags { "RenderType"="Opaque" }
	LOD 200
	/* Upgrade NOTE: commented out, possibly part of old style per-pixel lighting: Blend AppSrcAdd AppDstAdd */
	Fog { Color [_AddFog] }
	
	// ------------------------------------------------------------------
	// ARB fragment program
	
	#warning Upgrade NOTE: SubShader commented out; uses Unity 2.x per-pixel lighting. You should rewrite shader into a Surface Shader.
/*SubShader {
		// Ambient pass
		Pass {
			Name "BASE"
			Tags {"LightMode" = "Always" /* Upgrade NOTE: changed from PixelOrNone to Always */}
			Color [_PPLAmbient]
			SetTexture [_MainTex] {constantColor [_Color] Combine texture * primary DOUBLE, texture * constant}
		}
		// Vertex lights
		Pass { 
			Name "BASE"
			Tags {"LightMode" = "Vertex"}
			Lighting On
			Material {
				Diffuse [_Color]
				Emission [_PPLAmbient]
			}
			SetTexture [_MainTex] { constantColor [_Color] Combine texture * primary DOUBLE, texture * constant}
		}
		// Pixel lights
		Pass {
			Name "PPL"
			Tags { "LightMode" = "Pixel" }
CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members uv,normal,lightDir)
#pragma exclude_renderers d3d11 xbox360
// Upgrade NOTE: excluded shader from Xbox360; has structs without semantics (struct v2f members uv,normal,lightDir)
#pragma exclude_renderers xbox360
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_builtin
#pragma fragmentoption ARB_fog_exp2
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"
#include "AutoLight.cginc"

struct v2f {
	float4 pos : SV_POSITION;
	LIGHTING_COORDS
	float2	uv;
	float3	normal;
	float3	lightDir;
};

uniform float4 _MainTex_ST;

v2f vert (appdata_base v)
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	o.normal = v.normal;
	o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
	o.lightDir = ObjSpaceLightDir( v.vertex );
	TRANSFER_VERTEX_TO_FRAGMENT(o);
	return o;
}

uniform sampler2D _MainTex;

float4 frag (v2f i) : COLOR
{
	// The eternal tradeoff: do we normalize the normal?
	//float3 normal = normalize(i.normal);
	float3 normal = i.normal;
		
	half4 texcol = tex2D( _MainTex, i.uv );
	
	return DiffuseLight( i.lightDir, normal, texcol, LIGHT_ATTENUATION(i) );
}
ENDCG
		}
	}*/
	
 	// ------------------------------------------------------------------
	// Radeon 9000

	#warning Upgrade NOTE: SubShader commented out because of manual shader assembly
/*SubShader {
		// Ambient pass
		Pass {
			Name "BASE"
			Tags {"LightMode" = "Always" /* Upgrade NOTE: changed from PixelOrNone to Always */}
			Color [_PPLAmbient]
			SetTexture [_MainTex] {constantColor [_Color] Combine texture * primary DOUBLE, texture * constant}
		}
		// Vertex lights
		Pass { 
			Name "BASE"
			Tags {"LightMode" = "Vertex"}
			Lighting On
			Material {
				Diffuse [_Color]
				Emission [_PPLAmbient]
			}
			SetTexture [_MainTex] { constantColor [_Color] Combine texture * primary DOUBLE, texture * constant}
		}
		
		// Pixel lights with 0 light textures
		Pass { 
			Name "PPL"
			Tags { 
				"LightMode" = "Pixel" 
				"LightTexCount" = "0"
			}

CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
#pragma exclude_renderers gles
#pragma vertex vert
#include "UnityCG.cginc"

struct v2f {
	float4 pos : SV_POSITION;
	float2 uv		: TEXCOORD0;
	float3 normal	: TEXCOORD1;
	float3 lightDir	: TEXCOORD2;
};

uniform float4 _MainTex_ST;

v2f vert(appdata_base v)
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	o.normal = v.normal;
	o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
	o.lightDir = ObjSpaceLightDir( v.vertex );
	return o; 
}
ENDCG
			Program "" {
				SubProgram {
					Local 0, [_ModelLightColor0]
					Local 1, (0,0,0,0)

"!!ATIfs1.0
StartConstants;
	CONSTANT c0 = program.local[0];
	CONSTANT c1 = program.local[1];
EndConstants;

StartOutputPass;
	SampleMap r0, t0.str;			# main texture
	SampleMap r1, t2.str;			# normalized light dir
	PassTexCoord r2, t1.str;		# normal
	
	DOT3 r5.sat, r2, r1.2x.bias;	# R5 = diffuse (N.L)
	
	MUL r0, r0, r5;
	MUL r0.rgb.2x, r0, c0;
	MOV r0.a, c1;
EndPass; 
"
				}
			}
			SetTexture[_MainTex] {combine texture}
			SetTexture[_CubeNormalize] {combine texture}
		}
		
		// Pixel lights with 1 light texture
		Pass {
			Name "PPL"
			Tags { 
				"LightMode" = "Pixel" 
				"LightTexCount" = "1"
			}

CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
#pragma exclude_renderers gles
#pragma vertex vert
#include "UnityCG.cginc"

uniform float4 _MainTex_ST;
uniform float4x4 _SpotlightProjectionMatrix0;

struct v2f {
	float4 pos : SV_POSITION;
	float2 uv		: TEXCOORD0;
	float3 normal	: TEXCOORD1;
	float3 lightDir	: TEXCOORD2;
	float4 LightCoord0 : TEXCOORD3;
};

v2f vert(appdata_tan v)
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	o.normal = v.normal;
	o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
	o.lightDir = ObjSpaceLightDir( v.vertex );
	
	o.LightCoord0 = mul(_SpotlightProjectionMatrix0, v.vertex);
	
	return o; 
}
ENDCG
			Program "" {
				SubProgram {
					Local 0, [_ModelLightColor0]
					Local 1, (0,0,0,0)

"!!ATIfs1.0
StartConstants;
	CONSTANT c0 = program.local[0];
	CONSTANT c1 = program.local[1];
EndConstants;

StartOutputPass;
	SampleMap r0, t0.str;			# main texture
	SampleMap r1, t2.str;			# normalized light dir
	PassTexCoord r4, t1.str;		# normal
	SampleMap r2, t3.str;			# a = attenuation
	
	DOT3 r5.sat, r4, r1.2x.bias;	# R5 = diffuse (N.L)
	
	MUL r0, r0, r5;
	MUL r0.rgb.2x, r0, c0;
	MUL r0.rgb, r0, r2.a;			# attenuate
	MOV r0.a, c1;
EndPass; 
"
				}
			}
			SetTexture[_MainTex] {combine texture}
			SetTexture[_CubeNormalize] {combine texture}
			SetTexture[_LightTexture0] {combine texture}
		}
		
		// Pixel lights with 2 light textures
		Pass {
			Name "PPL"
			Tags {
				"LightMode" = "Pixel"
				"LightTexCount" = "2"
			}
CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
#pragma exclude_renderers gles
#pragma vertex vert
#include "UnityCG.cginc"

uniform float4 _MainTex_ST;
uniform float4x4 _SpotlightProjectionMatrix0;
uniform float4x4 _SpotlightProjectionMatrixB0;

struct v2f {
	float4 pos : SV_POSITION;
	float2 uv		: TEXCOORD0;
	float3 normal	: TEXCOORD1;
	float3 lightDir	: TEXCOORD2;
	float4 LightCoord0 : TEXCOORD3;
	float4 LightCoordB0 : TEXCOORD4;
};

v2f vert(appdata_tan v)
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	o.normal = v.normal;
	o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
	o.lightDir = ObjSpaceLightDir( v.vertex );
	
	o.LightCoord0 = mul(_SpotlightProjectionMatrix0, v.vertex);
	o.LightCoordB0 = mul(_SpotlightProjectionMatrixB0, v.vertex);
	
	return o; 
}
ENDCG
			Program "" {
				SubProgram {
					Local 0, [_ModelLightColor0]
					Local 1, (0,0,0,0)

"!!ATIfs1.0
StartConstants;
	CONSTANT c0 = program.local[0];
	CONSTANT c1 = program.local[1];
EndConstants;

StartOutputPass;
	SampleMap r0, t0.str;			# main texture
	SampleMap r1, t2.str;			# normalized light dir
	PassTexCoord r4, t1.str;		# normal
	SampleMap r2, t3.stq_dq;		# a = attenuation 1
	SampleMap r3, t4.stq_dq;		# a = attenuation 2
	
	DOT3 r5.sat, r4, r1.2x.bias;	# R5 = diffuse (N.L)
	
	MUL r0, r0, r5;
	MUL r0.rgb.2x, r0, c0;
	MUL r0.rgb, r0, r2.a;			# attenuate
	MUL r0.rgb, r0, r3.a;
	MOV r0.a, c1;
EndPass; 
"
				}
			}
			SetTexture[_MainTex] {combine texture}
			SetTexture[_CubeNormalize] {combine texture}
			SetTexture[_LightTexture0] {combine texture}
			SetTexture[_LightTextureB0] {combine texture}
		}
	}*/
	
	// ------------------------------------------------------------------
	// Radeon 7000
	
	Category {
		Material {
			Diffuse [_Color]
			Emission [_PPLAmbient]
		}
		Lighting On
		#warning Upgrade NOTE: SubShader commented out; uses Unity 2.x style fixed function per-pixel lighting. Per-pixel lighting is not supported without shaders anymore.
/*SubShader {
			// Ambient pass
			Pass {
				Name "BASE"
				Tags {"LightMode" = "Always" /* Upgrade NOTE: changed from PixelOrNone to Always */}
				Color [_PPLAmbient]
				Lighting Off
				SetTexture [_MainTex] {Combine texture * primary DOUBLE, primary * texture}
			}
			// Vertex lights
			Pass { 
				Name "BASE"
				Tags {"LightMode" = "Vertex"}
				Lighting On
				Material {
					Diffuse [_Color]
					Emission [_PPLAmbient]
				}
				SetTexture [_MainTex] {Combine texture * primary DOUBLE, primary * texture}
			}
			// Pixel lights with 2 light textures
			Pass {
				Name "PPL"
				Tags {
					"LightMode" = "Pixel"
					"LightTexCount"  = "2"
				}
				ColorMask RGB
				SetTexture [_LightTexture0]  { combine previous * texture alpha, previous }
				SetTexture [_LightTextureB0] { combine previous * texture alpha, previous }
				SetTexture [_MainTex] { combine previous * texture DOUBLE }
			}
			// Pixel lights with 1 light texture
			Pass {
				Name "PPL"
				Tags {
					"LightMode" = "Pixel"
					"LightTexCount"  = "1"
				}
				ColorMask RGB
				SetTexture [_LightTexture0] { combine previous * texture alpha, previous }
				SetTexture [_MainTex] { combine previous * texture DOUBLE }
			}
			// Pixel lights with 0 light textures
			Pass {
				Name "PPL"
				Tags {
					"LightMode" = "Pixel"
					"LightTexCount" = "0"
				}
				ColorMask RGB
				SetTexture[_MainTex] { combine previous * texture DOUBLE }
			}
		}*/
	}
}

Fallback "VertexLit", 2

}
