using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �^���N�f�[�^�x�[�X
/// </summary>
[CreateAssetMenu]
public class TankDataBase : ScriptableObject
{
	[SerializeField,TooltipAttribute("�^���N���X�g")] List<TankStatus> m_tankLists = new List<TankStatus>();

	//�^���N���X�g��Ԃ�
	public List<TankStatus> GetTankLists()
	{
		return m_tankLists;
	}
}