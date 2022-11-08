using UnityEngine;
using Photon.Pun;

/// <summary>
/// �I�����C���̍ۂ̃v���C���[�̏���������
/// </summary>
namespace nsTankLab
{
public class OnlinePlayerInit : MonoBehaviourPun
{
    [SerializeField, TooltipAttribute("�e�v���C���[�̃^���N�̃}�e���A��")] Material[] m_tankColor = new Material[2];

    SaveData m_saveData = null;

    void Start()
    {
        //�r�b�g���Z(�r���I�_���a)�𗘗p���āA
        //int�^�̃v���C���[�ԍ�0or1��0�Ȃ�1�ɁA
        //1�Ȃ�0�ɂ���悤�ɂ��Ă���B
        //�r���I�_���a�E�E�E�Q�̃I�y�����h�̃r�b�g���r���āA������������ 0 �ɁA�قȂ��Ă����� 1 �ɂȂ�r�b�g���Z
        //
        //��Βl�𗘗p���āA
        //�v�����[�ԍ���0�Ȃ�0-2=-2��2�ɁA
        //�v���C���[�ԍ���1�Ȃ�1-2=-1��1�ɂȂ�悤�ɂ��Ă���B
        //Mathf.Abs�E�E�E��Βl���Z�o����B

        //&�E�E�E��������

        SettingNameAndColor($"{Mathf.Abs(m_saveData.GetSetPlayerNum -2)}P", m_saveData.GetSetPlayerNum^=1);
    }

    //�^���N�̖��O�ƃJ���[��ݒ�
    void SettingNameAndColor(string name,int materialNum)
    {
        gameObject.name = name;
        gameObject.GetComponent<MeshRenderer>().material = m_tankColor[materialNum];
    }
}
}