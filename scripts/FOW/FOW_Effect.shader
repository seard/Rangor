// Shader created with Shader Forge v1.02 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.02;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:0,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:9972,x:34346,y:32379,varname:node_9972,prsc:2|custl-5791-OUT;n:type:ShaderForge.SFN_Code,id:5207,x:31810,y:31454,varname:node_5207,prsc:2,code:ZgBsAG8AYQB0ACAAcgBlAHQAIAA9ACAAMAA7AA0ACgAgACAAaQBuAHQAIABpAHQAZQByAGEAdABpAG8AbgBzACAAPQAgADYAOwANAAoAIAAgAGYAbwByACAAKABpAG4AdAAgAGkAIAA9ACAAMAA7ACAAaQAgADwAIABpAHQAZQByAGEAdABpAG8AbgBzADsAIAArACsAaQApAA0ACgAgACAAewANAAoAIAAgACAAIAAgAGYAbABvAGEAdAAyACAAcAAgAD0AIABmAGwAbwBvAHIAKABVAFYAIAAqACAAKABpACsAMQApACkAOwANAAoAIAAgACAAIAAgAGYAbABvAGEAdAAyACAAZgAgAD0AIABmAHIAYQBjACgAVQBWACAAKgAgACgAaQArADEAKQApADsADQAKACAAIAAgACAAIABmACAAPQAgAGYAIAAqACAAZgAgACoAIAAoADMALgAwACAALQAgADIALgAwACAAKgAgAGYAKQA7AA0ACgAgACAAIAAgACAAZgBsAG8AYQB0ACAAbgAgAD0AIABwAC4AeAAgACsAIABwAC4AeQAgACoAIAA1ADcALgAwADsADQAKACAAIAAgACAAIABmAGwAbwBhAHQANAAgAG4AbwBpAHMAZQAgAD0AIABmAGwAbwBhAHQANAAoAG4ALAAgAG4AIAArACAAMQAsACAAbgAgACsAIAA1ADcALgAwACwAIABuACAAKwAgADUAOAAuADAAKQA7AA0ACgAgACAAIAAgACAAbgBvAGkAcwBlACAAPQAgAGYAcgBhAGMAKABzAGkAbgAoAG4AbwBpAHMAZQApACoANAAzADcALgA1ADgANQA0ADUAMwApADsADQAKACAAIAAgACAAIAByAGUAdAAgACsAPQAgAGwAZQByAHAAKABsAGUAcgBwACgAbgBvAGkAcwBlAC4AeAAsACAAbgBvAGkAcwBlAC4AeQAsACAAZgAuAHgAKQAsACAAbABlAHIAcAAoAG4AbwBpAHMAZQAuAHoALAAgAG4AbwBpAHMAZQAuAHcALAAgAGYALgB4ACkALAAgAGYALgB5ACkAIAAqACAAKAAgAGkAdABlAHIAYQB0AGkAbwBuAHMAIAAvACAAKABpACsAMQApACkAOwANAAoAIAAgAH0ADQAKACAAIAByAGUAdAB1AHIAbgAgAHIAZQB0AC8AaQB0AGUAcgBhAHQAaQBvAG4AcwA7AA==,output:0,fname:Function_node_5207,width:247,height:115,input:1,input_1_label:UV|A-3457-OUT;n:type:ShaderForge.SFN_Multiply,id:3457,x:31647,y:31454,varname:node_3457,prsc:2|A-8017-OUT,B-9236-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9236,x:31449,y:31618,ptovrint:False,ptlb:perlinSize,ptin:_perlinSize,varname:node_9236,prsc:2,glob:False,v1:4;n:type:ShaderForge.SFN_ConstantClamp,id:6932,x:32130,y:31445,varname:node_6932,prsc:2,min:0,max:1|IN-5207-OUT;n:type:ShaderForge.SFN_Subtract,id:8017,x:31449,y:31454,varname:node_8017,prsc:2|A-3183-OUT,B-5948-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3183,x:31193,y:31379,varname:node_3183,prsc:2|A-6955-OUT,B-5365-OUT;n:type:ShaderForge.SFN_ScreenPos,id:4813,x:31011,y:31555,varname:node_4813,prsc:2,sctp:1;n:type:ShaderForge.SFN_ComponentMask,id:6955,x:31011,y:31379,varname:node_6955,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9460-XYZ;n:type:ShaderForge.SFN_ViewPosition,id:9460,x:30833,y:31379,varname:node_9460,prsc:2;n:type:ShaderForge.SFN_Panner,id:5948,x:31193,y:31555,varname:node_5948,prsc:2,spu:0.04,spv:0.01|UVIN-4813-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:448,x:30833,y:31311,ptovrint:False,ptlb:orthoSize,ptin:_orthoSize,varname:node_448,prsc:2,glob:False,v1:6;n:type:ShaderForge.SFN_Divide,id:5365,x:31011,y:31235,varname:node_5365,prsc:2|A-9488-OUT,B-448-OUT;n:type:ShaderForge.SFN_Vector1,id:9488,x:30833,y:31235,varname:node_9488,prsc:2,v1:-1;n:type:ShaderForge.SFN_Tex2d,id:1371,x:32298,y:31967,ptovrint:False,ptlb:Environment,ptin:_Environment,varname:node_2452,prsc:2,tex:39265ed93cd145a4bb0b1036ec294cdb,ntxv:0,isnm:False|UVIN-9728-OUT;n:type:ShaderForge.SFN_Tex2d,id:5389,x:32298,y:32363,ptovrint:False,ptlb:FowTex,ptin:_FowTex,varname:node_2452,prsc:2,tex:83fb45df235206a489089776d53b78c1,ntxv:0,isnm:False|UVIN-9728-OUT;n:type:ShaderForge.SFN_Tex2d,id:8814,x:32298,y:32565,ptovrint:False,ptlb:Interactives,ptin:_Interactives,varname:node_2452,prsc:2,tex:699100db9f9bfef49afcaf93e86001fa,ntxv:0,isnm:False|UVIN-9728-OUT;n:type:ShaderForge.SFN_Blend,id:5554,x:32812,y:32381,varname:node_5554,prsc:2,blmd:13,clmp:True|SRC-5389-A,DST-5389-A;n:type:ShaderForge.SFN_OneMinus,id:2492,x:32969,y:32381,varname:node_2492,prsc:2|IN-5554-OUT;n:type:ShaderForge.SFN_Lerp,id:9322,x:33165,y:31965,varname:node_9322,prsc:2|A-3231-OUT,B-3836-OUT,T-5389-A;n:type:ShaderForge.SFN_Color,id:7183,x:33505,y:32704,ptovrint:False,ptlb:SilhouetteColor,ptin:_SilhouetteColor,varname:node_3903,prsc:2,glob:False,c1:0.1372549,c2:0.2470588,c3:0.3294118,c4:1;n:type:ShaderForge.SFN_Lerp,id:4461,x:33389,y:31965,varname:node_4461,prsc:2|A-9322-OUT,B-8814-RGB,T-6610-OUT;n:type:ShaderForge.SFN_Lerp,id:5791,x:34050,y:32667,varname:node_5791,prsc:2|A-4461-OUT,B-60-OUT,T-4075-OUT;n:type:ShaderForge.SFN_Multiply,id:60,x:33718,y:32704,varname:node_60,prsc:2|A-7183-RGB,B-9843-OUT;n:type:ShaderForge.SFN_Tex2d,id:288,x:32298,y:32163,ptovrint:False,ptlb:Silhouettes,ptin:_Silhouettes,varname:node_2452,prsc:2,tex:e8d1215f292c8ec4b95aafedd46fdb4a,ntxv:0,isnm:False|UVIN-9728-OUT;n:type:ShaderForge.SFN_Multiply,id:6610,x:33163,y:32381,varname:node_6610,prsc:2|A-2492-OUT,B-8814-A;n:type:ShaderForge.SFN_Blend,id:5121,x:32984,y:32888,varname:node_5121,prsc:2,blmd:17,clmp:True|SRC-2045-OUT,DST-2119-OUT;n:type:ShaderForge.SFN_Ceil,id:1322,x:33156,y:32888,varname:node_1322,prsc:2|IN-5121-OUT;n:type:ShaderForge.SFN_Blend,id:9843,x:33505,y:32888,varname:node_9843,prsc:2,blmd:19,clmp:True|SRC-2588-OUT,DST-288-A;n:type:ShaderForge.SFN_Add,id:4327,x:32635,y:32888,varname:node_4327,prsc:2|A-1371-RGB,B-1371-RGB;n:type:ShaderForge.SFN_Add,id:7393,x:32635,y:33037,varname:node_7393,prsc:2|A-8814-RGB,B-8814-RGB;n:type:ShaderForge.SFN_Exp,id:2045,x:32815,y:32888,varname:node_2045,prsc:2,et:0|IN-4327-OUT;n:type:ShaderForge.SFN_Exp,id:2119,x:32815,y:33037,varname:node_2119,prsc:2,et:0|IN-7393-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2588,x:33332,y:32888,varname:node_2588,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-1322-OUT;n:type:ShaderForge.SFN_TexCoord,id:2012,x:31806,y:32143,varname:node_2012,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:9728,x:32068,y:32170,varname:node_9728,prsc:2|A-2012-UVOUT,B-9741-OUT;n:type:ShaderForge.SFN_Vector1,id:9741,x:31885,y:32339,varname:node_9741,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:6433,x:32832,y:30947,ptovrint:False,ptlb:ShadowColor,ptin:_ShadowColor,varname:node_6433,prsc:2,glob:False,c1:0.05147058,c2:0.05147058,c3:0.05147058,c4:0;n:type:ShaderForge.SFN_TexCoord,id:4432,x:32130,y:31099,varname:node_4432,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:9174,x:32491,y:31099,ptovrint:False,ptlb:Environment_1,ptin:_Environment_1,varname:node_9174,prsc:2,tex:39265ed93cd145a4bb0b1036ec294cdb,ntxv:0,isnm:False|UVIN-1151-OUT;n:type:ShaderForge.SFN_Vector2,id:1471,x:32130,y:31010,varname:node_1471,prsc:2,v1:-0.02,v2:-0.01;n:type:ShaderForge.SFN_Add,id:1151,x:32321,y:31099,varname:node_1151,prsc:2|A-1832-OUT,B-4432-UVOUT;n:type:ShaderForge.SFN_OneMinus,id:1721,x:32491,y:31263,varname:node_1721,prsc:2|IN-1371-A;n:type:ShaderForge.SFN_Multiply,id:4828,x:32832,y:31099,varname:node_4828,prsc:2|A-3509-OUT,B-3390-OUT;n:type:ShaderForge.SFN_Multiply,id:148,x:33029,y:31099,varname:node_148,prsc:2|A-6433-RGB,B-6433-A,C-4828-OUT;n:type:ShaderForge.SFN_Tex2d,id:4426,x:32298,y:31776,ptovrint:False,ptlb:Ground,ptin:_Ground,varname:node_4426,prsc:2,tex:6674492486a15884782d62d8d29c7f63,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:3231,x:32938,y:31844,varname:node_3231,prsc:2|A-8463-OUT,B-148-OUT,T-9112-OUT;n:type:ShaderForge.SFN_Lerp,id:8463,x:32683,y:31782,varname:node_8463,prsc:2|A-4426-RGB,B-1371-RGB,T-1371-A;n:type:ShaderForge.SFN_Round,id:3390,x:32663,y:31263,varname:node_3390,prsc:2|IN-1721-OUT;n:type:ShaderForge.SFN_Floor,id:3509,x:32663,y:31099,varname:node_3509,prsc:2|IN-9174-A;n:type:ShaderForge.SFN_Multiply,id:9112,x:33029,y:31252,varname:node_9112,prsc:2|A-6433-A,B-4828-OUT;n:type:ShaderForge.SFN_Vector4Property,id:6875,x:32130,y:30864,ptovrint:False,ptlb:ShadowVector,ptin:_ShadowVector,varname:node_6875,prsc:2,glob:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_ComponentMask,id:1832,x:32306,y:30864,varname:node_1832,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-6875-XYZ;n:type:ShaderForge.SFN_Multiply,id:4075,x:33718,y:32888,varname:node_4075,prsc:2|A-7183-A,B-9843-OUT;n:type:ShaderForge.SFN_Blend,id:3836,x:32785,y:32009,varname:node_3836,prsc:2,blmd:20,clmp:True|SRC-6932-OUT,DST-5389-RGB;proporder:9236-448-1371-5389-8814-7183-288-6433-9174-4426-6875;pass:END;sub:END;*/

