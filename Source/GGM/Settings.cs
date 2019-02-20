using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGM
{
    public static class Settings
    {
        public static string ChatPath = $"{Application.dataPath}/chat.txt";
        public static bool SpecMode = false;
        public static bool Suicide = false;

        #region Game Settings
        public static float MouseSensitivity;
        public static float CameraDistance;
        public static int CameraTilt;
        public static int InvertMouse;
        public static int FOV;
        public static float FOVvalue;

        public static int Snapshots;
        public static int SnapshotsInGame;
        public static string SnapshotsMinDamage;

        public static int UI;
        public static int Chat;
        public static int RCFormatting;
        public static string ChatMajorColor;
        public static string ChatMinorColor;
        public static int ChatMajorBold;
        public static int ChatMinorBold;
        public static int ChatMajorItalic;
        public static int ChatMinorItalic;
        public static string ChatSize;

        public static int FPS;
        public static int GameFeed;
        public static int DamageFeed;
        public static int Speedometer;
        public static int Minimap;

        public static int BodyLean;
        public static int NoGravity = 0;
        public static int NoClip = 0;
        public static int DoubleBurstRebind;
        public static string DashForce = "40";
        public static string DashDelay = "0.5";
        public static int Bouncy;
        public static int InfiniteBlades = 0;
        public static int InfiniteBullets = 0;
        public static int InfiniteGas = 0;
        
        public static string CannonSpeed;
        public static string CannonRotate;
        public static string CannonCooldown;
        #endregion
        #region Video Settings
        public static float OverallQuality;
        public static int TextureQuality;
        public static int VSync;
        public static string FPSLock;
        public static int MipMapping;
        public static float DrawDistance;
        public static float ShadowDistance;
        public static int AnisotropicFiltreing;
        public static int AntiAliasing;
        public static int BlendWeights;
        public static float MaximumLODLevel;
        public static float LODBias;
        public static int ShadowCascades;
        public static int ShadowProjection;
        public static int Blur;
        public static int Ambient;
        public static float AmbientDayColorR;
        public static float AmbientDayColorG;
        public static float AmbientDayColorB;
        public static float AmbientDawnColorR;
        public static float AmbientDawnColorG;
        public static float AmbientDawnColorB;
        public static float AmbientNightColorR;
        public static float AmbientNightColorG;
        public static float AmbientNightColorB;
        public static int Fog;
        public static float FogStartDistance;
        public static float FogEndDistance;
        public static float FogColorR;
        public static float FogColorG;
        public static float FogColorB;
        public static int Wind;
        #endregion
        #region Audio Settings
        public static float GlobalVolume;
        public static float AHSSVolume;
        public static float AirSlashVolume;
        public static float NapeSlashVolume;
        public static float BodySlashVolume;
        public static float HookVolume;
        public static float TitanErenRoarVolume;
        public static float SwingVolume;
        public static float ThunderVolume;
        public static float HeadExplosionVolume;
        public static float HeadPunchVolume;
        public static float BoomVolume;
        public static float StepVolume;
        #endregion
        #region Bombs
        public static float Bomb_ColorR;
        public static float Bomb_ColorG;
        public static float Bomb_ColorB;
        public static float Bomb_ColorA;
        public static int RandomBombColor;
        public static float Bomb_Radius;
        public static float Bomb_Range;
        public static float Bomb_Speed;
        public static float Bomb_Cooldown;
        public static float Bomb_Points;
        #endregion
        #region Human Skins
        public static int HumanSkins;
        public static readonly string[] HumanSkinLabels = new[]
        {
            "Horse:",//0
            "Hair:",//1
            "Eyes:",//2
            "Glass:",//3
            "Face:",//4
            "Skin:",//5
            "Costume",//6
            "Hoodie",//7
            "Left 3DMG",//8
            "Right 3DMG",//9
            "Gas",//10
            "Logo & Cape",//11
            "Weapon trail:"//12
        };
        public static int HumanSkinCount = 5;
        public static int HumanCurrentSkin;
        public static List<string[]> HumanSkinFields;
        public static List<string> HumanSetTitles;
        public static string[] HumanSkinSetTitles;

        public static int CustomGas;
        public static int BladeTrails;
        public static int BladeTrailsQuality;
        public static string BladeTrailsFadeTime;
        public static float BladeTrailsFrameRate;
        #endregion
        #region Titan Skins
        public static int TitanSkins;
        #endregion
        #region Location Skins
        public static int Particles;
        public static float ParticlesCount;
        public static float ParticlesHeight;
        public static float ParticlesLifeTimeStart;
        public static float ParticlesLifeTimeEnd;
        public static float ParticlesColorR;
        public static float ParticlesColorG;
        public static float ParticlesColorB;
        public static float ParticlesColorA;
        public static float ParticlesGravity;

        public static bool fog;
        public static bool ambient;
        public static float fog_start;
        public static float fog_end;
        public static Color fog_color;
        public static Color ambient_color;

        public static int LocationSkins;

        public static readonly string[] ForestSkinLabels = new[]
        {
            "Ground:",
            "Forest Trunk #1:",
            "Forest Trunk #2:",
            "Forest Trunk #3:",
            "Forest Trunk #4:",
            "Forest Trunk #5:",
            "Forest Trunk #6:",
            "Forest Trunk #7:",
            "Forest Trunk #8:",
            "Forest Leave #1:",
            "Forest Leave #2:",
            "Forest Leave #3:",
            "Forest Leave #4:",
            "Forest Leave #5:",
            "Forest Leave #6:",
            "Forest Leave #7:",
            "Forest Leave #8:",
            "Skybox Front",
            "Skybox Back",
            "Skybox Left",
            "Skybox Right",
            "Skybox Up",
            "Skybox Down",
        };
        public static int ForestSkinCount;
        public static int ForestCurrentSkin;
        public static List<string[]> ForestSkinFields;
        public static List<string> ForestSetTitles;
        public static string[] ForestSkinSetTitles;
        public static int ForestRandomizedPairs;

        public static List<int> ForestAmbient;
        public static List<float[]> ForestAmbientColor;
        public static List<int> ForestFog;
        public static List<float[]> ForestFogColor;
        public static List<float> ForestFogStartDistance;
        public static List<float> ForestFogEndDistance;

        public static readonly string[] CitySkinLabels = new[]
        {
            "Ground:",//0
            "Wall:",//1
            "Gate:",//2
            "Houses #1:",//3
            "Houses #2:",//4
            "Houses #3:",//5
            "Houses #4:",//6
            "Houses #5:",//7
            "Houses #6:",//8
            "Houses #7:",//9
            "Houses #8:",//10
            "Skybox Front",//11
            "Skybox Back",//12
            "Skybox Left",//13
            "Skybox Right",//14
            "Skybox Up",//15
            "Skybox Down",//16
        };
        public static int CitySkinCount;
        public static int CityCurrentSkin;
        public static List<string[]> CitySkinFields;
        public static List<string> CitySetTitles;
        public static string[] CitySkinSetTitles;
        public static List<int> CityAmbient;
        public static List<float[]> CityAmbientColor;

        public static List<int> CityFog;
        public static List<float[]> CityFogColor;
        public static List<float> CityFogStartDistance;
        public static List<float> CityFogEndDistance;
        #endregion

        public static void LoadHumanSkins()
        {
            HumanSkinFields = new List<string[]>();
            for (int i = 0; i < HumanSkinCount; i++)
            {
                HumanSkinFields.Add(PlayerPrefs.GetString("HumanSkin" + i.ToString(), "````````````").Split('`'));
            }

            HumanCurrentSkin = PlayerPrefs.GetInt("HumanCurrentSkin", 0);
            HumanSkinSetTitles = PlayerPrefs.GetString("SkinSlotsNames", "Set 1`Set 2`Set 3`Set 4`Set 5").Split('`');
            HumanSetTitles = new List<string>();
            foreach (string str in HumanSkinSetTitles)
            {
                HumanSetTitles.Add(str);
            }
        }
        public static void SaveHumanSkins()
        {
            PlayerPrefs.SetInt("HumanCurrentSkin", HumanCurrentSkin);
            for (int i = 0; i < HumanSkinCount; i++)
            {
                string str = "";
                for (int j = 0; j < 13; j++)
                {
                    str += HumanSkinFields[i][j] + (j != 12 ? "`" : "");
                }
                PlayerPrefs.SetString("HumanSkin" + i.ToString(), str);
            }
            string str2 = "";
            for (int i = 0; i < HumanSetTitles.Count; i++)
            {
                str2 += HumanSetTitles[i] + (i != HumanSetTitles.Count - 1 ? "`" : "");
            }
            PlayerPrefs.SetString("SkinSlotsNames", str2);
        }

        public static void LoadForestSkins()
        {
            ForestSkinCount = PlayerPrefs.GetInt("ForestSkinCount", 3);
            ForestSkinFields = new List<string[]>();
            ForestAmbient = new List<int>();
            ForestAmbientColor = new List<float[]>();
            ForestFog = new List<int>();
            ForestFogColor = new List<float[]>();
            ForestFogStartDistance = new List<float>();
            ForestFogEndDistance = new List<float>();

            for (int i = 0; i < ForestSkinCount; i++)
            {
                //Skin Fields
                ForestSkinFields.Add(PlayerPrefs.GetString("ForestSkin" + i.ToString(),
                "https://i.imgur.com/0LnL7A3.png`" +
                "https://i.imgur.com/ge3P6v6.png`" +
                "https://i.imgur.com/Kxfdck1.png`" +
                "https://i.imgur.com/VuExeHn.png`" +
                "https://i.imgur.com/ge3P6v6.png`" +
                "https://i.imgur.com/Kxfdck1.png`" +
                "https://i.imgur.com/VuExeHn.png`" +
                "https://i.imgur.com/ge3P6v6.png`" +
                "https://i.imgur.com/Kxfdck1.png`" +
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
                ////Ambient
                ForestAmbient.Add(PlayerPrefs.GetInt("ForestAmbient" + i.ToString(), 0));
                ForestAmbientColor.Add(new[] {
                    PlayerPrefs.GetFloat("ForestAmbientR" + i.ToString(), 0.851f),
                    PlayerPrefs.GetFloat("ForestAmbientG" + i.ToString(), 0.515f),
                    PlayerPrefs.GetFloat("ForestAmbientB" + i.ToString(), 0.818f)
                });
                //Fog
                ForestFog.Add(PlayerPrefs.GetInt("ForestFog" + i.ToString(), 0));
                ForestFogColor.Add(new[] {
                    PlayerPrefs.GetFloat("ForestFogR" + i.ToString(), 0.939f),
                    PlayerPrefs.GetFloat("ForestFogG" + i.ToString(), 0.548f),
                    PlayerPrefs.GetFloat("ForestFogB" + i.ToString(), 0.837f)
                });
                ForestFogStartDistance.Add(PlayerPrefs.GetFloat("ForestFogStartDistance" + i.ToString(), 0f));
                ForestFogEndDistance.Add(PlayerPrefs.GetFloat("ForestFogEndDistance" + i.ToString(), 1000f));
            }
            ForestCurrentSkin = PlayerPrefs.GetInt("ForestCurrentSkin", 0);
            ForestSkinSetTitles = PlayerPrefs.GetString("ForestSetTitles", "Sakura Forest`Sakura Forest`Sakura Forest").Split('`');
            ForestSetTitles = new List<string>();
            foreach (string str in ForestSkinSetTitles)
            {
                ForestSetTitles.Add(str);
            }
        }
        public static void SaveForestSkins()
        {
            PlayerPrefs.SetInt("ForestSkinCount", ForestSkinCount);
            PlayerPrefs.SetInt("ForestCurrentSkin", ForestCurrentSkin);
            for (int i = 0; i < ForestSkinCount; i++)
            {
                string fields = "";
                for (int j = 0; j < 23; j++)
                {
                    fields += ForestSkinFields[i][j] + (j != 22 ? "`" : "");
                }
                PlayerPrefs.SetString("ForestSkin" + i.ToString(), fields);
                PlayerPrefs.SetInt("ForestAmbient" + i.ToString(), ForestAmbient[ForestCurrentSkin]);
                PlayerPrefs.SetFloat("ForestAmbientR" + i.ToString(), ForestAmbientColor[i][0]);
                PlayerPrefs.SetFloat("ForestAmbientG" + i.ToString(), ForestAmbientColor[i][1]);
                PlayerPrefs.SetFloat("ForestAmbientB" + i.ToString(), ForestAmbientColor[i][2]);
                PlayerPrefs.SetInt("ForestFog" + i.ToString(), ForestFog[ForestCurrentSkin]);
                PlayerPrefs.SetFloat("ForestFogR" + i.ToString(), ForestFogColor[i][0]);
                PlayerPrefs.SetFloat("ForestFogG" + i.ToString(), ForestFogColor[i][1]);
                PlayerPrefs.SetFloat("ForestFogB" + i.ToString(), ForestFogColor[i][2]);
                PlayerPrefs.SetFloat("ForestFogStartDistance", ForestFogStartDistance[ForestCurrentSkin]);
                PlayerPrefs.SetFloat("ForestFogEndDistance", ForestFogEndDistance[ForestCurrentSkin]);
            }
            string title = "";
            for (int i = 0; i < ForestSetTitles.Count; i++)
            {
                title += ForestSetTitles[i] + (i != ForestSetTitles.Count - 1 ? "`" : "");
            }
            PlayerPrefs.SetString("ForestSetTitles", title);
        }

        public static void LoadCitySkins()
        {
            CitySkinCount = PlayerPrefs.GetInt("CitySkinCount", 3);
            CitySkinFields = new List<string[]>();
            CityAmbient = new List<int>();
            CityAmbientColor = new List<float[]>();
            CityFog = new List<int>();
            CityFogColor = new List<float[]>();
            CityFogStartDistance = new List<float>();
            CityFogEndDistance = new List<float>();

            for (int i = 0; i < CitySkinCount; i++)
            {
                //Skin Fields
                CitySkinFields.Add(PlayerPrefs.GetString("CitySkin" + i.ToString(), "````````````````").Split('`'));
                //Ambient
                CityAmbient.Add(PlayerPrefs.GetInt("CityAmbient" + i.ToString(), 0));
                CityAmbientColor.Add(new[] {
                    PlayerPrefs.GetFloat("CityAmbientR" + i.ToString(), 0.5f),
                    PlayerPrefs.GetFloat("CityAmbientG" + i.ToString(), 0.5f),
                    PlayerPrefs.GetFloat("CityAmbientB" + i.ToString(), 0.5f)
                });
                //Fog
                CityFog.Add(PlayerPrefs.GetInt("CityFog" + i.ToString(), 0));
                CityFogColor.Add(new[] {
                    PlayerPrefs.GetFloat("CityFogR" + i.ToString(), 0.066f),
                    PlayerPrefs.GetFloat("CityFogG" + i.ToString(), 0.066f),
                    PlayerPrefs.GetFloat("CityFogB" + i.ToString(), 0.066f)
                });
                CityFogStartDistance.Add(PlayerPrefs.GetFloat("CityFogStartDistance" + i.ToString(), 0f));
                CityFogEndDistance.Add(PlayerPrefs.GetFloat("CityFogEndDistance" + i.ToString(), 1000f));
            }
            CityCurrentSkin = PlayerPrefs.GetInt("CityCurrentSkin", 0);
            CitySkinSetTitles = PlayerPrefs.GetString("CitySetTitles", "Set 1`Set 2`Set 3").Split('`');
            CitySetTitles = new List<string>();
            foreach (string str in CitySkinSetTitles)
            {
                CitySetTitles.Add(str);
            }
        }
        public static void SaveCitySkins()
        {
            PlayerPrefs.SetInt("CitySkinCount", CitySkinCount);
            PlayerPrefs.SetInt("CityCurrentSkin", CityCurrentSkin);
            for (int i = 0; i < CitySkinCount; i++)
            {
                string fields = "";
                for (int j = 0; j < 17; j++)
                {
                    fields += CitySkinFields[i][j] + (j != 16 ? "`" : "");
                }
                PlayerPrefs.SetString("CitySkin" + i.ToString(), fields);
                PlayerPrefs.SetInt("CityAmbient" + i.ToString(), CityAmbient[CityCurrentSkin]);
                PlayerPrefs.SetFloat("CityAmbientR" + i.ToString(), CityAmbientColor[i][0]);
                PlayerPrefs.SetFloat("CityAmbientG" + i.ToString(), CityAmbientColor[i][1]);
                PlayerPrefs.SetFloat("CityAmbientB" + i.ToString(), CityAmbientColor[i][2]);
                PlayerPrefs.SetInt("CityFog" + i.ToString(), CityFog[CityCurrentSkin]);
                PlayerPrefs.SetFloat("CityFogR" + i.ToString(), CityFogColor[i][0]);
                PlayerPrefs.SetFloat("CityFogG" + i.ToString(), CityFogColor[i][1]);
                PlayerPrefs.SetFloat("CityFogB" + i.ToString(), CityFogColor[i][2]);
                PlayerPrefs.SetFloat("CityFogStartDistance", CityFogStartDistance[CityCurrentSkin]);
                PlayerPrefs.SetFloat("CityFogEndDistance", CityFogEndDistance[CityCurrentSkin]);
            }
            string title = "";
            for (int i = 0; i < CitySetTitles.Count; i++)
            {
                title += CitySetTitles[i] + (i != CitySetTitles.Count - 1 ? "`" : "");
            }
            PlayerPrefs.SetString("CitySetTitles", title);
        }

        public static void Save()
        {
            #region Game Settings
            PlayerPrefs.SetInt("InvertMouse", InvertMouse);
            PlayerPrefs.SetFloat("MouseSensitivity", MouseSensitivity);
            PlayerPrefs.SetFloat("CameraDistance", CameraDistance);
            PlayerPrefs.SetInt("CameraTilt", CameraTilt);
            PlayerPrefs.SetInt("FOV", FOV);
            PlayerPrefs.SetFloat("FOVvalue", FOVvalue);

            PlayerPrefs.SetInt("Snapshots", Snapshots);
            PlayerPrefs.SetInt("SnapshotsInGame", SnapshotsInGame);
            PlayerPrefs.SetString("SnapshotsMinDamage", SnapshotsMinDamage);

            PlayerPrefs.SetInt("UI", UI);
            PlayerPrefs.SetInt("Chat", Chat);
            PlayerPrefs.SetInt("RCFormatting", RCFormatting);
            PlayerPrefs.SetString("ChatMajorColor", ChatMajorColor);
            PlayerPrefs.SetString("ChatMinorColor", ChatMinorColor);
            PlayerPrefs.SetInt("ChatMajorBold", ChatMajorBold);
            PlayerPrefs.SetInt("ChatMinorBold", ChatMinorBold);
            PlayerPrefs.SetInt("ChatMajorItalic", ChatMajorItalic);
            PlayerPrefs.SetInt("ChatMinorItalic", ChatMinorItalic); ;
            PlayerPrefs.SetString("ChatSize", ChatSize);

            PlayerPrefs.SetInt("GameFeed", GameFeed);
            PlayerPrefs.SetInt("DamageFeed", DamageFeed);
            PlayerPrefs.SetInt("Speedometer", Speedometer);
            PlayerPrefs.SetInt("Minimap", Minimap);

            PlayerPrefs.SetInt("BodyLean", BodyLean);
            PlayerPrefs.SetInt("DoubleBurst", DoubleBurstRebind);
            PlayerPrefs.SetInt("Bouncy", Bouncy);

            PlayerPrefs.SetString("CannonSpeed", CannonSpeed);
            PlayerPrefs.SetString("CannonRotate", CannonRotate);
            PlayerPrefs.SetString("CannonCooldown", CannonCooldown);
            #endregion
            #region Video Settings
            PlayerPrefs.SetFloat("OverallQuality", OverallQuality);
            PlayerPrefs.SetInt("TextureQuality", TextureQuality);
            PlayerPrefs.SetInt("FPS", FPS);
            PlayerPrefs.SetInt("VSync", VSync);
            PlayerPrefs.SetString("FPSLock", FPSLock);
            PlayerPrefs.SetInt("MipMapping", MipMapping);
            PlayerPrefs.SetInt("AnisotropicFiltering", AnisotropicFiltreing);
            PlayerPrefs.SetInt("AntiAliasing", AntiAliasing);
            PlayerPrefs.SetInt("BlendWeights", BlendWeights);
            PlayerPrefs.SetFloat("MaximumLODLevel", MaximumLODLevel);
            PlayerPrefs.SetFloat("LODBias", LODBias);
            PlayerPrefs.SetFloat("DrawDistance", DrawDistance);
            PlayerPrefs.SetFloat("ShadowDistance", ShadowDistance);
            PlayerPrefs.SetInt("ShadowProjection", ShadowProjection);
            PlayerPrefs.SetInt("ShadowCascades", ShadowCascades);
            PlayerPrefs.SetInt("Wind", Wind);
            PlayerPrefs.SetInt("Blur", Blur);
            PlayerPrefs.SetInt("Ambient", Ambient);
            PlayerPrefs.SetFloat("AmbientDayColorR", AmbientDayColorR);
            PlayerPrefs.SetFloat("AmbientDayColorG", AmbientDayColorG);
            PlayerPrefs.SetFloat("AmbientDayColorB", AmbientDayColorB);
            PlayerPrefs.SetFloat("AmbientDawnColorR", AmbientDawnColorR);
            PlayerPrefs.SetFloat("AmbientDawnColorG", AmbientDawnColorG);
            PlayerPrefs.SetFloat("AmbientDawnColorB", AmbientDawnColorB);
            PlayerPrefs.SetFloat("AmbientNightColorR", AmbientNightColorR);
            PlayerPrefs.SetFloat("AmbientNightColorG", AmbientNightColorG);
            PlayerPrefs.SetFloat("AmbientNightColorB", AmbientNightColorB);
            PlayerPrefs.SetInt("Fog", Fog);
            PlayerPrefs.SetFloat("FogStartDistance", FogStartDistance);
            PlayerPrefs.SetFloat("FogEndDistance", FogEndDistance);
            PlayerPrefs.SetFloat("FogColorR", FogColorR);
            PlayerPrefs.SetFloat("FogColorG", FogColorG);
            PlayerPrefs.SetFloat("FogColorB", FogColorB);
            #endregion
            #region Audio Settings
            PlayerPrefs.SetFloat("GlobalVolume", GlobalVolume);
            PlayerPrefs.SetFloat("AHSSVolume", AHSSVolume);
            PlayerPrefs.SetFloat("AirSlashVolume", AirSlashVolume);
            PlayerPrefs.SetFloat("NapeSlashVolume", NapeSlashVolume);
            PlayerPrefs.SetFloat("BodySlashVolume", BodySlashVolume);
            PlayerPrefs.SetFloat("HookVolume", HookVolume);
            PlayerPrefs.SetFloat("TitanErenRoarVolume", TitanErenRoarVolume);
            PlayerPrefs.SetFloat("SwingVolume", SwingVolume);
            PlayerPrefs.SetFloat("ThunderVolume", ThunderVolume);
            PlayerPrefs.SetFloat("HeadExplosionVolume", HeadExplosionVolume);
            PlayerPrefs.SetFloat("HeadPunchVolume", HeadPunchVolume);
            PlayerPrefs.SetFloat("BoomVolume", BoomVolume);
            PlayerPrefs.SetFloat("StepVolume", StepVolume);
            #endregion
            #region Titan Skins
            PlayerPrefs.SetInt("TitanSkins", TitanSkins);
            #endregion
            #region Human Skins
            PlayerPrefs.SetInt("HumanSkins", HumanSkins);
            PlayerPrefs.SetInt("CustomGas", CustomGas);
            PlayerPrefs.SetInt("BladeTrails", BladeTrails);
            PlayerPrefs.SetInt("BladeTrailsQuality", BladeTrailsQuality);
            PlayerPrefs.SetString("BladeTrailsFadeTime", BladeTrailsFadeTime);
            PlayerPrefs.SetFloat("BladeTrailsFrameRate", BladeTrailsFrameRate);
            #endregion
            #region Location Skins
            PlayerPrefs.SetInt("Particles", Particles);
            PlayerPrefs.SetFloat("ParticlesCount", ParticlesCount);
            PlayerPrefs.SetFloat("ParticlesHeight", ParticlesHeight);
            PlayerPrefs.SetFloat("ParticlesLifeTimeStart", ParticlesLifeTimeStart);
            PlayerPrefs.SetFloat("ParticlesLifeTimeEnd", ParticlesLifeTimeEnd);
            PlayerPrefs.SetFloat("ParticlesColorR", ParticlesColorR);
            PlayerPrefs.SetFloat("ParticlesColorG", ParticlesColorG);
            PlayerPrefs.SetFloat("ParticlesColorB", ParticlesColorB);
            PlayerPrefs.SetFloat("ParticlesColorA", ParticlesColorA);
            PlayerPrefs.SetFloat("ParticlesGravity", ParticlesGravity);

            PlayerPrefs.SetInt("LevelSkins", LocationSkins);
            PlayerPrefs.SetInt("ForestRandomizedPairs", ForestRandomizedPairs);
            #endregion
            #region Bombs
            PlayerPrefs.SetFloat("Bomb_ColorR", Bomb_ColorR);
            PlayerPrefs.SetFloat("Bomb_ColorG", Bomb_ColorG);
            PlayerPrefs.SetFloat("Bomb_ColorB", Bomb_ColorB);
            PlayerPrefs.SetFloat("Bomb_ColorA", Bomb_ColorA);
            PlayerPrefs.SetInt("Bomb_RandomColor", RandomBombColor);
            PlayerPrefs.SetFloat("Bomb_Radius", Bomb_Radius);
            PlayerPrefs.SetFloat("Bomb_Range", Bomb_Range);
            PlayerPrefs.SetFloat("Bomb_Speed", Bomb_Speed);
            PlayerPrefs.SetFloat("Bomb_Cooldown", Bomb_Cooldown);
            #endregion
        }
        public static void LoadConfig()
        {
            #region Game Settings
            InvertMouse = PlayerPrefs.GetInt("InvertMouse", 0);
            MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f);
            CameraDistance = PlayerPrefs.GetFloat("CameraDistance", 1f);
            CameraTilt = PlayerPrefs.GetInt("CameraTilt", 0);
            FOV = PlayerPrefs.GetInt("FOV", 0);
            FOVvalue = PlayerPrefs.GetFloat("FOVvalue", 100);

            Snapshots = PlayerPrefs.GetInt("Snapshots", 0);
            SnapshotsInGame = PlayerPrefs.GetInt("SnapshotsInGame", 0);
            SnapshotsMinDamage = PlayerPrefs.GetString("SnapshotsMinDamage", "0");

            UI = PlayerPrefs.GetInt("UI", 1);
            Chat = PlayerPrefs.GetInt("Chat", 1);
            RCFormatting = PlayerPrefs.GetInt("RCFormatting", 1);
            ChatMajorColor = PlayerPrefs.GetString("ChatMajorColor", "FDBCB4");
            ChatMinorColor = PlayerPrefs.GetString("ChatMinorColor", "F08080");
            ChatMajorBold = PlayerPrefs.GetInt("ChatMajorBold", 1);
            ChatMinorBold = PlayerPrefs.GetInt("ChatMinorBold", 1);
            ChatMajorItalic = PlayerPrefs.GetInt("ChatMajorItalic", 0);
            ChatMinorItalic = PlayerPrefs.GetInt("ChatMinorItalic", 0); ;
            ChatSize = PlayerPrefs.GetString("ChatSize", "");

            GameFeed = PlayerPrefs.GetInt("GameFeed", 0);
            DamageFeed = PlayerPrefs.GetInt("DamageFeed", 0);
            Speedometer = PlayerPrefs.GetInt("Speedometer", 0);
            Minimap = PlayerPrefs.GetInt("Minimap", 0);

            BodyLean = PlayerPrefs.GetInt("BodyLean", 1);
            DoubleBurstRebind = PlayerPrefs.GetInt("DoubleBurst", 0);
            Bouncy = PlayerPrefs.GetInt("Bouncy", 0);

            CannonSpeed = PlayerPrefs.GetString("CannonSpeed", "50");
            CannonRotate = PlayerPrefs.GetString("CannonRotate", "25");
            CannonCooldown = PlayerPrefs.GetString("CannonCooldown", "3.5");
            #endregion
            #region Video Settings
            OverallQuality = PlayerPrefs.GetFloat("OverallQuality", 0f);
            TextureQuality = PlayerPrefs.GetInt("TextureQuality", 0);
            FPS = PlayerPrefs.GetInt("FPS", 1);
            VSync = PlayerPrefs.GetInt("VSync", 0);
            FPSLock = PlayerPrefs.GetString("FPSLock", "0");
            MipMapping = PlayerPrefs.GetInt("MipMapping", 0);
            AnisotropicFiltreing = PlayerPrefs.GetInt("AnisotropicFiltering", 0);
            AntiAliasing = PlayerPrefs.GetInt("AntiAliasing", 0);
            BlendWeights = PlayerPrefs.GetInt("BlendWeights", 2);
            MaximumLODLevel = PlayerPrefs.GetFloat("MaximumLODLevel", 0);
            LODBias = PlayerPrefs.GetFloat("LODBias", 2);
            DrawDistance = PlayerPrefs.GetInt("DrawDistance", 15);
            ShadowDistance = PlayerPrefs.GetFloat("ShadowDistance", 8);
            ShadowProjection = PlayerPrefs.GetInt("ShadowProjection", 1);
            ShadowCascades = PlayerPrefs.GetInt("ShadowCascades", 0);
            Wind = PlayerPrefs.GetInt("Wind", 0);
            Blur = PlayerPrefs.GetInt("Blur", 0);
            Ambient = PlayerPrefs.GetInt("Ambient", 0);
            AmbientDayColorR = PlayerPrefs.GetFloat("AmbientDayColorR", 0.494f);
            AmbientDayColorG = PlayerPrefs.GetFloat("AmbientDayColorG", 0.478f);
            AmbientDayColorB = PlayerPrefs.GetFloat("AmbientDayColorB", 0.447f);
            AmbientDawnColorR = PlayerPrefs.GetFloat("AmbientDawnColorR", 0.345f);
            AmbientDawnColorG = PlayerPrefs.GetFloat("AmbientDawnColorG", 0.305f);
            AmbientDawnColorB = PlayerPrefs.GetFloat("AmbientDawnColorB", 0.271f);
            AmbientNightColorR = PlayerPrefs.GetFloat("AmbientNightColorR", 0.05f);
            AmbientNightColorG = PlayerPrefs.GetFloat("AmbientNightColorG", 0.05f);
            AmbientNightColorB = PlayerPrefs.GetFloat("AmbientNightColorB", 0.05f);
            Fog = PlayerPrefs.GetInt("Fog", 1);
            FogStartDistance = PlayerPrefs.GetFloat("FogStartDistance", 0);
            FogEndDistance = PlayerPrefs.GetFloat("FogEndDistance", 1000);
            FogColorR = PlayerPrefs.GetFloat("FogColorR", 0.066f);
            FogColorG = PlayerPrefs.GetFloat("FogColorG", 0.066f);
            FogColorB = PlayerPrefs.GetFloat("FogColorB", 0.066f);
            #endregion
            #region Audio Settings
            GlobalVolume = PlayerPrefs.GetFloat("GlobalVolume", 1f);
            AHSSVolume = PlayerPrefs.GetFloat("AHSSVolume", 1f);
            AirSlashVolume = PlayerPrefs.GetFloat("AirSlashVolume", 1f);
            NapeSlashVolume = PlayerPrefs.GetFloat("NapeSlashVolume", 1f);
            BodySlashVolume = PlayerPrefs.GetFloat("BodySlashVolume", 1f);
            HookVolume = PlayerPrefs.GetFloat("HookVolume", 1f);
            TitanErenRoarVolume = PlayerPrefs.GetFloat("TitanErenRoarVolume", 1f);
            SwingVolume = PlayerPrefs.GetFloat("SwingVolume", 1f);
            ThunderVolume = PlayerPrefs.GetFloat("ThunderVolume", 1f);
            HeadExplosionVolume = PlayerPrefs.GetFloat("HeadExplosionVolume", 1f);
            HeadPunchVolume = PlayerPrefs.GetFloat("HeadPunchVolume", 1f);
            BoomVolume = PlayerPrefs.GetFloat("BoomVolume", 1f);
            StepVolume = PlayerPrefs.GetFloat("StepVolume", 1f);
            #endregion
            #region Titan Skins
            TitanSkins = PlayerPrefs.GetInt("TitanSkins", 0);
            #endregion
            #region Human Skins
            HumanSkins = PlayerPrefs.GetInt("HumanSkins", 0);
            CustomGas = PlayerPrefs.GetInt("CustomGas", 0);
            BladeTrails = PlayerPrefs.GetInt("BladeTrails", 1);
            BladeTrailsQuality = PlayerPrefs.GetInt("BladeTrailsQuality", 0);
            BladeTrailsFadeTime = PlayerPrefs.GetString("BladeTrailsFadeTime", "0.2");
            BladeTrailsFrameRate = PlayerPrefs.GetFloat("BladeTrailsFrameRate", 60f);
            #endregion
            #region Location Skins
            Particles = PlayerPrefs.GetInt("Particles", 0);
            ParticlesCount = PlayerPrefs.GetFloat("ParticlesCount", 3000);
            ParticlesHeight = PlayerPrefs.GetFloat("ParticlesHeight", 100);
            ParticlesLifeTimeStart = PlayerPrefs.GetFloat("ParticlesLifeTimeStart", 60f);
            ParticlesLifeTimeEnd = PlayerPrefs.GetFloat("ParticlesLifeTimeEnd", 120f);
            ParticlesColorR = PlayerPrefs.GetFloat("ParticlesColorR", 1f);
            ParticlesColorG = PlayerPrefs.GetFloat("ParticlesColorG", 1f);
            ParticlesColorB = PlayerPrefs.GetFloat("ParticlesColorB", 1f);
            ParticlesColorA = PlayerPrefs.GetFloat("ParticlesColorA", 1f);
            ParticlesGravity = PlayerPrefs.GetFloat("ParticlesGravity", 0.001f);
            LocationSkins = PlayerPrefs.GetInt("LevelSkins", 0);
            ForestRandomizedPairs = PlayerPrefs.GetInt("ForestRandomizedPairs", 0);
            #endregion
            #region Bombs
            Bomb_ColorR = PlayerPrefs.GetFloat("Bomb_ColorR", 1f);
            Bomb_ColorG = PlayerPrefs.GetFloat("Bomb_ColorG", 1f);
            Bomb_ColorB = PlayerPrefs.GetFloat("Bomb_ColorB", 1f);
            Bomb_ColorA = PlayerPrefs.GetFloat("Bomb_ColorA", 1f);
            RandomBombColor = PlayerPrefs.GetInt("Bomb_RandomColor", 0);
            Bomb_Radius = PlayerPrefs.GetFloat("Bomb_Radius", 5f);
            Bomb_Range = PlayerPrefs.GetFloat("Bomb_Range", 5f);
            Bomb_Speed = PlayerPrefs.GetFloat("Bomb_Speed", 5f);
            Bomb_Cooldown = PlayerPrefs.GetFloat("Bomb_Cooldown", 5f);
            #endregion
        }

        public static void InitSettings()
        {
            var player = Extensions.Player();
            
            if (UI == 0)
            {
                GGM.Extensions.DisableObject("UIflare1");
                GGM.Extensions.DisableObject("UIflare2");
                GGM.Extensions.DisableObject("UIflare3");
                GGM.Extensions.DisableObject("flareg1");
                GGM.Extensions.DisableObject("UIflare1");
                GGM.Extensions.DisableObject("flareg2");
                GGM.Extensions.DisableObject("flareg3");
                GGM.Extensions.DisableObject("bar");
                GGM.Extensions.DisableObject("locker");
                GGM.Extensions.DisableObject("stamina_titan");
                GGM.Extensions.DisableObject("stamina_titan_bottom");
                GGM.Extensions.DisableObject("flash");
                GGM.Extensions.DisableObject("skill_cd_bottom");
                GGM.Extensions.DisableObject("skill_cd_armin");
                GGM.Extensions.DisableObject("skill_cd_mikasa");
                GGM.Extensions.DisableObject("skill_cd_sasha");
                GGM.Extensions.DisableObject("skill_cd_petra");
                GGM.Extensions.DisableObject("skill_cd_levi");
                GGM.Extensions.DisableObject("skill_cd_jean");
                GGM.Extensions.DisableObject("skill_cd_marco");
                GGM.Extensions.DisableObject("skill_cd_eren");
                GGM.Extensions.DisableObject("GasUI");
            }

            if (player != null && player.GetComponent<HERO>() != null)
            {
                player.GetComponent<HERO>().gravity = NoGravity == 1 ? 0f : 20f;
                player.GetComponent<CapsuleCollider>().isTrigger = NoClip == 1 ? true : false;
                if (Bouncy == 1)
                {
                    player.collider.material.bounciness = 1;
                    player.collider.material.bounceCombine = PhysicMaterialCombine.Maximum;
                }
                else
                    player.collider.material.bounciness = 0;
            }
            AudioListener.volume = GlobalVolume;
            IN_GAME_MAIN_CAMERA.invertY = InvertMouse == 0 ? 1 : -1;
            IN_GAME_MAIN_CAMERA.sensitivityMulti = MouseSensitivity;
            IN_GAME_MAIN_CAMERA.cameraTilt = CameraTilt;
            IN_GAME_MAIN_CAMERA.cameraDistance = CameraDistance + 0.3f;
            PlayerPrefs.SetInt("Snapshots", Snapshots);
            PlayerPrefs.SetInt("SnapshotsInGame", SnapshotsInGame);
            InRoomChat.IsVisible = Chat == 1 ? true : false;
            #region Quality 
            QualitySettings.SetQualityLevel(Convert.ToInt32(Mathf.Round(OverallQuality)), true);
            QualitySettings.masterTextureLimit = TextureQuality == 0 ? 2 : TextureQuality == 1 ? 1 : 0;
            if (Convert.ToInt32(FPSLock) > 50) Application.targetFrameRate = Convert.ToInt32(FPSLock);
            else if (Convert.ToInt32(FPSLock) == 0) Application.targetFrameRate = -1;
            else Application.targetFrameRate = 50;
            QualitySettings.vSyncCount = VSync;
            QualitySettings.anisotropicFiltering = AnisotropicFiltreing == 0 ? AnisotropicFiltering.Disable : AnisotropicFiltreing == 1 ? AnisotropicFiltering.Enable : AnisotropicFiltering.ForceEnable;
            QualitySettings.antiAliasing = AntiAliasing == 0 ? 0 : AntiAliasing == 1 ? 2 : AntiAliasing == 2 ? 4 : 8;
            QualitySettings.blendWeights = BlendWeights == 0 ? UnityEngine.BlendWeights.OneBone : BlendWeights == 1 ? UnityEngine.BlendWeights.TwoBones : UnityEngine.BlendWeights.FourBones;
            QualitySettings.maximumLODLevel = Convert.ToInt32(Mathf.Round(MaximumLODLevel));
            QualitySettings.lodBias = LODBias;
            Camera.main.farClipPlane = DrawDistance * 100;
            QualitySettings.shadowDistance = ShadowDistance * 100;
            QualitySettings.shadowProjection = ShadowProjection == 0 ? UnityEngine.ShadowProjection.CloseFit : UnityEngine.ShadowProjection.StableFit;
            QualitySettings.shadowCascades = ShadowCascades == 0 ? 0 : ((ShadowCascades == 1) ? 2 : 4);
            Camera.main.GetComponent<TiltShift>().enabled = Blur != 0;
            #endregion

        }
        public static void InitLocationSkins()
        {
            if (LocationSkins == 0)
            {
                if (GGM.Extensions.Forest)
                {
                    RenderSettings.fog = Fog == 1 ? true : false;
                    RenderSettings.fogStartDistance = FogStartDistance;
                    RenderSettings.fogEndDistance = FogEndDistance;
                    RenderSettings.fogColor = new Color(
                        FogColorR,
                        FogColorG,
                        FogColorB);
                }
                else
                {
                    RenderSettings.fog = false;
                }
                if (Ambient == 1)
                {
                    RenderSettings.ambientLight =
                        IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? new Color(AmbientDayColorR, AmbientDayColorG, AmbientDayColorB) :
                        IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? new Color(AmbientDawnColorR, AmbientDawnColorG, AmbientDawnColorB) :
                        new Color(AmbientNightColorR, AmbientNightColorG, AmbientNightColorB);
                }
                else
                {
                    RenderSettings.ambientLight =
                         IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? new Color(0.494f, 0.478f, 0.447f) :
                         IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? new Color(0.729f, 0.643f, 0.458f) :
                         new Color(0.05f, 0.05f, 0.05f);
                }
            }
            else
            {
                if (GGM.Extensions.Forest && (PhotonNetwork.isMasterClient || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE))
                {

                    RenderSettings.fog = ForestFog[ForestCurrentSkin] == 1 ? true : false;
                    RenderSettings.fogStartDistance = ForestFogStartDistance[ForestCurrentSkin];
                    RenderSettings.fogEndDistance = ForestFogEndDistance[ForestCurrentSkin];
                    RenderSettings.fogColor = new Color(
                        ForestFogColor[ForestCurrentSkin][0],
                        ForestFogColor[ForestCurrentSkin][1],
                        ForestFogColor[ForestCurrentSkin][2]);
                    if (ForestAmbient[ForestCurrentSkin] == 1)
                    {
                        RenderSettings.ambientLight = new Color(
                            ForestAmbientColor[ForestCurrentSkin][0],
                            ForestAmbientColor[ForestCurrentSkin][1],
                            ForestAmbientColor[ForestCurrentSkin][2]);
                    }
                    else
                    {
                        RenderSettings.ambientLight =
                            IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? new Color(0.494f, 0.478f, 0.447f) :
                            IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? new Color(0.729f, 0.643f, 0.458f) :
                            new Color(0.05f, 0.05f, 0.05f);
                    }

                }
                else if (GGM.Extensions.City && (PhotonNetwork.isMasterClient || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE))
                {
                    RenderSettings.fog = CityFog[CityCurrentSkin] == 1 ? true : false;
                    RenderSettings.fogStartDistance = CityFogStartDistance[CityCurrentSkin];
                    RenderSettings.fogEndDistance = CityFogEndDistance[CityCurrentSkin];
                    RenderSettings.fogColor = new Color(
                        CityFogColor[CityCurrentSkin][0],
                        CityFogColor[CityCurrentSkin][1],
                        CityFogColor[CityCurrentSkin][2]);
                    if (CityAmbient[CityCurrentSkin] == 1)
                    {
                        RenderSettings.ambientLight = new Color(
                            CityAmbientColor[CityCurrentSkin][0],
                            CityAmbientColor[CityCurrentSkin][1],
                            CityAmbientColor[CityCurrentSkin][2]);
                    }
                    else
                    {
                        RenderSettings.ambientLight =
                            IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? new Color(0.494f, 0.478f, 0.447f) :
                            IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? new Color(0.729f, 0.643f, 0.458f) :
                            new Color(0.05f, 0.05f, 0.05f);
                    }
                }
                else if (!GGM.Extensions.City && !GGM.Extensions.Forest)
                {
                    RenderSettings.fog = false;
                    RenderSettings.ambientLight =
                            IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? new Color(0.494f, 0.478f, 0.447f) :
                            IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? new Color(0.729f, 0.643f, 0.458f) :
                            new Color(0.05f, 0.05f, 0.05f);
                }
                else if (!PhotonNetwork.isMasterClient && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && (GGM.Extensions.Forest || GGM.Extensions.City))
                {
                    if (ambient)
                    {
                        RenderSettings.ambientLight = ambient_color;
                    }
                    else
                    {
                        RenderSettings.ambientLight =
                                 IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day ? new Color(0.494f, 0.478f, 0.447f) :
                                 IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn ? new Color(0.729f, 0.643f, 0.458f) :
                                 new Color(0.05f, 0.05f, 0.05f);
                    }
                    if (fog)
                    {
                        RenderSettings.fog = true;
                        RenderSettings.fogColor = fog_color;
                        RenderSettings.fogStartDistance = fog_start;
                        RenderSettings.fogEndDistance = fog_end;
                    }
                    else
                    {
                        RenderSettings.fog = GGM.Extensions.Forest ? true : false;
                        RenderSettings.fogStartDistance = (Camera.main.farClipPlane / 7) - Camera.main.farClipPlane;
                        RenderSettings.fogEndDistance = Camera.main.farClipPlane;
                    }
                }
            }
        }
    }
}
