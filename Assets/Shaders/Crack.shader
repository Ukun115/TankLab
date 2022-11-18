Shader "Custom/Crack" {
    Properties{
        //�A���x�h
        [Header(Albedo)]
        //�x�[�X�J���[
        [MainColor] _BaseColor("Base Color", Color) = (1.0, 1.0, 1.0, 1.0)
        //�x�[�X�}�b�v
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}

        //�@��
        [Header(NormalMap)]
        [Toggle(_NORMALMAP)] _NORMALMAP("Normal Map�g�p�L��", Int) = 0
        //�o���v�}�b�v
        [NoScaleOffset] _BumpMap("Normal Map", 2D) = "bump" {}
        [HideInInspector] _BumpScale("Bump Scale", Float) = 1.0

        //�I�N���[�W����
        [Header(Occlution)]
        [Toggle(_OCCLUSIONMAP)] _OCCLUSIONMAP("Occlusion Map�g�p�L��", Int) = 0
            //�I�N���[�W�����}�b�v
        [NoScaleOffset] _OcclusionMap("Occlusion Map", 2D) = "white" {}
        [HideInInspector] _OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0

        //������&���炩��
        [Header(Metallic and Smoothness)]
        _Smoothness("Smoothness(Map�g�p����Alpha=1�̉ӏ��̒l)", Range(0.0, 1.0)) = 0.0
        [Toggle(_METALLICSPECGLOSSMAP)] _METALLICSPECGLOSSMAP("Metallic and Smoothness Map�g�p�L��", Int) = 0
        _Metallic("Metallic(Map�s�g�p���̂�)", Range(0.0, 1.0)) = 0.0
        //������&���炩���}�b�v
        [NoScaleOffset] _MetallicGlossMap("Metallic and Smoothnes Map", 2D) = "white" {}

        //���o
        [Header(Emission)]
        [Toggle(_EMISSION)] _EMISSION("Emission�g�p�L��", Int) = 0
        [HDR] _EmissionColor("Emission Color", Color) = (0.0 ,0.0, 0.0)
        //�G�~�b�V�����}�b�v
        [NoScaleOffset] _EmissionMap("Emission Map", 2D) = "white" {}

        //�Ђъ���
        [Header(Crack)]
        _CrackProgress("�N���b�N�i�s�", Range(0.0, 1.0)) = 0.0
        [HDR] _CrackColor("�N���b�N�F", Color) = (0.0, 0.0, 0.0, 1.0)
        _CrackDetailedness("�N���b�N�͗l�ׂ̍���", Range(0.0, 8.0)) = 3.0
        _CrackDepth("�N���b�N�̐[��", Range(0.0, 1.0)) = 0.5
        _AdditionalCrackDepthForLighting("���C�e�B���O�v�Z���Ɏ��ۂ̒l�ɒǉ�����N���b�N�[�� ", Float) = 1.0
        _CrackWidth("�N���b�N�̕�", Range(0.01, 0.1)) = 0.05
        _CrackWallWidth("�N���b�N�̕Ǖ����̕�", Range(0.001, 0.2)) = 0.08
            // �t���O�����g�V�F�[�_�[�ŃN���b�N�Ώۂ��ǂ����̍Čv�Z���s�����ǂ���
            [Toggle] _DrawsCrackWithPixelUnit("�s�N�Z���P�ʂŃN���b�N�͗l�̍Čv�Z���s����", Int) = 0

            [Space]
            _RandomSeed("�N���b�N�͗l�̃����_���V�[�h(�񕉐����̂݉�)", Int) = 0

            [Header(SubdividingPolygon)]
            _SubdividingCount("�ו������ɕӂ������ɕ������邩(1�ȉ��͕�������)", Int) = 1
            _SubdividingInsideScaleFactor("�ו������̃|���S�������ւ̐V�|���S�������x����", Range(0.0, 1.0)) = 1.0
            [Toggle] _PN_TRIANGLES("PN-Triangles�K�p�L��", Int) = 0
            _PnTriFactor("PN-Triangles�K�p�W��", Range(0.0, 1.0)) = 1.0
            [Toggle] _AdaptsPolygonEdgeToPnTri("PN-Triangles��ӂɂ��K�p���邩�ǂ���", Int) = 1
    }

        SubShader{
            Tags {
                "RenderType" = "Opaque"
                "RenderPipeline" = "UniversalPipeline"
                "UniversalMaterialType" = "Lit"
            }
            LOD 300

            HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            ENDHLSL

            Pass {
                Name "Crack"
                Tags { "LightMode" = "UniversalForward" }

                HLSLPROGRAM

                // -------------------------------------
                // Material Keywords
                #pragma shader_feature_local _NORMALMAP
                #pragma shader_feature_local_fragment _ALPHATEST_ON
                #pragma shader_feature_local_fragment _ALPHAPREMULTIPLY_ON
                #pragma shader_feature_local_fragment _EMISSION
                #pragma shader_feature_local_fragment _METALLICSPECGLOSSMAP
                #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                #pragma shader_feature_local_fragment _OCCLUSIONMAP
                #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
                #pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF
                #pragma shader_feature_local_fragment _SPECULAR_SETUP
                #pragma shader_feature_local _RECEIVE_SHADOWS_OFF

                // -------------------------------------
                // Universal Pipeline keywords
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
                #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
                #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
                #pragma multi_compile_fragment _ _SHADOWS_SOFT

                //--------------------------------------
                // GPU Instancing
                #pragma multi_compile_instancing

                // -------------------------------------
                // Local Keywords
                #pragma shader_feature_local _ _PN_TRIANGLES_ON


                #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/Shaders/LitForwardPass.hlsl"

                #pragma vertex Vert
                #pragma hull Hull
                #pragma domain Domain
                #pragma fragment Frag

                #pragma require tessellation tessHW


                // ---------------------------------------------------------------------------------------
                // �ϐ��錾
                // ---------------------------------------------------------------------------------------
                float _CrackProgress;
                half4 _CrackColor;
                float _CrackDetailedness;
                float _CrackDepth;
                float _AdditionalCrackDepthForLighting;
                float _CrackWidth;
                float _CrackWallWidth;
                bool _DrawsCrackWithPixelUnit;
                uint _RandomSeed;

                int  _SubdividingCount;
                float _SubdividingInsideScaleFactor;
    #ifdef _PN_TRIANGLES_ON
                float _PnTriFactor;
                bool _AdaptsPolygonEdgeToPnTri;

                static float OneThird = rcp(3.0);
                static float OneSixth = rcp(6.0);
    #endif


                // ---------------------------------------------------------------------------------------
                // �\����
                // ---------------------------------------------------------------------------------------
                struct v2d {
                    float4 positionOS : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normalOS : NORMAL;
                    float3 normalWS : TEXCOORD1;
    #ifdef _NORMALMAP
                    half4 tangentWS : TEXCOORD2;
    #endif
                };

                struct patchConstParam {
                    float edgeTessFactors[3] : SV_TessFactor;
                    float insideTessFactor : SV_InsideTessFactor;

    #ifdef _PN_TRIANGLES_ON
                    // PN-Triangles�v�Z�p�̃R���g���[���|�C���g
                    float3 b111 : TEXCOORD0;
                    float3 positionsOS[3][3] : TEXCOORD1;
    #endif
                };

                struct d2f {
                    float4 positionCS : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float3 initNormalWS : TEXCOORD1;
                    float3 positionOS : TEXCOORD2;
    #ifdef _NORMALMAP
                    half4 initTangentWS : TEXCOORD3;
    #endif
                    float3 initPositionOS : TEXCOORD4;
                    float3 initNormalOS : NORMAL;
                    float crackLevel : TEXCOORD5;
                };


                // ---------------------------------------------------------------------------------------
                // ���\�b�h
                // ---------------------------------------------------------------------------------------
                /**
                 * Xorshift32��p����32bit�̋[�������𐶐�����
                 */
                uint Xorshift32(uint value) {
                    value = value ^ (value << 13);
                    value = value ^ (value >> 17);
                    value = value ^ (value << 5);
                    return value;
                }

                /**
                 * �����̒l��1�����̏����Ƀ}�b�s���O����
                 */
                float MapToFloat(uint value) {
                    const float precion = 100000000.0;
                    return (value % precion) * rcp(precion);
                }

                /**
                 * 3�����̃����_���Ȓl���Z�o����
                 */
                float3 Random3(uint3 src, int seed) {
                    uint3 random;
                    random.x = Xorshift32(mad(src.x, src.y, src.z));
                    random.y = Xorshift32(mad(random.x, src.z, src.x) + seed);
                    random.z = Xorshift32(mad(random.y, src.x, src.y) + seed);
                    random.x = Xorshift32(mad(random.z, src.y, src.z) + seed);

                    return float3(MapToFloat(random.x), MapToFloat(random.y), MapToFloat(random.z));
                }

                /**
                 * �w�肵�����W�ɑ΂��āA�{���m�C�p�^�[���̍ł��߂������_���_�ƁA2�Ԗڂɋ߂������_���_���擾����
                 */
                void CreateVoronoi(float3 pos, out float3 closest, out float3 secondClosest, out float secondDistance) {
                    // �Z���ԍ������̒l�ƂȂ�Ȃ��悤�ɃI�t�Z�b�g���Z
                    const uint offset = 100;
                    uint3 cellIdx;
                    float3 reminders = modf(pos + offset, cellIdx);

                    // �Ώےn�_����������Z���Ɨאڂ���Z���S�Ăɑ΂��ă����_���_�Ƃ̋������`�F�b�N��
                    // 1�ԋ߂��_��2�Ԗڂɋ߂��_�����t����
                    float2 closestDistances = 8.0;

                    [unroll]
                    for (int i = -1; i <= 1; i++)
                    [unroll]
                    for (int j = -1; j <= 1; j++)
                    [unroll]
                    for (int k = -1; k <= 1; k++) {
                        int3 neighborIdx = int3(i, j, k);

                        // ���̃Z�����ł̃����_���_�̑��Έʒu���擾
                        float3 randomPos = Random3(cellIdx + neighborIdx, _RandomSeed);
                        // �Ώےn�_���烉���_���_�Ɍ������x�N�g��
                        float3 vec = randomPos + float3(neighborIdx)-reminders;
                        // �����͑S�ē��Ŕ�r
                        float distance = dot(vec, vec);

                        if (distance < closestDistances.x) {
                            closestDistances.y = closestDistances.x;
                            closestDistances.x = distance;
                            secondClosest = closest;
                            closest = vec;
                        }
     else if (distance < closestDistances.y) {
      closestDistances.y = distance;
      secondClosest = vec;
  }
}

secondDistance = closestDistances.y;
}

                /**
                 * �w�肵�����W���{���m�C�}�̋��E���ƂȂ邩�ǂ�����0�`1�ŕԂ�
                 */
                float GetVoronoiBorder(float3 pos, out float secondDistance) {
                    float3 a, b;
                    CreateVoronoi(pos, a, b, secondDistance);

                    /*
                     * �ȉ��̃x�N�g���̓��ς����E���܂ł̋����ƂȂ�
                     * �E�Ώےn�_����A1�ԋ߂������_���_��2�Ԗڂɋ߂��_�̒��_�Ɍ������x�N�g��
                     * �E1�ԋ߂��_��2�Ԗڂɋ߂��_�����Ԑ��̒P�ʃx�N�g��
                     */
                    float distance = dot(0.5 * (a + b), normalize(b - a));

                    return 1.0 - smoothstep(_CrackWidth, _CrackWidth + _CrackWallWidth, distance);
                }

                /**
                 * �w�肵�����W�̂Ђѓx������0�`1�ŕԂ�
                 */
                float GetCrackLevel(float3 pos) {
                    // �{���m�C�}�̋��E���ŋ[���I�ȂЂі͗l��\��
                    float secondDistance;
                    float level = GetVoronoiBorder(pos * _CrackDetailedness, secondDistance);

                    /*
                     * �����I�ɂЂт��������߂Ƀm�C�Y��ǉ�
                     * �v�Z�ʂ����Ȃ��čςނ悤�Ƀ{���m�C��F2(2�Ԗڂɋ߂��_�Ƃ̋���)�𗘗p����
                     * ���������l�ȉ��̏ꍇ�͂ЂёΏۂ���O��
                     */
                    float f2Factor = 1.0 - sin(_CrackProgress * PI * 0.5);
                    float minTh = (2.9 * f2Factor);
                    float maxTh = (3.5 * f2Factor);
                    float factor = smoothstep(minTh, maxTh, secondDistance * 2.0);
                    level *= factor;

                    return level;
                }

                /**
                 * �Ђт���������̍��W���v�Z����
                 */
                float3 CalcCrackedPos(float3 localPos, float3 localNormal, float3 worldNormal, out float crackLevel) {
                    crackLevel = (_CrackProgress == 0 || dot(worldNormal, GetViewForwardDir()) > 0.5) ? 0.0 : GetCrackLevel(localPos);

                    // �ЂёΏۂ̏ꍇ�͖@���Ƌt�����ɉ��܂���
                    float depth = crackLevel * _CrackDepth;
                    localPos -= localNormal * depth;

                    return localPos;
                }

    #ifdef _PN_TRIANGLES_ON
                /**
                 * [�e�b�Z���[�V�����V�F�[�_�[�p]
                 * PN-Triangles�p�̃R���g���[���|�C���g���Z�o����
                 */
                float3 CalcControlPointForPnTri(float3 posA, float3 posB, float3 normalA) {
                    // PosA��PosB�����Ԑ�����1:2�ɕ������n�_��PosA�̐ڕ��ʏ�Ɉړ��������W���Z�o
                    return (2.0 * posA + posB - (dot((posB - posA), normalA) * normalA)) * OneThird;
                }

                /**
                 * [�p�b�`�萔�֐��p]
                 * PN-Triangles��B111�̈ʒu���v�Z����
                 *
                 * �Q�l�Fhttps://alex.vlachos.com/graphics/CurvedPNTriangles.pdf
                 */
                float3 CalcPnTriB111Pos(float3 controlPoints[3][3]) {
                    float3 b300 = controlPoints[0][0];
                    float3 b210 = controlPoints[0][1];
                    float3 b120 = controlPoints[0][2];

                    float3 b030 = controlPoints[1][0];
                    float3 b021 = controlPoints[1][1];
                    float3 b012 = controlPoints[1][2];

                    float3 b003 = controlPoints[2][0];
                    float3 b102 = controlPoints[2][1];
                    float3 b201 = controlPoints[2][2];

                    float3 e = (b210 + b120 + b021 + b012 + b102 + b201) * OneSixth;
                    float3 v = (b003 + b030 + b300) * OneThird;

                    return e + ((e - v) * 0.5);
                }
    #endif

                /**
                 * [�h���C���V�F�[�_�[�p]
                 * OutputTopology:triangle_cw�Ő������ꂽ�d�S���W�n�̍��W��src�̋�Ԃ̍��W�Ɋ��Z����
                 */
                float3 CalcSubdividedPos(float3 src[3], float3 baryCentricCoords) {
                    return src[0] * baryCentricCoords.x + src[1] * baryCentricCoords.y + src[2] * baryCentricCoords.z;
                }

    #if _PN_TRIANGLES_ON
                /**
                 * [�h���C���V�F�[�_�[�p]
                 * PN-Triangles��p���ăJ�[�u��ɂȂ�悤�ɕψʂ��������W���Z�o����
                 *
                 * �ȉ����Q�l�ɂ���
                 * - PN-Triangles�̗��_
                 *   https://alex.vlachos.com/graphics/CurvedPNTriangles.pdf
                 * - ����(PN-Triangles-AEN�̎����ł͂��邪�APN-Triangles�Ƃ̋��ʕ���������)
                 *   https://developer.download.nvidia.com/whitepapers/2010/PN-AEN-Triangles-Whitepaper.pdf
                 */
                float3 CalcPnTriPosition(float3 controlPoints[3][3], float3 b111, float3 baryCentricCoords) {
                    float u = baryCentricCoords.x;
                    float v = baryCentricCoords.y;
                    float w = baryCentricCoords.z;
                    float uu = u * u;
                    float vv = v * v;
                    float ww = w * w;
                    float uu3 = 3.0 * uu;
                    float vv3 = 3.0 * vv;
                    float ww3 = 3.0 * ww;

                    return controlPoints[0][0] * u * uu
                        + controlPoints[1][0] * v * vv
                        + controlPoints[2][0] * w * ww
                        + controlPoints[0][1] * uu3 * v
                        + controlPoints[0][2] * vv3 * u
                        + controlPoints[1][1] * vv3 * w
                        + controlPoints[1][2] * ww3 * v
                        + controlPoints[2][1] * ww3 * u
                        + controlPoints[2][2] * uu3 * w
                        + b111 * 6.0 * w * u * v;
                }
    #endif

                /**
                 * [�t���O�����g�V�F�[�_�[�p]
                 * CrackLevel�ɉ�����Occlusion���Z�o����
                 */
                half CalcOcclusion(float crackLevel) {
                    // �Ђт̐[���ɉ����ĉe��Z������
                    half occlusion = pow(lerp(1.0, 0.9, crackLevel), 2.0);
                    // �Ђт��[�������ŁA�אڃs�N�Z���̍��፷���傫���ꍇ�͉e��Z������
                    occlusion *= (crackLevel > 0.95 ? lerp(0.9, 1.0, 1.0 - smoothstep(0.0, 0.1, max(abs(ddy(crackLevel)), abs(ddx(crackLevel))))) : 1.0);

                    return occlusion;
                }


                // ---------------------------------------------------------------------------------------
                // �V�F�[�_�[�֐�
                // ---------------------------------------------------------------------------------------
                /**
                 * ���_�V�F�[�_�[
                 */
                v2d Vert(Attributes input) {
                    v2d output;

                    output.positionOS = input.positionOS;
                    output.normalOS = input.normalOS;

                    Varyings varyings = LitPassVertex(input);
                    output.uv = varyings.uv;
                    output.normalWS = varyings.normalWS;

    #ifdef _NORMALMAP
                    output.tangentWS = varyings.tangentWS;
    #endif

                    return output;
                }

                /**
                 * ���C���n���V�F�[�_�[
                 */
                [domain("tri")]
                [partitioning("integer")]
                [outputtopology("triangle_cw")]
                [outputcontrolpoints(3)]
                [patchconstantfunc("PatchConstantFunc")]
                v2d Hull(InputPatch<v2d, 3> inputs, uint id:SV_OutputControlPointID) {
                    v2d output = inputs[id];
                    return output;
                }

                /**
                 * �p�b�`�萔�֐�
                 */
                patchConstParam PatchConstantFunc(InputPatch<v2d, 3> inputs) {
                    patchConstParam output;

                    int subdividingCount = (_CrackProgress == 0.0 || _SubdividingCount <= 1) ? 0 : _SubdividingCount;

                    [unroll]
                    for (uint i = 0; i < 3; i++) {
                        // �J�����������Ă��Ȃ��ʂ͕������Ȃ�
                        subdividingCount = subdividingCount > 0 && dot(inputs[i].normalWS, GetViewForwardDir()) <= 0.5 ? subdividingCount : 0;
                    }
                    // �v���p�e�B�ݒ�ɍ����������Z�o
                    float3 rawEdgeFactors = subdividingCount;
                    float3 roundedEdgeTessFactors;
                    float roundedInsideTessFactor;
                    float unroundedInsideTessFactor;
                    ProcessTriTessFactorsAvg(rawEdgeFactors, _SubdividingInsideScaleFactor, roundedEdgeTessFactors, roundedInsideTessFactor, unroundedInsideTessFactor);

                    // �ӑ��A�������ꂼ��̕��������w��
                    output.edgeTessFactors[0] = roundedEdgeTessFactors.x;
                    output.edgeTessFactors[1] = roundedEdgeTessFactors.y;
                    output.edgeTessFactors[2] = roundedEdgeTessFactors.z;
                    output.insideTessFactor = roundedInsideTessFactor;

    #ifdef _PN_TRIANGLES_ON
                    // PN-Triangles�v�Z�p�̃R���g���[���|�C���g�Z�o
                    bool usesPnTriangles = _PnTriFactor > 0.0 && subdividingCount > 1;

                    [unroll]
                    for (i = 0; i < 3; i++) {
                        uint nextId = (i + 1) % 3;
                        output.positionsOS[i][0] = inputs[i].positionOS.xyz;

                        if (usesPnTriangles) {
                            output.positionsOS[i][1]
                                = CalcControlPointForPnTri(inputs[i].positionOS.xyz, inputs[nextId].positionOS.xyz, inputs[i].normalOS);
                            output.positionsOS[i][2]
                                = CalcControlPointForPnTri(inputs[nextId].positionOS.xyz, inputs[i].positionOS.xyz, inputs[nextId].normalOS);
                        }
     else {
      output.positionsOS[i][1] = 0.0;
      output.positionsOS[i][2] = 0.0;
  }
}

output.b111 = usesPnTriangles ? CalcPnTriB111Pos(output.positionsOS) : 0.0;
#endif

                return output;
            }

                /**
                 * �h���C���V�F�[�_�[
                 */
                [domain("tri")]
                d2f Domain(patchConstParam param, const OutputPatch<v2d, 3> inputs, float3 baryCentricCoords:SV_DomainLocation) {
                    d2f output;

                    // �܂��̓t���b�g�ȃ|���S����ɎZ�o���ꂽ���W�����߂�
                    // �Z�o���ꂽ���W���d�S���W�n���烍�[�J�����W���Ɋ��Z
                    float3 srcLocalPositions[3];
                    float3 srcLocalNormals[3];
                    float3 srcUVs[3];
                    float3 srcWorldTangents[3];
                    [unroll]
                    for (uint i = 0; i < 3; i++) {
                        srcLocalPositions[i] = inputs[i].positionOS.xyz;
                        srcLocalNormals[i] = inputs[i].normalOS;
                        srcUVs[i] = float3(inputs[i].uv, 0.0);
    #ifdef _NORMALMAP
                        srcWorldTangents[i] = inputs[i].tangentWS.xyz;
    #endif
                    }
                    float3 flatLocalPos = CalcSubdividedPos(srcLocalPositions, baryCentricCoords);
                    output.uv = CalcSubdividedPos(srcUVs, baryCentricCoords).xy;

                    // �@���ɂ��Ă�PN-Triangles�Ōv�Z����ƂЂїp�̒��_�ړ����ɋT�􂪔������₷���Ȃ�̂ŁA�t���b�g�ȃ|���S���̖@�����̗p
                    float3 localNormal = CalcSubdividedPos(srcLocalNormals, baryCentricCoords);
                    output.initNormalOS = localNormal;
                    output.initNormalWS = TransformObjectToWorldNormal(localNormal);
    #ifdef _NORMALMAP
                    output.initTangentWS = half4(CalcSubdividedPos(srcWorldTangents, baryCentricCoords), inputs[0].tangentWS.w);
    #endif

    #ifdef _PN_TRIANGLES_ON
                    // PN-Triangles��K�p����ƋT�􂪔�������ꍇ�̓|���S���̕ӏ�̒��_�͕ψʂ����Ȃ�
                    // �i�d�S���W�n�ł͒��_����������̕ӂɌ������č��W��1��0�ƕω����邱�Ƃ𗘗p�j
                    bool isOnSides = min(min(baryCentricCoords.x, baryCentricCoords.y), baryCentricCoords.z) == 0;
                    if (_PnTriFactor == 0.0 || (!_AdaptsPolygonEdgeToPnTri && isOnSides)) {
                        output.initPositionOS = flatLocalPos;
                    }
     else {
                        // PN-Triangles��p���ăJ�[�u��ɂȂ�悤�ɍ��W�ψ�
                        float3 pnTriLocalPos = CalcPnTriPosition(param.positionsOS, param.b111, baryCentricCoords);

                        output.initPositionOS = lerp(flatLocalPos, pnTriLocalPos, _PnTriFactor);
                    }
    #else
                    output.initPositionOS = flatLocalPos;
    #endif

                    // ���_���Ђі͗l�ɏd�Ȃ�ꍇ�͉��܂���
                    output.positionOS = CalcCrackedPos(output.initPositionOS, output.initNormalOS, output.initNormalWS, output.crackLevel);
                    output.positionCS = TransformObjectToHClip(output.positionOS);

                    return output;
                }


                /**
                 * �t���O�����g�V�F�[�_�[
                 */
                half4 Frag(d2f input) : SV_Target {
                    float crackLevel = input.crackLevel;
                    float3 positionOS = _DrawsCrackWithPixelUnit ? CalcCrackedPos(input.positionOS, input.initNormalOS, input.initNormalWS, crackLevel) : input.positionOS;
                    positionOS -= input.initNormalOS * _AdditionalCrackDepthForLighting * crackLevel;

                    float3 positionWS = TransformObjectToWorld(positionOS);

                    // �אڂ̃s�N�Z���Ƃ̃��[���h���W�̍������擾��ɊO�ς����߂Ė@���Z�o
                    float3 normalWS = crackLevel > 0.0 ? normalize(cross(ddy(positionWS), ddx(positionWS))) : input.initNormalWS;

                    Varyings varyings = (Varyings)0;
                    varyings.positionCS = input.positionCS;
                    varyings.uv = input.uv;
                    varyings.positionWS = positionWS;
                    varyings.normalWS = normalWS;
    #ifdef _NORMALMAP
                    varyings.tangentWS = input.initTangentWS;
    #endif

                    SurfaceData surfaceData;
                    InitializeStandardLitSurfaceData(input.uv, surfaceData);

                    OUTPUT_SH(normalWS, varyings.vertexSH);

                    InputData inputData;
                    InitializeInputData(varyings, surfaceData.normalTS, inputData);
                    inputData.normalWS = crackLevel > 0.0 ? normalWS : inputData.normalWS;
                    inputData.vertexLighting = VertexLighting(positionWS, inputData.normalWS);


                    /* �Ђі͗l */
                    // �ЂёΏۂ̏ꍇ�̓N���b�N�J���[��ǉ�
                    surfaceData.albedo = lerp(surfaceData.albedo, _CrackColor.rgb, crackLevel);

                    // �Ђѕ�����AO�ݒ�
                    surfaceData.occlusion = min(surfaceData.occlusion, CalcOcclusion(crackLevel));

                    half4 color = UniversalFragmentPBR(inputData, surfaceData);

                    clip(color.a <= 0 ? -1 : 1);

                    return color;
                }
                ENDHLSL
            }
            }

                FallBack "Universal Render Pipeline/Lit"
}