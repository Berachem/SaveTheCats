Shader "Unlit/NuageShader"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1, 1, 1, 1)  // Couleur de base
        _PerlinScale("Perlin Scale", Float) = 5.0      // Echelle du bruit
        _CloudDensity("Cloud Density", Float) = 1.0    // Contrôle de l'opacité du nuage
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha  // Mélange pour gérer la transparence

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _MainColor;
            float _PerlinScale;
            float _CloudDensity;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0)).xyz;
                return o;
            }

            // Fonction pour générer le bruit
            float hash(float3 p)
            {
                p = frac(p * 0.3183099 + 0.1);
                p *= 17.0;
                return frac(p.x * p.y * p.z * (p.x + p.y + p.z));
            }

            float perlinNoise(float3 p)
            {
                float3 i = floor(p);  // Partie entière
                float3 f = frac(p);   // Partie fractionnaire

                // Lissage
                f = f * f * (3.0 - 2.0 * f);

                // Interpolation trilinéaire
                return lerp(lerp(lerp(hash(i + float3(0, 0, 0)), hash(i + float3(1, 0, 0)), f.x),
                                 lerp(hash(i + float3(0, 1, 0)), hash(i + float3(1, 1, 0)), f.x), f.y),
                            lerp(lerp(hash(i + float3(0, 0, 1)), hash(i + float3(1, 0, 1)), f.x),
                                 lerp(hash(i + float3(0, 1, 1)), hash(i + float3(1, 1, 1)), f.x), f.y),
                            f.z);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Calcul des coordonnées pour le bruit
                float3 noisePosition = i.worldPos * _PerlinScale;

                // Valeur du bruit
                float noiseValue = perlinNoise(noisePosition);

                // Ajuster l'alpha en fonction du bruit
                float alpha = saturate(noiseValue * _CloudDensity);

                // Générer la couleur finale avec transparence
                return fixed4(_MainColor.rgb, alpha);
            }
            ENDCG
        }
    }
    FallBack "Transparent/VertexLit"
}

