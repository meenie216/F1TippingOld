using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using F1Tipping;
using System.IO;
using System.Runtime.Serialization;

namespace F1TippingUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string appDataPath;
        private string persistFile;
        public TippingCompetition Comp { get; set; }

        public App()
        {
            appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "F1Tipping");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            persistFile = Path.Combine(appDataPath, "TippingComp.xml");

            if (File.Exists(persistFile))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(TippingCompetition));

                using (FileStream stream = new FileStream(persistFile, FileMode.Open, FileAccess.Read))
                {
                    Comp = (TippingCompetition)serializer.ReadObject(stream);
                }
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(TippingCompetition));

            using (FileStream stream = new FileStream(persistFile, FileMode.Create, FileAccess.Write))
            {
                serializer.WriteObject(stream, this.Comp);
            }

            base.OnExit(e);
        }



    }
}
