using UnityEngine;

/// <summary>
/// ボタンのマテリアルを変更する処理
/// </summary>
namespace nsTankLab
{
    public class ButtonMaterialChange : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("ブロックボタンのマテリアル")] Material[] m_blockButtonMaterial = null;

        //子オブジェクトマテリアル達を入れる配列
        MeshRenderer[] m_childMeshRenderer = { null };

        //カラーチェンジができるかどうか
        bool m_ableColorChange = false;

        //マテリアルを変える処理
        public void ChangeMaterial(RaycastHit hit)
        {
            // 子オブジェクトマテリアル達を入れる配列の初期化
            m_childMeshRenderer = new MeshRenderer[hit.collider.gameObject.transform.childCount];

            //子オブジェクトのマテリアルを取得
            for (int childrenNum = 0; childrenNum < hit.collider.gameObject.transform.childCount; childrenNum++)
            {
                m_childMeshRenderer[childrenNum] = hit.collider.gameObject.transform.GetChild(childrenNum).gameObject.GetComponent<MeshRenderer>();
            }

            //マテリアルのカラーチェンジ処理
            DecideMaterialColor("Gray_Light (Instance)", "Gray_Normal (Instance)", 1, true);
        }

        //マテリアルを戻す処理
        public void ReturnChangeMaterial()
        {
            if (m_childMeshRenderer is not null && m_ableColorChange)
            {
                //前回マウスカーソルが当たっていたブロックボタンオブジェクトをもとの色に戻す
                //マテリアルのカラーチェンジ処理
                DecideMaterialColor("Gray_Normal (Instance)", "Gray_Dark (Instance)", 0, false);
            }
        }

        //マテリアルのカラーチェンジ処理
        void DecideMaterialColor(string materialName1, string materialName2, int materialNum, bool ableColorChange)
        {
            //ブロックボタンの色(マテリアル)を変更する
            for (int childrenNum = 0; childrenNum < m_childMeshRenderer.Length; childrenNum++)
            {
                //割り当てられているマテリアルによって変更先のマテリアルを決定する
                if (m_childMeshRenderer[childrenNum].material.name == materialName1)
                {
                    m_childMeshRenderer[childrenNum].material = m_blockButtonMaterial[materialNum];
                }
                else if (m_childMeshRenderer[childrenNum].material.name == materialName2)
                {
                    m_childMeshRenderer[childrenNum].material = m_blockButtonMaterial[materialNum + 1];
                }
            }
            m_ableColorChange = ableColorChange;
        }
    }
}