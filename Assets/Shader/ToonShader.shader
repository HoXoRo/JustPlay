Shader "Unlit/ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineWidth("Outline Width", Range(0.01, 1)) = 1
        _OutLineColor("OutLine Color", Color) = (0.5, 0.5, 0.5, 1)
        
        _RampStart ("交界起始 RampStart", Range(0.1, 1)) = 0.3
        _RampSize ("交界大小 RampSize", Range(0, 1)) = 0.1
        _RampSmooth ("交界柔和度 RampSmooth", Range(0.01, 1)) = 0.1
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;

                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _RampStart;
            float _RampSize;
            float _RampStep;
            float _RampSmooth;
            float3 _DarkColor;
            float3 _LightColor;

            float linearstep (float min, float max, float t)
            {
                return saturate((t - min) / (max - min));
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.worldNormal = normalize(UnityObjectToWorldNormal(v.normal));
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // 得到顶点法线
                float3 normal = normalize(i.worldNormal);
                // 得到光照方向
                float3 worldLightDir = UnityWorldSpaceLightDir(i.worldPos);
                // NoL代表表面接受的能量大小
                float NoL = dot(i.worldNormal, worldLightDir);
                // 计算half-lambert亮度值
                float halfLambert = NoL * 0.5 + 0.5;

                float ramp = linearstep(_RampStart, _RampStart + _RampSize, halfLambert);
                float step = ramp * _RampStep;
                float gridStep = floor(step);
                float smoothStep = smoothstep(gridStep, gridStep + _RampSmooth, step) + gridStep;
                ramp = smoothStep / _RampStep;

                float3 rampColor = lerp(_DarkColor, _LightColor, ramp);
                rampColor *= col;

                float3 finalColor = saturate(rampColor);
                
                return float4(finalColor,1);;
            }
            ENDCG
        }
        
        Pass
        {
            Cull Front
            
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            float _OutlineWidth;
            float4 _OutLineColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv :TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v) 
            {
                v2f o;
                // 顶点沿着法线方向外扩(放大模型)
                float4 newVertex = float4(v.vertex.xyz + v.normal * _OutlineWidth * 0.01 ,1);
                // UnityObjectToClipPos(v.vertex) 将模型空间下的顶点转换到齐次裁剪空间
                o.vertex = UnityObjectToClipPos(newVertex);
                
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // 返回线条颜色
                return _OutLineColor;
            }
            
            ENDCG
        }
    }
}
