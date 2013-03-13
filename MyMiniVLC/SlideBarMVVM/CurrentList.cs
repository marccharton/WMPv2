using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlideBarMVVM
{
    public static class CurrentList
    {
        private static List<String> list = new List<string>();
        private static RepeatState repeat = RepeatState.NoRepeat;
        private static int idx = 0;

        public static void addElement(String s)
        {
            if (s != null)
                list.Add(s);
        }

        public static void addList(List<String> l)
        {
            if (l != null)
            {
                foreach (String s in l)
                {
                    if (s != null)
                        list.Add(s);
                }
            }
        }

        public static String getCurrentElement() 
        {
            return (list.ElementAt(idx));
        }

        public static String getNextElement()
        {
            if (idx + 1 == list.Count)
                return (list.ElementAt(0));                
            return (list.ElementAt(idx + 1));
        }

        public static String moveToNextElement() 
        {
            if (idx + 1 == list.Count)
                idx = 0;
            else
                ++idx;
            return (list.ElementAt(idx));
        }

        public static String getPrevElement()
        {
            if (idx - 1 < 0)
                return (list.ElementAt(list.Count - 1));
            return (list.ElementAt(idx - 1));
        }

        public static String moveToPrevElement()
        {
            if (idx - 1 < 0)
                idx = list.Count - 1;
            else
                --idx;
            return (list.ElementAt(idx));
        }

        public static int getSize() 
        {
            return (list.Count);
        }

        public static Boolean HasNextElement() 
        {
            if (idx + 1 >= list.Count)
                return (false);
            return (true);
        }

        public static void ResetIdx() 
        {
            idx = 0;
        }

        public static void ResetList() 
        {
            idx = 0;
            list.Clear();
        }

        public static void setRepeat(RepeatState r) 
        {
            repeat = r;
        }

        public static RepeatState getRepeat() 
        {
            return (repeat);
        }
    }
}
