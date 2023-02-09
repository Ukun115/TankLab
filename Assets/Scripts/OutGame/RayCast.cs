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
        [SerializeField, TooltipAttribute("�V�[���}�l�[�W���[")] GameObject m_sceneManager = null;

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

            //����ؑ�
            SwitchingOperation();
        }

        void Update()
        {
            //����ؑ�
            SwitchingOperation();

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

        //����ؑ�(�Q�[���p�b�hor�}�E�X�J�[�\��)
        void SwitchingOperation()
        {
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
                            m_sceneManager.GetComponent<DecideTank>().UpdateTankSkillInfo(m_playerNum, hit.collider.name);
                        }
                        break;

                    //�X�e�[�W�I���V�[���̏ꍇ
                    case SceneName.SelectStageScene:
                        //�J�[�\���|�W�V�������ړ�������
                        m_sceneManager.GetComponent<DecideStage>().SetCursorPosition(hit.collider.name);
                        break;

                    //�g���[�j���O�V�[���̏ꍇ�A
                    case SceneName.TrainingScene:
                        m_sceneManager.GetComponent<DisplayTankAndSkillWindow>().UpdateTankSkillInfo(hit.collider.name);
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
            if (hit.collider.CompareTag(TagName.BlockButton) || hit.collider.CompareTag(TagName.BackButton))
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
            if (hit.collider.CompareTag(TagName.BlockButton))
            {
                //���݂̃V�[���ɂ���ď�����ύX
                switch (SceneManager.GetActiveScene().name)
                {
                    //�^�C�g���V�[��
                    case SceneName.TitleScene:
                        //�����ꂽ�{�^���̕�����n��
                        m_sceneManager.GetComponent<DecideGameMode>().SetCharacter(hit.collider.name);
                        break;

                    //���O���߃V�[��
                    case SceneName.DecideNameScene:
                        //�����ꂽ�{�^���̕�����n��
                        m_sceneManager.GetComponent<DecidePlayerName>().SetCharacter(hit.collider.name);
                        break;
                    //�p�X���[�h���͉��
                    case SceneName.DecidePasswordScene:
                        //�����ꂽ�{�^���̕�����n��
                        m_sceneManager.GetComponent<DecidePassword>().SetCharacter(hit.collider.name);
                        break;
                        //�ݒ�V�[��
                        case SceneName.ConfigScene:
                        //�����ꂽ�{�^���̕�����n��
                        m_sceneManager.GetComponent<DecideVolume>().SetCharacter(hit.collider.name);
                            break;
                        //�^���N�I���V�[��
                        case SceneName.SelectTankScene:
                        if (m_saveData.GetSetSelectGameMode == "LOCALMATCH")
                        {
                            //�����ꂽ�{�^���̕�����n��
                            m_sceneManager.GetComponent<DecideTank>().SetCharacter(m_playerNum, hit.collider.name);
                        }
                        else
                        {
                            //�����ꂽ�{�^���̕�����n��
                            m_sceneManager.GetComponent<DecideTank>().SetCharacter(1, hit.collider.name);
                        }
                        break;

                    //�X�e�[�W�I���V�[��
                    case SceneName.SelectStageScene:
                        //�����ꂽ�{�^���̕�����n��
                        m_sceneManager.GetComponent<DecideStage>().SetStageNum(int.Parse(Regex.Replace(hit.collider.name, @"[^0-9]", string.Empty)));
                        break;

                    //���݂̃`�������W���J�E���g�V�[��
                    case SceneName.ChallengeNowNumCountScene:
                        //�`�������W�Q�[���V�[���ɑJ��
                        m_sceneSwitcher.StartTransition(SceneName.ChallengeGameScene);
                        break;


                    //�g���[�j���O�V�[��
                    case SceneName.TrainingScene:
                        //�^���N�{�^���������ꂽ�Ƃ��A
                        if (hit.collider.name.Contains("TANK"))
                        {
                            //�I�����Đ�
                            m_soundManager.PlaySE("SelectSE");

                            //�I������Ă���^���N��ύX
                            m_saveData.SetSelectTankName(1,hit.collider.name);

                            //�g���[�j���O�V�[���������[�h����
                            m_sceneSwitcher.StartTransition(SceneName.TrainingScene, false);

                            Debug.Log($"{hit.collider.name}�ɕύX");
                        }
                        //�X�L���{�^���������ꂽ�Ƃ��A
                        else if (hit.collider.name.Contains("SKILL"))
                        {
                            //�I�����Đ�
                            m_soundManager.PlaySE("SelectSE");

                            //�I������Ă���X�L����ύX
                            m_saveData.SetSelectSkillName(1, hit.collider.name);

                            //�g���[�j���O�V�[���������[�h����
                            m_sceneSwitcher.StartTransition(SceneName.TrainingScene, false);

                            Debug.Log($"{hit.collider.name}�ɕύX");
                        }
                        else
                        {
                            //�����ꂽ�{�^���̕�����n��
                            m_sceneManager.GetComponent<DisplayTankAndSkillWindow>().DisplayWindow(hit.collider.name);
                        }
                        break;
                }
                switch (SceneManager.GetActiveScene().name)
                {
                    case SceneName.TitleScene:
                        if (m_sceneManager.GetComponent<DecideGameMode>().GetNoGood())
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
                        if (m_sceneManager.GetComponent<DecidePlayerName>().GetNoGood())
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
                        if (m_sceneManager.GetComponent<DecidePassword>().GetNoGood())
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
            if (hit.collider.CompareTag(TagName.BackButton))
            {
                //1�O�̃V�[���ɖ߂�
                m_sceneSwitcher.BackScene();

                //�I��SE�Đ�
                m_soundManager.PlaySE("SelectSE");

                //�}�b�`���O��ʂ̂Ƃ��A
                if(SceneManager.GetActiveScene().name == SceneName.MatchingScene)
                {
                    //�I�����C���}�b�`���O���[�J�[�𓮓I�ɍ폜
                    GameObject.Find("PhotonController").GetComponent<OnlineMatchingMaker>().DestroyGameObject();
                }
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