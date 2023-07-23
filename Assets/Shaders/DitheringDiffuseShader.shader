Shader "Unlit/DitheringDiffuseShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Width ("Width", int) = 480
        _Height ("Height", int) = 270
        _Gray ("Gray", Range(0,1)) = 0.0
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Width;
            float _Height;
            float _Gray;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed floorFixed(fixed f)
            {
                return floor(f * 8 + 0.5) * 0.125;
            }
            fixed ditheredFixed(fixed f, fixed floorF, float2 uv)
            {
                int d = floor((f - floorF) * 64);
                int x = (uv.x * _Width);
                int y = (uv.y * _Height);

                if(d == 1)
                {
                    if((x % 2 == 0) & (y % 2 == 0) & ((x + y) % 4 == 0))
                    {
                        return floorF + 0.125;
                    }
                    return floorF;
                }
                
                if(d == 2)
                {
                    if((x + y) % 2 == 0)
                    {
                        return floorF + 0.125;
                    }
                    return floorF;
                }
                
                if(d == 3)
                {
                    if((x % 2 == 0) & (y % 2 == 0) & ((x + y) % 4 == 0))
                    {
                        return floorF;
                    }
                    return floorF + 0.125;
                }

                return floorF;
            }
            fixed4 sepiaCol(fixed4 col)
            {
                float colAverage = (col.x + col.y + col.z) / 3;
                fixed4 colGray = fixed4(colAverage, colAverage, colAverage, 0);
                col = lerp(col, colGray, _Gray);
                return col;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = sepiaCol(tex2D(_MainTex, i.uv));
                fixed4 col2 = fixed4(floorFixed(col.x), floorFixed(col.y), floorFixed(col.z), col.w);
                fixed4 colDithered = fixed4
                (
                    ditheredFixed(col.x, col2.x, i.uv),
                    ditheredFixed(col.y, col2.y, i.uv),
                    ditheredFixed(col.z, col2.z, i.uv),
                    col.w
                );
                return colDithered;
            }
            ENDCG
        }
    }
}
