﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarSystems.Data
{
    public class KspSystemDefinition
    {
        public KspSystemDefinition(RootDefinition Root)
        {
            this.Root = Root;
            Stars = new List<StarSystemDefintion>();
        }
        public KspSystemDefinition(RootDefinition Root, List<StarSystemDefintion> Stars)
        {

            this.Root = Root;
            this.Stars = Stars;
        }
        public List<StarSystemDefintion> Stars { get; set; }
        public RootDefinition Root { get; set; }
    }

    
}
