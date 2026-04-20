using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MixedSignals
{
    
[System.Serializable]
public class Alien 
{
    public string firstName;
    public string lastName;

    public string likes;
    public string dislikes;
    public AlienType alienType;
    public float loveMeter;

    public Color color;
    public AlienImagePair imagePair;
    public string hashX;
    public string hashY;

    public bool signalCloudy;

    private static System.Random rand = new System.Random();

    public Alien(string firstName, string lastName, string likes, string dislikes, AlienType alienType, Color color, AlienImagePair imagePair)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.likes = likes;
        this.dislikes = dislikes;
        this.alienType = alienType;
        this.loveMeter = 0f;
        this.color = color;
        
        //this.image = image;
        this.imagePair = imagePair;
        
        signalCloudy = false;

        GenerateHashes();
    }

    public string GetFullName()
    {
        return firstName + " " + lastName;
    }


        public void GenerateHashes()
    {

        hashX = GenerateHash();
        hashY = GenerateHash();
    }

    public String GenerateHash()
    {
        string chars = "123456789ABCDEF";
        string hash = "";

        for(int i = 0; i < 4; i++)
        {
            hash += chars[UnityEngine.Random.Range(0, chars.Length)];
        }

        return hash;

    }
    public enum AlienType
    {
        NERD,
        DEPRESSED,
        CHEERFUL,
        PARTY_GOER,
        LEGALLYDISTINCTDOORMAN,
    }
}
}