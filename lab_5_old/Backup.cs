using System;
using System.Collections.Generic;

namespace lab_4 {
    public class Backup {

        public ushort id = 0;
        public DateTime start_time_backup;

        public long files_count_backup = 0;

        private readonly List<Points> all_points = new List<Points>();
        private int save_full_points = 0;
        private int save_inc_points = 0;

        public readonly List<string> files_paths;
        public readonly List<File_info> only_file_info = new List<File_info>();
        public List<File_info> all_files_info;

        private IClear_points points_to_clear;
        
        public Backup(List<string> constr_file_paths) {

            id++;
            start_time_backup = DateTime.Now;
            files_paths = constr_file_paths;

            static File_info get_file_info(string path) => new File_info(path);
            all_files_info = files_paths.ConvertAll(new Converter<string, File_info>(get_file_info));

        }

        public void Add_file(string file_path) {
            files_paths.Add(file_path);
            all_files_info.Add(new File_info(file_path));
        }
        public void Delete_file(string file_path) {
            files_paths.Remove(file_path);
            all_files_info.Clear();
            static File_info get_file_info(string path) => new File_info(path);
            all_files_info = files_paths.ConvertAll(new Converter<string, File_info>(get_file_info));
        }

        public void Show_backup_size() {

            if (all_points == null || all_points.Count == 0)
                Console.WriteLine("No full");
            else
                Console.WriteLine(all_points[save_full_points].size);
        }

        public void Create_point(ICreate_points full) {

            Points point = full.Create(this);

            if (point == null) return;
            else if (point.inc_check)
                save_inc_points = all_points.Count;
            else save_full_points = all_points.Count;

            files_count_backup += point.size;
            all_points.Add(point);

            Console.WriteLine(all_points.Count);
        }
        public void Show_inc_point() {

            if (all_points == null || all_points.Count == 0 || save_inc_points == 0)
                Console.WriteLine("No inc");
            else
                Console.WriteLine(all_points[save_inc_points].size);
        }

        public void Clear_points_inst(IClear_points constr_points_to_clear) {
            points_to_clear = constr_points_to_clear;
        }
        public void Clear_points() {

            if (points_to_clear != null) {

                int points = points_to_clear.Get_points_to_clear(all_points);
                while (points > 0) {
                    all_points.RemoveAt(0);
                    points--;
                }
            }
            Console.WriteLine(all_points.Count);
        }
    }
}