using UnityEngine;

/// <summary>
/// コントローラーが切断されたときに呼ばれる処理
/// </summary>
namespace nsTankLab
{
    public class ControllerDisconnect : MonoBehaviour
    {
        ControllerData m_controllerData = null;
        SaveData m_saveData = null;

        [SerializeField]GameObject m_countDownObject = null;

        void Start()
        {
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Update()
        {
            //コントローラーが4つつながっていない状態になったら、
            if(m_controllerData.GetConnectGamepad() < 4)
            {
                //コントローラーが切断されている処理

                //ゲーム進行を止める
                m_saveData.GetSetmActiveGameTime = false;

                Debug.Log("コントローラーが切断されました。\n再接続してください。");
            }
            else if(m_countDownObject is null)
            {
                //ゲーム進行を進める
                m_saveData.GetSetmActiveGameTime = true;
            }
        }
    }
}