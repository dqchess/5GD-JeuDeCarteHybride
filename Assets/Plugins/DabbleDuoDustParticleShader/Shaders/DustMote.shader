Shader "Custom/DustMote" {
	Properties {
		_Shape("Particle Shape", 2D) = "gray" {}
		_Noise("Noise Tex", 2D) = "gray" {}
		_DoShape("Do Shape Tex", Range(0,1)) = 0
		_LightColor ("Light Color", Color) = (0,1,0,1)
		_ShadowColor("Shadow Color", Color) = (0,0,1,1)
		_Speckles("Speckle Color", Color) = (.7,.7,1,1)
		_Blur ("Blur", Range(0,.5)) = .05
		_Emission("Emission", Range(0,1))=1 
		_Transparency("Transparency", Range(0,1))=0
		_FlashSpeed("Flash Speed", Range(0,1))=.86
	
    
    }
    
	SubShader
	{
		Tags {"RenderType"="Transparent" "Queue" = "Transparent" "IgnoreProjector" = "True"}
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater .01
		Cull Off
		CGPROGRAM
		#pragma target 3.0 
		#pragma surface surf Particle
	
		
		struct Input 
		{
			float2 uv_Noise;
			float4 _Time;
			float4 color: Color;
			
		};
		//declare variables here

		float4 _LightColor;
		float _Blur;
		float _Emission;
		float _Transparency;
		sampler2D _Shape;
		float _DoShape;
		float4 _ShadowColor;
		float4 _Speckles;
		sampler2D _Noise; 
		float _FlashSpeed;
		  
		struct SurfaceOutputCustom{
			 fixed3 Albedo;
			 fixed3 Normal; 
			 fixed3 Emission;
			 fixed Specular;
			 fixed Alpha;

		};
		
		//uses the first light created to simulate scattering through particles
		half4 LightingParticle(SurfaceOutputCustom s, half3 lightDir, half3 viewDir, half atten)
		{ 
			half4 c;  
			c.rgb = s.Albedo;
			c.a =lerp(0,s.Alpha, 1-saturate(dot(normalize(viewDir), lightDir)));
			return c;
		}
		 
		
		void surf (Input IN, inout SurfaceOutputCustom o)
		{
			 
			o.Alpha = IN.color.a;
			o.Albedo = _LightColor.rgb;
			
			//blend an opacity towards the center of the dust particle
			float dist = distance(IN.uv_Noise.xy, float2(.5,.5));
			if(dist<(.5-_Blur)){
				o.Alpha = o.Alpha*max(1.0*pow(dist/(.5-_Blur),2),(1-_Transparency));
				//add a shadow color as the particle fades
				o.Albedo = lerp(_ShadowColor.rgb, o.Albedo,o.Alpha);
			} 
			//rim blur
			else if(dist<.5){
				o.Alpha = o.Alpha*(.5-dist)/_Blur;
			
				
			}
			
			//mix in pinks
			o.Albedo = lerp(o.Albedo,_Speckles.rgb,tex2D(_Noise,(IN.uv_Noise.xy*.1+_Time*.01)).r);

			
			//use texture shape
			if(_DoShape)
				o.Alpha = o.Alpha*tex2D(_Shape,IN.uv_Noise.xy).r;
			
			//otherwise make a circle
			else{
				if(dist>=.5)
				o.Alpha = 0;
			}
			
		 	 //add flashes
		 	 o.Emission = lerp(0,_Emission, smoothstep(_FlashSpeed,1,o.Alpha));

		}
		
		ENDCG
		
	}
	Fallback "Diffuse"
}	