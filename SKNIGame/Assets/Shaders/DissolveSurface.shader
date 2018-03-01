Shader "Custom/DissolveSurface" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_DissolveTexture ("Dissolve Texture", 2D) = "white" {}
		_DissolveStrength ("Dissolve Strength", Range(0,1)) = 0
		_DisLineWidth ("Dissolve Line Width", Range(0,1)) = 0		
		_DisLineColor ("Dissolve Color", Color) = (1,1,1,1)
		_DisLineWidthExtra ("Extra Dissolve Line Width", Range(0,1)) = 0				
		_DisLineColorExtra ("Extra Dissolve Color", Color) = (1,1,1,1)		
		_NoiseMax ("Noise Max Value", Range(0.5, 1)) = 1
		_NScale ("Noise Scale", Range(0, 10)) = 1
		_EmmisionMulti("Emission Multiplier", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
		LOD 200
		
        Blend SrcAlpha OneMinusSrcAlpha
        
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _DissolveTexture;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _DissolveStrength;
		float _DisLineWidth;
        fixed4 _DisLineColor;
		fixed4 _DisLineColorExtra;
		float _DisLineWidthExtra;
		float _NoiseMax;
		float _NScale;
		float _EmmisionMulti;
        
		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
		
		float inverseLerp(float a, float b, float t){
		    return (t - a) / (b - a);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			float dissolve = tex2D(_DissolveTexture, IN.worldPos.xy * _NScale).r;
			dissolve = inverseLerp(-_DisLineWidth - _DisLineWidthExtra, _NoiseMax, dissolve);
			
			float disline = step(dissolve - _DisLineWidth, _DissolveStrength);
			float disLineExtra = step(dissolve - (_DisLineWidth + _DisLineWidthExtra), _DissolveStrength) - disline;
			
			float NoDiss = 1 - disline - disLineExtra;
			
			o.Albedo = (c.rgb * NoDiss) + (disline * _DisLineColor) + (disLineExtra * _DisLineColorExtra);

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;

			o.Emission = (disline * _DisLineColor) + (disLineExtra * _DisLineColorExtra);
			o.Emission *= _EmmisionMulti;
			o.Alpha = step(_DissolveStrength, dissolve);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
