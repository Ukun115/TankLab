using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    /// <summary>
    /// �g���[�j���O�V�[���ł̃^���N�ƃX�L����I������E�B���h�E�\���N���X
    /// </summary>
    public class DisplayTankAndSkillWindow : MonoBehaviour
    {
        //�E�B���h�E2D
        [SerializeField] GameObject m_window2D = null;
        //�E�B���h�E�^�C�g���e�L�X�g
        [SerializeField] TextMeshProUGUI m_titleText = null;
        //�{�^���e�L�X�g
        [SerializeField] GameObject[] m_buttonText = { null };
        //�E�B���h�E3D
        [SerializeField] GameObject m_window3D = null;
        //�{�^���I�u�W�F�N�g
        [SerializeField] GameObject[] m_buttonObject = { null };
        //�^���N&�X�L���ڍ׃e�L�X�g
        [SerializeField] TextMeshProUGUI[] m_infoTankSkillText = null;
        [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("�ǂލ��ރX�L�������e�L�X�g���������܂�Ă���.txt�t�@�C��")] TextAsset[] m_skillTextAsset = { null };

        //�E�B���h�E��\�����Ă��邩�ǂ���
        bool m_isDisplay = false;

        //�O��I�����ꂽ�E�B���h�E
        string m_selectBefore = string.Empty;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        public void  DisplayWindow(string character)
        {
            //�Ō�ɉ����ꂽ�{�^�������񉟂��ꂽ�{�^���Ɠ����ꍇ
            if(m_selectBefore == character)
            {
                //���łɕ\������Ă���Ƃ��A
                if(m_isDisplay)
                {
                    //�E�B���h�E�����
                    WindowClose();

                    return;
                }
            }

            //�E�B���h�E���J��
            WindowOpen(character);
        }

        //�E�B���h�E���J������
        void WindowOpen(string character)
        {
            //�E�B���h�E���J��
            m_window2D.SetActive(true);
            m_window3D.SetActive(true);

            switch (character)
            {
                case "TankWindow":
                    //�{�^���e�L�X�g
                    m_buttonText[0].SetActive(true);
                    m_buttonText[1].SetActive(false);
                    //�{�^���I�u�W�F�N�g
                    m_buttonObject[0].SetActive(true);
                    m_buttonObject[1].SetActive(false);
                    //
                    m_infoTankSkillText[0].enabled = true;
                    m_infoTankSkillText[1].enabled = false;
                    //�^�C�g���e�L�X�g���X�V����
                    m_titleText.text = "TANK";
                    break;

                case "SkillWindow":
                    //�{�^���e�L�X�g
                    m_buttonText[0].SetActive(false);
                    m_buttonText[1].SetActive(true);
                    //�{�^���I�u�W�F�N�g
                    m_buttonObject[0].SetActive(false);
                    m_buttonObject[1].SetActive(true);
                    //
                    m_infoTankSkillText[0].enabled = false;
                    m_infoTankSkillText[1].enabled = true;
                    //�^�C�g���e�L�X�g���X�V����
                    m_titleText.text = "SKILL";
                    break;
            }

            //�v���C���[���������Ȃ��悤�ɂ���
            m_saveData.GetSetmActiveGameTime = false;
            //�\�����Ă����Ԃɂ���
            m_isDisplay = true;

            //�Ō�ɉ����ꂽ�{�^�������񉟂��ꂽ�{�^���ɍX�V
            m_selectBefore = character;
        }

        //�E�B���h�E����鏈��
        void WindowClose()
        {
            //�E�B���h�E�����
            m_window2D.SetActive(false);
            m_window3D.SetActive(false);

            //�v���C���[����������悤�ɂ���
            m_saveData.GetSetmActiveGameTime = true;
            //�\�����Ă��Ȃ���Ԃɂ���
            m_isDisplay = false;
        }

        public void UpdateTankSkillInfo(string character)
        {
            //�������X�V

            //�^���N
            if (character.Contains("TANK"))
            {
                //�I�����Ă���^���N�̃X�e�[�^�X
                TankStatus tankStatus = m_tankDataBase.GetTankLists()[int.Parse(Regex.Replace(character, @"[^0-9]", string.Empty)) - 1];
                m_infoTankSkillText[0].text = $"Tank Speed : {tankStatus.GetTankSpeed()}\nFire Speed : {tankStatus.GetBulletSpeed()}\nRapid Fire : {tankStatus.GetRapidFireNum()}\nSame Time Fire : {tankStatus.GetSameTimeBulletNum()}\nBullet Refrection:{tankStatus.GetBulletRefrectionNum()}";
            }
            //�X�L��
            else if (character.Contains("SKILL"))
            {
                m_infoTankSkillText[1].text = $"{ m_skillTextAsset[int.Parse(Regex.Replace(character, @"[^0-9]", string.Empty)) - 1].text}";
            }
        }
    }
}