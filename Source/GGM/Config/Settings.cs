using System;
using GGM.Storage;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.Config
{
    public static class Settings
    {
        public static IDataStorage Storage = null;
        private static Queue<ISetting> allSettings = null;

        public static List<string[]> HumanSkinsList;
        public static List<string> HumanSkinsTitlesList;
        public static string[] CopiedHumanSkins;

        public static List<string[]> LocationSkinsForestList;
        public static List<string> LocationSkinsForestTitlesList;
        public static List<int> LocationSkinsForestAmbientList;
        public static List<float[]> LocationSkinsForestAmbientSettingsList;
        public static List<int> LocationSkinsForestFogList;
        public static List<float[]> LocationSkinsForestFogSettingsList;
        public static List<int> LocationSkinsForestParticlesList;
        public static List<float[]> LocationSkinsForestParticlesSettingsList;
        public static string[] LocationSkinsForestCopiedSet;

        public static List<string[]> LocationSkinsCityList;
        public static List<string> LocationSkinsCityTitlesList;
        public static List<int> LocationSkinsCityAmbientList;
        public static List<float[]> LocationSkinsCityAmbientSettingsList;
        public static List<int> LocationSkinsCityFogList;
        public static List<float[]> LocationSkinsCityFogSettingsList;
        public static List<int> LocationSkinsCityParticlesList;
        public static List<float[]> LocationSkinsCityParticlesSettingsList;
        public static string[] LocationSkinsCityCopiedSet;

        #region Booleans
        //Game
        //Mouse
        public static BoolSetting MouseInvertYSetting = new BoolSetting("MouseInvertY");
        //Camera
        public static BoolSetting CameraTiltSetting = new BoolSetting("CameraTilt");
        public static BoolSetting CameraStaticFOVSetting = new BoolSetting("StaticFOV");

        public static BoolSetting[] CameraTypeSettings =
        {
            new BoolSetting("OriginalCamera", true), 
            new BoolSetting("TPSCamera", true), 
            new BoolSetting("WOWCamera"),
            new BoolSetting("OldTPS"), 
        };
        //Snapshots
        public static BoolSetting SnapshotsSetting = new BoolSetting("Snapshots");
        public static BoolSetting SnapshotsShowInGameSetting = new BoolSetting("SnapshotsShowInGame");
        //Resources
        public static BoolSetting InfiniteBladesSetting = new BoolSetting("InfiniteBlades");
        public static BoolSetting InfiniteBulletsSetting = new BoolSetting("InfiniteBullets");
        public static BoolSetting InfiniteGasSetting = new BoolSetting("InfiniteGas");
        //User Interface
        public static BoolSetting UserInterfaceSetting = new BoolSetting("UserInterface");
        public static BoolSetting PlayerListUISetting = new BoolSetting("PlayerListUI", true);
        public static BoolSetting GameInfoUISetting = new BoolSetting("GameInfoUI", true);
        public static BoolSetting ChatUISetting = new BoolSetting("ChatUI", true);
        public static BoolSetting SpritesUISetting = new BoolSetting("SpritesUI", true);
        public static BoolSetting DamageFeedUISetting = new BoolSetting("GameFeedUI", true);
        public static BoolSetting CrosshairUISetting = new BoolSetting("CrosshairUI", true);
        //Misc
        public static BoolSetting ChatFeedSetting = new BoolSetting("ChatFeed");
        public static BoolSetting MinimapSetting = new BoolSetting("Minimap");
        //Server
        public static BoolSetting CustomStarterTitansSetting = new BoolSetting("CustomStarterTitans");
        public static BoolSetting CustomTitansPerWaveSetting = new BoolSetting("CustomTitansPerWave");
        public static BoolSetting CustomSpawnRateSetting = new BoolSetting("CustomSpawnRate");
        public static BoolSetting PunkWavesSetting = new BoolSetting("PunkWaves");
        public static BoolSetting CustomSizeSetting = new BoolSetting("CustomSize");
        public static BoolSetting CustomWavesSetting = new BoolSetting("CustomWaves");
        public static BoolSetting HealthModeSetting = new BoolSetting("HealthMode");
        public static BoolSetting ArmorModeSetting = new BoolSetting("ArmorMode");
        public static BoolSetting ExplodeModeSetting = new BoolSetting("ExplodeMode");
        public static BoolSetting DisableRockThrowingSetting = new BoolSetting("DisableRockThrowing");
        public static BoolSetting PVPModeSetting = new BoolSetting("PVPMode");
        public static BoolSetting PointsModeSetting = new BoolSetting("PointsMode");
        public static BoolSetting TeamModeSetting = new BoolSetting("TeamMode");
        public static BoolSetting BombsModeSetting = new BoolSetting("BombsMode");
        public static BoolSetting InfectionModeSetting = new BoolSetting("InfectionMode");
        public static BoolSetting FriendlyModeSetting = new BoolSetting("FriendlyMode");
        public static BoolSetting AutoReviveSetting = new BoolSetting("AutoRevive");
        public static BoolSetting HorsesSetting = new BoolSetting("Horses");
        public static BoolSetting DisableMinimapsSetting = new BoolSetting("DisableMinimaps");
        public static BoolSetting DisableAHSSAirReloadingSetting = new BoolSetting("DisableAHSSAirReloading");
        public static BoolSetting DeadlyCannonsModeSetting = new BoolSetting("DeadlyCannonsMode");
        public static BoolSetting[] ChatMajorFormatSettings =
        {
            new BoolSetting("ChatMajorBold"),
            new BoolSetting("ChatMinorItalic")
        };
        public static BoolSetting[] ChatMinorFormatSettings =
        {
            new BoolSetting("ChatMinorBold"),
            new BoolSetting("ChatMinorItalic")
        };
        public static BoolSetting AntiTitanErenSetting = new BoolSetting("AntiTitanEren");
        //Video
        public static BoolSetting MipMappingSetting = new BoolSetting("MipMapping");
        public static BoolSetting WindSetting = new BoolSetting("Wind");
        public static BoolSetting BlurSetting = new BoolSetting("Blur");
        public static BoolSetting AmbientSetting = new BoolSetting("Ambient");
        public static BoolSetting FogSetting = new BoolSetting("Fog", true);
        //Rebinds
        public static BoolSetting[] ReelingSettings =
        {
            new BoolSetting("ReelIn"),
            new BoolSetting("ReelOut"),
        };
        public static BoolSetting DashSetting = new BoolSetting("Dash");
        //HumanSkins
        public static BoolSetting BladeTrailsSetting = new BoolSetting("BladeTrails", true);
        public static BoolSetting CustomGasSetting = new BoolSetting("CustomGas");
        public static BoolSetting BladeTrailsInfiniteLifetimeSetting = new BoolSetting("BladeTrailsInfiniteLifetime");
        //LocationSkins
        public static BoolSetting LocationSkinsRandomizedPairsSetting = new BoolSetting("LocationSkinsRandomizedPairs");
        #endregion

        #region Floats
        //Game
        //Mouse
        public static FloatSetting MouseSensitivitySetting = new FloatSetting("MouseSensitivity", 50f);
        //Camera
        public static FloatSetting CameraDistanceSetting = new FloatSetting("CameraDistance", 100f);
        public static FloatSetting CameraFOVSetting = new FloatSetting("CameraFOV", 110f);
        //Server
        public static FloatSetting[] SpawnRateSettings =
        {
            new FloatSetting("NormalSpawnRate", 100f),
            new FloatSetting("AbnormalSpawnRate"),
            new FloatSetting("JumperSpawnRate"),
            new FloatSetting("CrawlerSpawnRate"),
            new FloatSetting("PunkSpawnRate")
        };
        public static FloatSetting[] SizeSettings =
        {
            new FloatSetting("MinimumSize", 2.5f),
            new FloatSetting("MaximumSize", 3f)
        };
        //Video
        public static FloatSetting OverallQualitySetting = new FloatSetting("OverallQuality", 1);
        public static FloatSetting DrawDistanceSetting = new FloatSetting("DrawDistance", 1000f);
        public static FloatSetting ShadowDistanceSetting = new FloatSetting("ShadowDistance", 600f);
        public static FloatSetting[][] AmbientColorSetting = 
        {
            new[]
            {
                new FloatSetting("AmbientColorDayR", 0.494f),
                new FloatSetting("AmbientColorDayG", 0.478f),
                new FloatSetting("AmbientColorDayB", 0.447f)
            },
            new []
            {
                new FloatSetting("AmbientColorDawnR", 0.345f),
                new FloatSetting("AmbientColorDawnG", 0.305f),
                new FloatSetting("AmbientColorDawnB", 0.271f)
            },
            new []
            {
                new FloatSetting("AmbientColorNightR", 0.05f),
                new FloatSetting("AmbientColorNightG", 0.05f),
                new FloatSetting("AmbientColorNightB", 0.05f)
            }
        };
        public static FloatSetting[] FogColorSettings =
        {
            new FloatSetting("FogColorR", 0.066f),
            new FloatSetting("FogColorG", 0.066f),
            new FloatSetting("FogColorB", 0.066f)
        };
        public static FloatSetting[] FogDistanceSettings =
        {
            new FloatSetting("FogStartDistance"),
            new FloatSetting("FogEndDistance", 1000f)
        };
        //Audio
        public static FloatSetting GlobalVolumeSetting = new FloatSetting("GlobalVolume", 1f);
        public static FloatSetting AHSSShotVolumeSetting = new FloatSetting("AHSSShotVolume", 1f);
        public static FloatSetting AirSlashVolumeSetting = new FloatSetting("AirSlashVolume", 1f);
        public static FloatSetting NapeSlashVolumeSetting = new FloatSetting("NapeSlashVolume", 1f);
        public static FloatSetting BodySlashVolumeSetting = new FloatSetting("BodySlashVolume", 1f);
        public static FloatSetting HookVolumeSetting = new FloatSetting("HookVolume", 1f);
        public static FloatSetting TitanErenRoarVolumeSetting = new FloatSetting("TitanErenRoarVolume", 1f);
        public static FloatSetting SwingVolumeSetting = new FloatSetting("SwingVolume", 1f);
        public static FloatSetting ThunderVolumeSetting = new FloatSetting("ThunderVolume", 1f);
        public static FloatSetting HeadExplosionVolumeSetting = new FloatSetting("HeadExplosionVolume", 1f);
        public static FloatSetting HeadPunchVolumeSetting = new FloatSetting("HeadPunchVolume", 1f);
        public static FloatSetting BoomVolumeSetting = new FloatSetting("BoomVolume", 1f);
        public static FloatSetting StepVolumeSetting = new FloatSetting("StepVolume", 1f);
        //Bombs
        public static FloatSetting[] BombColorSetting =
        {
            new FloatSetting("BombColorR", 1f), 
            new FloatSetting("BombColorG", 1f), 
            new FloatSetting("BombColorB", 1f),
            //new FloatSetting("BombColorA", 1f)
        };
        #endregion

        #region Integers
        //Game
        public static IntSetting SnapshotsMinimumDamageSetting = new IntSetting("SnapshotsMinimumDamage");
        public static IntSetting SpeedometerSetting = new IntSetting("Speedometer");
        public static IntSetting SpeedometerAHSSSetting = new IntSetting("SpeedometerAHSS", 0);
        public static IntSetting StarterAmountSetting = new IntSetting("StarterAmount", 3);
        public static IntSetting TitansPerWaveSetting = new IntSetting("TitansPerWave", 2);
        public static IntSetting MaximumWavesSetting = new IntSetting("MaximumWaves", 20);
        public static IntSetting[] HealthSettings =
        {
            new IntSetting("HealthType"), 
            new IntSetting("MinimumTitansHealth", 100), 
            new IntSetting("MaximumTitansHealth", 200)
        };
        public static IntSetting ArmorSetting = new IntSetting("Armor", 1000);
        public static IntSetting ExplodeRadiusSetting = new IntSetting("ExplodeRadius", 30);
        public static IntSetting PVPTypeSetting = new IntSetting("PVPType");
        public static IntSetting InfectedTitansSetting = new IntSetting("InfectedTitans", 1);
        public static IntSetting PointsLimitSetting = new IntSetting("PointsLimit", 50);
        public static IntSetting TeamSortSetting = new IntSetting("TeamSort");
        public static IntSetting AutoReviveTimeSetting = new IntSetting("AutoReviveTime", 5);
        public static IntSetting ChatSizeSetting = new IntSetting("ChatSize", 13);
        //Video
        public static IntSetting TexturesSetting = new IntSetting("Textures", 2);
        public static IntSetting FPSLockSetting = new IntSetting("FPSLock");
        public static IntSetting AnisotropicFilteringSetting = new IntSetting("AnisotropicFiltering");
        public static IntSetting AntiAliasingSetting = new IntSetting("AntiAliasing");
        public static IntSetting BlendWeightsSetting = new IntSetting("BlendWeights", 2);
        public static IntSetting ShadowProjectionSetting = new IntSetting("ShadowProjectionSetting");
        public static IntSetting ShadowCascadesSetting = new IntSetting("ShadowCascades");
        //Bombs
        public static IntSetting[] BombSettings =
        {
            new IntSetting("BombRadius", 5),
            new IntSetting("BombRange", 5),
            new IntSetting("BombSpeed", 5),
            new IntSetting("BombCooldown", 5)
        };
        //HumanSkins
        public static IntSetting HumanSkinsSetting = new IntSetting("HumanSkins");
        public static IntSetting BladeTrailsAppearanceSetting = new IntSetting("BladeTrailsAppearance", 1);
        public static IntSetting BladeTrailsFrameRateSetting = new IntSetting("BladeTrailsFrameRate", 120);
        public static IntSetting HumanSkinsCountSetting = new IntSetting("HumanSkinsCount", 5);
        public static IntSetting HumanSkinsCurrentSetSetting = new IntSetting("HumanCurrentSkin");
        //LevelSkins
        public static IntSetting LocationSkinsSetting = new IntSetting("LocationSkins");
        public static IntSetting LocationSkinsForestCountSetting = new IntSetting("LocationSkinsForestCount", 1);
        public static IntSetting LocationSkinsForestCurrentSetSetting = new IntSetting("LocationSkinsForestCurrentSet");
        public static IntSetting LocationSkinsCityCountSetting = new IntSetting("LocationSkinsCityCount", 1);
        public static IntSetting LocationSkinsCityCurrentSetSetting = new IntSetting("LocationSkinsCityCurrentSet");
        #endregion

        #region Strings
        //Server
        public static StringSetting ChatMajorColorSetting = new StringSetting("ChatMajorColor", string.Empty);
        public static StringSetting ChatMinorColorSetting = new StringSetting("ChatMinorColor", string.Empty);
        public static StringSetting WelcomeMessageSetting = new StringSetting("WelcomeMessage");
        //Rebinds
        public static StringSetting[] HumanRebindsSetting =
        {
            new StringSetting("HumanForward"),
            new StringSetting("HumanBackward"),
            new StringSetting("HumanLeft"),
            new StringSetting("HumanRight"),
            new StringSetting("HumanJump"),
            new StringSetting("HumanDodge"),
            new StringSetting("HumanLeftHook"),
            new StringSetting("HumanRightHook"),
            new StringSetting("HumanBothHooks"),
            new StringSetting("HumanLock"),
            new StringSetting("HumanAttack"),
            new StringSetting("HumanSpecial"),
            new StringSetting("HumanSalute"),
            new StringSetting("HumanChangeCamera"),
            new StringSetting("HumanRestartSuicide"),
            new StringSetting("HumanMenu"), 
            new StringSetting("HumanShowHideCursor"),
            new StringSetting("HumanFullscreen"),
            new StringSetting("HumanReload"),
            new StringSetting("HumanFlareGreen"),
            new StringSetting("HumanFlareRed"),
            new StringSetting("HumanFlareBlack"),
            new StringSetting("HumanReelIn"),
            new StringSetting("HumanReelOut"),
            new StringSetting("HumanDash"),
            new StringSetting("HumanMinimapMaximize"),
            new StringSetting("HumanMinimapToggle"),
            new StringSetting("HumanMinimapReset"),
            new StringSetting("HumanChat"),
            new StringSetting("HumanLiveSpectate")
        };
        //HumanSkins
        public static StringSetting HumanSkinsTitlesSetting = new StringSetting("HumanSkinsSetTitles","Set 1`Set 2`Set 3`Set 4`Set 5");
        //LevelSkins
        public static StringSetting LocationSkinsForestSetTitlesSetting = new StringSetting("LocationSkinsForestSetTitles", "Sakura Forest`Set 2`Set 3");
        public static StringSetting LocationSkinsCitySetTitlesSetting = new StringSetting("LocationSkinsCitySetTitles", "Set 1`Set 2`Set 3");
        #endregion

        static Settings()
        {
            Load();
        }

        public static void LoadHumanSkins()
        {
            HumanSkinsList = new List<string[]>();
            for (var i = 0; i < HumanSkinsCountSetting; i++)
            {
                HumanSkinsList.Add(PlayerPrefs.GetString("HumanSkin_" + i, "````````````").Split('`'));
            }

            HumanSkinsTitlesList = new List<string>();
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
                PlayerPrefs.SetString("HumanSkin_" + i, str);
            }
            var str2 = string.Empty;
            for (var i = 0; i < HumanSkinsTitlesList.Count; i++)
            {
                str2 += HumanSkinsTitlesList[i] + (i != HumanSkinsTitlesList.Count - 1 ? "`" : "");
            }
            HumanSkinsTitlesSetting.Value = str2;
        }

        public static void LoadForestSkins()
        {
            LocationSkinsForestList = new List<string[]>();
            LocationSkinsForestAmbientList = new List<int>();
            LocationSkinsForestAmbientSettingsList = new List<float[]>();
            LocationSkinsForestFogList = new List<int>();
            LocationSkinsForestFogSettingsList = new List<float[]>();
            LocationSkinsForestParticlesList = new List<int>();
            LocationSkinsForestParticlesSettingsList = new List<float[]>();

            for (var i = 0; i < LocationSkinsForestCountSetting; i++)
            {
                //Skin Fields
                LocationSkinsForestList.Add(PlayerPrefs.GetString("ForestSkin_" + i, 
                    "`````````" + 
                    "https://i.imgur.com/tAzxZjG.png`" + 
                    "https://i.imgur.com/p4lwfdl.png`" +
                    "https://i.imgur.com/rilg26V.png`" +
                    "https://i.imgur.com/tAzxZjG.png`" +
                    "https://i.imgur.com/p4lwfdl.png`" +
                    "https://i.imgur.com/rilg26V.png`" +
                    "https://i.imgur.com/tAzxZjG.png`" +
                    "https://i.imgur.com/p4lwfdl.png`" +
                    "https://i.imgur.com/fxbU9wh.jpg`" +
                    "https://i.imgur.com/SASIAcM.jpg`" +
                    "https://i.imgur.com/V5dey1B.jpg`" +
                    "https://i.imgur.com/lRBZmja.jpg`" +
                    "https://i.imgur.com/PhjVKO4.jpg`" +
                    "https://i.imgur.com/i7mzHHN.jpg").Split('`'));
                //Ambient
                LocationSkinsForestAmbientList.Add(PlayerPrefs.GetInt("ForestAmbient_" + i, 1));
                LocationSkinsForestAmbientSettingsList.Add(new[] {
                    PlayerPrefs.GetFloat("ForestAmbientColorR_" + i, 0.850f),
                    PlayerPrefs.GetFloat("ForestAmbientColorG_" + i, 0.500f),
                    PlayerPrefs.GetFloat("ForestAmbientColorB_" + i, 0.810f)
                });
                //Fog
                LocationSkinsForestFogList.Add(PlayerPrefs.GetInt("ForestFog_" + i, 1));
                LocationSkinsForestFogSettingsList.Add(new[] {
                    PlayerPrefs.GetFloat("ForestFogColorR_" + i, 0.865f),
                    PlayerPrefs.GetFloat("ForestFogColorG_" + i, 0.600f),
                    PlayerPrefs.GetFloat("ForestFogColorB_" + i, 0.775f),
                    PlayerPrefs.GetFloat("ForestFogStartDistance_" + i, 0f),
                    PlayerPrefs.GetFloat("ForestFogEndDistance_" + i, 650f)
                });
                //Particles
                LocationSkinsForestParticlesList.Add(PlayerPrefs.GetInt("ForestParticles_" + i, 0));
                LocationSkinsForestParticlesSettingsList.Add(new []
                {
                    PlayerPrefs.GetFloat("ForestParticlesCount_" + i, 1500f),
                    PlayerPrefs.GetFloat("ForestParticlesHeight_" + i, 125f),
                    PlayerPrefs.GetFloat("ForestParticlesLifeTimeMinimum_" + i, 60f),
                    PlayerPrefs.GetFloat("ForestParticlesLifeTimeMaximum_" + i, 120f),
                    PlayerPrefs.GetFloat("ForestParticlesGravity_" + i, 0.001f),
                    PlayerPrefs.GetFloat("ForestParticlesColorR_" + i, 1f),
                    PlayerPrefs.GetFloat("ForestParticlesColorG_" + i, 1f),
                    PlayerPrefs.GetFloat("ForestParticlesColorB_" + i, 1f),
                    PlayerPrefs.GetFloat("ForestParticlesColorA_" + i, 1f),
                });
            }
            LocationSkinsForestTitlesList = new List<string>();
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
                PlayerPrefs.SetString("ForestSkin_" + i, fields);
                PlayerPrefs.SetInt("ForestAmbient_" + i, LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting]);
                PlayerPrefs.SetFloat("ForestAmbientColorR_" + i, LocationSkinsForestAmbientSettingsList[i][0]);
                PlayerPrefs.SetFloat("ForestAmbientColorG_" + i, LocationSkinsForestAmbientSettingsList[i][1]);
                PlayerPrefs.SetFloat("ForestAmbientColorB_" + i, LocationSkinsForestAmbientSettingsList[i][2]);
                PlayerPrefs.SetInt("ForestFog_" + i, LocationSkinsForestFogList[i]);
                PlayerPrefs.SetFloat("ForestFogColorR_" + i, LocationSkinsForestFogSettingsList[i][0]);
                PlayerPrefs.SetFloat("ForestFogColorG_" + i, LocationSkinsForestFogSettingsList[i][1]);
                PlayerPrefs.SetFloat("ForestFogColorB_" + i, LocationSkinsForestFogSettingsList[i][2]);
                PlayerPrefs.SetFloat("ForestFogStartDistance_", LocationSkinsForestFogSettingsList[i][3]);
                PlayerPrefs.SetFloat("ForestFogEndDistance_", LocationSkinsForestFogSettingsList[i][4]);
                PlayerPrefs.SetInt("ForestParticles_" + i, LocationSkinsForestParticlesList[i]);
                PlayerPrefs.SetFloat("ForestParticlesCount_" + i, LocationSkinsForestParticlesSettingsList[i][0]);
                PlayerPrefs.SetFloat("ForestParticlesHeight_" + i, LocationSkinsForestParticlesSettingsList[i][1]);
                PlayerPrefs.SetFloat("ForestParticlesLifeTimeMinimum_" + i, LocationSkinsForestParticlesSettingsList[i][2]);
                PlayerPrefs.SetFloat("ForestParticlesLifeTimeMaximum_" + i, LocationSkinsForestParticlesSettingsList[i][3]);
                PlayerPrefs.SetFloat("ForestParticlesGravity_" + i, LocationSkinsForestParticlesSettingsList[i][4]);
                PlayerPrefs.SetFloat("ForestParticlesColorR_" + i, LocationSkinsForestParticlesSettingsList[i][5]);
                PlayerPrefs.SetFloat("ForestParticlesColorG_" + i, LocationSkinsForestParticlesSettingsList[i][6]);
                PlayerPrefs.SetFloat("ForestParticlesColorB_" + i, LocationSkinsForestParticlesSettingsList[i][7]);
                PlayerPrefs.SetFloat("ForestParticlesColorA_" + i, LocationSkinsForestParticlesSettingsList[i][8]);
            }
            var titles = string.Empty;
            for (var i = 0; i < LocationSkinsForestTitlesList.Count; i++)
            {
                titles += LocationSkinsForestTitlesList[i] + (i != LocationSkinsForestTitlesList.Count - 1 ? "`" : "");
            }

            LocationSkinsForestSetTitlesSetting.Value = titles;
        }

        public static void LoadCitySkins()
        {
            LocationSkinsCityList = new List<string[]>();
            LocationSkinsCityTitlesList = new List<string>();
            LocationSkinsCityAmbientList = new List<int>();
            LocationSkinsCityAmbientSettingsList = new List<float[]>();
            LocationSkinsCityFogList = new List<int>();
            LocationSkinsCityFogSettingsList = new List<float[]>();
            LocationSkinsCityParticlesList = new List<int>();
            LocationSkinsCityParticlesSettingsList = new List<float[]>();

            for (var i = 0; i < LocationSkinsCityCountSetting; i++)
            {
                //Skin Fields
                LocationSkinsCityList.Add(PlayerPrefs.GetString("CitySkin_" + i, "````````````````").Split('`'));
                //Ambient
                LocationSkinsCityAmbientList.Add(PlayerPrefs.GetInt("CityAmbient_" + i, 0));
                LocationSkinsCityAmbientSettingsList.Add(new[] {
                    PlayerPrefs.GetFloat("CityAmbientColorR_" + i, 0.5f),
                    PlayerPrefs.GetFloat("CityAmbientColorG_" + i, 0.5f),
                    PlayerPrefs.GetFloat("CityAmbientColorB_" + i, 0.5f)
                });
                //Fog
                LocationSkinsCityFogList.Add(PlayerPrefs.GetInt("CityFog_" + i, 0));
                LocationSkinsCityFogSettingsList.Add(new[] {
                    PlayerPrefs.GetFloat("CityFogColorR_" + i, 0.066f),
                    PlayerPrefs.GetFloat("CityFogColorG_" + i, 0.066f),
                    PlayerPrefs.GetFloat("CityFogColorB_" + i, 0.066f),
                    PlayerPrefs.GetFloat("CityFogStartDistance" + i, 0f),
                    PlayerPrefs.GetFloat("CityFogEndDistance" + i, 1000f)
                });
                //Particles
                LocationSkinsCityParticlesList.Add(PlayerPrefs.GetInt("CityParticles_" + i, 0));
                LocationSkinsCityParticlesSettingsList.Add(new[]
                {
                    PlayerPrefs.GetFloat("CityParticlesCount_" + i, 1500f),
                    PlayerPrefs.GetFloat("CityParticlesHeight_" + i, 125f),
                    PlayerPrefs.GetFloat("CityParticlesLifeTimeMinimum_" + i, 60f),
                    PlayerPrefs.GetFloat("CityParticlesLifeTimeMaximum_" + i, 120f),
                    PlayerPrefs.GetFloat("CityParticlesGravity_" + i, 0.001f),
                    PlayerPrefs.GetFloat("CityParticlesColorR_" + i, 1f),
                    PlayerPrefs.GetFloat("CityParticlesColorG_" + i, 1f),
                    PlayerPrefs.GetFloat("CityParticlesColorB_" + i, 1f),
                    PlayerPrefs.GetFloat("CityParticlesColorA_" + i, 1f),
                });
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
                PlayerPrefs.SetString("CitySkin_" + i, fields);
                PlayerPrefs.SetInt("CityAmbient_" + i, LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting]);
                PlayerPrefs.SetFloat("CityAmbientColorR_" + i, LocationSkinsCityAmbientSettingsList[i][0]);
                PlayerPrefs.SetFloat("CityAmbientColorG_" + i, LocationSkinsCityAmbientSettingsList[i][1]);
                PlayerPrefs.SetFloat("CityAmbientColorB_" + i, LocationSkinsCityAmbientSettingsList[i][2]);
                PlayerPrefs.SetInt("CityFog_" + i, LocationSkinsCityFogList[i]);
                PlayerPrefs.SetFloat("CityFogColorR_" + i, LocationSkinsCityFogSettingsList[i][0]);
                PlayerPrefs.SetFloat("CityFogColorG_" + i, LocationSkinsCityFogSettingsList[i][1]);
                PlayerPrefs.SetFloat("CityFogColorB_" + i, LocationSkinsCityFogSettingsList[i][2]);
                PlayerPrefs.SetFloat("CityFogStartDistance_", LocationSkinsCityFogSettingsList[i][3]);
                PlayerPrefs.SetFloat("CityFogEndDistance_", LocationSkinsCityFogSettingsList[i][4]);
                PlayerPrefs.SetInt("CityParticles_" + i, LocationSkinsCityParticlesList[i]);
                PlayerPrefs.SetFloat("CityParticlesCount_" + i, LocationSkinsCityParticlesSettingsList[i][0]);
                PlayerPrefs.SetFloat("CityParticlesHeight_" + i, LocationSkinsCityParticlesSettingsList[i][1]);
                PlayerPrefs.SetFloat("CityParticlesLifeTimeMinimum_" + i, LocationSkinsCityParticlesSettingsList[i][2]);
                PlayerPrefs.SetFloat("CityParticlesLifeTimeMaximum_" + i, LocationSkinsCityParticlesSettingsList[i][3]);
                PlayerPrefs.SetFloat("CityParticlesGravity_" + i, LocationSkinsCityParticlesSettingsList[i][4]);
                PlayerPrefs.SetFloat("CityParticlesColorR_" + i, LocationSkinsCityParticlesSettingsList[i][5]);
                PlayerPrefs.SetFloat("CityParticlesColorG_" + i, LocationSkinsCityParticlesSettingsList[i][6]);
                PlayerPrefs.SetFloat("CityParticlesColorB_" + i, LocationSkinsCityParticlesSettingsList[i][7]);
                PlayerPrefs.SetFloat("CityParticlesColorA_" + i, LocationSkinsCityParticlesSettingsList[i][8]);
            }
            var titles = string.Empty;
            for (var i = 0; i < LocationSkinsCityTitlesList.Count; i++)
            {
                titles += LocationSkinsCityTitlesList[i] + (i != LocationSkinsCityTitlesList.Count - 1 ? "`" : "");
            }
            LocationSkinsCitySetTitlesSetting.Value = titles;
        }


        public static void AddSetting(ISetting set)
        {
            if (allSettings == null)
                allSettings = new Queue<ISetting>();
            allSettings.Enqueue(set);
        }

        private static void CreateStorage()
        {
            var choice = UnityEngine.PlayerPrefs.GetInt("StorageType", 0);
            switch (choice)
            {
                case 0:
                    Storage = new PrefStorage();
                    break;

                default:
                    break;
            }
        }

        public static void Load()
        {
            if (Storage == null)
                CreateStorage();
            foreach(var set in allSettings)
            {
                set.Load();
            }
            LoadHumanSkins();
            LoadForestSkins();
            LoadCitySkins();
        }

        public static void Save()
        {
            foreach(var set in allSettings)
            {
                set.Save();
            }
            SaveHumanSkins();
            SaveForestSkins();
            SaveCitySkins();
        }
    }
}