using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace MixedSignals
{
    
[Serializable]
public class Planet
{
    public String name;
    public Color color;
    public float size;
    public float orbitSpeed;
    public Alien loveInterest;

    public bool discovered;

    public Planet(string name, Color color, float size, float orbitSpeed)
    {
        this.name = name;
        this.color = color;
        this.size = size;
        this.orbitSpeed = orbitSpeed;
    }
}

}