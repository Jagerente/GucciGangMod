using GGM.Storage;
using System.Collections.Generic;

namespace GGM.Config
{
    public static class Settings
    {
        public static IDataStorage Storage = null;
        private static Queue<ISetting> allSettings = null;

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
        #endregion

        #region Integers
        //Game
        //Snapshots
        public static IntSetting SnapshotsMinimumDamageSetting = new IntSetting("SnapshotsMinimumDamage");
        //Misc
        public static IntSetting SpeedometerSetting = new IntSetting("Speedometer");
        public static IntSetting SpeedometerAHSSSetting = new IntSetting("SpeedometerAHSS", 0);
        //Server
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
        #endregion

        #region Strings
        //Server
        public static StringSetting ChatMajorColorSetting = new StringSetting("ChatMajorColor", string.Empty);
        public static StringSetting ChatMinorColorSetting = new StringSetting("ChatMinorColor", string.Empty);
        public static StringSetting WelcomeMessage = new StringSetting("WelcomeMessage");
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
        #endregion

        static Settings()
        {
            Load();
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
        }

        public static void Save()
        {
            foreach(var set in allSettings)
            {
                set.Save();
            }
        }
    }
}