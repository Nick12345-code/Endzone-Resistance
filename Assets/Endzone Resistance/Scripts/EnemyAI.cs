using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace EndzoneResistance.Cleon
{
	public class EnemyAI : MonoBehaviour
	{
		private List<Planet> planets = new List<Planet>();
		private List<Planet> enemyPlanets = new List<Planet>();
		private int enemySpaceShips;
		private Spawner spawner;

		private void Start()
		{
			spawner = FindObjectOfType<Spawner>();
			planets = FindObjectsOfType<Planet>().ToList();
			foreach(Planet planet in planets)
			{
				if(planet.team == Team.AI)
				{
					enemyPlanets.Add(planet);
				}
			}
		}

		private void Update()
		{
			enemySpaceShips = GetSpaceShips();
			if(enemySpaceShips == 40)
			{
				Planet target = FindTarget();
				spawner.SpawnSpaceShip(enemySpaceShips / 2, target, enemyPlanets[0]);
				enemyPlanets[0].planetShips -= enemySpaceShips / 2;
			}
		}

		private int GetSpaceShips()
		{
			int amount = 0;
			foreach(Planet enemyPlanet in enemyPlanets)
			{
				amount += enemyPlanet.planetShips;
			}
			return amount;
		}

		private Planet FindTarget()
		{
			Planet target = null;
			float dis = 0;
			foreach(Planet planet in planets)
			{
				if(planet.team == Team.Empty)
				{
					float distance = Vector3.Distance(enemyPlanets[0].gameObject.transform.position, planet.gameObject.transform.position);
					if(-distance > dis || dis == 0)
					{
						dis = -distance;
						target = planet;
					}
				}
			}
			return target;
		}
	}
}