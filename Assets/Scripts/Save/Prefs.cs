using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MountainSlide.Save
{
    public static class Prefs
    {
        private const string s_LevelCompl = "level completed";
        public static int LevelCompleted
        {
            get { return PlayerPrefs.GetInt(s_LevelCompl, 0); }
            set { PlayerPrefs.SetInt(s_LevelCompl, value); }
        }
    }
}