using System;
using System.IO;

namespace lab_4 {
    public  class File_info {

        private readonly FileInfo file_info; // for private enter
        public bool change_check = true;

        public string FullName => file_info.FullName;
        public long Length => file_info.Length;
        public DirectoryInfo Directory => file_info.Directory;
        public DateTime Last_change_time => file_info.LastWriteTime;
        public File_info(string path) => file_info = new FileInfo(path);
    }
}