using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    public class DecideVolume : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI[] m_soundVolumeText = null;

        [SerializeField] Transform[] m_soundVolumeCursorTransform = null;
        float m_newCursorPositionX = 0.0f;

        SoundManager m_soundManager = null;

        enum EnVolumeType
        {
            enMasterVolume,     //�}�X�^�[�{�����[��
            enBGMVolume,        //BGM�{�����[��
            enSEVolume,         //SE�{�����[��
            enTotalVolumeType   //�{�����[���^�C�v�̐�
        }


        void Start()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            for (int volumeType = (int)EnVolumeType.enMasterVolume; volumeType < (int)EnVolumeType.enTotalVolumeType; volumeType++)
            {
                //�e�L�X�g�̏�����
                m_soundVolumeText[volumeType].text = $"{NowVolumeValue(volumeType) * 10}";
                //�J�[�\���̏�����
                ChangeCursorPosition(volumeType, NowVolumeValue(volumeType));
            }

            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                m_soundManager.GetSetMasterVolume = PlayerPrefs.GetFloat("MasterVolume");
            }
            else
            {
                PlayerPrefs.SetFloat("MasterVolume", 0.5f);
            }
            if (PlayerPrefs.HasKey("BGMMasterVolume"))
            {
                m_soundManager.GetSetBGMMasterVolume = PlayerPrefs.GetFloat("BGMMasterVolume");
            }
            else
            {
                PlayerPrefs.SetFloat("BGMMasterVolume", 0.5f);
            }
            if (PlayerPrefs.HasKey("SEMasterVolume"))
            {
                m_soundManager.GetSetSEMasterVolume = PlayerPrefs.GetFloat("SEMasterVolume");
            }
            else
            {
                PlayerPrefs.SetFloat("SEMasterVolume", 0.5f);
            }
        }

        //�����ꂽ�{�^���̎�ނɂ���ď����𕪊�
        public void SetCharacter(string character)
        {
            float volume = float.Parse(Regex.Replace(character, @"[^0-9.]", string.Empty));

            //�}�X�^�[�{�����[���{�^��
            if (character.Contains("MasterVolume_"))
            {
                m_soundManager.GetSetMasterVolume = volume;

                m_soundVolumeText[(int)EnVolumeType.enMasterVolume].text = $"{volume*10}";

                ChangeCursorPosition((int)EnVolumeType.enMasterVolume, m_soundManager.GetSetMasterVolume);

                Debug.Log($"�}�X�^�[�{�����[����{volume*10}�ɕύX");
            }
            //BGM�{�����[���{�^��
            if (character.Contains("BGMVolume_"))
            {
                m_soundManager.GetSetBGMMasterVolume = volume;

                m_soundVolumeText[(int)EnVolumeType.enBGMVolume].text = $"{volume*10}";

                ChangeCursorPosition((int)EnVolumeType.enBGMVolume, m_soundManager.GetSetBGMMasterVolume);

                Debug.Log($"BGM�{�����[����{volume*10}�ɕύX");
            }
            //SE�{�����[���{�^��
            if (character.Contains("SEVolume_"))
            {
                m_soundManager.GetSetSEMasterVolume = volume;

                m_soundVolumeText[(int)EnVolumeType.enSEVolume].text = $"{volume*10}";

                ChangeCursorPosition((int)EnVolumeType.enSEVolume, m_soundManager.GetSetSEMasterVolume);

                Debug.Log($"SE�{�����[����{volume*10}�ɕύX");
            }
        }

        void ChangeCursorPosition(int soundNum,float volume)
        {
            m_newCursorPositionX = -0.689f + (1.25f * (volume * 10));
            m_soundVolumeCursorTransform[soundNum].position = new Vector3(
                m_newCursorPositionX,
                m_soundVolumeCursorTransform[soundNum].position.y,
                m_soundVolumeCursorTransform[soundNum].position.z
                );
        }

        float NowVolumeValue(int num)
        {
            switch((EnVolumeType)num)
            {
                case EnVolumeType.enMasterVolume:
                return m_soundManager.GetSetMasterVolume;
                case EnVolumeType.enBGMVolume:
                return m_soundManager.GetSetBGMMasterVolume;
                case EnVolumeType.enSEVolume:
                return m_soundManager.GetSetSEMasterVolume;
                default:
                    return 0.0f;
            }
        }
    }
}