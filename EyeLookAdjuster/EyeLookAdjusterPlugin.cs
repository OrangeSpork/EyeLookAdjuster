using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using KKAPI;
using KKAPI.Chara;
using KKAPI.Studio;
using Studio;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EyeLookAdjuster
{
    [BepInPlugin(GUID, PluginName, Version)]
    [BepInDependency(KoikatuAPI.GUID, KoikatuAPI.VersionConst)]
    public class EyeLookAdjusterPlugin : BaseUnityPlugin
    {
        public const string GUID = "orange.spork.eyelookadjuster";
        public const string PluginName = "Eye Look Adjuster";
        public const string Version = "1.0.0";


        public static EyeLookAdjusterPlugin Instance { get; set; }

        internal ManualLogSource Log => Logger;

        public EyeLookAdjusterPlugin()
        {
            if (Instance != null)
                throw new InvalidOperationException("Singleton only.");

            Instance = this;

            // Config Entries

            Front_ThresholdAngle = Config.Bind("Front", "Front Threshold Angle", 0f, "Allowed Base Eye Angle from Forward/Up");
            Front_BendingMultiplier = Config.Bind("Front", "Front Bending Multiplier", 0.4f, "Multiplier applied to all Angle Variation Checks");
            Front_MaxAngleDifference = Config.Bind("Front", "Front Max Angle Difference", 10f, "Maximum Absolute Angle Variation from Forward/Up");
            Front_UpBendingAngle = Config.Bind("Front", "Front Up Bending Angle", -10f, "Up Angle Adjust Limiter");
            Front_DownBendingAngle = Config.Bind("Front", "Front Down Bending Angle", 10f, "Down Angle Adjust Limiter");
            Front_MinBendingAngle = Config.Bind("Front", "Front Min Bending Angle", -13f, "Min Horiz Adjust Limiter");
            Front_MaxBendingAngle = Config.Bind("Front", "Front Max Bending Angle", 13f, "Max Horiz Adjust Limiter");
            Front_LeapSpeed = Config.Bind("Front", "Front Leap Speed", 2.5f, "Per Delta Angular Move Speed");
            Front_ForntTagDis = Config.Bind("Front", "Front Stare Distance", 500f, "Forward Start Distance Focus Point");
            Front_NearDis = Config.Bind("Front", "Front Near Distance", 20f, "Minimum Focus Distance");
            Front_HAngleLimit = Config.Bind("Front", "Front H Angle Limit", 110f, "Horizontal Target Limit");
            Front_VAngleLimit = Config.Bind("Front", "Front V Angle Limit", 80f, "Vertical Target Limit");

            Follow_ThresholdAngle = Config.Bind("Follow", "Follow Threshold Angle", 0f, "Allowed Base Eye Angle from Forward/Up");
            Follow_BendingMultiplier = Config.Bind("Follow", "Follow Bending Multiplier", 0.5f, "Multiplier applied to all Angle Variation Checks");
            Follow_MaxAngleDifference = Config.Bind("Follow", "Follow Max Angle Difference", 15f, "Maximum Absolute Angle Variation from Forward/Up");
            Follow_UpBendingAngle = Config.Bind("Follow", "Follow Up Bending Angle", -12f, "Up Angle Adjust Limiter");
            Follow_DownBendingAngle = Config.Bind("Follow", "Follow Down Bending Angle", 18f, "Down Angle Adjust Limiter");
            Follow_MinBendingAngle = Config.Bind("Follow", "Follow Min Bending Angle", -24f, "Min Horiz Adjust Limiter");
            Follow_MaxBendingAngle = Config.Bind("Follow", "Follow Max Bending Angle", 24f, "Max Horiz Adjust Limiter");
            Follow_LeapSpeed = Config.Bind("Follow", "Follow Leap Speed", 10f, "Per Delta Angular Move Speed");
            Follow_ForntTagDis = Config.Bind("Follow", "Follow Stare Distance", 500f, "Forward Start Distance Focus Point");
            Follow_NearDis = Config.Bind("Follow", "Follow Near Distance", 20f, "Minimum Focus Distance");
            Follow_HAngleLimit = Config.Bind("Follow", "Follow H Angle Limit", 160f, "Horizontal Target Limit");
            Follow_VAngleLimit = Config.Bind("Follow", "Follow V Angle Limit", 120f, "Vertical Target Limit");

            Avert_ThresholdAngle = Config.Bind("Avert", "Avert Threshold Angle", 0f, "Allowed Base Eye Angle from Forward/Up");
            Avert_BendingMultiplier = Config.Bind("Avert", "Avert Bending Multiplier", 0.4f, "Multiplier applied to all Angle Variation Checks");
            Avert_MaxAngleDifference = Config.Bind("Avert", "Avert Max Angle Difference", 10f, "Maximum Absolute Angle Variation from Forward/Up");
            Avert_UpBendingAngle = Config.Bind("Avert", "Avert Up Bending Angle", -10f, "Up Angle Adjust Limiter");
            Avert_DownBendingAngle = Config.Bind("Avert", "Avert Down Bending Angle", 5f, "Down Angle Adjust Limiter");
            Avert_MinBendingAngle = Config.Bind("Avert", "Avert Min Bending Angle", -12f, "Min Horiz Adjust Limiter");
            Avert_MaxBendingAngle = Config.Bind("Avert", "Avert Max Bending Angle", 12f, "Max Horiz Adjust Limiter");
            Avert_LeapSpeed = Config.Bind("Avert", "Avert Leap Speed", 5f, "Per Delta Angular Move Speed");
            Avert_ForntTagDis = Config.Bind("Avert", "Avert Stare Distance", 500f, "Forward Start Distance Focus Point");
            Avert_NearDis = Config.Bind("Avert", "Avert Near Distance", 20f, "Minimum Focus Distance");
            Avert_HAngleLimit = Config.Bind("Avert", "Avert H Angle Limit", 110f, "Horizontal Target Limit");
            Avert_VAngleLimit = Config.Bind("Avert", "Avert V Angle Limit", 80f, "Vertical Target Limit");


            Fixed_ThresholdAngle = Config.Bind("Fixed", "Fixed Threshold Angle", 0f, "Allowed Base Eye Angle from Forward/Up");
            Fixed_BendingMultiplier = Config.Bind("Fixed", "Fixed Bending Multiplier", 0.4f, "Multiplier applied to all Angle Variation Checks");
            Fixed_MaxAngleDifference = Config.Bind("Fixed", "Fixed Max Angle Difference", 10f, "Maximum Absolute Angle Variation from Forward/Up");
            Fixed_UpBendingAngle = Config.Bind("Fixed", "Fixed Up Bending Angle", -10f, "Up Angle Adjust Limiter");
            Fixed_DownBendingAngle = Config.Bind("Fixed", "Fixed Down Bending Angle", 10f, "Down Angle Adjust Limiter");
            Fixed_MinBendingAngle = Config.Bind("Fixed", "Fixed Min Bending Angle", -13f, "Min Horiz Adjust Limiter");
            Fixed_MaxBendingAngle = Config.Bind("Fixed", "Fixed Max Bending Angle", 13f, "Max Horiz Adjust Limiter");
            Fixed_LeapSpeed = Config.Bind("Fixed", "Fixed Leap Speed", 2.5f, "Per Delta Angular Move Speed");
            Fixed_ForntTagDis = Config.Bind("Fixed", "Fixed Stare Distance", 500f, "Forward Start Distance Focus Point");
            Fixed_NearDis = Config.Bind("Fixed", "Fixed Near Distance", 20f, "Minimum Focus Distance");
            Fixed_HAngleLimit = Config.Bind("Fixed", "Fixed H Angle Limit", 110f, "Horizontal Target Limit");
            Fixed_VAngleLimit = Config.Bind("Fixed", "Fixed V Angle Limit", 80f, "Vertical Target Limit");


            Adjust_ThresholdAngle = Config.Bind("Adjust", "Adjust Threshold Angle", 0f, "Allowed Base Eye Angle from Forward/Up");
            Adjust_BendingMultiplier = Config.Bind("Adjust", "Adjust Bending Multiplier", 0.5f, "Multiplier applied to all Angle Variation Checks");
            Adjust_MaxAngleDifference = Config.Bind("Adjust", "Adjust Max Angle Difference", 15f, "Maximum Absolute Angle Variation from Forward/Up");
            Adjust_UpBendingAngle = Config.Bind("Adjust", "Adjust Up Bending Angle", -20f, "Up Angle Adjust Limiter");
            Adjust_DownBendingAngle = Config.Bind("Adjust", "Adjust Down Bending Angle", 20f, "Down Angle Adjust Limiter");
            Adjust_MinBendingAngle = Config.Bind("Adjust", "Adjust Min Bending Angle", -25f, "Min Horiz Adjust Limiter");
            Adjust_MaxBendingAngle = Config.Bind("Adjust", "Adjust Max Bending Angle", 25f, "Max Horiz Adjust Limiter");
            Adjust_LeapSpeed = Config.Bind("Adjust", "Adjust Leap Speed", 2.5f, "Per Delta Angular Move Speed");
            Adjust_ForntTagDis = Config.Bind("Adjust", "Adjust Stare Distance", 500f, "Forward Start Distance Focus Point");
            Adjust_NearDis = Config.Bind("Adjust", "Adjust Near Distance", 10f, "Minimum Focus Distance");
            Adjust_HAngleLimit = Config.Bind("Adjust", "Adjust H Angle Limit", 180f, "Horizontal Target Limit");
            Adjust_VAngleLimit = Config.Bind("Adjust", "Adjust V Angle Limit", 180f, "Vertical Target Limit");

            if (StudioAPI.InsideStudio)
            {
                var harmony = new Harmony(GUID);
                harmony.Patch(typeof(MPCharCtrl).GetNestedType("IKInfo", AccessTools.all).GetMethod("Init"), null, new HarmonyMethod(typeof(EyeLookAdjusterUI).GetMethod(nameof(EyeLookAdjusterUI.InitUI), AccessTools.all)));
                harmony.Patch(typeof(MPCharCtrl).GetNestedType("IKInfo", AccessTools.all).GetMethod("UpdateInfo"), null, new HarmonyMethod(typeof(EyeLookAdjusterUI).GetMethod(nameof(EyeLookAdjusterUI.SetSelectedChar), AccessTools.all)));
                harmony.Patch(typeof(EyeLookCalc).GetMethod("EyeUpdateCalc"), null, new HarmonyMethod(typeof(EyeLookAdjusterUI).GetMethod(nameof(EyeLookAdjusterUI.LateUpdate), AccessTools.all)));                
            }
            else
            {
                Config.SettingChanged += Config_SettingChanged;
            }


#if DEBUG
            Log.LogInfo("EyeLookAdjuster Loaded.");
#endif
        }

        private void Config_SettingChanged(object sender, SettingChangedEventArgs e)
        {
            foreach (EyeLookAdjusterCharaController controller in Component.FindObjectsOfType<EyeLookAdjusterCharaController>())
            {
                foreach (EyeTypeStateSettings setting in controller.EyeTypeStateSettings)
                {
                    setting.LoadFromDefault();
                }
            }
        }

        private void Start()
        {
            CharacterApi.RegisterExtraBehaviour<EyeLookAdjusterCharaController>(GUID);
        }


        // Config Entries
        public static ConfigEntry<float> Front_ThresholdAngle { get; set; }
        public static ConfigEntry<float> Front_BendingMultiplier { get; set; }
        public static ConfigEntry<float> Front_MaxAngleDifference { get; set; }
        public static ConfigEntry<float> Front_UpBendingAngle { get; set; }
        public static ConfigEntry<float> Front_DownBendingAngle { get; set; }
        public static ConfigEntry<float> Front_MinBendingAngle { get; set; }
        public static ConfigEntry<float> Front_MaxBendingAngle { get; set; }
        public static ConfigEntry<float> Front_LeapSpeed { get; set; }
        public static ConfigEntry<float> Front_ForntTagDis { get; set; }
        public static ConfigEntry<float> Front_NearDis { get; set; }
        public static ConfigEntry<float> Front_HAngleLimit { get; set; }
        public static ConfigEntry<float> Front_VAngleLimit { get; set; }


        public static ConfigEntry<float> Follow_ThresholdAngle { get; set; }
        public static ConfigEntry<float> Follow_BendingMultiplier { get; set; }
        public static ConfigEntry<float> Follow_MaxAngleDifference { get; set; }
        public static ConfigEntry<float> Follow_UpBendingAngle { get; set; }
        public static ConfigEntry<float> Follow_DownBendingAngle { get; set; }
        public static ConfigEntry<float> Follow_MinBendingAngle { get; set; }
        public static ConfigEntry<float> Follow_MaxBendingAngle { get; set; }
        public static ConfigEntry<float> Follow_LeapSpeed { get; set; }
        public static ConfigEntry<float> Follow_ForntTagDis { get; set; }
        public static ConfigEntry<float> Follow_NearDis { get; set; }
        public static ConfigEntry<float> Follow_HAngleLimit { get; set; }
        public static ConfigEntry<float> Follow_VAngleLimit { get; set; }


        public static ConfigEntry<float> Avert_ThresholdAngle { get; set; }
        public static ConfigEntry<float> Avert_BendingMultiplier { get; set; }
        public static ConfigEntry<float> Avert_MaxAngleDifference { get; set; }
        public static ConfigEntry<float> Avert_UpBendingAngle { get; set; }
        public static ConfigEntry<float> Avert_DownBendingAngle { get; set; }
        public static ConfigEntry<float> Avert_MinBendingAngle { get; set; }
        public static ConfigEntry<float> Avert_MaxBendingAngle { get; set; }
        public static ConfigEntry<float> Avert_LeapSpeed { get; set; }
        public static ConfigEntry<float> Avert_ForntTagDis { get; set; }
        public static ConfigEntry<float> Avert_NearDis { get; set; }
        public static ConfigEntry<float> Avert_HAngleLimit { get; set; }
        public static ConfigEntry<float> Avert_VAngleLimit { get; set; }


        public static ConfigEntry<float> Fixed_ThresholdAngle { get; set; }
        public static ConfigEntry<float> Fixed_BendingMultiplier { get; set; }
        public static ConfigEntry<float> Fixed_MaxAngleDifference { get; set; }
        public static ConfigEntry<float> Fixed_UpBendingAngle { get; set; }
        public static ConfigEntry<float> Fixed_DownBendingAngle { get; set; }
        public static ConfigEntry<float> Fixed_MinBendingAngle { get; set; }
        public static ConfigEntry<float> Fixed_MaxBendingAngle { get; set; }
        public static ConfigEntry<float> Fixed_LeapSpeed { get; set; }
        public static ConfigEntry<float> Fixed_ForntTagDis { get; set; }
        public static ConfigEntry<float> Fixed_NearDis { get; set; }
        public static ConfigEntry<float> Fixed_HAngleLimit { get; set; }
        public static ConfigEntry<float> Fixed_VAngleLimit { get; set; }


        public static ConfigEntry<float> Adjust_ThresholdAngle { get; set; }
        public static ConfigEntry<float> Adjust_BendingMultiplier { get; set; }
        public static ConfigEntry<float> Adjust_MaxAngleDifference { get; set; }
        public static ConfigEntry<float> Adjust_UpBendingAngle { get; set; }
        public static ConfigEntry<float> Adjust_DownBendingAngle { get; set; }
        public static ConfigEntry<float> Adjust_MinBendingAngle { get; set; }
        public static ConfigEntry<float> Adjust_MaxBendingAngle { get; set; }
        public static ConfigEntry<float> Adjust_LeapSpeed { get; set; }
        public static ConfigEntry<float> Adjust_ForntTagDis { get; set; }
        public static ConfigEntry<float> Adjust_NearDis { get; set; }
        public static ConfigEntry<float> Adjust_HAngleLimit { get; set; }
        public static ConfigEntry<float> Adjust_VAngleLimit { get; set; }
    }
}
