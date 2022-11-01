using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タンクデータベース
/// </summary>
[CreateAssetMenu]
public class TankDataBase : ScriptableObject
{
	[SerializeField,TooltipAttribute("タンクリスト")] List<TankStatus> m_tankLists = new List<TankStatus>();

	//タンクリストを返す
	public List<TankStatus> GetTankLists()
	{
		return m_tankLists;
	}
}