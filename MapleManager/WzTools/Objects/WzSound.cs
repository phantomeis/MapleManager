﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapleManager.WzTools.Helpers;

namespace MapleManager.WzTools.Objects
{
    public class WzSound : PcomObject
    {
        public override void Read(BinaryReader reader)
        {
            // dont care
        }

        public override void Write(ArchiveWriter writer)
        {
            throw new NotImplementedException();
        }

        public override void Set(string key, object value)
        {
            return;
        }

        public override object Get(string key)
        {
            return null;
        }

        public override void Rename(string key, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
