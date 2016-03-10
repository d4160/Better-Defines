using System.Collections.Generic;
using UnityEngine;


public class BetterDefinesSettings : ScriptableObject
{
    public const string SETTINGS_RESOURCE_NAME = "BetterDefinesSettings";

    public static BetterDefinesSettings instance;

    public static BetterDefinesSettings Instance
    {
        get { return instance ?? (instance = Resources.Load<BetterDefinesSettings>(SETTINGS_RESOURCE_NAME)); }
    }

    public List<CustomDefine> Defines;
}