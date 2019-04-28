//----------------------------------------------
// File: TagsAndLayers.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public class TagsAndLayers 
    {
        public const string EnemyTag  = "Enemy";
        public const string PlayerTag = "Player";
        public const string CrateTag  = "Crate";
        public const string BarrelTag = "Barrel";

        public static bool IsShootable(string tag)
        {
            return  tag == TagsAndLayers.EnemyTag ||
                    tag == TagsAndLayers.CrateTag ||
                    tag == TagsAndLayers.BarrelTag;
        }
    }
}
