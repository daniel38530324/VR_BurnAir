﻿Shader "Hidden/FMDesktopMask"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_FlipX("FlipX", float) = 0
		_FlipY("FlipY", float) = 0

		_RangeX("RangeX", float) = 1
		_RangeY("RangeY", float) = 1

		_OffsetX("OffsetX", float) = 1
		_OffsetY("OffsetY", float) = 1

		_RotationAngle("Rotation(Angle)", float) = 1

        _Brightness ("Brightness", float) = 1
	}

	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			//#include "UnityCG.cginc"
			#define IF(a, b, c) lerp(b, c, step((float) (a), 0))
			#define PI 3.14159  

			float _FlipX;
			float _FlipY;
			float _RangeX;
			float _RangeY;
			float _OffsetX;
			float _OffsetY;

			float _RotationAngle;
            float _Brightness;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				#if !UNITY_UV_STARTS_AT_TOP
				o.uv.y = IF(_ProjectionParams.x < 0, 1.0 - o.uv.y, o.uv.y);
				#endif

				if (_FlipX == 1) o.uv.x = 1.0 - o.uv.x;
				if (_FlipY == 1) o.uv.y = 1.0 - o.uv.y;

				//correct orientation
				o.uv -= 0.5;
				float angle = (_RotationAngle / 180) * PI;

				float s = sin(angle);
				float c = cos(angle);
				float2x2 rotationMatrix = float2x2(c, -s, s, c);
				rotationMatrix *= 0.5;
				rotationMatrix += 0.5;
				rotationMatrix = rotationMatrix * 2.0 - 1.0;
				o.uv = mul(o.uv, rotationMatrix);
				o.uv += 0.5;

				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv;
				uv.x = uv.x * _RangeX;
				uv.y = uv.y * _RangeY;
				uv.x += (1.0 - _RangeX) * 0.5 + (_OffsetX);
				uv.y += (1.0 - _RangeY) * 0.5 + (_OffsetY);

				uv.x = uv.x % 1;
				uv.y = uv.y % 1;
				if (uv.x < 0) uv.x = 1.0 + uv.x;
				if (uv.y < 0) uv.y = 1.0 + uv.y;

				float4 col = tex2D(_MainTex, uv);
                col.rgb *= _Brightness;
				return col;
			}
			ENDCG
		}
	}
}
