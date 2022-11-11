using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンド管理
/// </summary>
namespace nsTankLab
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] AudioSource m_bgmAudioSource = null;
        [SerializeField] AudioSource m_seAudioSource = null;
        [SerializeField] List<BGMSoundData> m_bgmSoundDatas = null;
        [SerializeField] List<SESoundData> m_seSoundDatas = null;
        [SerializeField] float m_masterVolume = 1.0f;
        [SerializeField] float m_bgmMasterVolume = 1.0f;
        [SerializeField] float m_seMasterVolume = 1.0f;
        BGMSoundData m_bgmSoundData = null;
        SESoundData m_seSoundData = null;

        void Start()
        {
            //ゲーム起動時BGMを再生する
            PlayBGM("OutGameSceneBGM");
        }

        //BGM再生処理
        public void PlayBGM(string bgmName)
        {
            m_bgmSoundData = m_bgmSoundDatas.Find(m_bgmSoundData => m_bgmSoundData.GetBGMName() == bgmName);
            m_bgmAudioSource.clip = m_bgmSoundData.GetAudioClip();
            m_bgmAudioSource.volume = m_bgmSoundData.GetVolume() * m_bgmMasterVolume * m_masterVolume;
            m_bgmAudioSource.Play();
        }

        //効果音再生処理
        public void PlaySE(string seName)
        {
            m_seSoundData = m_seSoundDatas.Find(m_seSoundData => m_seSoundData.GetSEName() == seName);
            m_seAudioSource.volume = m_seSoundData.GetVolume() * m_seMasterVolume * m_masterVolume;
            m_seAudioSource.PlayOneShot(m_seSoundData.GetAudioClip());
        }

        public void StopBGM()
        {
            m_bgmAudioSource.Stop();
        }

        public void SetMasterVolume(int newMasterVolume)
        {
            m_masterVolume = newMasterVolume;
        }

        public void SetBGMMasterVolume(int newBGMMasterVolume)
        {
            m_bgmMasterVolume = newBGMMasterVolume;
        }

        public void SetSEMasterVolume(int newSEMasterVolume)
        {
            m_seMasterVolume = newSEMasterVolume;
        }
    }

    /// <summary>
    /// BGMのサウンドデータ
    /// </summary>
    [System.Serializable]
    public class BGMSoundData
    {
        [SerializeField] string m_bgmName = "";
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
    /// SEのサウンドデータ
    /// </summary>
    [System.Serializable]
    public class SESoundData
    {
        [SerializeField] string m_seName = "";
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