﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarSystems.Data;
using UnityEngine;

namespace StarSystems
{
    public class ConfigSolarNodes
    {
        private ConfigNode system_config;
        private bool system_config_valid;
        private static ConfigSolarNodes instance;

        private ConfigSolarNodes()
        {
        }

        public static ConfigSolarNodes Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigSolarNodes();
                }
                return instance;
            }
        }

        public StarSystemInfo GetConfigData()
        {
            if (system_config == null)
            {
                return null;
            }
            else
            {
                if (!system_config.HasData && !system_config_valid)
                {
                    return null;
                }
                else
                {
                    ConfigNode solarNode = system_config.GetNode("Solar");
                    StarSystemInfo starSystemInfo;
                    SunInfo sunInfo;
                    double sun_solar_mass;
                    SunType sun_solar_type;
                    try
                    {
                        sun_solar_mass = double.Parse(solarNode.GetNode("Sun").GetValue("SolarMasses")); 
                    }
                    catch
                    {
                        sun_solar_mass = 7700;
                    }
                    try
                    {
                        sun_solar_type = ((SunType)int.Parse(solarNode.GetNode("Sun").GetValue("Type"))); 
                    }
                    catch
                    {
                        sun_solar_type = SunType.Blackhole;
                    }
                    sunInfo = new SunInfo(sun_solar_mass, sun_solar_type);
                    try
                    {
                        starSystemInfo = new StarSystemInfo(sunInfo,
                            double.Parse(solarNode.GetNode("Kerbol").GetValue("semiMajorAxis")));
                    }
                    catch
                    {
                        starSystemInfo = new StarSystemInfo(sunInfo, 4500000000000);
                    }
                    starSystemInfo.Stars = getStars(solarNode.GetNode("Stars").GetNodes("Star"));
                }
            }
            return starSystemInfo;

        }
        List<StarInfo> getStars(ConfigNode[] stars_config)
        {
            List<StarInfo> returnValue = new List<StarInfo>();

            //Grab star info
            foreach (var star in stars_config)
            {
                if (IsStarValid(star))
                {
                    
                }
            }
            return null;
        }

        bool IsStarValid(ConfigNode star)
        {
            bool returnValue = false;
            if (star.HasNode("CelestialBody") && star.HasNode("Orbit"))
            {
                if (star.GetNode("CelestialBody").HasValue("name") &&
                    star.GetNode("CelestialBody").HasValue("flightGlobalIndex") &&
                    star.GetNode("Orbit").HasValue("semiMajorAxis"))
                {
                    int flightGlobalIndex;
                    double semiMajorAxis;
                    bool isflightGlobalIndexValueValid = int.TryParse(star.GetNode("CelestialBody").GetValue("flightGlobalIndex"), out flightGlobalIndex);
                    bool issemiMajorAxisValueValid = double.TryParse(star.GetNode("Orbit").GetValue("semiMajorAxis"), out semiMajorAxis);
                    if (isflightGlobalIndexValueValid && issemiMajorAxisValueValid &&
                        star.GetNode("CelestialBody").GetValue("name") != "")
                    {
                        returnValue = true;
                    }
                }
            }
            return returnValue;
        }
        public bool IsValid(string configname)
        {
            system_config_valid = false;
            if (configname == "")
            {
                return false;
            }
            system_config = ConfigNode.Load(string.Format("GameData/StarSystems/Config/{0}.cfg",configname));
            if (!system_config.HasData)
            {
                return false;
            }
            Debug.Log("Valid star configs.");
            if (system_config.HasNode("Solar"))
            {
                if (system_config.HasNode("Kerbol") && system_config.HasNode("Sun") && system_config.HasNode("Stars"))
                {
                    ConfigNode[] stars = system_config.GetNodes("Star");
                    if (stars.Count() != 0)
                    {
                        system_config_valid = true;
                    }
                }
            }
            return system_config_valid;
        }
    }
}
