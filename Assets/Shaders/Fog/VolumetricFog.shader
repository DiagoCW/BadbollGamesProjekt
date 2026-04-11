Shader "VolumetricFog"
{
	Properties 
	{ 
		_MaxDistance("Max Distance", Float) = 100
		_StepSize("Step Size", Range(0.1, 20)) = 1
		_DensityMultiplier("Density Multiplier", Range(0, 10)) = 1
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

		Pass
		{
			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

			float _MaxDistance;
			float _DensityMultiplier;
			float _StepSize;

			float get_density()
			{
				return _DensityMultiplier;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				float depth = SampleSceneDepth(IN.texcoord);
				float3 worldPos = ComputeWorldSpacePosition(IN.texcoord, depth, UNITY_MATRIX_I_VP);

				float3 entryPoint = _WorldSpaceCameraPos;
				float3 viewDir = worldPos - _WorldSpaceCameraPos;
				float viewLength = length(viewDir);
				float3 rayDir = normalize(viewDir);

				float distLimit = min(viewLength, _MaxDistance);
				float distTraveled = 0;
				float transmittance = 0;

				while(distTraveled < distLimit)
				{
					float Density = get_density();
					if (Density > 0)
					{
						transmittance += Density * _StepSize;
					}
					distTraveled += _StepSize;
				}

				return transmittance;
			}
			ENDHLSL
		}
	}
}