using GGM.Storage;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.Config
{
    public static class Settings
    {
        public static string[] CopiedHumanSkins;
        public static List<string[]> CustomMapSkinsList;
        public static List<string> CustomMapSkinsTitlesList;
        public static List<string> CustomMapScriptsList;
        public static List<string[]> HumanSkinsList;
        public static List<string> HumanSkinsTitlesList;
        public static List<int> LocationSkinsCityAmbientList;
        public static List<float[]> LocationSkinsCityAmbientSettingsList;
        public static string[] LocationSkinsCityCopiedSet;
        public static List<int> LocationSkinsCityFogList;
        public static List<float[]> LocationSkinsCityFogSettingsList;
        public static List<int> LocationSkinsCityLightList;
        public static List<float[]> LocationSkinsCityLightSettingsList;
        public static List<string[]> LocationSkinsCityList;
        public static List<int> LocationSkinsCityParticlesList;
        public static List<float[]> LocationSkinsCityParticlesSettingsList;
        public static List<string> LocationSkinsCityTitlesList;
        public static List<int> LocationSkinsForestAmbientList;
        public static List<float[]> LocationSkinsForestAmbientSettingsList;
        public static string[] LocationSkinsForestCopiedSet;
        public static List<int> LocationSkinsForestFogList;
        public static List<float[]> LocationSkinsForestFogSettingsList;
        public static List<int> LocationSkinsForestLightList;
        public static List<float[]> LocationSkinsForestLightSettingsList;
        public static List<string[]> LocationSkinsForestList;
        public static List<int> LocationSkinsForestParticlesList;
        public static List<float[]> LocationSkinsForestParticlesSettingsList;
        public static List<string> LocationSkinsForestTitlesList;
        public static List<string> MutedPlayers;
        public static List<object> ReceivedLocationSkinsData;
        public static IDataStorage Storage = null;
        private static Queue<ISetting> allSettings = null;

        #region Booleans

        public static BoolSetting AntiAbusiveModsSetting = new BoolSetting("GGM_AntiAbusiveMods");
        public static BoolSetting AntiGuestsSetting = new BoolSetting("GGM_AntiGuests");
        public static BoolSetting AntiTitanErenSetting = new BoolSetting("GGM_AntiTitanEren");
        public static BoolSetting ArmorModeSetting = new BoolSetting("GGM_ArmorMode");
        public static BoolSetting AutoReviveSetting = new BoolSetting("GGM_AutoRevive");
        public static BoolSetting BladeTrailsInfiniteLifetimeSetting = new BoolSetting("GGM_BladeTrailsInfiniteLifetime");
        public static BoolSetting BladeTrailsSetting = new BoolSetting("GGM_BladeTrails", true);
        public static BoolSetting BlurSetting = new BoolSetting("GGM_Blur");
        public static BoolSetting BodyLean = new BoolSetting("GGM_BodyLean", true);
        public static BoolSetting BombsModeSetting = new BoolSetting("GGM_BombsMode");
        public static BoolSetting CameraStaticFOVSetting = new BoolSetting("GGM_StaticFOV");
        public static BoolSetting CameraTiltSetting = new BoolSetting("GGM_CameraTilt");
        public static BoolSetting[] CameraTypeSettings = { new BoolSetting("GGM_OriginalCamera", true), new BoolSetting("GGM_TPSCamera", true), new BoolSetting("GGM_WOWCamera"), new BoolSetting("GGM_OldTPS"), };
        public static BoolSetting ChatBackground = new BoolSetting("GGM_ChatBackground");
        public static BoolSetting ChatFeedSeparateSetting = new BoolSetting("GGM_ChatFeedSeparate");
        public static BoolSetting ChatFeedSetting = new BoolSetting("GGM_ChatFeed");
        public static BoolSetting[] ChatMajorFormatSettings = { new BoolSetting("GGM_ChatMajorBold"), new BoolSetting("GGM_ChatMinorItalic") };
        public static BoolSetting[] ChatMinorFormatSettings = { new BoolSetting("GGM_ChatMinorBold"), new BoolSetting("GGM_ChatMinorItalic") };
        public static BoolSetting ChatUISetting = new BoolSetting("GGM_ChatUI", true);
        public static BoolSetting CrosshairUISetting = new BoolSetting("GGM_CrosshairUI", true);
        public static BoolSetting CustomAmbientSetting = new BoolSetting("GGM_CustomAmbient");
        public static BoolSetting CustomFogSetting = new BoolSetting("GGM_CustomFog");
        public static BoolSetting CustomGasSetting = new BoolSetting("GGM_CustomGas");
        public static BoolSetting CustomLightSetting = new BoolSetting("GGM_CustomLight");
        public static BoolSetting CustomSizeSetting = new BoolSetting("GGM_CustomSize");
        public static BoolSetting CustomSpawnRateSetting = new BoolSetting("GGM_CustomSpawnRate");
        public static BoolSetting CustomStarterTitansSetting = new BoolSetting("GGM_CustomStarterTitans");
        public static BoolSetting CustomTitansPerWaveSetting = new BoolSetting("GGM_CustomTitansPerWave");
        public static BoolSetting CustomWavesSetting = new BoolSetting("GGM_CustomWaves");
        public static BoolSetting DamageFeedUISetting = new BoolSetting("GGM_GameFeedUI", true);
        public static BoolSetting DashSetting = new BoolSetting("GGM_Dash");
        public static BoolSetting DeadlyCannonsModeSetting = new BoolSetting("GGM_DeadlyCannonsMode");
        public static BoolSetting DisableAHSSAirReloadingSetting = new BoolSetting("GGM_DisableAHSSAirReloading");
        public static BoolSetting DisableMinimapsSetting = new BoolSetting("GGM_DisableMinimaps");
        public static BoolSetting DisableRockThrowingSetting = new BoolSetting("GGM_DisableRockThrowing");
        public static BoolSetting ExplodeModeSetting = new BoolSetting("GGM_ExplodeMode");
        public static BoolSetting FriendlyModeSetting = new BoolSetting("GGM_FriendlyMode");
        public static BoolSetting GameInfoUISetting = new BoolSetting("GGM_GameInfoUI", true);
        public static BoolSetting HealthModeSetting = new BoolSetting("GGM_HealthMode");
        public static BoolSetting HorsesSetting = new BoolSetting("GGM_Horses");
        public static BoolSetting InfectionModeSetting = new BoolSetting("GGM_InfectionMode");
        public static BoolSetting InfiniteBladesSetting = new BoolSetting("GGM_InfiniteBlades");
        public static BoolSetting InfiniteBulletsNoReloadingSetting = new BoolSetting("GGM_InfiniteBulletsNoReloading");
        public static BoolSetting InfiniteBulletsSetting = new BoolSetting("GGM_InfiniteBullets");
        public static BoolSetting InfiniteGasSetting = new BoolSetting("GGM_InfiniteGas");
        public static BoolSetting LegacyChatSetting = new BoolSetting("GGM_LegacyChat");
        public static BoolSetting LegacyLabelsSetting = new BoolSetting("GGM_LegacyLabels");
        public static BoolSetting LocationSkinsRandomizedPairsSetting = new BoolSetting("GGM_LocationSkinsRandomizedPairs");
        public static BoolSetting MinimapSetting = new BoolSetting("GGM_Minimap");
        public static BoolSetting MipMappingSetting = new BoolSetting("GGM_MipMapping");
        public static BoolSetting MouseInvertYSetting = new BoolSetting("GGM_MouseInvertY");
        public static BoolSetting PlayerListUISetting = new BoolSetting("GGM_PlayerListUI", true);
        public static BoolSetting PointsModeSetting = new BoolSetting("GGM_PointsMode");
        public static BoolSetting PunkWavesSetting = new BoolSetting("GGM_PunkWaves");
        public static BoolSetting PVPModeSetting = new BoolSetting("GGM_PVPMode");
        public static BoolSetting[] ReelingSettings = { new BoolSetting("GGM_ReelIn", true), new BoolSetting("GGM_ReelOut", true) };
        public static BoolSetting SnapshotsSetting = new BoolSetting("GGM_Snapshots");
        public static BoolSetting SnapshotsShowInGameSetting = new BoolSetting("GGM_SnapshotsShowInGame");
        public static BoolSetting SpritesUISetting = new BoolSetting("GGM_SpritesUI", true);
        public static BoolSetting TeamModeSetting = new BoolSetting("GGM_TeamMode");
        public static BoolSetting UserInterfaceSetting = new BoolSetting("GGM_UserInterface");
        public static BoolSetting WindSetting = new BoolSetting("GGM_Wind");
        public static BoolSetting PrivateMessageSoundSetting = new BoolSetting("GGM_PrivateMessageSound", true);

        #endregion Booleans

        #region Floats

        public static FloatSetting[] BombColorSetting = { new FloatSetting("GGM_BombColorR", 1f), new FloatSetting("GGM_BombColorG", 1f), new FloatSetting("GGM_BombColorB", 1f), };
        public static FloatSetting CameraDistanceSetting = new FloatSetting("GGM_CameraDistance", 1f);
        public static FloatSetting CameraFOVSetting = new FloatSetting("GGM_CameraFOV", 110f);
        public static FloatSetting CannonCooldown = new FloatSetting("GGM_CannonCooldown", 3.5f);
        public static FloatSetting CannonMovementSpeedSetting = new FloatSetting("GGM_CannonSpeed", 40f);
        public static FloatSetting CannonRotateSpeedSetting = new FloatSetting("GGM_CannonRotate", 40f);
        public static FloatSetting ChatHeightSetting = new FloatSetting("GGM_ChatHeight", 470f);
        public static FloatSetting ChatOpacitySetting = new FloatSetting("GGM_ChatOpacity", 0.3f);
        public static FloatSetting ChatWidthSetting = new FloatSetting("GGM_ChatHWidth", 300f);
        public static FloatSetting[][] CustomAmbientColorSetting = { new[] { new FloatSetting("GGM_AmbientColorDayR", 0.494f), new FloatSetting("GGM_AmbientColorDayG", 0.478f), new FloatSetting("GGM_AmbientColorDayB", 0.447f) }, new[] { new FloatSetting("GGM_AmbientColorDawnR", 0.345f), new FloatSetting("GGM_AmbientColorDawnG", 0.305f), new FloatSetting("GGM_AmbientColorDawnB", 0.271f) }, new[] { new FloatSetting("GGM_AmbientColorNightR", 0.05f), new FloatSetting("GGM_AmbientColorNightG", 0.05f), new FloatSetting("GGM_AmbientColorNightB", 0.05f) } };
        public static FloatSetting[][] CustomLightColorSettings = { new[] { new FloatSetting("GGM_LightColorDayR", 1f), new FloatSetting("GGM_LightColorDayG", 1f), new FloatSetting("GGM_LightColorDayB", 1f) }, new[] { new FloatSetting("GGM_LightColorDawnR", 0.729f), new FloatSetting("GGM_LightColorDawnG", 0.643f), new FloatSetting("GGM_LightColorDawnB", 0.458f) }, new[] { new FloatSetting("GGM_LightColorNightR", 0.08f), new FloatSetting("GGM_LightColorNightG", 0.08f), new FloatSetting("GGM_LightColorNightB", 0.1f) } };
        public static FloatSetting DrawDistanceSetting = new FloatSetting("GGM_DrawDistance", 5000f);
        public static FloatSetting[] FogColorSettings = { new FloatSetting("GGM_FogColorR", 0.066f), new FloatSetting("GGM_FogColorG", 0.066f), new FloatSetting("GGM_FogColorB", 0.066f) };
        public static FloatSetting[] FogDistanceSettings = { new FloatSetting("GGM_FogStartDistance"), new FloatSetting("GGM_FogEndDistance", 1000f) };
        public static FloatSetting GlobalVolumeSetting = new FloatSetting("GGM_GlobalVolume", 1f);
        public static FloatSetting MouseSensitivitySetting = new FloatSetting("GGM_MouseSensitivity", 0.5f);
        public static FloatSetting NapeSlashVolumeSetting = new FloatSetting("GGM_NapeSlashVolume", 1f);
        public static FloatSetting OverallQualitySetting = new FloatSetting("GGM_OverallQuality", 1);
        public static FloatSetting ShadowDistanceSetting = new FloatSetting("GGM_ShadowDistance", 600f);
        public static FloatSetting[] SizeSettings = { new FloatSetting("GGM_MinimumSize", 2.5f), new FloatSetting("GGM_MaximumSize", 3f) };
        public static FloatSetting[] SpawnRateSettings = { new FloatSetting("GGM_NormalSpawnRate", 100f), new FloatSetting("GGM_AbnormalSpawnRate"), new FloatSetting("GGM_JumperSpawnRate"), new FloatSetting("GGM_CrawlerSpawnRate"), new FloatSetting("GGM_PunkSpawnRate") };

        #endregion Floats

        #region Integers

        public static IntSetting AnisotropicFilteringSetting = new IntSetting("GGM_AnisotropicFiltering", 1);
        public static IntSetting AntiAliasingSetting = new IntSetting("GGM_AntiAliasing", 1);
        public static IntSetting ArmorSetting = new IntSetting("GGM_Armor", 1000);
        public static IntSetting AutoReviveTimeSetting = new IntSetting("GGM_AutoReviveTime", 5);
        public static IntSetting BladeTrailsAppearanceSetting = new IntSetting("GGM_BladeTrailsAppearance", 1);
        public static IntSetting BladeTrailsFrameRateSetting = new IntSetting("GGM_BladeTrailsFrameRate", 120);
        public static IntSetting BlendWeightsSetting = new IntSetting("GGM_BlendWeights", 2);
        public static IntSetting[] BombSettings = { new IntSetting("GGM_BombRadius", 5), new IntSetting("GGM_BombRange", 5), new IntSetting("GGM_BombSpeed", 5), new IntSetting("GGM_BombCooldown", 5) };
        public static IntSetting CannonTypeSetting = new IntSetting("GGM_CannonType");
        public static IntSetting ChatSizeSetting = new IntSetting("GGM_ChatSize", 13);
        public static IntSetting ConnectionProtocolSettings = new IntSetting("GGM_ConnectionProtocol");
        public static IntSetting CustomMapSkinsCountSetting = new IntSetting("GGM_CustomMapSkinsCount", 3);
        public static IntSetting CustomMapSkinsCurrentSetSetting = new IntSetting("GGM_CustomMapCurrentSkin");
        public static IntSetting ExplodeRadiusSetting = new IntSetting("GGM_ExplodeRadius", 30);
        public static IntSetting FPSLockSetting = new IntSetting("GGM_FPSLock", 240);
        public static IntSetting[] HealthSettings = { new IntSetting("GGM_HealthType"), new IntSetting("GGM_MinimumTitansHealth", 100), new IntSetting("GGM_MaximumTitansHealth", 200) };
        public static IntSetting HumanSkinsCountSetting = new IntSetting("GGM_HumanSkinsCount", 5);
        public static IntSetting HumanSkinsCurrentSetSetting = new IntSetting("GGM_HumanCurrentSkin");
        public static IntSetting HumanSkinsSetting = new IntSetting("GGM_HumanSkins");
        public static IntSetting InfectedTitansSetting = new IntSetting("GGM_InfectedTitans", 1);
        public static IntSetting LocationSkinsCityCountSetting = new IntSetting("GGM_LocationSkinsCityCount", 1);
        public static IntSetting LocationSkinsCityCurrentSetSetting = new IntSetting("GGM_LocationSkinsCityCurrentSet");
        public static IntSetting LocationSkinsForestCountSetting = new IntSetting("GGM_LocationSkinsForestCount", 1);
        public static IntSetting LocationSkinsForestCurrentSetSetting = new IntSetting("GGM_LocationSkinsForestCurrentSet");
        public static IntSetting LocationSkinsSetting = new IntSetting("GGM_LocationSkins");
        public static IntSetting MaximumWavesSetting = new IntSetting("GGM_MaximumWaves", 20);
        public static IntSetting MessagesCache = new IntSetting("GGM_MessagesCache", 30);
        public static IntSetting PointsLimitSetting = new IntSetting("GGM_PointsLimit", 50);
        public static IntSetting PVPTypeSetting = new IntSetting("GGM_PVPType");
        public static IntSetting ShadowCascadesSetting = new IntSetting("GGM_ShadowCascades", 2);
        public static IntSetting ShadowProjectionSetting = new IntSetting("GGM_ShadowProjectionSetting");
        public static IntSetting SnapshotsMinimumDamageSetting = new IntSetting("GGM_SnapshotsMinimumDamage");
        public static IntSetting SpeedometerAHSSSetting = new IntSetting("GGM_SpeedometerAHSS", 0);
        public static IntSetting SpeedometerSetting = new IntSetting("GGM_Speedometer");
        public static IntSetting StarterAmountSetting = new IntSetting("GGM_StarterAmount", 3);
        public static IntSetting TeamSortSetting = new IntSetting("GGM_TeamSort");
        public static IntSetting TexturesSetting = new IntSetting("GGM_Textures", 2);
        public static IntSetting TitansPerWaveSetting = new IntSetting("GGM_TitansPerWave", 2);
        public static IntSetting TitansSpawnCapSetting = new IntSetting("GGM_TitansSpawnCap", 20);
        #endregion Integers

        #region Strings

        public static StringSetting ChatMajorColorSetting = new StringSetting("GGM_ChatMajorColor", "FDBCB4");
        public static StringSetting ChatMinorColorSetting = new StringSetting("GGM_ChatMinorColor", "F08080");
        public static StringSetting CustomMapSkinsTitlesSetting = new StringSetting("GGM_CustomMapkinsSetTitles", "Preset 1`Preset 2`Preset 3");
        public static StringSetting[] HumanRebindsSetting = { new StringSetting("GGM_HumanForward"), new StringSetting("GGM_HumanBackward"), new StringSetting("GGM_HumanLeft"), new StringSetting("GGM_HumanRight"), new StringSetting("GGM_HumanJump"), new StringSetting("GGM_HumanDodge"), new StringSetting("GGM_HumanLeftHook"), new StringSetting("GGM_HumanRightHook"), new StringSetting("GGM_HumanBothHooks"), new StringSetting("GGM_HumanLock"), new StringSetting("GGM_HumanAttack"), new StringSetting("GGM_HumanSpecial"), new StringSetting("GGM_HumanSalute"), new StringSetting("GGM_HumanChangeCamera"), new StringSetting("GGM_HumanRestartSuicide"), new StringSetting("GGM_HumanMenu"), new StringSetting("GGM_HumanShowHideCursor"), new StringSetting("GGM_HumanFullscreen"), new StringSetting("GGM_HumanReload"), new StringSetting("GGM_HumanFlareGreen"), new StringSetting("GGM_HumanFlareRed"), new StringSetting("GGM_HumanFlareBlack"), new StringSetting("GGM_HumanReelIn"), new StringSetting("GGM_HumanReelOut"), new StringSetting("GGM_HumanDash"), new StringSetting("GGM_HumanMinimapMaximize"), new StringSetting("GGM_HumanMinimapToggle"), new StringSetting("GGM_HumanMinimapReset"), new StringSetting("GGM_HumanChat"), new StringSetting("GGM_HumanLiveSpectate") };
        public static StringSetting HumanSkinsTitlesSetting = new StringSetting("GGM_HumanSkinsSetTitles", "Set 1`Set 2`Set 3`Set 4`Set 5");
        public static StringSetting LocationSkinsCitySetTitlesSetting = new StringSetting("GGM_LocationSkinsCitySetTitles", "Set 1");
        public static StringSetting LocationSkinsForestSetTitlesSetting = new StringSetting("GGM_LocationSkinsForestSetTitles", "Sakura Forest");
        public static StringSetting WelcomeMessageSetting = new StringSetting("GGM_WelcomeMessage");

        #endregion Strings

        private static bool updateHUD;

        static Settings()
        {
            Load();
        }

        #region Custom Map Skins

        public static void LoadCustomMapSkins()
        {
            CustomMapSkinsList = new List<string[]>();
            CustomMapSkinsTitlesList = new List<string>();
            CustomMapScriptsList = new List<string>();

            for (var i = 0; i < CustomMapSkinsCountSetting; i++)
            {
                CustomMapSkinsList.Add(PlayerPrefs.GetString("GGM_CustomMapSkin_" + i, "``````").Split('`'));
                CustomMapScriptsList.Add(PlayerPrefs.GetString("GGM_CustomMapScript_" + i, string.Empty));
            }

            foreach (var str in CustomMapSkinsTitlesSetting.Value.Split('`'))
            {
                CustomMapSkinsTitlesList.Add(str);
            }
        }

        public static void SaveCustomMapSkins()
        {
            for (var i = 0; i < CustomMapSkinsCountSetting; i++)
            {
                var str = string.Empty;
                for (var j = 0; j < 7; j++)
                {
                    str += CustomMapSkinsList[i][j] + (j != 6 ? "`" : string.Empty);
                }

                PlayerPrefs.SetString("GGM_CustomMapSkin_" + i, str);
                PlayerPrefs.SetString("GGM_CustomMapScript_" + i, CustomMapScriptsList[i]);
            }

            var str2 = string.Empty;
            for (var i = 0; i < CustomMapSkinsTitlesList.Count; i++)
            {
                str2 += CustomMapSkinsTitlesList[i] + (i != CustomMapSkinsTitlesList.Count - 1 ? "`" : string.Empty);
            }

            CustomMapSkinsTitlesSetting.Value = str2;
        }

        #endregion

        #region Human Skins

        public static void LoadHumanSkins()
        {
            HumanSkinsList = new List<string[]>();
            HumanSkinsTitlesList = new List<string>();

            for (var i = 0; i < HumanSkinsCountSetting; i++)
            {
                HumanSkinsList.Add(PlayerPrefs.GetString("GGM_HumanSkin_" + i, "````````````").Split('`'));
            }

            foreach (var str in HumanSkinsTitlesSetting.Value.Split('`'))
            {
                HumanSkinsTitlesList.Add(str);
            }
        }

        public static void SaveHumanSkins()
        {
            for (var i = 0; i < HumanSkinsCountSetting; i++)
            {
                var str = string.Empty;
                for (var j = 0; j < 13; j++)
                {
                    str += HumanSkinsList[i][j] + (j != 12 ? "`" : "");
                }

                PlayerPrefs.SetString("GGM_HumanSkin_" + i, str);
            }

            var str2 = string.Empty;
            for (var i = 0; i < HumanSkinsTitlesList.Count; i++)
            {
                str2 += HumanSkinsTitlesList[i] + (i != HumanSkinsTitlesList.Count - 1 ? "`" : "");
            }

            HumanSkinsTitlesSetting.Value = str2;
        }

        #endregion Human Skins

        #region Forest Skins

        public static void LoadForestSkins()
        {
            LocationSkinsForestList = new List<string[]>();
            LocationSkinsForestTitlesList = new List<string>();
            LocationSkinsForestAmbientList = new List<int>();
            LocationSkinsForestAmbientSettingsList = new List<float[]>();
            LocationSkinsForestFogList = new List<int>();
            LocationSkinsForestFogSettingsList = new List<float[]>();
            LocationSkinsForestLightList = new List<int>();
            LocationSkinsForestLightSettingsList = new List<float[]>();
            LocationSkinsForestParticlesList = new List<int>();
            LocationSkinsForestParticlesSettingsList = new List<float[]>();
            for (var i = 0; i < LocationSkinsForestCountSetting; i++)
            {
                //Skin Fields
                LocationSkinsForestList.Add(PlayerPrefs.GetString("GGM_ForestSkin_" + i, "`````````" + "https://i.imgur.com/tAzxZjG.png`" + "https://i.imgur.com/p4lwfdl.png`" + "https://i.imgur.com/rilg26V.png`" + "https://i.imgur.com/tAzxZjG.png`" + "https://i.imgur.com/p4lwfdl.png`" + "https://i.imgur.com/rilg26V.png`" + "https://i.imgur.com/tAzxZjG.png`" + "https://i.imgur.com/p4lwfdl.png`" + "https://i.imgur.com/fxbU9wh.jpg`" + "https://i.imgur.com/SASIAcM.jpg`" + "https://i.imgur.com/V5dey1B.jpg`" + "https://i.imgur.com/lRBZmja.jpg`" + "https://i.imgur.com/PhjVKO4.jpg`" + "https://i.imgur.com/i7mzHHN.jpg").Split('`'));
                //Ambient
                LocationSkinsForestAmbientList.Add(PlayerPrefs.GetInt("GGM_ForestAmbient_" + i, 1));
                LocationSkinsForestAmbientSettingsList.Add(new[] { PlayerPrefs.GetFloat("GGM_ForestAmbientColorR_" + i, 0.850f), PlayerPrefs.GetFloat("GGM_ForestAmbientColorG_" + i, 0.500f), PlayerPrefs.GetFloat("GGM_ForestAmbientColorB_" + i, 0.810f) });
                //Fog
                LocationSkinsForestFogList.Add(PlayerPrefs.GetInt("GGM_ForestFog_" + i, 1));
                LocationSkinsForestFogSettingsList.Add(new[] { PlayerPrefs.GetFloat("GGM_ForestFogColorR_" + i, 0.865f), PlayerPrefs.GetFloat("GGM_ForestFogColorG_" + i, 0.600f), PlayerPrefs.GetFloat("GGM_ForestFogColorB_" + i, 0.775f), PlayerPrefs.GetFloat("GGM_ForestFogStartDistance_" + i, 0f), PlayerPrefs.GetFloat("GGM_ForestFogEndDistance_" + i, 650f) });
                //Color
                LocationSkinsForestLightList.Add(PlayerPrefs.GetInt("GGM_ForestLight_" + i, 0));
                LocationSkinsForestLightSettingsList.Add(new[] { PlayerPrefs.GetFloat("GGM_ForestLightColorR_" + i, 1f), PlayerPrefs.GetFloat("GGM_ForestLightColorG_" + i, 1f), PlayerPrefs.GetFloat("GGM_ForestLightColorB_" + i, 1f) });
                //Particles
                LocationSkinsForestParticlesList.Add(PlayerPrefs.GetInt("GGM_ForestParticles_" + i, 0));
                LocationSkinsForestParticlesSettingsList.Add(new[] { PlayerPrefs.GetFloat("GGM_ForestParticlesCount_" + i, 1500f), PlayerPrefs.GetFloat("GGM_ForestParticlesHeight_" + i, 125f), PlayerPrefs.GetFloat("GGM_ForestParticlesLifeTimeMinimum_" + i, 60f), PlayerPrefs.GetFloat("GGM_ForestParticlesLifeTimeMaximum_" + i, 120f), PlayerPrefs.GetFloat("GGM_ForestParticlesGravity_" + i, 0.001f), PlayerPrefs.GetFloat("GGM_ForestParticlesColorR_" + i, 1f), PlayerPrefs.GetFloat("GGM_ForestParticlesColorG_" + i, 1f), PlayerPrefs.GetFloat("GGM_ForestParticlesColorB_" + i, 1f), PlayerPrefs.GetFloat("GGM_ForestParticlesColorA_" + i, 1f), });
            }

            foreach (var str in LocationSkinsForestSetTitlesSetting.Value.Split('`'))
            {
                LocationSkinsForestTitlesList.Add(str);
            }
        }

        public static void SaveForestSkins()
        {
            for (var i = 0; i < LocationSkinsForestCountSetting; i++)
            {
                var fields = string.Empty;
                for (var j = 0; j < 23; j++)
                {
                    fields += LocationSkinsForestList[i][j] + (j != 22 ? "`" : "");
                }

                PlayerPrefs.SetString("GGM_ForestSkin_" + i, fields);
                PlayerPrefs.SetInt("GGM_ForestAmbient_" + i, LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting]);
                PlayerPrefs.SetFloat("GGM_ForestAmbientColorR_" + i, LocationSkinsForestAmbientSettingsList[i][0]);
                PlayerPrefs.SetFloat("GGM_ForestAmbientColorG_" + i, LocationSkinsForestAmbientSettingsList[i][1]);
                PlayerPrefs.SetFloat("GGM_ForestAmbientColorB_" + i, LocationSkinsForestAmbientSettingsList[i][2]);
                PlayerPrefs.SetInt("GGM_ForestFog_" + i, LocationSkinsForestFogList[i]);
                PlayerPrefs.SetFloat("GGM_ForestFogColorR_" + i, LocationSkinsForestFogSettingsList[i][0]);
                PlayerPrefs.SetFloat("GGM_ForestFogColorG_" + i, LocationSkinsForestFogSettingsList[i][1]);
                PlayerPrefs.SetFloat("GGM_ForestFogColorB_" + i, LocationSkinsForestFogSettingsList[i][2]);
                PlayerPrefs.SetFloat("GGM_ForestFogStartDistance_", LocationSkinsForestFogSettingsList[i][3]);
                PlayerPrefs.SetFloat("GGM_ForestFogEndDistance_", LocationSkinsForestFogSettingsList[i][4]);
                PlayerPrefs.SetInt("GGM_ForestLight_" + i, LocationSkinsForestLightList[i]);
                PlayerPrefs.SetFloat("GGM_ForestLightColorR_" + i, LocationSkinsForestLightSettingsList[i][0]);
                PlayerPrefs.SetFloat("GGM_ForestLightColorG_" + i, LocationSkinsForestLightSettingsList[i][1]);
                PlayerPrefs.SetFloat("GGM_ForestLightColorB_" + i, LocationSkinsForestLightSettingsList[i][2]);
                PlayerPrefs.SetInt("GGM_ForestParticles_" + i, LocationSkinsForestParticlesList[i]);
                PlayerPrefs.SetFloat("GGM_ForestParticlesCount_" + i, LocationSkinsForestParticlesSettingsList[i][0]);
                PlayerPrefs.SetFloat("GGM_ForestParticlesHeight_" + i, LocationSkinsForestParticlesSettingsList[i][1]);
                PlayerPrefs.SetFloat("GGM_ForestParticlesLifeTimeMinimum_" + i, LocationSkinsForestParticlesSettingsList[i][2]);
                PlayerPrefs.SetFloat("GGM_ForestParticlesLifeTimeMaximum_" + i, LocationSkinsForestParticlesSettingsList[i][3]);
                PlayerPrefs.SetFloat("GGM_ForestParticlesGravity_" + i, LocationSkinsForestParticlesSettingsList[i][4]);
                PlayerPrefs.SetFloat("GGM_ForestParticlesColorR_" + i, LocationSkinsForestParticlesSettingsList[i][5]);
                PlayerPrefs.SetFloat("GGM_ForestParticlesColorG_" + i, LocationSkinsForestParticlesSettingsList[i][6]);
                PlayerPrefs.SetFloat("GGM_ForestParticlesColorB_" + i, LocationSkinsForestParticlesSettingsList[i][7]);
                PlayerPrefs.SetFloat("GGM_ForestParticlesColorA_" + i, LocationSkinsForestParticlesSettingsList[i][8]);
            }

            var titles = string.Empty;
            for (var i = 0; i < LocationSkinsForestTitlesList.Count; i++)
            {
                titles += LocationSkinsForestTitlesList[i] + (i != LocationSkinsForestTitlesList.Count - 1 ? "`" : "");
            }

            LocationSkinsForestSetTitlesSetting.Value = titles;
        }

        #endregion Forest Skins

        #region City Skins

        public static void LoadCitySkins()
        {
            LocationSkinsCityList = new List<string[]>();
            LocationSkinsCityTitlesList = new List<string>();
            LocationSkinsCityAmbientList = new List<int>();
            LocationSkinsCityAmbientSettingsList = new List<float[]>();
            LocationSkinsCityFogList = new List<int>();
            LocationSkinsCityFogSettingsList = new List<float[]>();
            LocationSkinsCityLightList = new List<int>();
            LocationSkinsCityLightSettingsList = new List<float[]>();
            LocationSkinsCityParticlesList = new List<int>();
            LocationSkinsCityParticlesSettingsList = new List<float[]>();

            for (var i = 0; i < LocationSkinsCityCountSetting; i++)
            {
                //Skin Fields
                LocationSkinsCityList.Add(PlayerPrefs.GetString("GGM_CitySkin_" + i, "````````````````").Split('`'));
                //Ambient
                LocationSkinsCityAmbientList.Add(PlayerPrefs.GetInt("GGM_CityAmbient_" + i, 0));
                LocationSkinsCityAmbientSettingsList.Add(new[] { PlayerPrefs.GetFloat("GGM_CityAmbientColorR_" + i, 0.5f), PlayerPrefs.GetFloat("GGM_CityAmbientColorG_" + i, 0.5f), PlayerPrefs.GetFloat("GGM_CityAmbientColorB_" + i, 0.5f) });
                //Fog
                LocationSkinsCityFogList.Add(PlayerPrefs.GetInt("GGM_CityFog_" + i, 0));
                LocationSkinsCityFogSettingsList.Add(new[] { PlayerPrefs.GetFloat("GGM_CityFogColorR_" + i, 0.066f), PlayerPrefs.GetFloat("GGM_CityFogColorG_" + i, 0.066f), PlayerPrefs.GetFloat("GGM_CityFogColorB_" + i, 0.066f), PlayerPrefs.GetFloat("GGM_CityFogStartDistance" + i, 0f), PlayerPrefs.GetFloat("GGM_CityFogEndDistance" + i, 1000f) });
                //Color
                LocationSkinsCityLightList.Add(PlayerPrefs.GetInt("GGM_CityLight_" + i, 0));
                LocationSkinsCityLightSettingsList.Add(new[] { PlayerPrefs.GetFloat("GGM_CityLightColorR_" + i, 1f), PlayerPrefs.GetFloat("GGM_CityLightColorG_" + i, 1f), PlayerPrefs.GetFloat("GGM_CityLightColorB_" + i, 1f) });
                //Particles
                LocationSkinsCityParticlesList.Add(PlayerPrefs.GetInt("GGM_CityParticles_" + i, 0));
                LocationSkinsCityParticlesSettingsList.Add(new[] { PlayerPrefs.GetFloat("GGM_CityParticlesCount_" + i, 1500f), PlayerPrefs.GetFloat("GGM_CityParticlesHeight_" + i, 125f), PlayerPrefs.GetFloat("GGM_CityParticlesLifeTimeMinimum_" + i, 60f), PlayerPrefs.GetFloat("GGM_CityParticlesLifeTimeMaximum_" + i, 120f), PlayerPrefs.GetFloat("GGM_CityParticlesGravity_" + i, 0.001f), PlayerPrefs.GetFloat("GGM_CityParticlesColorR_" + i, 1f), PlayerPrefs.GetFloat("GGM_CityParticlesColorG_" + i, 1f), PlayerPrefs.GetFloat("GGM_CityParticlesColorB_" + i, 1f), PlayerPrefs.GetFloat("GGM_CityParticlesColorA_" + i, 1f), });
            }

            foreach (var str in LocationSkinsCitySetTitlesSetting.Value.Split('`'))
            {
                LocationSkinsCityTitlesList.Add(str);
            }
        }

        public static void SaveCitySkins()
        {
            for (var i = 0; i < LocationSkinsCityCountSetting; i++)
            {
                var fields = string.Empty;
                for (var j = 0; j < 17; j++)
                {
                    fields += LocationSkinsCityList[i][j] + (j != 16 ? "`" : "");
                }

                PlayerPrefs.SetString("GGM_CitySkin_" + i, fields);
                PlayerPrefs.SetInt("GGM_CityAmbient_" + i, LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting]);
                PlayerPrefs.SetFloat("GGM_CityAmbientColorR_" + i, LocationSkinsCityAmbientSettingsList[i][0]);
                PlayerPrefs.SetFloat("GGM_CityAmbientColorG_" + i, LocationSkinsCityAmbientSettingsList[i][1]);
                PlayerPrefs.SetFloat("GGM_CityAmbientColorB_" + i, LocationSkinsCityAmbientSettingsList[i][2]);
                PlayerPrefs.SetInt("GGM_CityFog_" + i, LocationSkinsCityFogList[i]);
                PlayerPrefs.SetFloat("GGM_CityFogColorR_" + i, LocationSkinsCityFogSettingsList[i][0]);
                PlayerPrefs.SetFloat("GGM_CityFogColorG_" + i, LocationSkinsCityFogSettingsList[i][1]);
                PlayerPrefs.SetFloat("GGM_CityFogColorB_" + i, LocationSkinsCityFogSettingsList[i][2]);
                PlayerPrefs.SetFloat("GGM_CityFogStartDistance_", LocationSkinsCityFogSettingsList[i][3]);
                PlayerPrefs.SetFloat("GGM_CityFogEndDistance_", LocationSkinsCityFogSettingsList[i][4]);
                PlayerPrefs.SetInt("GGM_CityLight_" + i, LocationSkinsCityLightList[i]);
                PlayerPrefs.SetFloat("GGM_CityLightColorR_" + i, LocationSkinsCityLightSettingsList[i][0]);
                PlayerPrefs.SetFloat("GGM_CityLightColorG_" + i, LocationSkinsCityLightSettingsList[i][1]);
                PlayerPrefs.SetFloat("GGM_CityLightColorB_" + i, LocationSkinsCityLightSettingsList[i][2]);
                PlayerPrefs.SetInt("GGM_CityParticles_" + i, LocationSkinsCityParticlesList[i]);
                PlayerPrefs.SetFloat("GGM_CityParticlesCount_" + i, LocationSkinsCityParticlesSettingsList[i][0]);
                PlayerPrefs.SetFloat("GGM_CityParticlesHeight_" + i, LocationSkinsCityParticlesSettingsList[i][1]);
                PlayerPrefs.SetFloat("GGM_CityParticlesLifeTimeMinimum_" + i, LocationSkinsCityParticlesSettingsList[i][2]);
                PlayerPrefs.SetFloat("GGM_CityParticlesLifeTimeMaximum_" + i, LocationSkinsCityParticlesSettingsList[i][3]);
                PlayerPrefs.SetFloat("GGM_CityParticlesGravity_" + i, LocationSkinsCityParticlesSettingsList[i][4]);
                PlayerPrefs.SetFloat("GGM_CityParticlesColorR_" + i, LocationSkinsCityParticlesSettingsList[i][5]);
                PlayerPrefs.SetFloat("GGM_CityParticlesColorG_" + i, LocationSkinsCityParticlesSettingsList[i][6]);
                PlayerPrefs.SetFloat("GGM_CityParticlesColorB_" + i, LocationSkinsCityParticlesSettingsList[i][7]);
                PlayerPrefs.SetFloat("GGM_CityParticlesColorA_" + i, LocationSkinsCityParticlesSettingsList[i][8]);
            }

            var titles = string.Empty;
            for (var i = 0; i < LocationSkinsCityTitlesList.Count; i++)
            {
                titles += LocationSkinsCityTitlesList[i] + (i != LocationSkinsCityTitlesList.Count - 1 ? "`" : "");
            }

            LocationSkinsCitySetTitlesSetting.Value = titles;
        }

        #endregion City Skins

        public static void AddSetting(ISetting set)
        {
            if (allSettings == null)
                allSettings = new Queue<ISetting>();
            allSettings.Enqueue(set);
        }

        public static void Load()
        {
            if (Storage == null)
                CreateStorage();
            foreach (var set in allSettings)
            {
                set.Load();
            }

            LoadCustomMapSkins();
            LoadHumanSkins();
            LoadForestSkins();
            LoadCitySkins();
        }

        public static void Save()
        {
            foreach (var set in allSettings)
            {
                set.Save();
            }

            SaveCustomMapSkins();
            SaveHumanSkins();
            SaveForestSkins();
            SaveCitySkins();
        }

        public static void Update()
        {
            if (Application.loadedLevel == 0 || Application.loadedLevelName == "characterCreation" || Application.loadedLevelName == "SnapShot" || FengGameManagerMKII.inputManager.menuOn)
            {
                Application.targetFrameRate = 60;
                return;
            }

            if (UserInterfaceSetting)
            {
                Extensions.DisableObject("UIflare1");
                Extensions.DisableObject("UIflare2");
                Extensions.DisableObject("UIflare3");
                Extensions.DisableObject("flareg1");
                Extensions.DisableObject("UIflare1");
                Extensions.DisableObject("flareg2");
                Extensions.DisableObject("flareg3");
                Extensions.DisableObject("bar");
                Extensions.DisableObject("locker");
                Extensions.DisableObject("stamina_titan");
                Extensions.DisableObject("stamina_titan_bottom");
                Extensions.DisableObject("flash");
                Extensions.DisableObject("skill_cd_bottom");
                Extensions.DisableObject("skill_cd_armin");
                Extensions.DisableObject("skill_cd_mikasa");
                Extensions.DisableObject("skill_cd_sasha");
                Extensions.DisableObject("skill_cd_petra");
                Extensions.DisableObject("skill_cd_levi");
                Extensions.DisableObject("skill_cd_jean");
                Extensions.DisableObject("skill_cd_marco");
                Extensions.DisableObject("skill_cd_eren");
                Extensions.DisableObject("GasUI");
                updateHUD = true;
            }
            else
            {
                if (updateHUD)
                {
                    updateHUD = false;
                    Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setHUDposition();
                }
            }

            IN_GAME_MAIN_CAMERA.sensitivityMulti = MouseSensitivitySetting;
            IN_GAME_MAIN_CAMERA.cameraDistance = CameraDistanceSetting + 0.3f;
            Caching.GameObjectCache.Find("MainCamera").GetComponent<TiltShift>().enabled = BlurSetting;
            QualitySettings.SetQualityLevel(Convert.ToInt32(OverallQualitySetting), true);
            Application.targetFrameRate = FPSLockSetting > 0 && FPSLockSetting < 60 || FPSLockSetting < 0 ? 60 : FPSLockSetting == 0 ? -1 : FPSLockSetting;
            QualitySettings.masterTextureLimit = TexturesSetting == 0 ? 2 : TexturesSetting == 2 ? 0 : 1;
            QualitySettings.anisotropicFiltering = AnisotropicFilteringSetting == 0 ? AnisotropicFiltering.Disable : AnisotropicFilteringSetting == 1 ? AnisotropicFiltering.Enable : AnisotropicFiltering.ForceEnable;
            QualitySettings.antiAliasing = AntiAliasingSetting == 0 ? 0 : AntiAliasingSetting == 1 ? 2 : AntiAliasingSetting == 2 ? 4 : 8;
            QualitySettings.blendWeights = BlendWeightsSetting == 0 ? BlendWeights.OneBone : BlendWeightsSetting == 1 ? BlendWeights.TwoBones : BlendWeights.FourBones;
            Camera.main.farClipPlane = DrawDistanceSetting;
            QualitySettings.shadowDistance = ShadowDistanceSetting;
            QualitySettings.shadowProjection = ShadowProjectionSetting == 0 ? ShadowProjection.StableFit : ShadowProjection.CloseFit;
            QualitySettings.shadowCascades = ShadowCascadesSetting == 0 ? 0 : ShadowCascadesSetting == 1 ? 2 : 4;
            AudioListener.volume = GlobalVolumeSetting;

            if (LocationSkinsSetting == 0 || !Application.loadedLevelName.Contains("Forest") && !Application.loadedLevelName.Contains("City"))
            {
                switch (IN_GAME_MAIN_CAMERA.dayLight)
                {
                    case DayLight.Day:
                        RenderSettings.ambientLight = CustomAmbientSetting ? new Color(CustomAmbientColorSetting[0][0], CustomAmbientColorSetting[0][1], CustomAmbientColorSetting[0][2]) : FengColor.dayAmbientLight;
                        Caching.GameObjectCache.Find("mainLight").GetComponent<Light>().color = CustomLightSetting ? new Color(CustomLightColorSettings[0][0], CustomLightColorSettings[0][1], CustomLightColorSettings[0][2]) : FengColor.dayLight;
                        break;

                    case DayLight.Dawn:
                        RenderSettings.ambientLight = CustomAmbientSetting ? new Color(CustomAmbientColorSetting[1][0], CustomAmbientColorSetting[1][1], CustomAmbientColorSetting[1][2]) : FengColor.dawnAmbientLight;
                        Caching.GameObjectCache.Find("mainLight").GetComponent<Light>().color = CustomLightSetting ? new Color(CustomLightColorSettings[1][0], CustomLightColorSettings[1][1], CustomLightColorSettings[1][2]) : FengColor.dawnLight;
                        break;

                    case DayLight.Night:
                        RenderSettings.ambientLight = CustomAmbientSetting ? new Color(CustomAmbientColorSetting[2][0], CustomAmbientColorSetting[2][1], CustomAmbientColorSetting[2][2]) : FengColor.nightAmbientLight;
                        Caching.GameObjectCache.Find("mainLight").GetComponent<Light>().color = CustomLightSetting ? new Color(CustomLightColorSettings[2][0], CustomLightColorSettings[2][1], CustomLightColorSettings[2][2]) : FengColor.nightLight;
                        break;
                }

                RenderSettings.fog = CustomFogSetting || !CustomFogSetting && Application.loadedLevelName.Contains("Forest");
                RenderSettings.fogColor = CustomFogSetting ? new Color(FogColorSettings[0], FogColorSettings[1], FogColorSettings[2]) : new Color(0.066f, 0.066f, 0.066f);
                RenderSettings.fogStartDistance = CustomFogSetting ? FogDistanceSettings[0] : 0f;
                RenderSettings.fogEndDistance = CustomFogSetting ? FogDistanceSettings[1] : 1000f;
            }
            else if (LocationSkinsSetting == 1 || LocationSkinsSetting == 2 && (PhotonNetwork.isMasterClient || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE))
            {
                if (Application.loadedLevelName.Contains("Forest"))
                {
                    RenderSettings.ambientLight = LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting] == 1 ? new Color(LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting][0], LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting][1], LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting][2]) : CustomAmbientSetting ? new Color(CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][0], CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][1], CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][2]) : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? FengColor.dayAmbientLight : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? FengColor.dawnAmbientLight : FengColor.nightAmbientLight;
                    Caching.GameObjectCache.Find("mainLight").GetComponent<Light>().color = LocationSkinsForestLightList[LocationSkinsForestCurrentSetSetting] == 1 ? new Color(LocationSkinsForestLightSettingsList[LocationSkinsForestCurrentSetSetting][0], LocationSkinsForestLightSettingsList[LocationSkinsForestCurrentSetSetting][1], LocationSkinsForestLightSettingsList[LocationSkinsForestCurrentSetSetting][2]) : CustomLightSetting ? new Color(CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][0], CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][1], CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][2]) : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? FengColor.dayLight : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? FengColor.dawnLight : FengColor.nightLight;
                    RenderSettings.fog = LocationSkinsForestFogList[LocationSkinsForestCurrentSetSetting] == 1;
                    RenderSettings.fogColor = new Color(LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][0], LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][1], LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][2]);
                    RenderSettings.fogStartDistance = LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3];
                    RenderSettings.fogEndDistance = LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][4];
                }
                else if (Application.loadedLevelName.Contains("City"))
                {
                    RenderSettings.ambientLight = LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting] == 1 ? new Color(LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting][0], LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting][1], LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting][2]) : CustomAmbientSetting ? new Color(CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][0], CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][1], CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][2]) : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? FengColor.dayAmbientLight : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? FengColor.dawnAmbientLight : FengColor.nightAmbientLight;
                    Caching.GameObjectCache.Find("mainLight").GetComponent<Light>().color = LocationSkinsCityLightList[LocationSkinsCityCurrentSetSetting] == 1 ? new Color(LocationSkinsCityLightSettingsList[LocationSkinsCityCurrentSetSetting][0], LocationSkinsCityLightSettingsList[LocationSkinsCityCurrentSetSetting][1], LocationSkinsCityLightSettingsList[LocationSkinsCityCurrentSetSetting][2]) : CustomLightSetting ? new Color(CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][0], CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][1], CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][2]) : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? FengColor.dayLight : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? FengColor.dawnLight : FengColor.nightLight;
                    RenderSettings.fog = LocationSkinsCityFogList[LocationSkinsCityCurrentSetSetting] == 1;
                    RenderSettings.fogColor = new Color(LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][0], LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][1], LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][2]);
                    RenderSettings.fogStartDistance = LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3];
                    RenderSettings.fogEndDistance = LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][4];
                }
            }
            else if (LocationSkinsSetting == 2 && !PhotonNetwork.isMasterClient && ReceivedLocationSkinsData != null && PhotonNetwork.masterClient.GucciGangMod)
            {
                if (Application.loadedLevelName.Contains("Forest"))
                {
                    RenderSettings.ambientLight = (int)ReceivedLocationSkinsData[0] == 1 ? new Color((float)ReceivedLocationSkinsData[1], (float)ReceivedLocationSkinsData[2], (float)ReceivedLocationSkinsData[3]) : CustomAmbientSetting ? new Color(CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][0], CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][1], CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][2]) : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? FengColor.dayAmbientLight : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? FengColor.dawnAmbientLight : FengColor.nightAmbientLight;
                    Caching.GameObjectCache.Find("mainLight").GetComponent<Light>().color = (int)ReceivedLocationSkinsData[4] == 1 ? new Color((float)ReceivedLocationSkinsData[5], (float)ReceivedLocationSkinsData[6], (float)ReceivedLocationSkinsData[7]) : CustomLightSetting ? new Color(CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][0], CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][1], CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][2]) : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? FengColor.dayLight : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? FengColor.dawnLight : FengColor.nightLight;
                    RenderSettings.fog = (int)ReceivedLocationSkinsData[8] == 1;
                    RenderSettings.fogColor = new Color((float)ReceivedLocationSkinsData[9], (float)ReceivedLocationSkinsData[10], (float)ReceivedLocationSkinsData[11]);
                    RenderSettings.fogStartDistance = (float)ReceivedLocationSkinsData[12];
                    RenderSettings.fogEndDistance = (float)ReceivedLocationSkinsData[12];
                }
                else if (Application.loadedLevelName.Contains("City"))
                {
                    RenderSettings.ambientLight = (int)ReceivedLocationSkinsData[1] == 1 ? new Color((float)ReceivedLocationSkinsData[1], (float)ReceivedLocationSkinsData[2], (float)ReceivedLocationSkinsData[3]) : CustomAmbientSetting ? new Color(CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][0], CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][1], CustomAmbientColorSetting[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][2]) : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? FengColor.dayAmbientLight : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? FengColor.dawnAmbientLight : FengColor.nightAmbientLight;
                    Caching.GameObjectCache.Find("mainLight").GetComponent<Light>().color = (int)ReceivedLocationSkinsData[4] == 1 ? new Color((float)ReceivedLocationSkinsData[5], (float)ReceivedLocationSkinsData[6], (float)ReceivedLocationSkinsData[7]) : CustomLightSetting ? new Color(CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][0], CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][1], CustomLightColorSettings[IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? 0 : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? 1 : 2][2]) : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? FengColor.dayLight : IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? FengColor.dawnLight : FengColor.nightLight;
                    RenderSettings.fog = (float)ReceivedLocationSkinsData[8] == 1;
                    RenderSettings.fogColor = new Color((float)ReceivedLocationSkinsData[9], (float)ReceivedLocationSkinsData[10], (float)ReceivedLocationSkinsData[11]);
                    RenderSettings.fogStartDistance = (float)ReceivedLocationSkinsData[12];
                    RenderSettings.fogEndDistance = (float)ReceivedLocationSkinsData[12];
                }
            }
        }

        private static void CreateStorage()
        {
            var choice = PlayerPrefs.GetInt("StorageType", 0);
            switch (choice)
            {
                case 0:
                    Storage = new PrefStorage();
                    break;
            }
        }
    }
}