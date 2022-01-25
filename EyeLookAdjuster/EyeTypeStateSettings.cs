using ExtensibleSaveFormat;
using System;
using System.Collections.Generic;
using System.Text;

namespace EyeLookAdjuster
{
    public class EyeTypeStateSettings : EyeTypeState
    {
        public int EyePtn { get; set; }

        public EyeTypeStateSettings(int ptn)
        {
            EyePtn = ptn;
            switch (ptn)
            {
                case 0:
                    lookType = EYE_LOOK_TYPE.FORWARD;
                    break;
                case 1:
                    lookType = EYE_LOOK_TYPE.TARGET;
                    break;
                case 2:
                    lookType = EYE_LOOK_TYPE.AWAY;
                    break;
                case 3:
                    lookType = EYE_LOOK_TYPE.NO_LOOK;
                    break;
                case 4:
                    lookType = EYE_LOOK_TYPE.CONTROL;
                    break;
            }
        }

        public void SaveSettings(PluginData pluginData)
        {
            pluginData.data[EyePtn + "_thresholdAngleDifference"] = thresholdAngleDifference;
            pluginData.data[EyePtn + "_bendingMultiplier"] = bendingMultiplier;
            pluginData.data[EyePtn + "_maxAngleDifference"] = maxAngleDifference;
            pluginData.data[EyePtn + "_upBendingAngle"] = upBendingAngle;
            pluginData.data[EyePtn + "_downBendingAngle"] = downBendingAngle;
            pluginData.data[EyePtn + "_minBendingAngle"] = minBendingAngle;
            pluginData.data[EyePtn + "_maxBendingAngle"] = maxBendingAngle;
            pluginData.data[EyePtn + "_leapSpeed"] = leapSpeed;
            pluginData.data[EyePtn + "_forntTagDis"] = forntTagDis;
            pluginData.data[EyePtn + "_nearDis"] = nearDis;
            pluginData.data[EyePtn + "_hAngleLimit"] = hAngleLimit;
            pluginData.data[EyePtn + "_vAngleLimit"] = vAngleLimit;

        }

        public void LoadSettings(PluginData pluginData)
        {
            if (pluginData.data.TryGetValue(EyePtn + "_thresholdAngleDifference", out var value1)) { thresholdAngleDifference = (float)value1; };
            if (pluginData.data.TryGetValue(EyePtn + "_bendingMultiplier", out var value2)) { bendingMultiplier = (float)value2; };
            if (pluginData.data.TryGetValue(EyePtn + "_maxAngleDifference", out var value3)) { maxAngleDifference = (float)value3; };
            if (pluginData.data.TryGetValue(EyePtn + "_upBendingAngle", out var value4)) { upBendingAngle = (float)value4; };
            if (pluginData.data.TryGetValue(EyePtn + "_downBendingAngle", out var value5)) { downBendingAngle = (float)value5; };
            if (pluginData.data.TryGetValue(EyePtn + "_minBendingAngle", out var value6)) { minBendingAngle = (float)value6; };
            if (pluginData.data.TryGetValue(EyePtn + "_maxBendingAngle", out var value7)) { maxBendingAngle = (float)value7; };
            if (pluginData.data.TryGetValue(EyePtn + "_leapSpeed", out var value8)) { leapSpeed = (float)value8; };
            if (pluginData.data.TryGetValue(EyePtn + "_forntTagDis", out var value9)) { forntTagDis = (float)value9; };
            if (pluginData.data.TryGetValue(EyePtn + "_nearDis", out var value10)) { nearDis = (float)value10; };
            if (pluginData.data.TryGetValue(EyePtn + "_hAngleLimit", out var value11)) { hAngleLimit = (float)value11; };
            if (pluginData.data.TryGetValue(EyePtn + "_vAngleLimit", out var value12)) { vAngleLimit = (float)value12; };
        }

