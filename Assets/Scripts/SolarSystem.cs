using System;
using System.Collections.Generic;
using UnityEngine;

namespace MixedSignals
{
    
[Serializable]
public class SolarSystem
{
    public String name;

    public List<Planet> planets;

    public SolarSystem(string name)
    {
        this.name = name;

        planets = new List<Planet>();
    }
}

}