using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Windows.Forms;
using System.Xml.Serialization;
using icModel.Model.Entities;

namespace icApplication.Helper
{
    public static class Serializer
    {
        public static bool XmlSerialization(List<ExaminationVariant> list, string path)
        {
            ExaminationVariant[] arr = new ExaminationVariant[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                arr[i] = list[i];
            }

            using (FileStream fs = new FileStream(path + "\\test_variants.bin", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, arr);
            }

            return true;
        }
        public static List<ExaminationVariant> XmlDeserialization(string path)
        {
            ExaminationVariant[] arr;

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                arr = (ExaminationVariant[])formatter.Deserialize(fs);
            }

            return arr.ToList();
        }

        public static string OpenFileDialog()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            dlg.DefaultExt = ".bin";
            dlg.Filter = "Bin Files (*.bin)|*.bin";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                return dlg.FileName;
            }
            throw new OperationCanceledException();
        }

        public static string OpenDirectoryDialog()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                return dialog.SelectedPath;
            }
            throw new OperationCanceledException();
        }
    }
}