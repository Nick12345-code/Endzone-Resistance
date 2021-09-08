using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndzoneResistance.Cleon
{
    public class Planet : MonoBehaviour
    {
        public Team team;
        public Level planetLevel = Level.One;
        
        public int planetShips;
        private float timer = 1;

        private void Start()
        {
            SetUp();
        }
        
        private void Update()
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                Generate();
                timer = 1;
            }

            if(team != Team.Empty)
            {
                UpGrade();
            }
        }

        private void SetUp()
        {
            planetShips = team switch
            {
                Team.Player => 20,
                Team.AI => 20,
                Team.Empty => 10,
                _ => planetShips
            };
        }
        
        private void Generate()
        {
            switch(planetLevel)
            {
                case Level.One:
                    if(team == Team.Empty)
                    {
                        planetShips++;
                    }
                    else
                    {
                        planetShips += 2;
                    }
                    break;
                case Level.Two:
                    planetShips += 3;
                    break;
                case Level.Three:
                    planetShips += 4;
                    break;
                case Level.Four:
                    planetShips += 5;
                    break;
            }
        }

        private void UpGrade()
        {
            switch(planetLevel)
            {
                case Level.One:
                    if(planetShips >= 50)
                    {
                        planetLevel = Level.Two;
                    }
                    break;
                case Level.Two:
                    if(planetShips >= 200)
                    {
                        planetLevel = Level.Three;
                    }
                    break;
                case Level.Three:
                    if(planetShips >= 500)
                    {
                        planetLevel = Level.Four;
                    }
                    break;
            }
        }
    }

    public enum Level
    {
        One,
        Two,
        Three,
        Four
    }

    public enum Team
    {
        Player,
        AI,
        Empty
    }
}