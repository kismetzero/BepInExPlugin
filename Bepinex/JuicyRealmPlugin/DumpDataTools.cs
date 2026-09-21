using SpaceCan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace JuicyRealmPlugin
{
    public class DumpDataTools
    {
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
                string key = i.Key.Contains(",") ? $"\"{i.Key}\"" : i.Key;
                string buff = i.Buff.Contains(",") ? $"\"{i.Buff}\"" : i.Buff;
                sb.AppendLine($"{i.ID},{key},{buff},{i.Prefab},Item,Item/{i.Prefab}");
            }
            foreach (var i in weapons)
            {
                string key = i.Key.Contains(",") ? $"\"{i.Key}\"" : i.Key;
                string buff = i.Buff.Contains(",") ? $"\"{i.Buff}\"" : i.Buff;
                sb.AppendLine($"{i.ID},{key},{buff},{i.Prefab},Weapon,Weapon/{i.Prefab}");
            }
            foreach (var i in pets)
            {
                string key = i.Key.Contains(",") ? $"\"{i.Key}\"" : i.Key;
                sb.AppendLine($"{i.ID},{key},NoField,{i.Prefab},Pet,Pet/{i.Prefab}");
            }
            foreach (var i in weaponBuffs)
            {
                string key = i.Key.Contains(",") ? $"\"{i.Key}\"" : i.Key;
                sb.AppendLine($"{i.ID},{key},NoField,ItemWeaponBuff,WeaponBuffs,Item/ItemWeaponBuff");
            }
            foreach (var i in enemies)
            {
                string key = i.Key.Contains(",") ? $"\"{i.Key}\"" : i.Key;
                string buff = i.SpawnBuff.Contains(",") ? $"\"{i.SpawnBuff}\"" : i.SpawnBuff;
                sb.AppendLine($"{i.ID},{key},{buff},{i.Prefab},Enemy,Enemy/{i.Prefab}");
            }
            string path = Path.Combine(Application.dataPath, "../DumpData.csv");
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }
    }
}
