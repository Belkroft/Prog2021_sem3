using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace Backups
{
    class Program
    {
        class Backup_job
        {
            //List<List<string>> rp_info = new List<List<string>>(); // IFNO ALL RESTORE POINTS
            private byte rp_num = 1;
            private readonly string rp_folder;
            List<FileInfo> active_files = new List<FileInfo>();

            internal Backup_job(List<FileInfo> files, string folder)
            {
                rp_folder = folder;
                DirectoryInfo active = new DirectoryInfo(".");
                active.CreateSubdirectory("active_files");

                for (byte i = 0; i < files.Count; i++)
                {
                    if (files[i].Exists)
                    {
                        File.Copy(files[i].FullName, @".\active_files\" + files[i].Name, true);
                        active_files.Add(files[i]);
                    }
                }
            }
            internal void RP_single()
            {
                if (Directory.GetDirectories(@".\active_files").Length + Directory.GetFiles(@".\active_files").Length > 0)
                {
                    ZipFile.CreateFromDirectory(@".\active_files", rp_folder + @"\RP" + rp_num.ToString() + @".zip");
                    rp_num++;
                }
                else
                {
                    Console.WriteLine("No active files");
                }
            }
            internal void RP_split()
            {
                if (Directory.GetDirectories(@".\active_files").Length + Directory.GetFiles(@".\active_files").Length > 0)
                {
                    Directory.CreateDirectory(rp_folder + @"\RP" + rp_num.ToString());

                    for (byte i = 0; i < active_files.Count; i++)
                    {
                        using ZipArchive zipArchive = ZipFile.Open(@".\RP" + rp_num.ToString() + @"\" + active_files[i].Name + @".zip", ZipArchiveMode.Create);
                        zipArchive.CreateEntryFromFile(@".\active_files\" + active_files[i].Name, active_files[i].Name);
                    }
                    rp_num++;
                }
                else
                {
                    Console.WriteLine("No active files");
                }
            }
            internal void Delete_file(FileInfo file)
            {
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }
        static void Main(string[] args)
        {
            File.Create(@".\A.txt").Close();
            FileInfo a = new FileInfo(@".\A.txt");
            File.Create(@".\B.txt").Close();
            FileInfo b = new FileInfo(@".\B.txt");
            File.Create(@".\C.txt").Close();
            FileInfo c = new FileInfo(@".\C.txt");

            List<FileInfo> files = new List<FileInfo>() { a, b, c};

            Backup_job backup = new Backup_job(files, ".");


            // TEST
            backup.RP_split();
            FileInfo del = new FileInfo(@".\active_files\C.txt");
            backup.Delete_file(del);
            backup.RP_single();
        }
    }
}
