Shader "VolumetricFog"
{
	Properties 
	{ 
		// Modified Area
		_MaxHorizontalDistance("Max Horizontal Distance", Float) = 100
		_MaxVerticalDistance("Max Vertical Distance", Float) = 50
		// Modified Area

		_Color("Color", Color) = (1,1,1,1)
		_MaxDistance("Max Distance", Float) = 100
		_StepSize("Step Size", Range(0.1, 20)) = 1
		_DensityMultiplier("density Multiplier", Range(0, 10)) = 1
		_NoiseOffset("Noise Offset", float) = 0

		_FogNoise("Fog noise", 3D) = "white" {}
		_NoiseTiling("Noise tiling", float) = 1
		_DensityThreshold("Density threshold", Range(0, 1)) = 0.1

		[HDR]_LightContribution("Light Contribution", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

		Pass
		{
			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment frag
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

			// Modified Area
			float _MaxHorizontalDistance;
			float _MaxVerticalDistance;
			// Modified Area

			float4 _Color;
			float _MaxDistance;
			float _DensityMultiplier;
			float _StepSize;
			float _NoiseOffset;
			TEXTURE3D(_FogNoise);
			float _DensityThreshold;
			float _NoiseTiling;
			float4 _LightContribution;

			float get_density(float3 worldPos)
			{
				float4 noise = _FogNoise.SampleLevel(sampler_TrilinearRepeat, worldPos * 0.01 * _NoiseTiling, 0);
				float density = dot(noise, noise);
				density = saturate(density - _DensityThreshold) * _DensityMultiplier;
				return density;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				float4 col = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, IN.texcoord);
				float depth = SampleSceneDepth(IN.texcoord);
				float3 worldPos = ComputeWorldSpacePosition(IN.texcoord, depth, UNITY_MATRIX_I_VP);

				float3 entryPoint = _WorldSpaceCameraPos;
				float3 viewDir = worldPos - _WorldSpaceCameraPos;
				float viewLength = length(viewDir);
				
				// Early exit if view direction is too small (prevents division by zero)
				if (viewLength < 0.001)
					return col;
				
				float3 rayDir = normalize(viewDir);

				float2 pixelCoords = IN.texcoord * _BlitTexture_TexelSize.zw;
				float distLimit = min(viewLength, _MaxDistance);
				float distTraveled = InterleavedGradientNoise(pixelCoords, (int)(_Time.y / max(HALF_EPS, unity_DeltaTime.x))) * _NoiseOffset;
				float transmittance = 1;
				float4 fogCol = _Color;
				
				// Ensure step size is never zero
				float safeStepSize = max(_StepSize, 0.1);

				while(distTraveled < distLimit)
				{
					float3 rayPos = entryPoint + rayDir * distTraveled;
					
					// Skip first steps near camera to avoid shadow coordinate issues
					if (distTraveled < 0.5)
					{
						distTraveled += safeStepSize;
						continue;
					}
					
					// Modified Area
					float3 offset = rayPos - _WorldSpaceCameraPos;
					float horizontalDist = length(offset.xz);
					float verticalDist = abs(offset.y);
					if (horizontalDist > _MaxHorizontalDistance || verticalDist > _MaxVerticalDistance)
						break;
					// Modified Area
					
					float density = get_density(rayPos);
					if (density > 0.001)
					{
						// Validate rayPos is far enough from camera before shadow sampling
						float distFromCamera = length(rayPos - _WorldSpaceCameraPos);
						if (distFromCamera > 0.5 && !any(isinf(rayPos)) && !any(isnan(rayPos)))
						{
							#if defined(_MAIN_LIGHT_SHADOWS) || defined(_MAIN_LIGHT_SHADOWS_CASCADE) || defined(_MAIN_LIGHT_SHADOWS_SCREEN)
								float4 shadowCoord = TransformWorldToShadowCoord(rayPos);
								// Additional safety: verify shadowCoord.w is valid before GetMainLight uses it
								if (abs(shadowCoord.w) > 0.00001)
								{
									Light mainLight = GetMainLight(shadowCoord);
									fogCol.rgb += mainLight.color.rgb * _LightContribution.rgb * density * mainLight.shadowAttenuation * safeStepSize;
								}
								else
								{
									// Fallback: use light without shadows
									Light mainLight = GetMainLight();
									fogCol.rgb += mainLight.color.rgb * _LightContribution.rgb * density * safeStepSize;
								}
							#else
								Light mainLight = GetMainLight();
								fogCol.rgb += mainLight.color.rgb * _LightContribution.rgb * density * safeStepSize;
							#endif
							
							transmittance *= exp(-density * safeStepSize);
						}
					}
					distTraveled += safeStepSize;
				}

				return lerp(col, fogCol, 1.0 - saturate(transmittance));
			}
			ENDHLSL
		}
	}
}