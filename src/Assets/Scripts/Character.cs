using System;
using System.Collections.Generic;

[Serializable]
public class Configuration
{
    public string version;
    public string characterWWW;
}
[Serializable]
public class Data
{
    public List<Character> Characters= new List<Character>();
    public string Confessions = "";
    public string Soldiers = "";
    public string Generals = "";
}
[Serializable]
public class Character
{
    public string Name;
    public List<CharacterSkins> Skins = new List<CharacterSkins>();
    public bool Playable;
}
[Serializable]
public class CharacterSkins
{
    public string Name;
    public string Addressable;
    public bool Playable;

    public CharacterSkins(string name, string addressable, bool playable)
    {
        Name = name;
        Addressable = addressable;
        Playable = playable;
    }
}
