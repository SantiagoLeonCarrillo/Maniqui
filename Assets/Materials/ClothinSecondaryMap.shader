Shader "Custom/ClothingWithSecondaryMap"
{
    Properties
    {
        _Color ("Main Color", Color) = (1, 1, 1, 1)
        _MainTex ("Base Texture", 2D) = "white" {}
        _SecondaryTex ("Secondary Texture", 2D) = "white" {}
        _SecondaryTexRotation ("Secondary Texture Rotation", Range(0, 360)) = 0
        _SecondaryTexOffset ("Secondary Texture Offset", Vector) = (0, 0, 0, 0)
        _SecondaryTexScale ("Secondary Texture Scale", Vector) = (1, 1, 1, 1)
        _SecondaryTexOpacity ("Secondary Texture Opacity", Range(0, 1)) = 1.0
        _Metallic ("Metallic", Range(0, 1)) = 0.0
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5
        _BumpMap ("Normal Map", 2D) = "bump" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };

        fixed4 _Color;
        sampler2D _MainTex;
        sampler2D _SecondaryTex;
        float _SecondaryTexRotation;
        float4 _SecondaryTexOffset;
        float4 _SecondaryTexScale;
        float _SecondaryTexOpacity;
        sampler2D _BumpMap;
        half _Metallic;
        half _Smoothness;

        float2 RotateUV(float2 uv, float angle, float2 pivot)
        {
            // Convertir ángulo a radianes
            float radians = angle * UNITY_PI / 180.0;
            float sinA, cosA;
            sincos(radians, sinA, cosA);
            
            // Mover UV al punto de pivote (centro), rotar y volver
            uv -= pivot;
            float2 rotatedUV;
            rotatedUV.x = uv.x * cosA - uv.y * sinA;
            rotatedUV.y = uv.x * sinA + uv.y * cosA;
            rotatedUV += pivot;
            
            return rotatedUV;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Color base
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // Ajustar UVs para rotación centrada y escala
            float2 pivot = float2(0.5, 0.5);
            float2 scaledUV = (IN.uv_MainTex - 0.5) * _SecondaryTexScale.xy + 0.5 + _SecondaryTexOffset.xy;
            float2 secondaryUV = RotateUV(scaledUV, _SecondaryTexRotation, pivot);

            // Corregir inversión en Y (opcional)
            secondaryUV.y = 1.0 - secondaryUV.y;

            // --- CAMBIO CLAVE: Descartar píxeles fuera del rango [0, 1] ---
            if (secondaryUV.x < 0.0 || secondaryUV.x > 1.0 || secondaryUV.y < 0.0 || secondaryUV.y > 1.0)
            {
                // Si está fuera del rango, no mostrar textura secundaria
                o.Albedo = c.rgb;
            }
            else
            {
                // Muestrear textura secundaria (sin repetición)
                fixed4 secondary = tex2D(_SecondaryTex, secondaryUV) * _SecondaryTexOpacity;
                o.Albedo = lerp(c.rgb, secondary.rgb, secondary.a);
            }

            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    Fallback "Standard"
}