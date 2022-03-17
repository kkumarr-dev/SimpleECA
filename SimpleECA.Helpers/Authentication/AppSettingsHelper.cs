using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Helpers
{
    public class AppSettingsHelper
    {
        public AppSettingsHelper(Secret secret)
        {
            Secret = secret;
        }
        public Secret Secret { get; }
    }

    public class Secret
    {
        public string Key { get; set; }
    }
}
