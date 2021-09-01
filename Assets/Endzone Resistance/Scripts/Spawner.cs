using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace EndzoneResistance.Cleon
{
	public class Spawner : MonoBehaviour
	{
		public GameObject spaceShipPrefab;

		public void SpawnSpaceShip(int _amount, Planet _target, Planet _current)
		{
			for(int i = 0; i < _amount + 1; i++)
			{
				GameObject spaceShip = Instantiate(spaceShipPrefab, transform);
				spaceShip.transform.position = Random.onUnitSphere * 1.5f + _current.gameObject.transform.position;
				spaceShip.AddComponent<SpaceShip>();
				spaceShip.GetComponent<SpaceShip>().MoveSpaceShip(_target);
				spaceShip.GetComponent<SpaceShip>().shipTeam = _current.team switch
				{
					Team.Player => ShipTeam.Player,
					Team.AI => ShipTeam.Enemy,
					_ => spaceShip.GetComponent<SpaceShip>().shipTeam
				};
			}
		}
	}
}