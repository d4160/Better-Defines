using System.Collections.Generic;
using UnityEngine;

namespace BetterDefines.Editor.Entity
{
    public class BetterDefinesSettings : ScriptableObject
    {
        public const string SETTINGS_RESOURCE_NAME = "BetterDefinesSettings";

        private static BetterDefinesSettings _instance;

        public List<CustomDefine> Defines;


        public static BetterDefinesSettings Instance
        {
            get { return _instance ?? (_instance = Resources.Load<BetterDefinesSettings>(SETTINGS_RESOURCE_NAME)); }
        }
    }
}