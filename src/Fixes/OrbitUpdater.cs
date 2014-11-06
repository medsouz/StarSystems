﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StarSystems.Fixes
{
    class OrbitUpdater : MonoBehaviour
    {
        void Update()
        {
            if (FlightGlobals.ActiveVessel != null)
            {
                foreach (var Orb in Planetarium.Orbits)
                {
                    Orb.UpdateOrbit();
                }
            }
        }
    }
}