        public void LoadFromDefault()
        {
            switch (EyePtn)
            {
                case 0:
                    thresholdAngleDifference = EyeLookAdjusterPlugin.Front_ThresholdAngle.Value;
                    bendingMultiplier =  EyeLookAdjusterPlugin.Front_BendingMultiplier.Value;
                    maxAngleDifference = EyeLookAdjusterPlugin.Front_MaxAngleDifference.Value;
                    upBendingAngle = EyeLookAdjusterPlugin.Front_UpBendingAngle.Value;
                    downBendingAngle = EyeLookAdjusterPlugin.Front_DownBendingAngle.Value;
                    minBendingAngle = EyeLookAdjusterPlugin.Front_MinBendingAngle.Value;
                    maxBendingAngle = EyeLookAdjusterPlugin.Front_MaxBendingAngle.Value;
                    leapSpeed = EyeLookAdjusterPlugin.Front_LeapSpeed.Value;
                    forntTagDis = EyeLookAdjusterPlugin.Front_ForntTagDis.Value;
                    nearDis = EyeLookAdjusterPlugin.Front_NearDis.Value;
                    hAngleLimit = EyeLookAdjusterPlugin.Front_HAngleLimit.Value;
                    vAngleLimit = EyeLookAdjusterPlugin.Front_VAngleLimit.Value;
                    break;
                case 1:
                    thresholdAngleDifference = EyeLookAdjusterPlugin.Follow_ThresholdAngle.Value;
                    bendingMultiplier = EyeLookAdjusterPlugin.Follow_BendingMultiplier.Value;
                    maxAngleDifference = EyeLookAdjusterPlugin.Follow_MaxAngleDifference.Value;
                    upBendingAngle = EyeLookAdjusterPlugin.Follow_UpBendingAngle.Value;
                    downBendingAngle = EyeLookAdjusterPlugin.Follow_DownBendingAngle.Value;
                    minBendingAngle = EyeLookAdjusterPlugin.Follow_MinBendingAngle.Value;
                    maxBendingAngle = EyeLookAdjusterPlugin.Follow_MaxBendingAngle.Value;
                    leapSpeed = EyeLookAdjusterPlugin.Follow_LeapSpeed.Value;
                    forntTagDis = EyeLookAdjusterPlugin.Follow_ForntTagDis.Value;
                    nearDis = EyeLookAdjusterPlugin.Follow_NearDis.Value;
                    hAngleLimit = EyeLookAdjusterPlugin.Follow_HAngleLimit.Value;
                    vAngleLimit = EyeLookAdjusterPlugin.Follow_VAngleLimit.Value;
                    break;
                case 2:
                    thresholdAngleDifference = EyeLookAdjusterPlugin.Avert_ThresholdAngle.Value;
                    bendingMultiplier = EyeLookAdjusterPlugin.Avert_BendingMultiplier.Value;
                    maxAngleDifference = EyeLookAdjusterPlugin.Avert_MaxAngleDifference.Value;
                    upBendingAngle = EyeLookAdjusterPlugin.Avert_UpBendingAngle.Value;
                    downBendingAngle = EyeLookAdjusterPlugin.Avert_DownBendingAngle.Value;
                    minBendingAngle = EyeLookAdjusterPlugin.Avert_MinBendingAngle.Value;
                    maxBendingAngle = EyeLookAdjusterPlugin.Avert_MaxBendingAngle.Value;
                    leapSpeed = EyeLookAdjusterPlugin.Avert_LeapSpeed.Value;
                    forntTagDis = EyeLookAdjusterPlugin.Avert_ForntTagDis.Value;
                    nearDis = EyeLookAdjusterPlugin.Avert_NearDis.Value;
                    hAngleLimit = EyeLookAdjusterPlugin.Avert_HAngleLimit.Value;
                    vAngleLimit = EyeLookAdjusterPlugin.Avert_VAngleLimit.Value;
                    break;
                case 3:
                    thresholdAngleDifference = EyeLookAdjusterPlugin.Fixed_ThresholdAngle.Value;
                    bendingMultiplier = EyeLookAdjusterPlugin.Fixed_BendingMultiplier.Value;
                    maxAngleDifference = EyeLookAdjusterPlugin.Fixed_MaxAngleDifference.Value;
                    upBendingAngle = EyeLookAdjusterPlugin.Fixed_UpBendingAngle.Value;
                    downBendingAngle = EyeLookAdjusterPlugin.Fixed_DownBendingAngle.Value;
                    minBendingAngle = EyeLookAdjusterPlugin.Fixed_MinBendingAngle.Value;
                    maxBendingAngle = EyeLookAdjusterPlugin.Fixed_MaxBendingAngle.Value;
                    leapSpeed = EyeLookAdjusterPlugin.Fixed_LeapSpeed.Value;
                    forntTagDis = EyeLookAdjusterPlugin.Fixed_ForntTagDis.Value;
                    nearDis = EyeLookAdjusterPlugin.Fixed_NearDis.Value;
                    hAngleLimit = EyeLookAdjusterPlugin.Fixed_HAngleLimit.Value;
                    vAngleLimit = EyeLookAdjusterPlugin.Fixed_VAngleLimit.Value;
                    break;
                case 4:
                    thresholdAngleDifference = EyeLookAdjusterPlugin.Adjust_ThresholdAngle.Value;
                    bendingMultiplier = EyeLookAdjusterPlugin.Adjust_BendingMultiplier.Value;
                    maxAngleDifference = EyeLookAdjusterPlugin.Adjust_MaxAngleDifference.Value;
                    upBendingAngle = EyeLookAdjusterPlugin.Adjust_UpBendingAngle.Value;
                    downBendingAngle = EyeLookAdjusterPlugin.Adjust_DownBendingAngle.Value;
                    minBendingAngle = EyeLookAdjusterPlugin.Adjust_MinBendingAngle.Value;
                    maxBendingAngle = EyeLookAdjusterPlugin.Adjust_MaxBendingAngle.Value;
                    leapSpeed = EyeLookAdjusterPlugin.Adjust_LeapSpeed.Value;
                    forntTagDis = EyeLookAdjusterPlugin.Adjust_ForntTagDis.Value;
                    nearDis = EyeLookAdjusterPlugin.Adjust_NearDis.Value;
                    hAngleLimit = EyeLookAdjusterPlugin.Adjust_HAngleLimit.Value;
                    vAngleLimit = EyeLookAdjusterPlugin.Adjust_VAngleLimit.Value;
                    break;

            }
        }

