using System.Threading;

namespace lab_6 {
    class Start {
        static void Main() {

            //Team

            int leader_1 = Team.Add_worker("L"); // Team is static

            int worker_1 = Team.Add_worker("D");
            Team.Add_pair(leader_1, worker_1); // Workers ID

            int leader_2 = Team.Add_worker("T");
            Team.Add_pair(leader_1, leader_2);

            int worker_2 = Team.Add_worker("M");
            Team.Add_pair(leader_2, worker_2);


            //Tasks

            Work_controller tasks = new Work_controller("keeping", "keeping");

            int task_1 = tasks.Add_task("Task_1", "First task descriprion"); // name and description
            int task_2 = tasks.Add_task("Task_2", "Second task descriprion");
            int task_3 = tasks.Add_task("Task_3", "Third task descriprion");


            //Appoint

            tasks.Appoint_worker(task_1, worker_2);
            tasks.Appoint_worker(task_2, worker_1);
            tasks.Appoint_worker(task_3, leader_2);


            //Work

            tasks.Edit_description(task_2, "You can start task_2");
            tasks.Edit_task_name(task_2, "SUPERTASK");
            tasks.Edit_main_state(task_2, All_states.Active);
            tasks.Appoint_worker(task_3, leader_1);
            tasks.Edit_description(task_2, "Task_2 in time");
            
            Thread.Sleep(6666);


            //Results

            foreach (var result in tasks.Get_sprint_reports()) {
                result.Write_results();
            }
        }
    }
}