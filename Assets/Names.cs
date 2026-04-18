using System.Collections.Generic;
using UnityEngine;

public static class Names
{
    public static List<string> firstNames = new List<string>()
    {
        "Glargina",
        "Ziffany",
        "Blorpina",
        "Glurgy",
        "Blibsa",
        "Gooette",
        "Alienetta",
        "Glorb",
        "Flurb",
        "Zarn",
        "Kree",
        "Yerp",
        "Yee",
        "Blip",
        "Gnorts",
        "Zorp",
        "Snurp",
        "John",
        "Jane",
        "Spliffany",
        "Glibberina",
        "Zorbletta",
    };

    public static List<string> lastNames = new List<string>()
    {
        "Knorpelstein",
        "Zorbleglorp",
        "Blorpington",
        "Glooblesnort",
        "Wobbleflorp",
        "Snorfleblorp",
        "Zibblezorp",
        "Zorbelstein",
        "von Blorpenberg",
        "Glorpenstein",
        "von Zorble",
        "Smith",
        "Doe",
        "Blorp",
        "Goop",
        "Zoopenheim",
    };

    public static string GetRandomFirstName()
    {
        return firstNames[Random.Range(0, firstNames.Count)];
    }

    public static string GetRandomLastName()
    {
        return lastNames[Random.Range(0, lastNames.Count)];
    }


}
