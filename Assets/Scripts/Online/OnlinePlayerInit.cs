using UnityEngine;
using Photon.Pun;

/// <summary>
/// オンラインの際のプレイヤーの初期化処理
/// </summary>
namespace nsTankLab
{
public class OnlinePlayerInit : MonoBehaviourPun
{
    [SerializeField, TooltipAttribute("各プレイヤーのタンクのマテリアル")] Material[] m_tankColor = new Material[2];

    SaveData m_saveData = null;

    void Start()
    {
        //ビット演算(排他的論理和)を利用して、
        //int型のプレイヤー番号0or1を0なら1に、
        //1なら0にするようにしている。
        //排他的論理和・・・２つのオペランドのビットを比較して、同じだったら 0 に、異なっていたら 1 になるビット演算
        //
        //絶対値を利用して、
        //プレヤー番号が0なら0-2=-2で2に、
        //プレイヤー番号が1なら1-2=-1で1になるようにしている。
        //Mathf.Abs・・・絶対値を算出する。

        //&・・・文字列補間

        SettingNameAndColor($"{Mathf.Abs(m_saveData.GetSetPlayerNum -2)}P", m_saveData.GetSetPlayerNum^=1);
    }

    //タンクの名前とカラーを設定
    void SettingNameAndColor(string name,int materialNum)
    {
        gameObject.name = name;
        gameObject.GetComponent<MeshRenderer>().material = m_tankColor[materialNum];
    }
}
}