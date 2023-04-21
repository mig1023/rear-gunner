Shader "S_Ships"
{
	Properties
	{
		_yourTexture("your Texture", 2D) = "white" {}
		_Albedo("Albedo", 2D) = "white" {}	
		_Metallic("Metallic", 2D) = "white" {}		
		_Normal("Normal", 2D) = "bump" {}		
		_AO("AO", 2D) = "white" {}			
		_Emission("Emission", 2D) = "black" {}
		_TextureTeam("Texture Team", 2D) = "white" {}
		_ColoryourTexture("Color your Texture", Color) = (1,1,1,0)
		_ColorTeam("Color Team", Color) = (1,1,1,0)
		_Smoothness("Smoothness", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
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
		uniform float4 _ColoryourTexture;
		uniform sampler2D _yourTexture;
		uniform float4 _yourTexture_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _ColorTeam;
		uniform sampler2D _TextureTeam;
		uniform float4 _TextureTeam_ST;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform float _Smoothness;
		uniform sampler2D _AO;
		uniform float4 _AO_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float2 uv_yourTexture = i.uv_texcoord * _yourTexture_ST.xy + _yourTexture_ST.zw;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode17 = tex2D( _Albedo, uv_Albedo );
			float4 lerpResult15 = lerp( ( _ColoryourTexture * tex2D( _yourTexture, uv_yourTexture ) ) , tex2DNode17 , tex2DNode17.a);
			float2 uv_TextureTeam = i.uv_texcoord * _TextureTeam_ST.xy + _TextureTeam_ST.zw;
			float4 tex2DNode22 = tex2D( _TextureTeam, uv_TextureTeam );
			float4 lerpResult23 = lerp( lerpResult15 , ( _ColorTeam * tex2DNode22 ) , ( _ColorTeam.a * tex2DNode22.a ));
			o.Albedo = lerpResult23.rgb;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			o.Emission = tex2D( _Emission, uv_Emission ).rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			o.Metallic = tex2D( _Metallic, uv_Metallic ).r;
			o.Smoothness = _Smoothness;
			float2 uv_AO = i.uv_texcoord * _AO_ST.xy + _AO_ST.zw;
			o.Occlusion = tex2D( _AO, uv_AO ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
}
