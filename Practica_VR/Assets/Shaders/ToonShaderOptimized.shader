Shader "Custom/ToonShaderOptimized"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.6,1)
		_Glossiness("Glossiness", Float) = 32
		_UseSpecular("UseSpecular1to0", Range(0, 1)) = 0
		_UseVertexColor("UseVertexColor1to0", Range(0, 1)) = 0
	}
		SubShader
		{
			Lighting On
			Pass
			{

				Tags
				{
					"RenderType"="Opaque"
					"LightMode"="ForwardBase"
					"PassFlags"="OnlyDirectional"
				}
				//ZWrite On
				//Blend SrcAlpha OneMinusSrcAlpha
				//BlendOp Add
				//Blend One One
				ZWrite On
				LOD 200

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fwdbase

				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "AutoLight.cginc"

				struct vertexInput
				{
					float4 vertex : POSITION;
					float4 uv : TEXCOORD0;
					float3 normal : NORMAL;
					float4 color : COLOR;
				};

				struct vertexOutput
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD2;
					float3 worldNormal : NORMAL;
					float3 viewDir : TEXCOORD3;
					float4 color : COLOR;
					LIGHTING_COORDS(0, 1)
					//SHADOW_COORDS(4)
				};

				uniform float4 _MainTex_ST;
				uniform sampler2D _MainTex;

				vertexOutput vert(vertexInput IN)
				{

					vertexOutput v;
					UNITY_INITIALIZE_OUTPUT(vertexOutput, v);
					v.pos = UnityObjectToClipPos(IN.vertex);
					v.uv = TRANSFORM_TEX(IN.uv, _MainTex);
					v.worldNormal = UnityObjectToWorldNormal(IN.normal);
					v.viewDir = WorldSpaceViewDir(IN.vertex);
					v.color = IN.color;
					//TRANSFER_SHADOW(OUT)
					TRANSFER_VERTEX_TO_FRAGMENT(v)
					return v;
				}

				uniform float _Glossiness;
				uniform float _UseSpecular;
				uniform float _UseVertexColor;
				uniform float4 _Color;
				uniform float4 _AmbientColor;

				float4 frag(vertexOutput IN) : SV_Target
				{

					//DIFFUSE
					float3 normal = normalize(IN.worldNormal);
					float NdotL = /*sqrt(saturate(*/dot(_WorldSpaceLightPos0, normal)/*+0.1 * length(_LightColor0.xyz)))*/;
					//float shadow = SHADOW_ATTENUATION(IN) / 2;
					float shadow = LIGHT_ATTENUATION(IN);

					float lightIntensity = smoothstep(0.1, 0.12, NdotL * shadow);

					//SPECULAR
					float3 viewDir = normalize(IN.viewDir);
					float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
					float NdotH = dot(normal, halfVector);
					float specularIntensity = pow(NdotH, _Glossiness * _Glossiness);
					float specular = smoothstep(0.005, 0.008, specularIntensity) * lightIntensity;

					//RIM
					float rimAmount = 0.06;
					float4 rimDot = (1 - dot(viewDir, normal)) * rimAmount;

					//RETURN
					float4 sample = tex2D(_MainTex, IN.uv);
					float finalLight = (lightIntensity + specular * _UseSpecular + rimDot)/* * _LightColor0*/;

					float4 usingColor = sample * (1 - _UseVertexColor) + IN.color * _UseVertexColor;

					float4 finalColor = (usingColor * _Color * finalLight * _LightColor0 + usingColor * _AmbientColor *(1 - finalLight));
					finalColor.a = 1;
					return finalColor;
				}
				ENDCG
			}

			//Pass{
			//	Tags{
			//		"LightMode" = "ForwardAdd"
			//		//"DisableBatching" = "True"
			//		//"RenderType" = "Opaque"
			//	}
			//	LOD 200
			//
			//	Blend One One
			//	BlendOp Add
			//	//BlendOp Max
			//
			//	CGPROGRAM
			//	#pragma vertex vert
			//	#pragma fragment frag
			//	#pragma multi_compile_fwdadd_fullshadows
			//	#include "UnityCG.cginc"
			//	#include "Lighting.cginc"
			//	#include "AutoLight.cginc"
			//
			//	
			//
			//	struct vertexInput { //appdata
			//		float4 vertex : POSITION;
			//		float4 uv : TEXCOORD0;
			//		float3 normal : NORMAL;
			//		float4 color : COLOR;
			//	};
			//
			//	struct vertexOutput { //v2f
			//		float4 pos : SV_POSITION;
			//		float2 uv : TEXCOORD3;
			//		float4 posWorld : TEXCOORD4;
			//		float3 viewDir : TEXCOORD2;
			//		float3 worldNormal : NORMAL;
			//		float4 color : COLOR;
			//		LIGHTING_COORDS(0,1)
			//		//SHADOW_COORDS(2)
			//	};
			//
			//	uniform float4 _MainTex_ST;
			//	uniform sampler2D _MainTex;
			//	//uniform sampler2D _ShadowMapTexture;
			//
			//	vertexOutput vert(vertexInput v) {
			//		
			//
			//		vertexOutput OUT;
			//		float4x4 modelMatrix = unity_ObjectToWorld;
			//
			//		OUT.posWorld = mul(modelMatrix, v.vertex);
			//		OUT.pos = UnityObjectToClipPos(v.vertex);
			//		OUT.uv = TRANSFORM_TEX(v.uv, _MainTex);
			//		OUT.worldNormal = UnityObjectToWorldNormal(v.normal);
			//		OUT.color = v.color;
			//		OUT.viewDir = WorldSpaceViewDir(v.vertex);
			//		//TRANSFER_SHADOW(OUT)
			//		//vertexInput v = IN;
			//		TRANSFER_VERTEX_TO_FRAGMENT(OUT);
			//		return OUT;
			//
			//	}
			//
			//	//uniform float4 _LightColor0;
			//	uniform float _Glossiness;
			//	uniform float _UseSpecular;
			//	uniform float _UseVertexColor;
			//	uniform float4 _Color;
			//	//uniform float4 _AmbientColor;
			//
			//	float4 frag(vertexOutput IN) : COLOR
			//	{
			//
			//
			//
			//		float colIntensity = length(_LightColor0.xyz);
			//
			//		//DIFFUSE
			//		float3 normal = normalize(IN.worldNormal);
			//		float atten = 1.0;
			//		float3 lightDir = (IN.posWorld.xyz - _WorldSpaceLightPos0.xyz);
			//		atten = length(lightDir) / length(_LightColor0.xyz);
			//		lightDir = normalize(lightDir);
			//		
			//		float NdotL = dot(lightDir, -normal);
			//		//if (NdotL >= 0) NdotL = 1;
			//		
			//		float shadow = LIGHT_ATTENUATION(IN);
			//		
			//		float lightIntensity = smoothstep(0.1, 0.12, NdotL * shadow);
			//
			//		//SPECULAR
			//		float3 viewDir = normalize(IN.viewDir);
			//		float3 halfVector = normalize(-lightDir + viewDir);
			//		float NdotH = dot(normal, halfVector)/(atten*atten);
			//		float specularIntensity = pow(NdotH, _Glossiness * _Glossiness);
			//		float specular = smoothstep(0.005, 0.008, specularIntensity) * lightIntensity;
			//		//if (check1 == 0) specular = 0;
			//
			//		//RIM
			//		float rimAmount = 0.06;
			//		float4 rimDot = (1 - dot(viewDir, normal)) * rimAmount;
			//
			//		//RETURN
			//		float4 sample = tex2D(_MainTex, IN.uv);
			//		float finalLight = (lightIntensity + specular * _UseSpecular /*+ rimDot*/)/* * _LightColor0*/;
			//
			//		//return finalLight * _LightColor0;
			//
			//		//return sample * LIGHT_ATTENUATION(IN);
			//		//return tex2D(_ShadowMapTexture, IN.uv);
			//
			//		float4 usingColor2 = sample * (1 - _UseVertexColor) + IN.color * _UseVertexColor;
			//
			//		float4 finalColor = (usingColor2 * _Color * finalLight * _LightColor0/(colIntensity/2) /*+ sample * _AmbientColor *(1 - finalLight)*/);
			//		return finalColor;
			//	}
			//	ENDCG
			//}
			//
			UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"

		
		}
		FallBack "Diffuse"
}