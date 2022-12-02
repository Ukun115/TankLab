using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    public class DecideVolume : MonoBehaviour
    {
        [SerializeField]TextMeshProUGUI[] m_text = null;

        SoundManager m_soundManager = null;

        void Start()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            m_text[0].text = $"{m_soundManager.GetSetMasterVolume*10}";
            m_text[1].text = $"{m_soundManager.GetSetBGMMasterVolume*10}";
            m_text[2].text = $"{m_soundManager.GetSetSEMasterVolume*10}";
        }

        //�����ꂽ�{�^���̎�ނɂ���ď����𕪊�
        public void SetCharacter(string character)
        {
            float volume = float.Parse(Regex.Replace(character, @"[^0-9.]", ""));

            //�}�X�^�[�{�����[���{�^��
            if (character.Contains("MasterVolume_"))
            {
                m_soundManager.GetSetMasterVolume = volume;

                m_text[0].text = $"{volume*10}";

                Debug.Log($"�}�X�^�[�{�����[����{volume*10}�ɕύX");
            }
            //BGM�{�����[���{�^��
            if (character.Contains("BGMVolume_"))
            {
                m_soundManager.GetSetBGMMasterVolume = volume;

                m_text[1].text = $"{volume*10}";

                Debug.Log($"BGM�{�����[����{volume*10}�ɕύX");
            }
            //SE�{�����[���{�^��
            if (character.Contains("SEVolume_"))
            {
                m_soundManager.GetSetSEMasterVolume = volume;

                m_text[2].text = $"{volume*10}";

                Debug.Log($"SE�{�����[����{volume*10}�ɕύX");
            }
        }
    }
}