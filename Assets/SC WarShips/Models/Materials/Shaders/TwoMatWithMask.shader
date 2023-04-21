Shader "TwoMatWithMask"
{
	Properties
	{
		_ColorMain("ColorMain", Color) = (1,1,1,0)
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_TilingMain("TilingMain", Vector) = (1,1,0,0)
		_TextureMain("TextureMain", 2D) = "white" {}
		_NormalMain("NormalMain", 2D) = "bump" {}
		_MetallicMain("MetallicMain", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_MaskLight("MaskLight", 2D) = "white" {}
		_ColorAdd("ColorAdd", Color) = (1,1,1,0)
		_ColorLight("ColorLight", Color) = (0,0,0,0)
		_TilingAdd("TilingAdd", Vector) = (1,1,0,0)
		_TextureAdd("TextureAdd", 2D) = "white" {}
		_NormalAdd("NormalAdd", 2D) = "bump" {}
		_MetallicAdd("MetallicAdd", 2D) = "white" {}
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
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
			float2 uv2_texcoord2;
		};

		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform sampler2D _NormalMain;
		uniform float2 _TilingMain;
		uniform sampler2D _NormalAdd;
		uniform float2 _TilingAdd;
		uniform sampler2D _MaskLight;
		uniform float4 _MaskLight_ST;
		uniform float4 _ColorMain;
		uniform sampler2D _TextureMain;
		uniform float4 _ColorAdd;
		uniform sampler2D _TextureAdd;
		uniform float4 _ColorLight;
		uniform sampler2D _MetallicMain;
		uniform sampler2D _MetallicAdd;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float grayscale29 = Luminance(tex2D( _Mask, uv_Mask ).rgb);
			float2 uv2_TexCoord50 = i.uv2_texcoord2 * _TilingMain;
			float temp_output_36_0 = ( 1.0 - grayscale29 );
			float2 uv2_TexCoord53 = i.uv2_texcoord2 * _TilingAdd;
			o.Normal = ( ( grayscale29 * UnpackNormal( tex2D( _NormalMain, uv2_TexCoord50 ) ) ) + ( temp_output_36_0 * UnpackNormal( tex2D( _NormalAdd, uv2_TexCoord53 ) ) ) );
			float2 uv_MaskLight = i.uv_texcoord * _MaskLight_ST.xy + _MaskLight_ST.zw;
			float4 tex2DNode57 = tex2D( _MaskLight, uv_MaskLight );
			float4 temp_output_21_0 = ( ( grayscale29 * ( _ColorMain * tex2D( _TextureMain, uv2_TexCoord50 ) ) ) + ( temp_output_36_0 * ( _ColorAdd * tex2D( _TextureAdd, uv2_TexCoord53 ) ) ) );
			o.Albedo = ( ( tex2DNode57 * temp_output_21_0 ) + ( ( 1.0 - tex2DNode57 ) * temp_output_21_0 * _ColorLight ) ).rgb;
			o.Metallic = ( ( grayscale29 * tex2D( _MetallicMain, uv2_TexCoord50 ) ) + ( temp_output_36_0 * tex2D( _MetallicAdd, uv2_TexCoord53 ) ) ).r;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}

}