        public void SaveAsDefault()
        {
            switch (EyePtn)
            {
                case 0:
                    EyeLookAdjusterPlugin.Front_ThresholdAngle.Value = thresholdAngleDifference;
                    EyeLookAdjusterPlugin.Front_BendingMultiplier.Value = bendingMultiplier;
                    EyeLookAdjusterPlugin.Front_MaxAngleDifference.Value = maxAngleDifference;
                    EyeLookAdjusterPlugin.Front_UpBendingAngle.Value = upBendingAngle;
                    EyeLookAdjusterPlugin.Front_DownBendingAngle.Value = downBendingAngle;
                    EyeLookAdjusterPlugin.Front_MinBendingAngle.Value = minBendingAngle;
                    EyeLookAdjusterPlugin.Front_MaxBendingAngle.Value = maxBendingAngle;
                    EyeLookAdjusterPlugin.Front_LeapSpeed.Value = leapSpeed;
                    EyeLookAdjusterPlugin.Front_ForntTagDis.Value = forntTagDis;
                    EyeLookAdjusterPlugin.Front_NearDis.Value = nearDis;
                    EyeLookAdjusterPlugin.Front_HAngleLimit.Value = hAngleLimit;
                    EyeLookAdjusterPlugin.Front_VAngleLimit.Value = vAngleLimit;
                    break;
                case 1:
                    EyeLookAdjusterPlugin.Follow_ThresholdAngle.Value = thresholdAngleDifference;
                    EyeLookAdjusterPlugin.Follow_BendingMultiplier.Value = bendingMultiplier;
                    EyeLookAdjusterPlugin.Follow_MaxAngleDifference.Value = maxAngleDifference;
                    EyeLookAdjusterPlugin.Follow_UpBendingAngle.Value = upBendingAngle;
                    EyeLookAdjusterPlugin.Follow_DownBendingAngle.Value = downBendingAngle;
                    EyeLookAdjusterPlugin.Follow_MinBendingAngle.Value = minBendingAngle;
                    EyeLookAdjusterPlugin.Follow_MaxBendingAngle.Value = maxBendingAngle;
                    EyeLookAdjusterPlugin.Follow_LeapSpeed.Value = leapSpeed;
                    EyeLookAdjusterPlugin.Follow_ForntTagDis.Value = forntTagDis;
                    EyeLookAdjusterPlugin.Follow_NearDis.Value = nearDis;
                    EyeLookAdjusterPlugin.Follow_HAngleLimit.Value = hAngleLimit;
                    EyeLookAdjusterPlugin.Follow_VAngleLimit.Value = vAngleLimit;
                    break;
                case 2:
                    EyeLookAdjusterPlugin.Avert_ThresholdAngle.Value = thresholdAngleDifference;
                    EyeLookAdjusterPlugin.Avert_BendingMultiplier.Value = bendingMultiplier;
                    EyeLookAdjusterPlugin.Avert_MaxAngleDifference.Value = maxAngleDifference;
                    EyeLookAdjusterPlugin.Avert_UpBendingAngle.Value = upBendingAngle;
                    EyeLookAdjusterPlugin.Avert_DownBendingAngle.Value = downBendingAngle;
                    EyeLookAdjusterPlugin.Avert_MinBendingAngle.Value = minBendingAngle;
                    EyeLookAdjusterPlugin.Avert_MaxBendingAngle.Value = maxBendingAngle;
                    EyeLookAdjusterPlugin.Avert_LeapSpeed.Value = leapSpeed;
                    EyeLookAdjusterPlugin.Avert_ForntTagDis.Value = forntTagDis;
                    EyeLookAdjusterPlugin.Avert_NearDis.Value = nearDis;
                    EyeLookAdjusterPlugin.Avert_HAngleLimit.Value = hAngleLimit;
                    EyeLookAdjusterPlugin.Avert_VAngleLimit.Value = vAngleLimit;
                    break;
                case 3:
                    EyeLookAdjusterPlugin.Fixed_ThresholdAngle.Value = thresholdAngleDifference;
                    EyeLookAdjusterPlugin.Fixed_BendingMultiplier.Value = bendingMultiplier;
                    EyeLookAdjusterPlugin.Fixed_MaxAngleDifference.Value = maxAngleDifference;
                    EyeLookAdjusterPlugin.Fixed_UpBendingAngle.Value = upBendingAngle;
                    EyeLookAdjusterPlugin.Fixed_DownBendingAngle.Value = downBendingAngle;
                    EyeLookAdjusterPlugin.Fixed_MinBendingAngle.Value = minBendingAngle;
                    EyeLookAdjusterPlugin.Fixed_MaxBendingAngle.Value = maxBendingAngle;
                    EyeLookAdjusterPlugin.Fixed_LeapSpeed.Value = leapSpeed;
                    EyeLookAdjusterPlugin.Fixed_ForntTagDis.Value = forntTagDis;
                    EyeLookAdjusterPlugin.Fixed_NearDis.Value = nearDis;
                    EyeLookAdjusterPlugin.Fixed_HAngleLimit.Value = hAngleLimit;
                    EyeLookAdjusterPlugin.Fixed_VAngleLimit.Value = vAngleLimit;
                    break;
                case 4:
                    EyeLookAdjusterPlugin.Adjust_ThresholdAngle.Value = thresholdAngleDifference;
                    EyeLookAdjusterPlugin.Adjust_BendingMultiplier.Value = bendingMultiplier;
                    EyeLookAdjusterPlugin.Adjust_MaxAngleDifference.Value = maxAngleDifference;
                    EyeLookAdjusterPlugin.Adjust_UpBendingAngle.Value = upBendingAngle;
                    EyeLookAdjusterPlugin.Adjust_DownBendingAngle.Value = downBendingAngle;
                    EyeLookAdjusterPlugin.Adjust_MinBendingAngle.Value = minBendingAngle;
                    EyeLookAdjusterPlugin.Adjust_MaxBendingAngle.Value = maxBendingAngle;
                    EyeLookAdjusterPlugin.Adjust_LeapSpeed.Value = leapSpeed;
                    EyeLookAdjusterPlugin.Adjust_ForntTagDis.Value = forntTagDis;
                    EyeLookAdjusterPlugin.Adjust_NearDis.Value = nearDis;
                    EyeLookAdjusterPlugin.Adjust_HAngleLimit.Value = hAngleLimit;
                    EyeLookAdjusterPlugin.Adjust_VAngleLimit.Value = vAngleLimit;
                    break;
            }
        }

