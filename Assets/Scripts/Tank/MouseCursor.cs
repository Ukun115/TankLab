using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class MouseCursor : Photon.Pun.MonoBehaviourPun
{
    Plane plane = new Plane();
    float distance = 0;

    SaveData m_saveData = null;
    ControllerData m_controllerData = null;

    [SerializeField, TooltipAttribute("�J�[�\���摜�̈ʒu")] Transform m_cursorPosition = null;

    [SerializeField, TooltipAttribute("��C�̊�_�g�����X�t�H�[��")] Transform m_cannonPivotTransform = null;

    Vector3 rayTarget;

    void Start()
    {
        //plane.SetNormalAndPosition(Vector3.back, transform.localPosition);

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
    }

    void Update()
    {
        // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h�ł̑���
        if (m_controllerData.GetIsConnectedController(int.Parse(Regex.Replace(this.transform.root.name, @"[^0-9]", ""))))
        {
            rayTarget = m_cursorPosition.position;
        }
        else
        {
            rayTarget = Input.mousePosition;
        }

        //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            return;
        }

        var ray = Camera.main.ScreenPointToRay(rayTarget);
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (plane.Raycast(ray, out distance))
        {
            var lookPoint = ray.GetPoint(distance);
            m_cannonPivotTransform.LookAt(lookPoint);
        }
    }
}
