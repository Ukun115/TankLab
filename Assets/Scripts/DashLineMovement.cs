using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

/// <summary>
/// �_�b�V�����C���ړ�����
/// </summary>
namespace nsTankLab
{
    public class DashLineMovement : MonoBehaviour
    {
        LineRenderer line = null;
        Camera m_mainCamera = null;
        Plane plane = new Plane();
        float distance = 0.0f;
        Vector3 rayTarget = Vector3.zero;
        ControllerData m_controllerData = null;

        Transform m_cursorPosition = null;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();

            line.positionCount = 2;
            m_mainCamera = Camera.main;
            plane.SetNormalAndPosition(Vector3.up, new Vector3(transform.position.x, 4.0f, transform.position.z - 3));

        }

        void Update()
        {
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
            {
                // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h�ł̑���
                if (m_controllerData.GetGamepad(1) is not null)
                {
                    if (m_cursorPosition is null)
                    {
                        m_cursorPosition = GameObject.Find("Cursor").GetComponent<Transform>();
                    }
                    rayTarget = m_cursorPosition.position;
                }
                else
                {
                    rayTarget = Mouse.current.position.ReadValue();
                }
            }
            else
            {
                // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h�ł̑���
                if (m_controllerData.GetGamepad(int.Parse(Regex.Replace(gameObject.transform.root.name, @"[^1-4]", string.Empty))) is not null)
                {
                    if (m_cursorPosition is null)
                    {
                        m_cursorPosition = GameObject.Find(transform.root.name + "Cursor").GetComponent<Transform>();
                    }
                    rayTarget = m_cursorPosition.position;
                }
                else
                {
                    rayTarget = Mouse.current.position.ReadValue();
                }
            }

            var ray = m_mainCamera.ScreenPointToRay(rayTarget);
            if (plane.Raycast(ray, out distance))
            {
                var lookPoint = ray.GetPoint(distance);
                line.SetPosition(0, lookPoint);
                line.SetPosition(1, new Vector3(transform.position.x, 4.0f, transform.position.z - 4.0f));
            }
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            line = GetComponent<LineRenderer>();
        }
    }
}