        public void RestoreIllusionDefaults()
        {
            switch (EyePtn)
            {
                case 0:
                    thresholdAngleDifference = 0;
                    bendingMultiplier = .4f;
                    maxAngleDifference = 10;
                    upBendingAngle = -10;
                    downBendingAngle = 10;
                    minBendingAngle = -13;
                    maxBendingAngle = 13;
                    leapSpeed = 2.5f;
                    forntTagDis = 500f;
                    nearDis = 20f;
                    hAngleLimit = 110;
                    vAngleLimit = 80;
                    break;
                case 1:
                    thresholdAngleDifference = 0;
                    bendingMultiplier = .4f;
                    maxAngleDifference = 10;
                    upBendingAngle = -8;
                    downBendingAngle = 12;
                    minBendingAngle = -20;
                    maxBendingAngle = 20;
                    leapSpeed = 10f;
                    forntTagDis = 500f;
                    nearDis = 20f;
                    hAngleLimit = 160;
                    vAngleLimit = 120;
                    break;
                case 2:
                    thresholdAngleDifference = 0;
                    bendingMultiplier = .4f;
                    maxAngleDifference = 10;
                    upBendingAngle = -10;
                    downBendingAngle = 5;
                    minBendingAngle = -12;
                    maxBendingAngle = 12;
                    leapSpeed = 5f;
                    forntTagDis = 500f;
                    nearDis = 20f;
                    hAngleLimit = 110;
                    vAngleLimit = 80;
                    break;
                case 3:
                    thresholdAngleDifference = 0;
                    bendingMultiplier = .4f;
                    maxAngleDifference = 10;
                    upBendingAngle = -10;
                    downBendingAngle = 10;
                    minBendingAngle = -13;
                    maxBendingAngle = 13;
                    leapSpeed = 2.5f;
                    forntTagDis = 500f;
                    nearDis = 20f;
                    hAngleLimit = 110;
                    vAngleLimit = 80;
                    break;
                case 4:
                    thresholdAngleDifference = 0;
                    bendingMultiplier = .4f;
                    maxAngleDifference = 10;
                    upBendingAngle = -10;
                    downBendingAngle = 10;
                    minBendingAngle = -13;
                    maxBendingAngle = 13;
                    leapSpeed = 2.5f;
                    forntTagDis = 500f;
                    nearDis = 20f;
                    hAngleLimit = 180;
                    vAngleLimit = 180;
                    break;
            }
        }
    }
}
