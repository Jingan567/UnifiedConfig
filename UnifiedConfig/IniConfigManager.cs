﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.XPath;

namespace UnifiedConfig
{
    internal class IniConfigManager : XmlConfig
    {

        public IniConfigManager(string filepath)
            : base(filepath, File.ReadAllLines(filepath).ToXml())
        {

        }

        public override void Save(string filepath = null)
        {
            File.WriteAllText(filepath ?? sourceFilePath, xDoc.ToIni());
        }

        public override string this[string xPath]
        {
            get => base[Decorate(xPath)];
            set => base[Decorate(xPath)] = value;
        }
        /// <summary>
        /// Ini file does not contains an root element. Hence a decorator is necessary.
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        private string Decorate(string xPath)
        {
            if (xPath.StartsWith("/")) xPath = "/" + xDoc.Root.Name.LocalName + xPath;
            return xPath;
        }

        public override IEnumerable<XmlConfig> Elements(string xPath)
        {
            return base.Elements(this.AddRoot(xPath));
        }
    }
}
