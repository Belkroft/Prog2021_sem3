using System;
using System.Collections.Generic;
using System.IO;

namespace lab_4 {

    public interface ICreate_points {
        public Points Create(Backup backup);
    }

    public class Full_point : ICreate_points {
        public Full_point() { }

        public Points Create(Backup backup) {
            for (int i = 0; i < backup.all_files_info.Count; i++) {
                File_info file = backup.all_files_info[i];
                file.change_check = false;
            }
            return new Points(backup.files_paths, false);
        }
    }

    public class Inc_point : ICreate_points{
        public Inc_point() { }

        public Points Create(Backup backup) {

            if (backup.files_count_backup == 0) {
                Console.Error.WriteLine("No full points");
                return null;
            }

            backup.only_file_info.Clear();

            for (int i = 0; i < backup.all_files_info.Count; i++) {
                File_info file = backup.all_files_info[i];
                File_info file_to_add = new File_info(file.FullName);

                if (file.change_check || file.Last_change_time != file_to_add.Last_change_time) {
                    file.change_check = file_to_add.change_check = false;
                    backup.only_file_info.Add(file_to_add);
                }
            }

            return new Points(backup.only_file_info, true);
        }
    }

    public class Points {

        public readonly DateTime time;
        public readonly long size;
        public readonly bool inc_check;

        protected List<File_info> files = new List<File_info>();

        public Points(List<string> console_paths, bool console_check) { // FOR FULL POINT

            time = DateTime.Now;
            inc_check = console_check;

            static File_info get_file_info(string file) {

                File_info ret_file = new File_info(file) {
                    change_check = false
                };
                return ret_file;
            }

            files = console_paths.ConvertAll(new Converter<string, File_info>(get_file_info));
            size = Get_size();
        }

        public Points(List<File_info> console_info, bool console_check) { // FOR INC POINT

            time = DateTime.Now;
            inc_check = console_check;

            files = console_info;
            size = Get_size();
        }

        protected static long Direct(DirectoryInfo dir) {

            long dir_file_size = 0;

            FileInfo[] file = dir.GetFiles();
            foreach (FileInfo i in file)
                dir_file_size += i.Length;

            DirectoryInfo[] dir_ = dir.GetDirectories();
            foreach (DirectoryInfo i in dir_)
                dir_file_size += Direct(i);

            return dir_file_size;
        }

        protected long Get_size() {

            long file_size = 0;

            for (int i = 0; i < files.Count; i++) {
                File_info files_info = files[i];

                    file_size += files_info.Length;
                    if (Directory.Exists(files_info.FullName))
                        file_size += Direct(files_info.Directory);
            }
            return file_size;
        }
    }
}