using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Helpers
{
    public class AppSettingsHelper
    {
        public AppSettingsHelper(Secret secret, GoogleSecrets googlesecrets, FaceBookSecrets facebooksecrets)
        {
            Secret = secret;
            GoogleSecrets = googlesecrets;
            FaceBookSecrets = facebooksecrets;
        }
        public Secret Secret { get; }
        public GoogleSecrets GoogleSecrets { get; }
        public FaceBookSecrets FaceBookSecrets { get; }
    }

    public class Secret
    {
        public string Key { get; set; }
    }
    public class GoogleSecrets
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
    public class FaceBookSecrets
    {
        public string app_id { get; set; }
        public string app_secret { get; set; }
    }
}
