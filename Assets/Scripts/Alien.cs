using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Alien 
{
    public string firstName;
    public string lastName;

    public string likes;
    public string dislikes;
    public float loveMeter;

    public Color color;
    public Image image;

    public string hashX;
    public string hashY;

    private static System.Random rand = new System.Random();

    public Alien(string firstName, string lastName, string likes, string dislikes, Color color, Image image)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.likes = likes;
        this.dislikes = dislikes;
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

    /*
    public enum AlienType
    {
        
    }
    */
}