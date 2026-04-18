using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Alien 
{
    public string firstName;
    public string lastName;

    public float loveMeter;

    public Color color;
    public Image image;

    public Alien(string firstName, string lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.loveMeter = 0f;
    }

    public string GetFullname()
    {
        return firstName + " " + lastName;
    }



}

public enum AlienType
{
    
}