using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

/// <summary>
/// �v���C���[�̌����Ă�����������߂鏈��
/// </summary>
namespace nsTankLab
{
    public class PlayerLookDirection : Photon.Pun.MonoBehaviourPun
    {
        Plane plane = new Plane();
        float distance = 0.0f;
        float m_mouseDistance = 0.0f;

        SaveData m_saveData = null;
        ControllerData m_controllerData = null;

        RectTransform m_cursorPosition = null;

        Camera m_camera = null;

        int m_playerNum = 0;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();

            plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
            m_camera = Camera.main;


            m_playerNum = int.Parse(Regex.Replace(transform.root.name, @"[^1-4]", string.Empty));
        }

        void Update()
        {
            //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
            if (m_saveData.GetSetIsOnline && SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            //�Q�[���i�s���~�܂��Ă���Ƃ��͎��s���Ȃ�
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            var ray = m_camera.ScreenPointToRay(DecideRayTarget());
            plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
            if (plane.Raycast(ray, out distance))
            {
                var lookPoint = ray.GetPoint(distance);
                m_mouseDistance = Vector3.Distance(lookPoint, transform.position);
                if(m_mouseDistance > 1.2f)
                {
                    transform.LookAt(lookPoint);
                }
            }
            //�ˏo�����̃f�o�b�N�\��
            Debug.DrawRay(transform.position, transform.forward * 5.0f, Color.red);
        }

        Vector3 DecideRayTarget()
        {
           // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h�ł̑���
           if (m_controllerData.GetGamepad(m_playerNum) is not null)
           {
                if(m_cursorPosition is null)
                {
                    m_cursorPosition = GameObject.Find($"{m_playerNum}PCursor").GetComponent<RectTransform>();
                }

               return m_cursorPosition.position;
           }
           else
           {
               return Mouse.current.position.ReadValue();
           }
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        }
    }
}