Shader "Hologram V2 Y Wireframe"
{
	Properties
	{
		_Scanline1Amount("Scanline 1 Amount", Float) = 10
		_Scanline1Speed("Scanline 1 Speed", Float) = 1
		_Scnaline2Amount("Scnaline 2 Amount", Float) = 1.25
		_Scanline2Speed("Scanline 2 Speed", Float) = 0.19
		_Color("Color", Color) = (0.6179246,1,1,1)
		[HDR]_FresnelColor("Fresnel Color", Color) = (0.8588235,0.3529412,0,1)
		[HideInInspector] __dirty( "", Int ) = 1

		_WireThickness ("Wire Thickness", RANGE(0, 800)) = 100
		_WireSmoothness ("Wire Smoothness", RANGE(0, 20)) = 3
		[HDR]_WireColor ("Wire Color", Color) = (0.0, 1.0, 0.0, 1.0)
		_BaseColor ("Base Color", Color) = (0.0, 0.0, 0.0, 0.0)
		_MaxTriSize ("Max Tri Size", RANGE(0, 200)) = 25
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
			Cull Back

			// Wireframe shader based on the the following
			// http://developer.download.nvidia.com/SDK/10/direct3d/Source/SolidWireframe/Doc/SolidWireframe.pdf

			CGPROGRAM
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Wireframe.cginc"

			ENDCG
		}

		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
		};

		UNITY_INSTANCING_BUFFER_START(HologramV2Y)
			UNITY_DEFINE_INSTANCED_PROP(float4, _FresnelColor)
#define _FresnelColor_arr HologramV2Y
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
#define _Color_arr HologramV2Y
			UNITY_DEFINE_INSTANCED_PROP(float, _Scanline2Speed)
#define _Scanline2Speed_arr HologramV2Y
			UNITY_DEFINE_INSTANCED_PROP(float, _Scnaline2Amount)
#define _Scnaline2Amount_arr HologramV2Y
			UNITY_DEFINE_INSTANCED_PROP(float, _Scanline1Amount)
#define _Scanline1Amount_arr HologramV2Y
			UNITY_DEFINE_INSTANCED_PROP(float, _Scanline1Speed)
#define _Scanline1Speed_arr HologramV2Y
		UNITY_INSTANCING_BUFFER_END(HologramV2Y)

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float temp_output_7_0 = ( ase_vertex3Pos.y + _SinTime.y );
			float mulTime19 = _Time.y * 3.0;
			float3 appendResult37 = (float3(( 0.0 + ( ( ( ( ( step( 0.0 , temp_output_7_0 ) * step( temp_output_7_0 , 0.2 ) ) * 0.1 ) * step( 0.98 , sin( mulTime19 ) ) ) * _SinTime.w ) * 1.5 ) ) , 0.0 , 0.0));
			v.vertex.xyz += appendResult37;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _Color_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float _Scanline1Amount_Instance = UNITY_ACCESS_INSTANCED_PROP(_Scanline1Amount_arr, _Scanline1Amount);
			float _Scanline1Speed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Scanline1Speed_arr, _Scanline1Speed);
			float mulTime43 = _Time.y * _Scanline1Speed_Instance;
			float temp_output_45_0 = frac( ( ( ase_vertex3Pos.y * _Scanline1Amount_Instance ) + mulTime43 ) );
			float _Scnaline2Amount_Instance = UNITY_ACCESS_INSTANCED_PROP(_Scnaline2Amount_arr, _Scnaline2Amount);
			float _Scanline2Speed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Scanline2Speed_arr, _Scanline2Speed);
			float temp_output_52_0 = frac( ( ( ase_vertex3Pos.y * _Scnaline2Amount_Instance ) - ( _Time.y * _Scanline2Speed_Instance ) ) );
			float2 temp_cast_0 = (unity_DeltaTime.x).xx;
			float dotResult4_g1 = dot( temp_cast_0 , float2( 12.9898,78.233 ) );
			float lerpResult10_g1 = lerp( 0.0 , 1.0 , frac( ( sin( dotResult4_g1 ) * 43758.55 ) ));
			float temp_output_70_0 = lerpResult10_g1;
			float temp_output_71_0 = (( temp_output_70_0 > 0.8 ) ? temp_output_70_0 :  0.8 );
			o.Albedo = ( ( _Color_Instance * ( temp_output_45_0 + temp_output_52_0 ) ) * temp_output_71_0 ).rgb;
			float4 _FresnelColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_FresnelColor_arr, _FresnelColor);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV64 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode64 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV64, 3.0 ) );
			o.Emission = ( ( ( _FresnelColor_Instance * fresnelNode64 ) + ( _Color_Instance * temp_output_45_0 ) ) * temp_output_71_0 ).rgb;
			o.Alpha = ( ( ( temp_output_45_0 * temp_output_52_0 ) * temp_output_71_0 ) * _Color_Instance.a );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG

		

		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD1;
				float3 worldNormal : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}