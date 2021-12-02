using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace DayLightKeyGenerator.Utility
{
    class LicenseHandler
    {
        public LicenseHandler()
        {
        }

        public string CreateLicense(string machineId)
        {
            string license = "";

            for (int i = 0; i < machineId.Length; i++)
            {
                int tempInt = Convert.ToInt32(machineId[machineId.Length - i - 1]) - 48;
                license += ((tempInt + 11) * 4).ToString();
            }
            license = license.Insert(4, "-");
            license = license.Insert(9, "-");
            license = license.Insert(14, "-");
            return license;
        }

        public bool DecodeLicense(string license, string machineId)
        {
            string decodedId = "";
            license = license.Replace("-", String.Empty);

            for (int i = 0; i <= license.Length - 2; i += 2)
            {
                int birler = Convert.ToInt32(license[license.Length - i - 1]) - 48;
                int onlar = Convert.ToInt32(license[license.Length - i - 2]) - 48;
                onlar = onlar * 10;
                double decoded = (birler + onlar) / 4f - 11f;
                decodedId += decoded.ToString();
            }
            return decodedId == machineId;
        }
    }
}
