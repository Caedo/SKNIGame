﻿Shader "Custom/Additive Distortion"
{
	Properties
	{
		_MainColorA ("Main Color A", Color) = (1,1,1)
		_MainColorB ("Main Color B", Color) = (1,1,1)		
		_MainTex ("Main Texture", 2D) = "white" {}
		_Offset ("Offset Gradient", Range(-1,1)) = 0	
		_DistortionTex("Distortion Texture", 2D) = "white" {}
		_DistortStr("Distortion Strengh", Range(0,1)) = .5
	}
	SubShader
	{
		Tags { "RenderType"="Opaque"
				"Queue" = "Transparent" }
		LOD 100
		ZWrite Off
		Blend One One

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				//float2 distUV : TEXCOORD1;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			sampler2D _MainTex;
			sampler2D _DistortionTex;
			fixed4 _MainColorA;
			fixed4 _MainColorB;
			float4 _MainTex_ST;
			float _Offset;
			float _DistortStr;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				float colBlend = v.vertex.y * 0.5 + 0.5;
				o.color = lerp(_MainColorA, _MainColorB, colBlend + _Offset);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed dist = tex2D(_DistortionTex, i.uv - _Time.x).r * _DistortStr;
				fixed4 col = tex2D(_MainTex, i.uv + dist) * i.color;

				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
