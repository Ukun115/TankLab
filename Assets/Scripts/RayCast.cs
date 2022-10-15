using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �}�E�X�J�[�\����Ray���΂���Ray�Ƀq�b�g�����I�u�W�F�N�g�𔻕ʂ��鏈��
/// </summary>
public class RayCast : MonoBehaviour
{
    //�q�I�u�W�F�N�g�B������z��
    Transform[] m_childrenObject;

    //�u���b�N�{�^���Ŏg���Ă���}�e���A��
    [SerializeField] Material[] m_blockButtonMaterial = null;

    //Ray���q�b�g�����I�u�W�F�N�g
    GameObject m_rayHitObject = null;

    //�J���[�`�F���W���ł��邩�ǂ���
    bool m_ableColorChange = false;

    //�V�[���X�C�b�`���[�X�N���v�g
    SceneSwitcher m_sceneSwitcher = null;

    Camera m_camera = null;

    void Start()
    {
        m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();

        m_camera = Camera.main;
    }

    void Update()
    {
        //Ray�𐶐�
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
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
            if (m_rayHitObject != null && m_childrenObject != null)
            {
                if (m_ableColorChange)
                {
                    //�O��}�E�X�J�[�\�����������Ă����u���b�N�{�^���I�u�W�F�N�g�����Ƃ̐F�ɖ߂�
                    //�}�e���A���̃J���[�`�F���W����
                    ChangeMaterialColor("Gray_Normal (Instance)", "Gray_Dark (Instance)", 0, false);
                }
            }

            //�}�E�X�J�[�\���������Ă���I�u�W�F�N�g���u���b�N�{�^���̂Ƃ��A
            if (hit.collider.CompareTag("BlockButton") || hit.collider.CompareTag("BackButton"))
            {
                // �q�I�u�W�F�N�g�B������z��̏�����
                m_childrenObject = new Transform[hit.collider.gameObject.transform.childCount];

                //�q�I�u�W�F�N�g���擾
                for (int i = 0; i < hit.collider.gameObject.transform.childCount; i++)
                {
                    m_childrenObject[i] = hit.collider.gameObject.transform.GetChild(i); // GetChild()�Ŏq�I�u�W�F�N�g���擾
                }

                //�}�e���A���̃J���[�`�F���W����
                ChangeMaterialColor("Gray_Light (Instance)", "Gray_Normal (Instance)", 1, true);
            }
            //�ڐG���Ă���I�u�W�F�N�g��ۑ�
            m_rayHitObject = hit.collider.gameObject;

            //�X�e�[�W�I���V�[���̎��A
            if (SceneManager.GetActiveScene().name == "SelectStageScene")
            {
                //�J�[�\���|�W�V�������ړ�������
                GameObject.Find("SceneManager").GetComponent<DecideStage>().SetCursorPosition(hit.collider.name);
            }
        }

        //���N���b�N���ꂽ�Ƃ��A
        if (Input.GetMouseButtonDown(0))
        {
            //���N���b�N���ꂽ�Ƃ��̏���
            FireLeftClick(hit);
        }
    }

    //���N���b�N���ꂽ�Ƃ��̏���
    void FireLeftClick(RaycastHit hit)
    {
        //BlockButton�^�O�������珈�����s��
        if (hit.collider.CompareTag("BlockButton"))
        {
            //���݂̃V�[���ɂ���ď�����ύX
            switch (SceneManager.GetActiveScene().name)
            {
                //�^�C�g���V�[��
                case "TitleScene":
                    //�����ꂽ�{�^���̕�����n��
                    GameObject.Find("SceneManager").GetComponent<DecideGameMode>().SetCharacter(hit.collider.name);
                    break;

                //���O���߃V�[��
                case "DecideNameScene":
                    //�����ꂽ�{�^���̕�����n��
                    GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter(hit.collider.name);
                    break;
                //�p�X���[�h���͉��
                case "InputPasswordScene":
                    //�����ꂽ�{�^���̕�����n��
                    GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter(hit.collider.name);
                    break;
                //�^���N�I���V�[��
                case "SelectTankScene":
                    //�����ꂽ�{�^���̕�����n��
                    GameObject.Find("SceneManager").GetComponent<DecideTank>().SetCharacter(hit.collider.name);
                    break;

                //�X�e�[�W�I���V�[��
                case "SelectStageScene":
                    //�����ꂽ�{�^���̕�����n��
                    GameObject.Find("SceneManager").GetComponent<DecideStage>().SetCharacter(hit.collider.name);
                    break;
            }
        }

        //BackButton�^�O�������珈�����s��
        if (hit.collider.CompareTag("BackButton"))
        {
            //1�O�̃V�[���ɖ߂�
            m_sceneSwitcher.BackScene();
        }
    }

    //�}�e���A���̃J���[�`�F���W����
    void ChangeMaterialColor(string materialName1,string materialName2,int materialNum,bool ableColorChange)
    {
        //�u���b�N�{�^���̐F(�}�e���A��)��ύX����
        for (int i = 0; i < m_childrenObject.Length; i++)
        {
            //���蓖�Ă��Ă���}�e���A���ɂ���ĕύX��̃}�e���A�������肷��
            if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == materialName1)
            {
                m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[materialNum];
            }
            else if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == materialName2)
            {
                m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[materialNum+1];
            }
        }
        m_ableColorChange = ableColorChange;
    }
}