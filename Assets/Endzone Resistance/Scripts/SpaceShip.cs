using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndzoneResistance.Cleon
{
	public class SpaceShip : MonoBehaviour
	{
		[SerializeField] private float speed = 5f;
		private Planet target;
		public ShipTeam shipTeam;
	
		public void MoveSpaceShip(Planet _target)
		{
			target = _target;
		}

		private void Update()
		{
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.gameObject.transform.position, speed * Time.deltaTime); 
            }
		}

		private void OnTriggerEnter(Collider _other)
		{
			if(_other.GetComponent<Planet>())
			{
				Team otherTeam = _other.GetComponent<Planet>().team;
				switch(otherTeam)
				{
					case Team.Player:
						switch(shipTeam)
						{
							case ShipTeam.Player:
								_other.GetComponent<Planet>().planetShips++;
								Destroy(this.gameObject);
								break;
							case ShipTeam.Enemy:
								_other.GetComponent<Planet>().planetShips--;
								Destroy(this.gameObject);
								if(_other.GetComponent<Planet>().planetShips < 0)
								{
									Planet planet = _other.gameObject.GetComponent<Planet>();
									planet.team = Team.AI;
									planet.planetShips += 10;
									planet.planetLevel = Level.One;
								}
								break;
						}
						break;
					case Team.AI:
						switch(shipTeam)
						{
							case ShipTeam.Player:
								_other.GetComponent<Planet>().planetShips--;
								Destroy(this.gameObject);
								if(_other.GetComponent<Planet>().planetShips < 0)
								{
									Planet planet = _other.gameObject.GetComponent<Planet>();
									planet.team = Team.Player;
									planet.planetShips += 10;
									planet.planetLevel = Level.One;
								}
								break;
							case ShipTeam.Enemy:
								_other.GetComponent<Planet>().planetShips++;
								Destroy(this.gameObject);
								break;
						}
						break;
					case Team.Empty:
						_other.GetComponent<Planet>().planetShips--;
						Destroy(this.gameObject);
						if(_other.GetComponent<Planet>().planetShips <= 0)
						{
							Planet planet = _other.gameObject.GetComponent<Planet>();
							planet.planetShips += 10;
							planet.planetLevel = Level.One;
							planet.team = shipTeam switch
							{
								ShipTeam.Player => Team.Player,
								ShipTeam.Enemy => Team.AI,
								_ => planet.team
							};
						}
						break;
				}
			}
		}
	}
	
	public enum ShipTeam
	{
		Player,
		Enemy
	}
}