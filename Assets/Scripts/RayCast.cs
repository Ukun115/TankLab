using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCast : MonoBehaviour
{
    Transform[] m_childrenObject; // �q�I�u�W�F�N�g�B������z��

    //�u���b�N�{�^���Ŏg���Ă���}�e���A��
    [SerializeField] Material[] m_blockButtonMaterial = null;

    GameObject m_rayHitObject = null;

    bool m_isColorChange = false;

    void Update()
    {
        //Ray�𐶐�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Ray�𓊎�
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject != m_rayHitObject)
            {
                if (m_rayHitObject != null && m_childrenObject != null)
                {
                    if (m_isColorChange)
                    {
                        //�O��}�E�X�J�[�\�����������Ă����u���b�N�{�^���I�u�W�F�N�g�����Ƃ̐F�ɖ߂�
                        for (int i = 0; i < m_childrenObject.Length; i++)
                        {
                            //���蓖�Ă��Ă���}�e���A���ɂ���ĕύX��̃}�e���A�������肷��
                            if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == "Gray_Normal (Instance)")
                            {
                                m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[0];
                            }
                            else if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == "Gray_Dark (Instance)")
                            {
                                m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[1];
                            }
                        }
                        m_isColorChange = false;
                    }
                }

                //�}�E�X�J�[�\���������Ă���I�u�W�F�N�g���u���b�N�{�^���̂Ƃ��A
                if (hit.collider.CompareTag("BlockButton"))
                {
                    // �q�I�u�W�F�N�g�B������z��̏�����
                    m_childrenObject = new Transform[hit.collider.gameObject.transform.childCount];

                    //�q�I�u�W�F�N�g���擾
                    for (int i = 0; i < hit.collider.gameObject.transform.childCount; i++)
                    {
                        m_childrenObject[i] = hit.collider.gameObject.transform.GetChild(i); // GetChild()�Ŏq�I�u�W�F�N�g���擾
                    }

                    //�u���b�N�{�^���̐F(�}�e���A��)��ύX����
                    for (int i = 0; i < m_childrenObject.Length; i++)
                    {
                        //���蓖�Ă��Ă���}�e���A���ɂ���ĕύX��̃}�e���A�������肷��
                        if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == "Gray_Light (Instance)")
                        {
                            m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[1];
                        }
                        else if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == "Gray_Normal (Instance)")
                        {
                            m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[2];
                        }
                    }

                    m_isColorChange = true;
                }
                //�ڐG���Ă���I�u�W�F�N�g��ۑ�
                m_rayHitObject = hit.collider.gameObject;
            }

            //���N���b�N���ꂽ�Ƃ��A
            if (Input.GetMouseButtonDown(0))
            {
                //Ray���Փ˂����I�u�W�F�N�g�̖��O�����O�\��
                Debug.Log(hit.collider.name);

                //BlockButton�^�O�������珈�����s��
                if (hit.collider.CompareTag("BlockButton"))
                {
                    //���݂̃V�[���ɂ���ď�����ύX
                    switch(SceneManager.GetActiveScene().name)
                    {
                        case "TitleScene":
                            break;
                            //���O���߃V�[��
                        case "DecideNameScene":
                            //�����ꂽ�{�^���̕�����n��
                            GameObject.Find("SceneManager").GetComponent< DecidePlayerName>().SetNameText(hit.collider.name);
                            break;
                    }
                }
            }
        }
    }
}