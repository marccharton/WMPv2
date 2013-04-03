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
        private List<CurrentListObject> _list;
        private List<CurrentListObject> _originalList;
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
            _list = new List<CurrentListObject>();
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
                _list.Add(new CurrentListObject() { Content = s, Index = this.findMinIdx()});
                if (this.Shuffle) 
                {
                    this.ResetRandom();
                    this.Random();
                }
                this.ModifiedEvent(this, null);
            }
        }

        public void removeElement(int idx)
        {
            if (idx >= 0)
            {
                try
                {
                    this._list.RemoveAt(idx);
                    this.ModifiedEvent(this, null);
                    if (idx < this._idx)
                        this._idx--;
                    else if (idx == this._idx)
                        this.ChangedEvent(this, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }
            }
        }

        public void addList(List<String> l)
        {
            if (l != null)
            {
                foreach (String s in l)
                {
                    if (s != null)
                        _list.Add(new CurrentListObject() { Content = s, Index = this.findMinIdx() });
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
            return (_list.ElementAt(_idx).Content);
        }

        public String getNextElement()
        {
            if (_idx + 1 == _list.Count)
                return (_list.ElementAt(0).Content);
            return (_list.ElementAt(_idx + 1).Content);
        }

        public String moveToNextElement() 
        {
            if (_idx + 1 == _list.Count)
                _idx = 0;
            else
                ++_idx;
            return (_list.ElementAt(_idx).Content);
        }

        public String moveToIdx(int idx) 
        {
            if (idx >= this._list.Count || idx < 0)
                return (null);
            this._idx = idx;
            return (this._list.ElementAt(this._idx).Content);
        }

        public String getPrevElement()
        {
            if (_idx - 1 < 0)
                return (_list.ElementAt(_list.Count - 1).Content);
            return (_list.ElementAt(_idx - 1).Content);
        }

        public String moveToPrevElement()
        {
            if (_idx - 1 < 0)
                _idx = _list.Count - 1;
            else
                --_idx;
            return (_list.ElementAt(_idx).Content);
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
            CurrentListObject s;

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

            s = this._list.ElementAt(this._idx).Content;
            this._list = this._originalList;
            this._idx = this._list.FindIndex(song => song.Content == s);
        }

        public List<CurrentListObject> getAllElement() 
        {
            return (this._list);
        }

        public void InsertAt(int idx, string item) 
        {
            if (idx >= 0 && item != null)
            {
                this._list.Insert(idx, new CurrentListObject() { Content = item, Index = this.findMinIdx() });
                this.ModifiedEvent(this, null);
            }
        }

        public void RemoveAt(int idx) 
        {
            this._list.RemoveAt(idx);
            this.ModifiedEvent(this, null);
        }

        private int findMinIdx() 
        {
            int max = -1;
            foreach (CurrentListObject t in this._list)
            {
                if (t.Index > max)
                    max = t.Index;
            }
            return (max + 1);
        }

        public int getCurrentElementIdx() 
        {
            return (this._idx);
        }

        public void setIsPlaying(bool isplay, int idx) 
        {
            if (idx >= 0 && idx < this._list.Count)
            {
                this._list.ElementAt(idx).IsPlaying = isplay;
                this.ModifiedEvent(this, null);
            }
        }
    }
}
