Shader "Manu/EPBaklen"
{
	Properties
	{
		[NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
	_Color("Color", Color) = (1,0,0,1)
		_Swipe("Swipe Percentage", Float) = 0.5
	}
		SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }

		Pass
	{
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

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

	sampler2D _MainTex;
	float4 _Color;
	float _Swipe;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.uv;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 col = tex2D(_MainTex, i.uv);

	if (_Swipe > i.uv.x)
		col.rgb += _Color.rgb * _Color.a;

	return col;
	}
		ENDCG
	}
	}
}
