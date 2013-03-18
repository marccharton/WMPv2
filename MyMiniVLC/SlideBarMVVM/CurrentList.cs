using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO;

namespace SlideBarMVVM
{
    public class CurrentList
    {
        private static CurrentList _object = null;
        
        private int _idx;
        private List<String> _list;
        private List<String> _originalList;
        private Double _speed;
        public Double Speed
        {
            get
            { 
                return (_speed);
            }
            set 
            {
                if (this._speed != value)
                {
                    this._speed = value;
                    if (this.SpeedChanged != null)
                        this.SpeedChanged(this, null);
                }
            }
        }
        public EventHandler SpeedChanged { get; set; }
        public RepeatState Repeat { get; set; }
        public Boolean Shuffle { get; set; }
        public EventHandler DropEvent;
        public EventHandler ChangedEvent;
        public EventHandler ModifiedEvent;
        public Boolean IsMovingPosition { get; set; }

        private CurrentList() 
        {
            _idx = 0;
            _list = new List<string>();
            this.Repeat = RepeatState.NoRepeat;
            _speed = 1.0;
            this.Shuffle = false;
            this.IsMovingPosition = false;
        }

        public static CurrentList getInstance()
        {
            if (_object == null)
                _object = new CurrentList();
            return (_object);
        }

        public void addElement(String s)
        {
            if (s != null)
            {
                _list.Add(s);
                if (this.Shuffle) 
                {
                    this.ResetRandom();
                    this.Random();
                }
                this.ModifiedEvent(this, null);
            }
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
                if (this.Shuffle)
                {
                    this.ResetRandom();
                    this.Random();
                }
                this.ModifiedEvent(this, null);
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

        public String moveToIdx(int idx) 
        {
            if (idx >= this._list.Count || idx < 0)
                return (null);
            this._idx = idx;
            return (this._list.ElementAt(this._idx));
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
        }

        public void Random()
        {
            String s;

            if (this._list.Count > 0)
            {
                this._originalList = this._list;
                s = this._list.ElementAt(this._idx);
                this._list = this._list.OrderBy(song => Guid.NewGuid()).ToList();
                this._idx = 0;
                this._list.Remove(s);
                this._list.Insert(0, s);
            }
        }

        public void ResetRandom() 
        {
            String s;

            s = this._list.ElementAt(this._idx);
            this._list = this._originalList;
            this._idx = this._list.FindIndex(song => song == s);
        }

        public List<String> getAllElement() 
        {
            return (this._list);
        }

        public void InsertAfter(String s, String toAdd) 
        {
            int idx = 0;

            foreach (String tmp in this._list)
            {
                if (tmp == s)
                    break;
                ++idx;
            }

            if (idx >= this._list.Count)
            {
                 this.addElement(toAdd);
            }
            else
            {
                this._list.Insert(idx + 1, toAdd);
                this.ModifiedEvent(this, null);
            }
       }
    }
}
