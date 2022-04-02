using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace JuicyRealmPlugin
{
    class FlagControl
    {
        public bool value;      //值
        private String name;    //功能名

        private FlagControl() { }
        private FlagControl(string name)
        {
            this.value = false;
            this.name = name;
        }

        public void SwitchFlag()    //开关
        {
            this.value = !this.value;
            if (this.value)
            {
                Debug.Log(this.name + "已开启");
            }
            else
            {
                Debug.Log(this.name + "已关闭");
            }
        }

        public String Status()
        {
            return this.name + "的状态：" + this.value;
        }

        //上帝模式
        private static FlagControl GodModOn = null;
        public static FlagControl GodMod
        {
            get
            {
                if (GodModOn == null)
                {
                    GodModOn = new FlagControl("上帝模式");
                }
                return GodModOn;
            }
        }

        //锁定生命
        private static FlagControl LockLiveOn = null;
        public static FlagControl LockLive
        {
            get
            {
                if (LockLiveOn == null)
                {
                    LockLiveOn = new FlagControl("锁定生命");
                }
                return LockLiveOn;
            }
        }

        //无限生命
        private static FlagControl infiniteLiveOn = null;
        public static FlagControl infiniteALive
        {
            get
            {
                if (infiniteLiveOn == null)
                {
                    infiniteLiveOn = new FlagControl("无限生命");
                }
                return infiniteLiveOn;
            }
        }

        //锁定弹药
        private static FlagControl LockAmmoOn = null;
        public static FlagControl LockAmmo
        {
            get
            {
                if (LockAmmoOn == null)
                {
                    LockAmmoOn = new FlagControl("锁定弹药");
                }
                return LockAmmoOn;
            }
        }

        //无限弹药
        private static FlagControl infiniteAmmoOn = null;
        public static FlagControl infiniteAmmo
        {
            get
            {
                if (infiniteAmmoOn == null)
                {
                    infiniteAmmoOn = new FlagControl("无限弹药");
                }
                return infiniteAmmoOn;
            }
        }

        //无限金币
        private static FlagControl infiniteGoldOn = null;
        public static FlagControl infiniteGold
        {
            get
            {
                if (infiniteGoldOn == null)
                {
                    infiniteGoldOn = new FlagControl("无限金币");
                }
                return infiniteGoldOn;
            }
        }

        //无限技能
        private static FlagControl infiniteSkillOn = null;
        public static FlagControl infiniteSkill
        {
            get
            {
                if (infiniteSkillOn == null)
                {
                    infiniteSkillOn = new FlagControl("无限技能");
                }
                return infiniteSkillOn;
            }
        }

        //功能窗口
        private static FlagControl WindowsDisplayOn = null;
        public static FlagControl WindowsDisplay
        {
            get
            {
                if (WindowsDisplayOn == null)
                {
                    WindowsDisplayOn = new FlagControl("功能窗口");
                }
                return WindowsDisplayOn;
            }
        }

    }
}
