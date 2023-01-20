using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// �_�b�V�����C���𐶐����鏈��
/// </summary>
namespace nsTankLab
{
public class DashLine : MonoBehaviourPun
{
    [SerializeField] GameObject m_dashLinePrefab = null;

    void Start()
    {
            //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            GameObject m_dashLineObject = Instantiate(
            m_dashLinePrefab,
            transform.position,
            new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
            );
        m_dashLineObject.transform.parent = transform;
            //�^���N�̐F�ɍ��킹�ă_�b�V�����C���̐F��ύX
            m_dashLineObject.GetComponent<Renderer>().material.color = transform.root.GetComponent<MeshRenderer>().material.color;
    }
}
}