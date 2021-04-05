using System;
using System.Collections.Generic;
using System.Text;

namespace VaccinePuppeteer
{
    public class AppSettings
    {
        public string CvsInput { get; set; }

        public string WalgreensInput { get; set; }

        public decimal RefreshRate { get; set; }

        public AppSettingsRiteAid RiteAid { get; set; }

        public AppSettingsCvs Cvs { get; set; }

        public AppSettingsWalgreens Walgreens { get; set; }

    }

    public class AppSettingsRiteAid
    {
        //public string Dob { get; set; }
       // public string Occupation { get; set; }
        public string State { get; set; }
        //public string City { get; set; }
        public string Zip { get; set; }
        //public string MedicalConditions { get; set; }
        public Boolean Enabled { get; set; }
       
    }

    public class AppSettingsCvs
    {
        //public string Dob { get; set; }
        // public string Occupation { get; set; }
        public string State { get; set; }
        //public string City { get; set; }
        public string Zip { get; set; }
        //public string MedicalConditions { get; set; }
        public Boolean Enabled { get; set; }

    }

    public class AppSettingsWalgreens
    {
        public Boolean Enabled { get; set; }
        public string Input { get; set; }
    }
}
