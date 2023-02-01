using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �T�E���h�Ǘ�
/// </summary>
namespace nsTankLab
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] AudioSource m_bgmAudioSource = null;
        [SerializeField] AudioSource m_seAudioSource = null;
        [SerializeField] List<BGMSoundData> m_bgmSoundDatas = null;
        [SerializeField] List<SESoundData> m_seSoundDatas = null;

        float m_masterVolume = 0.5f;
        float m_bgmMasterVolume = 0.5f;
        float m_seMasterVolume = 0.5f;
        BGMSoundData m_bgmSoundData = null;
        SESoundData m_seSoundData = null;

        void Start()
        {
            //�Q�[���N����BGM���Đ�����
            PlayBGM("OutGameSceneBGM");

            //�}�X�^�[�{�����[��
            if(PlayerPrefs.HasKey("MasterVolume"))
            {
                m_masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                GetSetMasterVolume = m_masterVolume;
            }
            else
            {
                PlayerPrefs.SetFloat("MasterVolume", m_masterVolume);
            }
            //BGM�{�����[��
            if (PlayerPrefs.HasKey("BGMMasterVolume"))
            {
                m_bgmMasterVolume = PlayerPrefs.GetFloat("BGMMasterVolume");
                GetSetBGMMasterVolume = m_bgmMasterVolume;
            }
            else
            {
                PlayerPrefs.SetFloat("BGMMasterVolume", m_bgmMasterVolume);
            }
            //SE�{�����[��
            if (PlayerPrefs.HasKey("SEMasterVolume"))
            {
                m_seMasterVolume = PlayerPrefs.GetFloat("SEMasterVolume");
                GetSetSEMasterVolume = m_seMasterVolume;
            }
            else
            {
                PlayerPrefs.SetFloat("SEMasterVolume", m_seMasterVolume);
            }
        }

        //BGM�Đ�����
        public void PlayBGM(string bgmName)
        {
            m_bgmSoundData = m_bgmSoundDatas.Find(m_bgmSoundData => m_bgmSoundData.GetBGMName() == bgmName);
            m_bgmAudioSource.clip = m_bgmSoundData.GetAudioClip();
            m_bgmAudioSource.volume = m_bgmSoundData.GetVolume() * m_bgmMasterVolume * m_masterVolume;
            m_bgmAudioSource.Play();
        }

        //���ʉ��Đ�����
        public void PlaySE(string seName)
        {
            m_seSoundData = m_seSoundDatas.Find(m_seSoundData => m_seSoundData.GetSEName() == seName);
            m_seAudioSource.volume = m_seSoundData.GetVolume() * m_seMasterVolume * m_masterVolume;
            m_seAudioSource.PlayOneShot(m_seSoundData.GetAudioClip());
        }

        //BGM�T�E���h��~
        public void StopBGM()
        {
            m_bgmAudioSource.Stop();
        }

        public float GetSetMasterVolume
        {
            get { return m_masterVolume; }
            set
            {
                m_masterVolume = value;
                m_bgmAudioSource.volume = m_bgmSoundData.GetVolume() * m_bgmMasterVolume * m_masterVolume;
                PlayerPrefs.SetFloat("MasterVolume", value);
            }
        }

        public float GetSetBGMMasterVolume
        {
            get { return m_bgmMasterVolume; }
            set
            {
                m_bgmMasterVolume = value;
                m_bgmAudioSource.volume = m_bgmSoundData.GetVolume() * m_bgmMasterVolume * m_masterVolume;
                PlayerPrefs.SetFloat("BGMMasterVolume", value);
            }
        }

        public float GetSetSEMasterVolume
        {
            get { return m_seMasterVolume; }
            set
            {
                m_seMasterVolume = value;
                PlayerPrefs.SetFloat("SEMasterVolume", value);
            }
        }
    }

    /// <summary>
    /// BGM�̃T�E���h�f�[�^
    /// </summary>
    [System.Serializable]
    public class BGMSoundData
    {
        [SerializeField] string m_bgmName = string.Empty;
        [SerializeField] AudioClip m_audioClip = null;
        [SerializeField, Range(0, 1)] float m_volume = 1.0f;

        public string GetBGMName()
        {
            return m_bgmName;
        }

        public AudioClip GetAudioClip()
        {
            return m_audioClip;
        }

        public float GetVolume()
        {
            return m_volume;
        }
    }

    /// <summary>
    /// SE�̃T�E���h�f�[�^
    /// </summary>
    [System.Serializable]
    public class SESoundData
    {
        [SerializeField] string m_seName = string.Empty;
        [SerializeField] AudioClip m_audioClip;
        [SerializeField, Range(0, 1)] float m_volume = 1.0f;

        public string GetSEName()
        {
            return m_seName;
        }

        public AudioClip GetAudioClip()
        {
            return m_audioClip;
        }

        public float GetVolume()
        {
            return m_volume;
        }
    }
}