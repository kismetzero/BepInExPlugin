using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace StreetsOfRoguePlugin
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
            GameController gc = GameController.gameController;
            NameDB ndb = gc.nameDB;
            List<Unlock> agentUnlocks = gc.sessionDataBig.agentUnlocks;
            List<Unlock> abilityUnlocks = gc.sessionDataBig.abilityUnlocks;
            List<Unlock> itemUnlocks = gc.sessionDataBig.itemUnlocks;
            List<Unlock> freeItemUnlocks = gc.sessionDataBig.freeItemUnlocks;
            List<Unlock> itemUnlocksCharacterCreation = gc.sessionDataBig.itemUnlocksCharacterCreation;
            List<Unlock> traitUnlocks = gc.sessionDataBig.traitUnlocks;
            List<Unlock> challengeUnlocks = gc.sessionDataBig.challengeUnlocks;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("unlock,unlockName,unlockNameType,unlockType,unlockDescriptionType,name,description");

            foreach (var i in agentUnlocks)
            {
                sb.AppendLine($"agentUnlocks,{ConvComma(i.unlockName)},{ConvComma(i.unlockNameType)},{ConvComma(i.unlockType)},{ConvComma(i.unlockDescriptionType)},{ConvComma(ndb.GetName(i.unlockName, i.unlockNameType))},{ConvComma(ndb.GetName(i.unlockName, i.unlockDescriptionType))}");
            }
            foreach (var i in abilityUnlocks)
            {
                sb.AppendLine($"abilityUnlocks,{ConvComma(i.unlockName)},{ConvComma(i.unlockNameType)},{ConvComma(i.unlockType)},{ConvComma(i.unlockDescriptionType)},{ConvComma(ndb.GetName(i.unlockName, i.unlockNameType))},{ConvComma(ndb.GetName(i.unlockName, i.unlockDescriptionType))}");
            }
            foreach (var i in itemUnlocks)
            {
                sb.AppendLine($"itemUnlocks,{ConvComma(i.unlockName)},{ConvComma(i.unlockNameType)},{ConvComma(i.unlockType)},{ConvComma(i.unlockDescriptionType)},{ConvComma(ndb.GetName(i.unlockName, i.unlockNameType))},{ConvComma(ndb.GetName(i.unlockName, i.unlockDescriptionType))}");
            }
            foreach (var i in freeItemUnlocks)
            {
                sb.AppendLine($"freeItemUnlocks,{ConvComma(i.unlockName)},{ConvComma(i.unlockNameType)},{ConvComma(i.unlockType)},{ConvComma(i.unlockDescriptionType)},{ConvComma(ndb.GetName(i.unlockName, i.unlockNameType))},{ConvComma(ndb.GetName(i.unlockName, i.unlockDescriptionType))}");
            }
            foreach (var i in itemUnlocksCharacterCreation)
            {
                sb.AppendLine($"itemUnlocksCharacterCreation,{ConvComma(i.unlockName)},{ConvComma(i.unlockNameType)},{ConvComma(i.unlockType)},{ConvComma(i.unlockDescriptionType)},{ConvComma(ndb.GetName(i.unlockName, i.unlockNameType))},{ConvComma(ndb.GetName(i.unlockName, i.unlockDescriptionType))}");
            }
            foreach (var i in traitUnlocks)
            {
                sb.AppendLine($"traitUnlocks,{ConvComma(i.unlockName)},{ConvComma(i.unlockNameType)},{ConvComma(i.unlockType)},{ConvComma(i.unlockDescriptionType)},{ConvComma(ndb.GetName(i.unlockName, i.unlockNameType))},{ConvComma(ndb.GetName(i.unlockName, i.unlockDescriptionType))}");
            }
            foreach (var i in challengeUnlocks)
            {
                sb.AppendLine($"challengeUnlocks,{ConvComma(i.unlockName)},{ConvComma(i.unlockNameType)},{ConvComma(i.unlockType)},{ConvComma(i.unlockDescriptionType)},{ConvComma(ndb.GetName(i.unlockName, i.unlockNameType))},{ConvComma(ndb.GetName("D_"+i.unlockName, i.unlockDescriptionType))}");
            }

            SaveFiles(sb.ToString(), "../DumpData.csv");
        }
    }
}
