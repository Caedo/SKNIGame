Shader "Custom/Additive Distortion"
{
	Properties
	{
		_MainColorA ("Main Color A", Color) = (1,1,1)
		_MainColorB ("Main Color B", Color) = (1,1,1)		
		_MainTex ("Main Texture", 2D) = "white" {}
		_Intensity("Intensity Multiplier", Float) = 1		
		_Offset ("Offset Gradient", Range(-1,1)) = 0	
		_DistortionTex("Distortion Texture", 2D) = "white" {}
		_DistortStr("Distortion Strength", Range(0,1)) = .5
		_DistrSpeedX("Distortion Speed X", Float) = 1
		_DistrSpeedY("Distortion Speed Y", Float) = 1	
		_Mask("Mask", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Transparent"
				"Queue" = "Transparent"
				"DisableBatching" = "True" }
		//LOD 100
		ZWrite Off
		Blend One One 
		Cull Off

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
				float4 modelPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D _DistortionTex;
			sampler2D _Mask;
			fixed4 _MainColorA;
			fixed4 _MainColorB;
			float4 _MainTex_ST;
			float _Offset;
			float _DistortStr;
			float _DistrSpeedX;
			float _DistrSpeedY;			
			float _Intensity;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				float colBlend = v.uv.y;
				o.color = lerp(_MainColorA, _MainColorB, colBlend + _Offset);
				o.modelPos = v.vertex;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed2 distUV = fixed2(i.uv.x - _Time.x * _DistrSpeedX, i.uv.y - _Time.x * _DistrSpeedY);				
				fixed dist = tex2D(_DistortionTex, distUV).r * _DistortStr;
				fixed mask = tex2D(_Mask, i.uv).r;
				fixed4 col = tex2D(_MainTex, i.uv + dist) * i.color * mask;

				UNITY_APPLY_FOG(i.fogCoord, col);
				return col * _Intensity;
			}
			ENDCG
		}
	}
}
