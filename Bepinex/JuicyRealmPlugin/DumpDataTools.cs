using SpaceCan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

namespace JuicyRealmPlugin
{
    public class DumpDataTools
    {
        public static void SaveFiles(string s, string path)
        {
            string tmp_path = Path.Combine(Application.dataPath, path);
            File.WriteAllText(tmp_path, s, Encoding.UTF8);
        }
        public static string ConvComma(string s)
        {
            return s.Contains(",") ? $"\"{s}\"" : s;
        }
        public static void DumpData()
        {
            DataManager dataManager = GameManager.GetManager<DataManager>();
            List<ItemDefine> items = dataManager.config.Item;
            List<WeaponDefine> weapons = dataManager.config.Weapon;
            List<PetDefine> pets = dataManager.config.Pet;
            List<WeaponBuffDefine> weaponBuffs = dataManager.config.WeaponBuff;
            List<EnemyDefine> enemies = dataManager.config.Enemy;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID,Key,Buff,Prefab,Type,FullPrefab");

            foreach (var i in items)
            {
                sb.AppendLine($"{i.ID},{ConvComma(i.Key)},{ConvComma(i.Buff)},{i.Prefab},Item,Item/{i.Prefab}");
            }
            foreach (var i in weapons)
            {
                sb.AppendLine($"{i.ID},{ConvComma(i.Key)},{ConvComma(i.Buff)},{i.Prefab},Weapon,Weapon/{i.Prefab}");
            }
            foreach (var i in pets)
            {
                sb.AppendLine($"{i.ID},{ConvComma(i.Key)},NoField,{i.Prefab},Pet,Pet/{i.Prefab}");
            }
            foreach (var i in weaponBuffs)
            {
                sb.AppendLine($"{i.ID},{ConvComma(i.Key)},NoField,ItemWeaponBuff,WeaponBuffs,Item/ItemWeaponBuff");
            }
            foreach (var i in enemies)
            {
                sb.AppendLine($"{i.ID},{ConvComma(i.Key)},{ConvComma(i.SpawnBuff)},{i.Prefab},Enemy,Enemy/{i.Prefab}");
            }

            SaveFiles(sb.ToString(), "../DumpData.csv");
        }
    }
}
