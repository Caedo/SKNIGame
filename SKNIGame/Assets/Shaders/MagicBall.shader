Shader "Custom/MagicBall" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Emission ("Emission", 2D) = "white" {}
		_EmissionColor ("Emission Color", Color) = (1,1,1,1)
		_EmissionMultiplier("Emision multiplier", float) = 1
		_EmissionSpeedX("Emission Scroll Speed X", float) = 1
		_EmissionSpeedY("Emission Scroll Speed Y", float) = 1
		_EmissionThreshold("Emission Threshold", Range(0,1)) = 0	
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Emission;
		float _EmissionMultiplier;

		float _EmissionSpeedX;
		float _EmissionSpeedY;
		fixed4 _EmissionColor;
		float _EmissionThreshold;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Emission;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed2 emmissionUV = IN.uv_Emission + float2(_EmissionSpeedX, _EmissionSpeedY) * _Time.x;
			fixed emission = tex2D(_Emission, emmissionUV).r;
			if(emission < _EmissionThreshold){
				emission = 0;
			}

			emission *= _EmissionMultiplier;

			o.Albedo = c.rgb;
			o.Emission = emission * _EmissionColor;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
