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
            enMasterVolume,     //マスターボリューム
            enBGMVolume,        //BGMボリューム
            enSEVolume,         //SEボリューム
            enTotalVolumeType   //ボリュームタイプの数
        }


        void Start()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            for (int volumeType = (int)EnVolumeType.enMasterVolume; volumeType < (int)EnVolumeType.enTotalVolumeType; volumeType++)
            {
                //テキストの初期化
                m_soundVolumeText[volumeType].text = $"{NowVolumeValue(volumeType) * 10}";
                //カーソルの初期化
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

        //押されたボタンの種類によって処理を分岐
        public void SetCharacter(string character)
        {
            float volume = float.Parse(Regex.Replace(character, @"[^0-9.]", string.Empty));

            //マスターボリュームボタン
            if (character.Contains("MasterVolume_"))
            {
                m_soundManager.GetSetMasterVolume = volume;

                m_soundVolumeText[(int)EnVolumeType.enMasterVolume].text = $"{volume*10}";

                ChangeCursorPosition((int)EnVolumeType.enMasterVolume, m_soundManager.GetSetMasterVolume);

                Debug.Log($"マスターボリュームを{volume*10}に変更");
            }
            //BGMボリュームボタン
            if (character.Contains("BGMVolume_"))
            {
                m_soundManager.GetSetBGMMasterVolume = volume;

                m_soundVolumeText[(int)EnVolumeType.enBGMVolume].text = $"{volume*10}";

                ChangeCursorPosition((int)EnVolumeType.enBGMVolume, m_soundManager.GetSetBGMMasterVolume);

                Debug.Log($"BGMボリュームを{volume*10}に変更");
            }
            //SEボリュームボタン
            if (character.Contains("SEVolume_"))
            {
                m_soundManager.GetSetSEMasterVolume = volume;

                m_soundVolumeText[(int)EnVolumeType.enSEVolume].text = $"{volume*10}";

                ChangeCursorPosition((int)EnVolumeType.enSEVolume, m_soundManager.GetSetSEMasterVolume);

                Debug.Log($"SEボリュームを{volume*10}に変更");
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