using GUITree;
using KKAPI.Studio;
using Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EyeLookAdjuster
{
    public class EyeLookAdjusterUI
    {
        private static GameObject DetailPanel;

        private static Button EyeLookAdjustButton;
        private static Button LoadFromDefaultButton;
        private static Button LoadFromIllusionButton;
        private static Button SaveAsDefaultButton;

        //       private static Toggle OverrideDefaultToggle;

        private static Text ModeNameText;

        private static Text ThresholdAngleText;
        private static InputField ThresholdAngleField;

        private static Text BendingMultiplierText;
        private static InputField BendingMultiplierField;

        private static Text MaxAngleDifferenceText;
        private static InputField MaxAngleDifferenceField;

        private static Text UpBendingAngleText;
        private static InputField UpBendingAngleField;

        private static Text DownBendingAngleText;
        private static InputField DownBendingAngleField;

        private static Text MinBendingAngleText;
        private static InputField MinBendingAngleField;

        private static Text MaxBendingAngleText;
        private static InputField MaxBendingAngleField;

        private static Text LeapSpeedText;
        private static InputField LeapSpeedField;

        private static Text FrontDistanceText;
        private static InputField FrontDistanceField;

        private static Text NearDistanceText;
        private static InputField NearDistanceField;

        private static Text HAngleLimitText;
        private static InputField HAngleLimitField;

        private static Text VAngleLimitText;
        private static InputField VAngleLimitField;

        private static OCIChar selectedChar;


        public static void SetSelectedChar(OCIChar _char)
        {
            selectedChar = _char;
            lastPtn = selectedChar.charInfo.eyeLookCtrl.ptnNo;
            UpdateControls();
            UpdateButtonText();
#if DEBUG
            EyeLookAdjusterPlugin.Instance.Log.LogInfo($"New Selected Char: {selectedChar.charInfo.chaFile.parameter.fullname} {lastPtn}");
#endif
        }

        private static int lastPtn = -1;
        public static void LateUpdate(int ptnNo, EyeLookCalc __instance)
        {                  
            if (selectedChar != null && lastPtn != ptnNo && __instance == selectedChar.GetChaControl().eyeLookCtrl.eyeLookScript)
            {
                lastPtn = ptnNo;
                UpdateControls();
                UpdateButtonText();
#if DEBUG
                EyeLookAdjusterPlugin.Instance.Log.LogInfo($"Updating UI for {selectedChar.charInfo.chaFile.parameter.fullname} Ptn: {lastPtn}");
#endif                
            }            
        }

        public static void UpdateControls()
        {
            updating = true;
            if (selectedChar != null)
            {
                EyeTypeStateSettings state = (EyeTypeStateSettings)selectedChar.charInfo.eyeLookCtrl.eyeLookScript.eyeTypeStates[lastPtn];
                ThresholdAngleField.text = state.thresholdAngleDifference.ToString("0.00");
                BendingMultiplierField.text = state.bendingMultiplier.ToString("0.00");
                MaxAngleDifferenceField.text = state.maxAngleDifference.ToString("0.00");
                UpBendingAngleField.text = state.upBendingAngle.ToString("0.00");
                DownBendingAngleField.text = state.downBendingAngle.ToString("0.00");
                MinBendingAngleField.text = state.minBendingAngle.ToString("0.00");
                MaxBendingAngleField.text = state.maxBendingAngle.ToString("0.00");
                LeapSpeedField.text = state.leapSpeed.ToString("0.00");
                FrontDistanceField.text = state.forntTagDis.ToString("0.00");
                NearDistanceField.text = state.nearDis.ToString("0.00");
                HAngleLimitField.text = state.hAngleLimit.ToString("0.00");
                VAngleLimitField.text = state.vAngleLimit.ToString("0.00");
            }
            updating = false;
        }

        private static void UpdateButtonText()
        {
            string modeName = "";
            switch (lastPtn)
            {
                case 0:
                    modeName = "Front";
                    break;
                case 1:
                    modeName = "Follow";
                    break;
                case 2:
                    modeName = "Avert";
                    break;
                case 3:
                    modeName = "Fixed";
                    break;
                case 4:
                    modeName = "Adjust";
                    break;
            }

            ModeNameText.text = $"{modeName} Settings";
            (LoadFromDefaultButton.transform.GetChild(0).GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI).text = $"Load {modeName} Default";
            (SaveAsDefaultButton.transform.GetChild(0).GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI).text = $"Save as {modeName} Default";
            (LoadFromIllusionButton.transform.GetChild(0).GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI).text = $"Load {modeName} Illusion Default";
        }
        private static bool UIInitialized;
        public static void InitUI()
        {
            GameObject lookAtPanel = GameObject.Find("StudioScene/Canvas Main Menu/02_Manipulate/00_Chara/02_Kinematic/02_LookAt");

            DetailPanel = GameObject.Instantiate(lookAtPanel, lookAtPanel.transform);
            DetailPanel.name = "Gaze Detail Details Panel";
            DetailPanel.transform.localPosition = new Vector3(200, 0, 0);
            DetailPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(240, 440);

            Texture2D tex = new Texture2D(240, 440);
#if HS2
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream($"HS2_EyeLookAdjuster.resources.GazeControlDetail.png"))
#elif AI
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream($"AI_EyeLookAdjuster.resources.GazeControlDetail.png"))
#endif
            {
                byte[] bs = new byte[s.Length];
                s.Read(bs, 0, bs.Length);
                tex.LoadImage(bs);
            }

            Image detailPanelImage = DetailPanel.GetComponent<Image>();
            Sprite detailPanelSprite = Sprite.Create(tex, new Rect(0, 0, 240, 440), detailPanelImage.sprite.pivot);
            detailPanelImage.sprite = detailPanelSprite;

            foreach (Transform child in DetailPanel.transform)
            {
                GameObject.Destroy(child.gameObject);
            }


            // Setup Detail Panel Activation/Deactivation Button
            GameObject FollowButton = GameObject.Find("StudioScene/Canvas Main Menu/02_Manipulate/00_Chara/02_Kinematic/02_LookAt/Button Camera");
            GameObject FKButtonGO = GameObject.Find("StudioScene/Canvas Main Menu/02_Manipulate/00_Chara/02_Kinematic/Viewport/Content/FK");
            EyeLookAdjustButton = GameObject.Instantiate(FKButtonGO.GetComponent<Button>(), lookAtPanel.transform);
            EyeLookAdjustButton.name = "EyeLookAdjustButton";
            EyeLookAdjustButton.transform.localPosition = new Vector3(145, -95, 0);
            EyeLookAdjustButton.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 50);
            EyeLookAdjustButton.GetComponent<PreferredSizeFitter>().preferredWidth = 60;

            var EyeLookAdjustButtonText = EyeLookAdjustButton.transform.GetChild(0).GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
            EyeLookAdjustButtonText.text = "Details";
            EyeLookAdjustButtonText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 70);

            EyeLookAdjustButton.onClick = new Button.ButtonClickedEvent();
            EyeLookAdjustButton.image.color = Color.white;
            EyeLookAdjustButton.image.rectTransform.sizeDelta = new Vector2(90, 20);

            EyeLookAdjustButton.onClick.AddListener(() =>
            {
                DetailPanel.SetActive(!DetailPanel.activeSelf);
                ColorBlock cb = EyeLookAdjustButton.colors;
                if (DetailPanel.activeSelf)
                    cb.normalColor = Color.green;
                else
                    cb.normalColor = Color.white;
                EyeLookAdjustButton.colors = cb;
            });

            // Instantiate Detail Panel Controls
            GameObject camRotXField = GameObject.Find("StudioScene/Canvas Main Menu/04_System/02_Option/Option/Viewport/Content/Camera/Rot Speed X/InputField");

            // Default Toggle
            GameObject defaultTogglePrefab = GameObject.Find("StudioScene/Canvas Main Menu/02_Manipulate/00_Chara/02_Kinematic/00_FK/Toggle Neck");
            GameObject textPrefab = GameObject.Find("StudioScene/Canvas Main Menu/02_Manipulate/00_Chara/02_Kinematic/00_FK/Text Neck");

            /*          Text OverrideDefaultText = BuildTextField("Override Text", "Override: ", 120, textPrefab, DetailPanel);

                      OverrideDefaultToggle = GameObject.Instantiate(defaultTogglePrefab.GetComponent<Toggle>(), DetailPanel.transform);
                      OverrideDefaultToggle.name = "Override Default Toggle";
                      OverrideDefaultToggle.transform.localPosition = new Vector3(OverrideDefaultToggle.transform.localPosition.x + 120, OverrideDefaultToggle.transform.localPosition.y + 120, 0);
                      OverrideDefaultToggle.onValueChanged.RemoveAllListeners(); */

            ModeNameText = BuildTextField("Mode Name Text", "", 130, textPrefab, DetailPanel);
            ModeNameText.gameObject.transform.Translate(new Vector3(40, 0, 0));

            ThresholdAngleText = BuildTextField("Threshold Angle Text", "Threshold Angle: ", 105, textPrefab, DetailPanel);
            ThresholdAngleField = BuildInputField("ThresholdAngleField", -55, camRotXField, DetailPanel);
            ThresholdAngleField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].thresholdAngleDifference = value;
                    }
                }
            });

            BendingMultiplierText = BuildTextField("Bending Multiplier Text", "Bending Mult: ", 80, textPrefab, DetailPanel);
            BendingMultiplierField = BuildInputField("BendingMultiplierField", -80, camRotXField, DetailPanel);
            BendingMultiplierField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].bendingMultiplier = value;
                    }
                }
            });

            MaxAngleDifferenceText = BuildTextField("MaxAngle Diff Text", "Max Angle Diff: ", 55, textPrefab, DetailPanel);
            MaxAngleDifferenceField = BuildInputField("MaxAngleDiffField", -105, camRotXField, DetailPanel);
            MaxAngleDifferenceField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].maxAngleDifference = value;
                    }
                }
            });

            UpBendingAngleText = BuildTextField("UpBending Angle Text", "Up Bend Angle: ", 30, textPrefab, DetailPanel);
            UpBendingAngleField = BuildInputField("UpBendingAngleField", -130, camRotXField, DetailPanel);
            UpBendingAngleField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].upBendingAngle = value;
                    }
                }
            });

            DownBendingAngleText = BuildTextField("DownBending Angle Text", "Down Bend Angle: ", 5, textPrefab, DetailPanel);
            DownBendingAngleField = BuildInputField("DownBendingAngleField", -155, camRotXField, DetailPanel);
            DownBendingAngleField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].downBendingAngle = value;
                    }
                }
            });

            MinBendingAngleText = BuildTextField("MinBend Angle Text", "Min Bend Angle: ", -20, textPrefab, DetailPanel);
            MinBendingAngleField = BuildInputField("MinBendAngleField", -180, camRotXField, DetailPanel);
            MinBendingAngleField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].minBendingAngle = value;
                    }
                }
            });

            MaxBendingAngleText = BuildTextField("MaxBend Angle Text", "Max Bend Angle: ", -45, textPrefab, DetailPanel);
            MaxBendingAngleField = BuildInputField("MaxBendAngleField", -205, camRotXField, DetailPanel);
            MaxBendingAngleField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].maxBendingAngle = value;
                    }
                }
            });

            LeapSpeedText = BuildTextField("Leap Speed Text", "Track Speed: ", -70, textPrefab, DetailPanel);
            LeapSpeedField = BuildInputField("LeapSpeedField", -230, camRotXField, DetailPanel);
            LeapSpeedField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].leapSpeed = value;
                    }
                }
            });

            FrontDistanceText = BuildTextField("Front Distance Text", "Front Focus Dist: ", -95, textPrefab, DetailPanel);
            FrontDistanceField = BuildInputField("FrontDistanceField", -255, camRotXField, DetailPanel);
            FrontDistanceField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].forntTagDis = value;
                    }
                }
            });

            NearDistanceText = BuildTextField("Near Distance Text", "Min Focus Dist: ", -120, textPrefab, DetailPanel);
            NearDistanceField = BuildInputField("NearDistanceField", -280, camRotXField, DetailPanel);
            NearDistanceField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].nearDis = value;
                    }
                }
            });

            HAngleLimitText = BuildTextField("HLimit Angle Text", "H Angle Limit: ", -145, textPrefab, DetailPanel);
            HAngleLimitField = BuildInputField("HLimitAngleField", -305, camRotXField, DetailPanel);
            HAngleLimitField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].hAngleLimit = value;
                    }
                }
            });

            VAngleLimitText = BuildTextField("VLimit Angle Text", "V Angle Limit: ", -170, textPrefab, DetailPanel);
            VAngleLimitField = BuildInputField("VLimitAngleField", -330, camRotXField, DetailPanel);
            VAngleLimitField.onEndEdit.AddListener((string s) => {
                if (float.TryParse(s, out float value))
                {
                    if (updating)
                        return;

                    foreach (EyeLookAdjusterCharaController controller in StudioAPI.GetSelectedControllers<EyeLookAdjusterCharaController>())
                    {
                        controller.EyeTypeStateSettings[controller.ChaControl.eyeLookCtrl.eyeLookScript.nowPtnNo].vAngleLimit = value;
                    }
                }
            });

            // Actions
            SaveAsDefaultButton = GameObject.Instantiate(FKButtonGO.GetComponent<Button>(), DetailPanel.transform);
            SaveAsDefaultButton.name = "SaveAsDefaultButton";
            SaveAsDefaultButton.transform.localPosition = new Vector3(textPrefab.transform.localPosition.x + 120, -375, 0);
            SaveAsDefaultButton.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 20);
            SaveAsDefaultButton.GetComponent<PreferredSizeFitter>().preferredWidth = 200;

            var SaveAsDefaultButtonText = SaveAsDefaultButton.transform.GetChild(0).GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
            SaveAsDefaultButtonText.text = "Save as Default";
            SaveAsDefaultButtonText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);

            SaveAsDefaultButton.onClick = new Button.ButtonClickedEvent();
            SaveAsDefaultButton.image.color = Color.white;
            SaveAsDefaultButton.image.rectTransform.sizeDelta = new Vector2(200, 20);

            SaveAsDefaultButton.onClick.AddListener(() =>
            {
                if (selectedChar != null)
                {
                    selectedChar.GetChaControl().gameObject.GetComponent<EyeLookAdjusterCharaController>().EyeTypeStateSettings[selectedChar.GetChaControl().eyeLookCtrl.eyeLookScript.nowPtnNo].SaveAsDefault();
                }
            });

            LoadFromDefaultButton = GameObject.Instantiate(FKButtonGO.GetComponent<Button>(), DetailPanel.transform);
            LoadFromDefaultButton.name = "LoadFromDefaultButton";
            LoadFromDefaultButton.transform.localPosition = new Vector3(textPrefab.transform.localPosition.x + 120, -400, 0);
            LoadFromDefaultButton.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 20);
            LoadFromDefaultButton.GetComponent<PreferredSizeFitter>().preferredWidth = 200;

            var LoadFromDefaultButtonText = LoadFromDefaultButton.transform.GetChild(0).GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
            LoadFromDefaultButtonText.text = "Load From Default";
            LoadFromDefaultButtonText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);

            LoadFromDefaultButton.onClick = new Button.ButtonClickedEvent();
            LoadFromDefaultButton.image.color = Color.white;
            LoadFromDefaultButton.image.rectTransform.sizeDelta = new Vector2(200, 20);

            LoadFromDefaultButton.onClick.AddListener(() =>
            {
                if (selectedChar != null)
                {
                    selectedChar.GetChaControl().gameObject.GetComponent<EyeLookAdjusterCharaController>().EyeTypeStateSettings[selectedChar.GetChaControl().eyeLookCtrl.eyeLookScript.nowPtnNo].LoadFromDefault();
                    UpdateControls();
                }
            });

            LoadFromIllusionButton = GameObject.Instantiate(FKButtonGO.GetComponent<Button>(), DetailPanel.transform);
            LoadFromIllusionButton.name = "LoadFromIllusionButton";
            LoadFromIllusionButton.transform.localPosition = new Vector3(textPrefab.transform.localPosition.x + 120, -425, 0);
            LoadFromIllusionButton.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 20);
            LoadFromIllusionButton.GetComponent<PreferredSizeFitter>().preferredWidth = 200;

            var LoadFromIllusionButtonText = LoadFromIllusionButton.transform.GetChild(0).GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
            LoadFromIllusionButtonText.text = "Load Illusion Default";
            LoadFromIllusionButtonText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);

            LoadFromIllusionButton.onClick = new Button.ButtonClickedEvent();
            LoadFromIllusionButton.image.color = Color.white;
            LoadFromIllusionButton.image.rectTransform.sizeDelta = new Vector2(200, 20);

            LoadFromIllusionButton.onClick.AddListener(() =>
            {
                if (selectedChar != null)
                {
                    selectedChar.GetChaControl().gameObject.GetComponent<EyeLookAdjusterCharaController>().EyeTypeStateSettings[selectedChar.GetChaControl().eyeLookCtrl.eyeLookScript.nowPtnNo].RestoreIllusionDefaults();
                    UpdateControls();
                }
            });

            UIInitialized = true;

        }

        private static Text BuildTextField(string name, string text, int y, GameObject prefab, GameObject panel)
        {
            Text textGO = GameObject.Instantiate(prefab.GetComponent<Text>(), panel.transform);
            textGO.name = name;
            textGO.text = text;
            textGO.transform.localPosition = new Vector3(textGO.transform.localPosition.x + 20, textGO.transform.localPosition.y + y, 0);
            textGO.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 20);
            return textGO;
        }

        private static bool updating = false;
        private static InputField BuildInputField(string name, int y, GameObject prefab, GameObject panel)
        {
            // Clone Text
            InputField inputField = GameObject.Instantiate(prefab.GetComponent<InputField>(), panel.transform);
            inputField.name = name;
            inputField.transform.localPosition = new Vector3(150, inputField.transform.localPosition.y + y, 0);
            inputField.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 20);
            inputField.onValueChanged = new InputField.OnChangeEvent();
            inputField.onEndEdit = new InputField.SubmitEvent();
            
            return inputField;
        }
    }
}
