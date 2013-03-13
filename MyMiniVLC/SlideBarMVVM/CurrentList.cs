using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SlideBarMVVM
{
    public class CurrentList
    {
        private static CurrentList _object = null;
        
        private int _idx;
        private List<String> _list;
        public RepeatState Repeat { get; set; }

        private CurrentList() 
        {
            _idx = 0;
            _list = new List<string>();
            this.Repeat = RepeatState.NoRepeat;
        }

        public static CurrentList getInstance()
        {
            if (_object == null)
                _object = new CurrentList();
            return (_object);
        }

        //private static List<String> list = new List<string>();
        //private static RepeatState repeat = RepeatState.NoRepeat;
        //private static int idx = 0;
        //public static EventHandler DropEvent;

        public void addElement(String s)
        {
            if (s != null)
                _list.Add(s);
        }

        public void addList(List<String> l)
        {
            if (l != null)
            {
                foreach (String s in l)
                {
                    if (s != null)
                        _list.Add(s);
                }
            }
        }

        public String getCurrentElement() 
        {
            return (_list.ElementAt(_idx));
        }

        public String getNextElement()
        {
            if (_idx + 1 == _list.Count)
                return (_list.ElementAt(0));
            return (_list.ElementAt(_idx + 1));
        }

        public String moveToNextElement() 
        {
            if (_idx + 1 == _list.Count)
                _idx = 0;
            else
                ++_idx;
            return (_list.ElementAt(_idx));
        }

        public String getPrevElement()
        {
            if (_idx - 1 < 0)
                return (_list.ElementAt(_list.Count - 1));
            return (_list.ElementAt(_idx - 1));
        }

        public String moveToPrevElement()
        {
            if (_idx - 1 < 0)
                _idx = _list.Count - 1;
            else
                --_idx;
            return (_list.ElementAt(_idx));
        }

        public int getSize() 
        {
            return (_list.Count);
        }

        public Boolean HasNextElement() 
        {
            if (_idx + 1 >= _list.Count)
                return (false);
            return (true);
        }

        public void ResetIdx() 
        {
            _idx = 0;
        }

        public void ResetList() 
        {
            _idx = 0;
            _list.Clear();
            //DropEvent(CurrentList.get, null);
        }
    }
}
