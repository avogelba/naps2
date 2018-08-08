﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NAPS2.Util
{
    public class SecureString : IXmlSerializable
    {
        private string value;
        private string valueEncrypted;

        public SecureString(string value)
        {
            this.value = value;
        }

        // ReSharper disable once UnusedMember.Local
        private SecureString()
        {
        }

        public static implicit operator SecureString(string s)
        {
            return new SecureString(s);
        }

        public static implicit operator string(SecureString s)
        {
            return s.ToString();
        }

        public override string ToString()
        {
            value = value ?? SecureStorage.Decrypt(valueEncrypted);
            return value;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            valueEncrypted = reader.ReadString();
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            valueEncrypted = valueEncrypted ?? SecureStorage.Encrypt(value);
            writer.WriteString(valueEncrypted);
        }
    }
}
