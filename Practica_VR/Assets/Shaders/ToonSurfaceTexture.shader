// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Custom/ToonSurfaceTexture" {
	//show values to edit in inspector
	Properties{
		_Color("Tint", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}
		_Glossiness("Glossiness", Float) = 32
		_UseSpecular("UseSpecular1to0", Range(0, 1)) = 0.5
	}
		SubShader{
		//the material is completely non-transparent and is rendered at the same time as the other opaque geometry
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

		CGPROGRAM

		//the shader is a surface shader, meaning that it will be extended by unity in the background to have fancy lighting and other features
		//our surface shader function is called surf and we use our custom lighting model
		//fullforwardshadows makes sure unity adds the shadow passes the shader might need
		#pragma surface surf Custom noforwardadd
		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		uniform float _Glossiness;
		uniform float _UseSpecular;

		//our lighting function. Will be called once per light
		float4 LightingCustom(SurfaceOutput s, float3 lightDir, half3 viewDir, float atten) {
			//how much does the normal point towards the light?
			float towardsLight = dot(s.Normal, lightDir);
			float3 lightIntensity = smoothstep(0.4, 0.41, towardsLight);

			viewDir = normalize(viewDir);
			float3 halfVector = normalize(lightDir + viewDir);
			float NdotH = (dot(s.Normal, halfVector)*0.5+0.5) / (atten*atten);
			float specularIntensity = pow(NdotH, _Glossiness * _Glossiness);
			float specular = smoothstep(0.005, 0.008, specularIntensity) * lightIntensity;

			lightIntensity += specular * _UseSpecular;

			//combine the color
			float4 col;
			//intensity we calculated previously, diffuse color, light falloff and shadowcasting, color of the light
			col.rgb = lightIntensity * s.Albedo * atten * _LightColor0.rgb;
			//in case we want to make the shader transparent in the future - irrelevant right now
			col.a = s.Alpha;

			return col;
		}

		//input struct which is automatically filled by unity
		struct Input {
			float2 uv_MainTex;
		};

		//the surface shader function which sets parameters the lighting function then uses
		void surf(Input i, inout SurfaceOutput o) {
			//sample and tint albedo texture
			fixed4 col = tex2D(_MainTex, i.uv_MainTex);

			col *= _Color;
			o.Albedo = col.rgb;
		}
		ENDCG
	}

	Fallback "Mobile/VertexLit"
}
