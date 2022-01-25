using ExtensibleSaveFormat;
using KKAPI;
using KKAPI.Chara;
using KKAPI.Studio;
using System;
using System.Collections.Generic;
using System.Text;

namespace EyeLookAdjuster
{
    public class EyeLookAdjusterCharaController : CharaCustomFunctionController
    {

        public EyeTypeStateSettings[] EyeTypeStateSettings { get; }

        public EyeLookAdjusterCharaController()
        {
            EyeTypeStateSettings = new EyeTypeStateSettings[5];
            for (int i = 0; i < 5; i++)
            {
                EyeTypeStateSettings[i] = new EyeTypeStateSettings(i);
            }
        }

        protected override void OnCardBeingSaved(GameMode currentGameMode)
        {
            if (!StudioAPI.InsideStudio)
                return;

            PluginData pluginData = new PluginData();
            foreach (EyeTypeStateSettings setting in EyeTypeStateSettings)
            {
                setting.SaveSettings(pluginData);
            }
            SetExtendedData(pluginData);
        }

        protected override void OnReload(GameMode currentGameMode, bool maintainState)
        {
            if (maintainState)
                return;

            if (StudioAPI.InsideStudio)
            {

                PluginData pluginData = GetExtendedData();
                if (pluginData != null && pluginData.data != null)
                {
                    foreach (EyeTypeStateSettings setting in EyeTypeStateSettings)
                    {
                        setting.LoadSettings(pluginData);
                    }
                }
                else
                {
                    foreach (EyeTypeStateSettings setting in EyeTypeStateSettings)
                    {
                        setting.LoadFromDefault();
                    }
                }
            }
            else
            {
                foreach (EyeTypeStateSettings setting in EyeTypeStateSettings)
                {
                    setting.LoadFromDefault();
                }
            }

            ApplyEyeStateChanges();
        }

        public void ApplyEyeStateChanges()
        {
            for (int i = 0; i < EyeTypeStateSettings.Length; i++)
            {
                ChaControl.eyeLookCtrl.eyeLookScript.eyeTypeStates[i] = EyeTypeStateSettings[i];
            }
            if (StudioAPI.InsideStudio)
                EyeLookAdjusterUI.UpdateControls();
        }

        
    }
}
