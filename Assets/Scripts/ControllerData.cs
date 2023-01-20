using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine;

/// <summary>
/// �ڑ�����Ă���R���g���[���[�̃f�[�^��ۑ����Ă���ꏊ
/// </summary>
namespace nsTankLab
{
    public class ControllerData : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�}�E�X�J�[�\���摜�I�u�W�F�N�g")] Texture2D[] m_mouseCursorTexture = null;

        int m_controllerNum = 0;

        //1=1P,2=2P
        int m_cursorColorNum = 1;

        void Start()
        {
            SearchConnectedController();

            Debug.Log($"<color=yellow>�ڑ�����Ă���R���g���[���[��:{m_controllerNum}</color>");
        }

        void Update()
        {
            SearchConnectedController();

            if(Gamepad.current is not null)
            {
                //�}�E�X�J�[�\���̉摜�𖳂����B
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                //�}�E�X�J�[�\���̉摜�����e�B�N���ɂ���
                Cursor.SetCursor(m_mouseCursorTexture[m_cursorColorNum-1], new Vector2(m_mouseCursorTexture[m_cursorColorNum - 1].width/2, m_mouseCursorTexture[m_cursorColorNum - 1].height/2), CursorMode.Auto);
            }
        }

        void SearchConnectedController()
        {
            if(m_controllerNum != Gamepad.all.Count)
            {
                Debug.Log($"�ڑ�����Ă���R���g���[���[�� : {m_controllerNum}��{Gamepad.all.Count}");
            }

                // �ڑ�����Ă���R���g���[���[���𒲂ׂ�
                m_controllerNum = Gamepad.all.Count;
        }

        public void SetCursorColor(int playerNum)
        {
            m_cursorColorNum = playerNum;
        }

        public Gamepad GetGamepad(int playerNum)
        {
            if(m_controllerNum < playerNum)
            {
                return null;
            }

            return Gamepad.all[playerNum-1];
        }

        public int GetConnectGamepad()
        {
            return m_controllerNum;
        }
    }
}