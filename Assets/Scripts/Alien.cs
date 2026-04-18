using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[System.Serializable]
public class Alien 
{
    public string firstName;
    public string lastName;

    public float loveMeter;

    public Color color;
    public Image image;

    public string hashX;
    public string hashY;

    public Alien(string firstName, string lastName, Color color, Image image)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.loveMeter = 0f;
        this.color = color;
        this.image = image;

        GenerateHashes();
    }

    public string GetFullName()
    {
        return firstName + " " + lastName;
    }


    public void GenerateHashes()
{
    List <String> hashes = new List<String>();

    hashX = GenerateHash();
    hashY = GenerateHash();
}

public String GenerateHash()
{
    string chars = "123456789ABCDEF";
    string hash = "";
    System.Random rand = new System.Random();

    for(int i = 0; i < 4; i++)
    {
        hash += chars[rand.Next(0, chars.Length)];
    }

    return hash;

}

public enum AlienType
{
    
}

}