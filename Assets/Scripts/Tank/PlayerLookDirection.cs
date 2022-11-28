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

    Transform m_cursorPosition = null;

    Camera m_camera = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();

        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        m_camera = Camera.main;

            m_cursorPosition = GameObject.Find("1PCursor").GetComponent<Transform>();
    }

    void Update()
    {
        //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
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
       if (Gamepad.current is not null)
       {
           return m_cursorPosition.position;
       }
       else
       {
           return Mouse.current.position.ReadValue();
       }
    }
}
}