Shader "Shader Forge/FOW_Effect" {
    Properties {
        _perlinSize ("perlinSize", Float ) = 4
        _orthoSize ("orthoSize", Float ) = 6
        _Environment ("Environment", 2D) = "white" {}
        _FowTex ("FowTex", 2D) = "white" {}
        _Interactives ("Interactives", 2D) = "white" {}
        _SilhouetteColor ("SilhouetteColor", Color) = (0.1372549,0.2470588,0.3294118,1)
        _Silhouettes ("Silhouettes", 2D) = "white" {}
        _ShadowColor ("ShadowColor", Color) = (0.05147058,0.05147058,0.05147058,0)
        _Environment_1 ("Environment_1", 2D) = "white" {}
        _Ground ("Ground", 2D) = "white" {}
        _ShadowVector ("ShadowVector", Vector) = (0,0,0,0)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            float Function_node_5207( float2 UV ){
            float ret = 0;
              int iterations = 6;
              for (int i = 0; i < iterations; ++i)
              {
                 float2 p = floor(UV * (i+1));
                 float2 f = frac(UV * (i+1));
                 f = f * f * (3.0 - 2.0 * f);
                 float n = p.x + p.y * 57.0;
                 float4 noise = float4(n, n + 1, n + 57.0, n + 58.0);
                 noise = frac(sin(noise)*437.585453);
                 ret += lerp(lerp(noise.x, noise.y, f.x), lerp(noise.z, noise.w, f.x), f.y) * ( iterations / (i+1));
              }
              return ret/iterations;
            }
            
            uniform float _perlinSize;
            uniform float _orthoSize;
            uniform sampler2D _Environment; uniform float4 _Environment_ST;
            uniform sampler2D _FowTex; uniform float4 _FowTex_ST;
            uniform sampler2D _Interactives; uniform float4 _Interactives_ST;
            uniform float4 _SilhouetteColor;
            uniform sampler2D _Silhouettes; uniform float4 _Silhouettes_ST;
            uniform float4 _ShadowColor;
            uniform sampler2D _Environment_1; uniform float4 _Environment_1_ST;
            uniform sampler2D _Ground; uniform float4 _Ground_ST;
            uniform float4 _ShadowVector;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
/////// Vectors:
////// Lighting:
                float4 _Ground_var = tex2D(_Ground,TRANSFORM_TEX(i.uv0, _Ground));
                float2 node_9728 = (i.uv0*1.0);
                float4 _Environment_var = tex2D(_Environment,TRANSFORM_TEX(node_9728, _Environment));
                float2 node_1151 = (_ShadowVector.rgb.rg+i.uv0);
                float4 _Environment_1_var = tex2D(_Environment_1,TRANSFORM_TEX(node_1151, _Environment_1));
                float node_4828 = (floor(_Environment_1_var.a)*round((1.0 - _Environment_var.a)));
                float4 node_2206 = _Time + _TimeEditor;
                float4 _FowTex_var = tex2D(_FowTex,TRANSFORM_TEX(node_9728, _FowTex));
                float4 _Interactives_var = tex2D(_Interactives,TRANSFORM_TEX(node_9728, _Interactives));
                float4 _Silhouettes_var = tex2D(_Silhouettes,TRANSFORM_TEX(node_9728, _Silhouettes));
                float node_9843 = saturate((_Silhouettes_var.a-ceil(saturate(abs(exp((_Environment_var.rgb+_Environment_var.rgb))-exp((_Interactives_var.rgb+_Interactives_var.rgb))))).r));
                float3 finalColor = lerp(lerp(lerp(lerp(lerp(_Ground_var.rgb,_Environment_var.rgb,_Environment_var.a),(_ShadowColor.rgb*_ShadowColor.a*node_4828),(_ShadowColor.a*node_4828)),saturate((_FowTex_var.rgb/clamp(Function_node_5207( (((_WorldSpaceCameraPos.rg*((-1.0)/_orthoSize))-(float2(i.screenPos.x*(_ScreenParams.r/_ScreenParams.g), i.screenPos.y).rg+node_2206.g*float2(0.04,0.01)))*_perlinSize) ),0,1))),_FowTex_var.a),_Interactives_var.rgb,((1.0 - saturate(( _FowTex_var.a > 0.5 ? (_FowTex_var.a/((1.0-_FowTex_var.a)*2.0)) : (1.0-(((1.0-_FowTex_var.a)*0.5)/_FowTex_var.a)))))*_Interactives_var.a)),(_SilhouetteColor.rgb*node_9843),(_SilhouetteColor.a*node_9843));
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
