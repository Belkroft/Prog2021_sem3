using System;
using System.Collections.Generic;

namespace lab_4 {
    class Start {
        static void Main() {
            
            // CASE 1

            Backup c1 = new Backup(new List<string>(){"D:/3 sem/OOP/lab_4/text_1.txt", "D:/3 sem/OOP/lab_4/text_2.txt"});

            c1.Create_point(new Full_point());
            c1.Create_point(new Full_point());

            c1.Clear_points_inst(new Clear_count(1));
            c1.Clear_points();

            Console.WriteLine("------------------------");


            // CASE 2
            
            // (3915 + 3915) = 7830b
            Backup c2 = new Backup(new List<string>(){"D:/3 sem/OOP/lab_4/text_1.txt", "D:/3 sem/OOP/lab_4/text_2.txt"}); 

            c2.Create_point(new Full_point());
            c2.Create_point(new Full_point());

            c2.Show_backup_size();

            c2.Clear_points_inst(new Clear_to_size(7829)); //bytes
            c2.Clear_points();
        }
    }
}