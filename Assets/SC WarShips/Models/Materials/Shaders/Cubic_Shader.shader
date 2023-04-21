
Shader "Tanks"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Albedo1("AlbedoMask", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Metallic("Metallic", 2D) = "white" {}
		_Color0("Color", Color) = (1,1,1,0)
		_Smoothness("Smoothness", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform float4 _Color0;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Albedo1;
		uniform float4 _Albedo1_ST;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float2 uv_Albedo1 = i.uv_texcoord * _Albedo1_ST.xy + _Albedo1_ST.zw;
			float4 tex2DNode17 = tex2D( _Albedo1, uv_Albedo1 );
			float4 lerpResult15 = lerp( ( _Color0 * tex2D( _Albedo, uv_Albedo ) ) , tex2DNode17 , tex2DNode17.a);
			o.Albedo = lerpResult15.rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			o.Metallic = tex2D( _Metallic, uv_Metallic ).r;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}

}
