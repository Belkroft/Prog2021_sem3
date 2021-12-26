using System;
using System.Collections.Generic;

namespace lab_6 {

    public class Worker {

        private static ushort new_worker_id = 0;
        public int id;
        public string name;
        public Worker Leader;

        public Dictionary<int, Worker> workers_list = new Dictionary<int, Worker>();
        public static Dictionary<int, Worker> project_team_list = new Dictionary<int, Worker>();

        public Worker(string console_name) {
            id = new_worker_id++;
            name = console_name;
            project_team_list.Add(id, this);
        }
    }

    public static class Team {

        public static int Add_worker(string console_name) => 
            new Worker(console_name).id; // work with id

        public static bool Check_worker(int id) {

            if (!Worker.project_team_list.ContainsKey(id)) {
                Console.Error.WriteLine("No such worker");
                return false;
            } else { return true;
            }
        }

        public static Worker Get_team_id(int id) => 
            Worker.project_team_list[id];

        public static void Add_pair(int leader, int new_worker) {
            if (Check_worker(leader) && Check_worker(new_worker)) {
                Worker.project_team_list[leader].workers_list.Add(new_worker, Worker.project_team_list[new_worker]); // to team
                Worker.project_team_list[new_worker].Leader = Worker.project_team_list[leader]; // to leaders list
            }
        }

        public static List<Worker> Get_workers() {

            List<Worker> list = new List<Worker>();
            foreach (var i in Worker.project_team_list) {
                list.Add(i.Value);
            }
            return list;
        }

        public static void Change_leader(int id, Worker new_leader) => 
            Worker.project_team_list[id].Leader = new_leader;
    }
}