using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class BuildPlatform
{
    public bool Enabled;

    [IgnoreDataMember]
    public Texture2D Icon;

    /// <summary>
    /// Used to load default platform icon
    /// </summary>
    public string Id;
    public string Name;

    public BuildPlatform(string name, string id, bool enabled)
    {
        Name = name;
        Id = id;
        Enabled = enabled;
    }
}