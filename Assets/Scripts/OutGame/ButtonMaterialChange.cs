using UnityEngine;

/// <summary>
/// ボタンのマテリアルを変更する処理
/// </summary>
public class ButtonMaterialChange : MonoBehaviour
{
    //子オブジェクト達を入れる配列
    Transform[] m_childrenObject;

    [SerializeField, TooltipAttribute("ブロックボタンのマテリアル")] Material[] m_blockButtonMaterial = null;

    //カラーチェンジができるかどうか
    bool m_ableColorChange = false;

    //マテリアルを戻す処理
    public void ReturnChangeMaterial()
    {
        if (m_childrenObject is not null && m_ableColorChange)
        {
            //前回マウスカーソルが当たっていたブロックボタンオブジェクトをもとの色に戻す
            //マテリアルのカラーチェンジ処理
            DecideMaterialColor("Gray_Normal (Instance)", "Gray_Dark (Instance)", 0, false);
        }
    }
    //マテリアルを変える処理
    public void ChangeMaterial(RaycastHit hit)
    {
        // 子オブジェクト達を入れる配列の初期化
        m_childrenObject = new Transform[hit.collider.gameObject.transform.childCount];

        //子オブジェクトを取得
        for (int childrenNum = 0; childrenNum < hit.collider.gameObject.transform.childCount; childrenNum++)
        {
            m_childrenObject[childrenNum] = hit.collider.gameObject.transform.GetChild(childrenNum); // GetChild()で子オブジェクトを取得
        }

        //マテリアルのカラーチェンジ処理
        DecideMaterialColor("Gray_Light (Instance)", "Gray_Normal (Instance)", 1, true);
    }

    //マテリアルのカラーチェンジ処理
    void DecideMaterialColor(string materialName1, string materialName2, int materialNum, bool ableColorChange)
    {
        //ブロックボタンの色(マテリアル)を変更する
        for (int childrenNum = 0; childrenNum < m_childrenObject.Length; childrenNum++)
        {
            //割り当てられているマテリアルによって変更先のマテリアルを決定する
            if (m_childrenObject[childrenNum].gameObject.GetComponent<Renderer>().material.name == materialName1)
            {
                m_childrenObject[childrenNum].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[materialNum];
            }
            else if (m_childrenObject[childrenNum].gameObject.GetComponent<Renderer>().material.name == materialName2)
            {
                m_childrenObject[childrenNum].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[materialNum + 1];
            }
        }
        m_ableColorChange = ableColorChange;
    }
}
