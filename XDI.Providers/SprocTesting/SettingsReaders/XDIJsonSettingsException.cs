using System;
using System.Runtime.Serialization;

namespace XDI.Providers.SprocTesting.SettingsReaders
{
    [Serializable]
    internal class XDIJsonSettingsException : Exception
    {
        public XDIJsonSettingsException()
        {
        }

        public XDIJsonSettingsException(string message) : base(message)
        {
        }
    }
}