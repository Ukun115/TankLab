using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

/// <summary>
/// �}�E�X�J�[�\����Ray���΂���Ray�Ƀq�b�g�����I�u�W�F�N�g�𔻕ʂ��鏈��
/// </summary>
namespace nsTankLab
{
    public class RayCast : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�J�[�\���摜�̈ʒu")] Transform m_cursorImagePosition = null;
        [SerializeField, TooltipAttribute("�v���C���[�ԍ�"), Range(1,4)]int m_playerNum = 1;
        [SerializeField, TooltipAttribute("�J�[�\���摜�I�u�W�F�N�g")] GameObject m_cursorObject = null;

        //Ray���q�b�g�����I�u�W�F�N�g
        GameObject m_rayHitObject = null;

        ButtonMaterialChange m_buttonMaterialChange = null;

        //�V�[���X�C�b�`���[�X�N���v�g
        SceneSwitcher m_sceneSwitcher = null;

        Camera m_camera = null;

        Vector3 m_rayPoint = Vector3.zero;

        SaveData m_saveData = null;
        SoundManager m_soundManager = null;
        ControllerData m_controllerData = null;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();

            m_camera = Camera.main;

            // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h����
            if (m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                m_rayPoint = m_cursorImagePosition.position;

                //�J�[�\���摜��\��
                m_cursorObject.SetActive(true);
            }
            //�Q�[���p�b�h���ڑ�����Ă��Ȃ�������}�E�X����
            else
            {
                m_rayPoint = Mouse.current.position.ReadValue();

                //�J�[�\���摜���\��
                m_cursorObject.SetActive(false);
            }
        }

        void Update()
        {
            // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h����
            if(m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                m_rayPoint = m_cursorImagePosition.position;

                //�J�[�\���摜��\��
                m_cursorObject.SetActive(true);
            }
            //�Q�[���p�b�h���ڑ�����Ă��Ȃ�������}�E�X����
            else
            {
                m_rayPoint = Mouse.current.position.ReadValue();

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


                switch(SceneManager.GetActiveScene().name)
                {
                    //�^���N�I���V�[���̏ꍇ
                    case SceneName.SelectTankScene:
                        //Rey���q�b�g���Ă���I�u�W�F�N�g���^���N�ƃX�L���̃{�^���̏ꍇ�̂݃e�L�X�g�X�V
                        if (hit.collider.name.Contains("TANK") || hit.collider.name.Contains("SKILL"))
                        {
                            //���������X�V
                            GameObject.Find("SceneManager").GetComponent<DecideTank>().UpdateTankSkillInfo(m_playerNum, hit.collider.name);
                        }
                        break;

                    //�X�e�[�W�I���V�[���̏ꍇ
                    case SceneName.SelectStageScene:
                        //�J�[�\���|�W�V�������ړ�������
                        GameObject.Find("SceneManager").GetComponent<DecideStage>().SetCursorPosition(hit.collider.name);
                        break;
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

                //�J�[�\���q�b�gSE�Đ�
                m_soundManager.PlaySE("CursorHitSE");
            }
        }

        //�{�^���������ꂽ�Ƃ��̏���
        void FireButton(RaycastHit hit)
        {
            //�Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h����
            if (m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                //A�{�^���������ꂽ�Ƃ��A
                if (m_controllerData.GetGamepad(m_playerNum).rightShoulder.wasPressedThisFrame)
                {
                    //������n��
                    PassCharacter(hit);
                }
            }
            //�Q�[���p�b�h���ڑ�����Ă��Ȃ���΃}�E�X�ł̑���
            else
            {
                //���N���b�N���ꂽ�Ƃ��A
                if (Mouse.current.leftButton.wasPressedThisFrame)
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
                    case SceneName.TitleScene:
                        //�����ꂽ�{�^���̕�����n��
                        GameObject.Find("SceneManager").GetComponent<DecideGameMode>().SetCharacter(hit.collider.name);
                        break;

                    //���O���߃V�[��
                    case SceneName.DecideNameScene:
                        //�����ꂽ�{�^���̕�����n��
                        GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter(hit.collider.name);
                        break;
                    //�p�X���[�h���͉��
                    case SceneName.DecidePasswordScene:
                        //�����ꂽ�{�^���̕�����n��
                        GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter(hit.collider.name);
                        break;
                        //�ݒ�V�[��
                        case SceneName.ConfigScene:
                        //�����ꂽ�{�^���̕�����n��
                        GameObject.Find("SceneManager").GetComponent<DecideVolume>().SetCharacter(hit.collider.name);
                            break;
                        //�^���N�I���V�[��
                        case SceneName.SelectTankScene:
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
                    case SceneName.SelectStageScene:
                        //�����ꂽ�{�^���̕�����n��
                        GameObject.Find("SceneManager").GetComponent<DecideStage>().SetStageNum(int.Parse(Regex.Replace(hit.collider.name, @"[^0-9]", "")));
                        break;

                        //���݂̃`�������W���J�E���g�V�[��
                        case SceneName.ChallengeNowNumCountScene:
                            //�`�������W�Q�[���V�[���ɑJ��
                            m_sceneSwitcher.StartTransition("ChallengeGameScene",true);
                            break;
                }
                switch (SceneManager.GetActiveScene().name)
                {
                    case SceneName.TitleScene:
                        if (GameObject.Find("SceneManager").GetComponent<DecideGameMode>().GetNoGood())
                        {
                            //�_��SE�Đ�
                            m_soundManager.PlaySE("NoGoodSE");
                        }
                        else
                        {
                            //�I��SE�Đ�
                            m_soundManager.PlaySE("SelectSE");
                        }
                        break;

                    case SceneName.DecideNameScene:
                        if (GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().GetNoGood())
                        {
                            //�_��SE�Đ�
                            m_soundManager.PlaySE("NoGoodSE");
                        }
                        else
                        {
                            //�I��SE�Đ�
                            m_soundManager.PlaySE("SelectSE");
                        }
                        break;

                        case SceneName.DecidePasswordScene:
                        if (GameObject.Find("SceneManager").GetComponent<DecidePassword>().GetNoGood())
                        {
                            //�_��SE�Đ�
                            m_soundManager.PlaySE("NoGoodSE");
                        }
                        else
                        {
                            //�I��SE�Đ�
                            m_soundManager.PlaySE("SelectSE");
                        }
                        break;

                    default:
                        //�I��SE�Đ�
                        m_soundManager.PlaySE("SelectSE");
                        break;
                }
            }

            //BackButton�^�O�������珈�����s��
            if (hit.collider.CompareTag("BackButton"))
            {
                //1�O�̃V�[���ɖ߂�
                m_sceneSwitcher.BackScene();

                //�I��SE�Đ�
                m_soundManager.PlaySE("SelectSE");
            }
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            m_buttonMaterialChange = GetComponent<ButtonMaterialChange>();
        }
    }
}