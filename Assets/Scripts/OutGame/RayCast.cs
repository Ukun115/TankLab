using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �}�E�X�J�[�\����Ray���΂���Ray�Ƀq�b�g�����I�u�W�F�N�g�𔻕ʂ��鏈��
/// </summary>
public class RayCast : MonoBehaviour
{
    //Ray���q�b�g�����I�u�W�F�N�g
    GameObject m_rayHitObject = null;

    ButtonMaterialChange m_buttonMaterialChange = null;

    //�V�[���X�C�b�`���[�X�N���v�g
    SceneSwitcher m_sceneSwitcher = null;

    Camera m_camera = null;

    Vector3 m_rayPoint = Vector3.zero;

    [SerializeField, TooltipAttribute("�J�[�\���摜�̈ʒu")] Transform m_cursorImagePosition = null;
    [SerializeField, TooltipAttribute("�v���C���[�ԍ�"), Range(1,4)]int m_playerNum = 1;

    SaveData m_saveData = null;
    ControllerData m_controllerData = null;

    [SerializeField, TooltipAttribute("�J�[�\���摜�I�u�W�F�N�g")] GameObject m_cursorObject = null;

    void Start()
    {
        m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();

        m_camera = Camera.main;

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();

        m_buttonMaterialChange = GameObject.Find(this.name).GetComponent<ButtonMaterialChange>();
    }

    void Update()
    {
        // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h����
        if (m_controllerData.GetIsConnectedController(m_playerNum))
        {
            m_rayPoint = m_cursorImagePosition.position;

            //�J�[�\���摜��\��
            m_cursorObject.SetActive(true);
        }
        //�Q�[���p�b�h���ڑ�����Ă��Ȃ�������}�E�X����
        else
        {
            m_rayPoint = Input.mousePosition;

            //�J�[�\���摜���\��
            m_cursorObject.SetActive(false);
        }

        //Ray�𐶐�
        Ray ray = m_camera.ScreenPointToRay(m_rayPoint);
        RaycastHit hit;

        //Ray�𓊎�
        if (Physics.Raycast(ray, out hit))
        {
            //Ray���R���C�_�[�ɂ��������Ƃ��̏���
            RayHit(hit);
        }
    }

    //Ray���R���C�_�[�ɂ��������Ƃ��̏���
    void RayHit(RaycastHit hit)
    {
        if (hit.collider.gameObject != m_rayHitObject)
        {
            //�{�^���̃}�e���A����ύX���鏈��
            ChangeMaterial(hit);

            //�ڐG���Ă���I�u�W�F�N�g��ۑ�
            m_rayHitObject = hit.collider.gameObject;

            //�X�e�[�W�I���V�[���̎��A
            if (SceneManager.GetActiveScene().name == "SelectStageScene")
            {
                //�J�[�\���|�W�V�������ړ�������
                GameObject.Find("SceneManager").GetComponent<DecideStage>().SetCursorPosition(hit.collider.name);
            }
        }

        //�{�^���������ꂽ�Ƃ��A
        FireButton(hit);
    }

    //�{�^���̃}�e���A����ύX���鏈��
    void ChangeMaterial(RaycastHit hit)
    {
        if (m_rayHitObject != null)
        {
            m_buttonMaterialChange.ReturnChangeMaterial();
        }
        //�}�E�X�J�[�\���������Ă���I�u�W�F�N�g���u���b�N�{�^���̂Ƃ��A
        if (hit.collider.CompareTag("BlockButton") || hit.collider.CompareTag("BackButton"))
        {
            m_buttonMaterialChange.ChangeMaterial(hit);
        }
    }

    //�{�^���������ꂽ�Ƃ��̏���
    void FireButton(RaycastHit hit)
    {
        //�Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h����
        if (m_controllerData.GetIsConnectedController(m_playerNum))
        {
            //RB�{�^���������ꂽ�Ƃ��A
            if (Input.GetButtonDown($"{m_playerNum}PJoystickRB"))
            {
                //������n��
                PassCharacter(hit);
            }
        }
        //�Q�[���p�b�h���ڑ�����Ă��Ȃ���΃}�E�X�ł̑���
        else
        {
            //���N���b�N���ꂽ�Ƃ��A
            if (Input.GetMouseButtonDown(0))
            {
                //������n��
                PassCharacter(hit);
            }
        }
    }

    //������n������
    void PassCharacter(RaycastHit hit)
    {
        //BlockButton�^�O�������珈�����s��
        if (hit.collider.CompareTag("BlockButton"))
        {
            //���݂̃V�[���ɂ���ď�����ύX
            switch (SceneManager.GetActiveScene().name)
            {
                //�^�C�g���V�[��
                case "TitleScene":
                    //�����ꂽ�{�^���̕�����n��
                    GameObject.Find("SceneManager").GetComponent<DecideGameMode>().SetCharacter(hit.collider.name);
                    break;

                //���O���߃V�[��
                case "DecideNameScene":
                    //�����ꂽ�{�^���̕�����n��
                    GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter(hit.collider.name);
                    break;
                //�p�X���[�h���͉��
                case "InputPasswordScene":
                    //�����ꂽ�{�^���̕�����n��
                    GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter(hit.collider.name);
                    break;
                //�^���N�I���V�[��
                case "SelectTankScene":
                    if (m_saveData.GetSetSelectGameMode == "LOCALMATCH")
                    {
                        //�����ꂽ�{�^���̕�����n��
                        GameObject.Find("SceneManager").GetComponent<DecideTank>().SetCharacter(m_playerNum, hit.collider.name);
                    }
                    else
                    {
                        //�����ꂽ�{�^���̕�����n��
                        GameObject.Find("SceneManager").GetComponent<DecideTank>().SetCharacter(1, hit.collider.name);
                    }
                    break;

                //�X�e�[�W�I���V�[��
                case "SelectStageScene":
                    //�����ꂽ�{�^���̕�����n��
                    GameObject.Find("SceneManager").GetComponent<DecideStage>().SetCharacter(hit.collider.name);
                    break;
            }
        }

        //BackButton�^�O�������珈�����s��
        if (hit.collider.CompareTag("BackButton"))
        {
            //1�O�̃V�[���ɖ߂�
            m_sceneSwitcher.BackScene();
        }
    }
}