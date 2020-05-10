// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Custom/ToonSurfaceVertexUnlit" {
	//show values to edit in inspector
	Properties{
		_Color("Tint", Color) = (0, 0, 0, 1)
	}
		SubShader{
		//the material is completely non-transparent and is rendered at the same time as the other opaque geometry
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

		CGPROGRAM

		//the shader is a surface shader, meaning that it will be extended by unity in the background to have fancy lighting and other features
		//our surface shader function is called surf and we use our custom lighting model
		//fullforwardshadows makes sure unity adds the shadow passes the shader might need
		#pragma surface surf Custom noforwardadd nolightmap
		#pragma target 3.0

		fixed4 _Color;

		//our lighting function. Will be called once per light
		float4 LightingCustom(SurfaceOutput s, float3 lightDir, half3 viewDir) {
			return float4(s.Albedo.x, s.Albedo.y, s.Albedo.z, 1.0);
		}

		//input struct which is automatically filled by unity
		struct Input {
			float4 color : COLOR;
		};

		//the surface shader function which sets parameters the lighting function then uses
		void surf(Input i, inout SurfaceOutput o) {
			//sample and tint albedo texture
			fixed4 col = i.color;
			col *= _Color*2.0;
			o.Albedo = col.rgb;
		}
		ENDCG
	}

	Fallback "Mobile/VertexLit"
}
