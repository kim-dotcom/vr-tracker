// Upgrade NOTE: replaced 'PositionFog()' with transforming position into clip space.
// Upgrade NOTE: replaced 'V2F_POS_FOG' with 'float4 pos : SV_POSITION'
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Glass/Reflective" { 
Properties { 
   _Color ("Main Color", Color) = (0.3,0.3,0.3,0) 
   _SpecularColor ("Specular Color", Color) = (0.7,0.7,0.7,1) 
   _Cube ("Reflection Cubemap", Cube) = "_Skybox" {TexGen CubeNormal} 
} 

Category { 
   Tags {Queue=Transparent} 
   ZWrite Off 
   Blend SrcAlpha OneMinusSrcAlpha 
   Lighting Off 
   Colormask RGB 

   SubShader { 
      Pass { 
	  Cull Off  //Makes it doublesided
          
CGPROGRAM 
// profiles arbfp1 
// fragment frag 
// fragmentoption ARB_fog_exp2 
// fragmentoption ARB_precision_hint_fastest 
// vertex vert 
#include "UnityCG.cginc" 

struct v2f { 
   float4 pos : SV_POSITION; 
   float3  normal   : TEXCOORD0; 
   float3   viewDir   : TEXCOORD1; 
}; 

v2f vert (appdata_tan v) 
{    
   v2f o; 
   o.pos = UnityObjectToClipPos (v.vertex); 
   o.normal = mul( (float3x3)unity_ObjectToWorld, v.normal ); 
   o.viewDir = mul( (float3x3)unity_ObjectToWorld, ObjSpaceViewDir(v.vertex) ); 
    
   return o; 
} 

uniform samplerCUBE _Cube : register(s0); 
uniform float4 _Color; 
uniform float4 _SpecularColor; 

float4 frag (v2f i)  : COLOR 
{ 
   float3 normal = i.normal; 
   i.viewDir = normalize(i.viewDir); 
   half nsv = saturate(dot( normal, i.viewDir )); 
    
   // calculate reflection vector in world space 
   half3 r = reflect(-i.viewDir, normal); 
    
   half4 reflcolor = texCUBE(_Cube, r); 
    
   half fresnel = 1 - nsv*0.5; 
   half3 c = lerp( _Color.rgb, reflcolor.rgb*_SpecularColor.rgb, fresnel ); 
    
   return half4( c, fresnel*0.6 ); 
} 
ENDCG  
         SetTexture [_Cube] {combine texture} 
      } 
   } 
    
   SubShader { 
      Pass { 
         Color [_Color] 
      } 
   } 
} 

}