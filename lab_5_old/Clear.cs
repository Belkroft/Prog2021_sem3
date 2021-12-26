using System;
using System.Collections.Generic;

namespace lab_4 {
    public interface IClear_points {
        public int Get_points_to_clear(List<Points> points);
    }

    public class Clear_count : IClear_points {

        private int limit_count;

        public Clear_count(int num) {
            limit_count = num;
        }

        public int Get_points_to_clear(List<Points> points) {

            if (points.Count > limit_count) {

                int p = points.Count - limit_count;
                while (points[p - 1].inc_check)
                    p--;

                if (!points[p].inc_check)
                    return p;
                limit_count++;
                return Get_points_to_clear(points);

            } else return 0;
        }
    }

    public class Clear_to_size : IClear_points {

        private readonly int size_limit;
        public Clear_to_size(int size) => size_limit = size;

        public int Get_points_to_clear(List<Points> points) {

            List<KeyValuePair<long, int>>
                sizes = new List<KeyValuePair<long, int>>();
            int p = 0;
            long full_size = 0;

            while (p+1 < points.Count) {

                if (!points[p].inc_check) {

                    long size = points[p].size;
                    p++;
                    int count = 1;

                    while (p < points.Count && points[p].inc_check) {
                        size += points[p].size;
                        count++;
                        p++;
                    }

                    sizes.Add(new KeyValuePair<long, int>(size, count));
                    full_size += size;
                }
            }

            p = 0;
            if (sizes.Count > 0) {

                int points_to_clear = 0;
                while (full_size > size_limit ) {
                    full_size -= sizes[p].Key;
                    points_to_clear += sizes[p].Value;
                    p++;
                }
                return points_to_clear;
            }
            return 0;
        }
    }

    public class Clear_to_time : IClear_points {

        private readonly DateTime time_limit;

        public Clear_to_time(DateTime restrictionTime) {
            time_limit = restrictionTime;
        }

        public int Get_points_to_clear(List<Points> restorePoints) {

            var timesLastRestore = new List<KeyValuePair<DateTime, int>>();

            int i = 0;
            while (i < restorePoints.Count) {

                if (!restorePoints[i].inc_check) {

                    DateTime last = restorePoints[i].time;
                    i++;
                    int count = 1;

                    while (restorePoints[i].inc_check) {

                        last = restorePoints[i].time;
                        i++;
                        count++;
                    }
                    timesLastRestore.Add(new KeyValuePair<DateTime, int>(last, count));
                }
            }

            i = 0;
            int countToDel = 0;
            while (timesLastRestore[i].Key < time_limit) {
                countToDel += timesLastRestore[i].Value;
                i++;
            }
            return 0;
        }
    }

    public class ClearGybrid : IClear_points {

        private readonly bool check_max;
        private readonly List<IClear_points> algos;

        public ClearGybrid(bool max, List<IClear_points> algorhytms) {
            check_max = max;
            algos = algorhytms;
        }

        public int Get_points_to_clear(List<Points> restorePoints) {

            int clear_count = check_max ? 0 : 100000000; //MAX
            for (int i = 0; i < algos.Count; i++) {
                IClear_points clear = algos[i];
                clear_count = check_max
                    ? Math.Max(clear_count, clear.Get_points_to_clear(restorePoints))
                    : Math.Min(clear_count, clear.Get_points_to_clear(restorePoints));
            }
            return clear_count;
        }
    }
}