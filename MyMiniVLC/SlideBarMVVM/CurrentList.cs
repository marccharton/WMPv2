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
        private int _shuffleIdx;
        private List<CurrentListObject> _shuffleList;
        private Boolean _shuffle;
        public Boolean Shuffle
        {
            get
            {
                return (this._shuffle);
            }
            set
            {
                if (this._shuffle != value)
                {
                    this._shuffle = value;
                    if (this._shuffle)
                        this.Random();
                }
            }
        }
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
        public EventHandler DropEvent;
        public EventHandler ChangedEvent;
        public EventHandler ModifiedEvent;
        public Boolean IsMovingPosition { get; set; }

        private CurrentList()
        {
            this._idx = 0;
            this._shuffleIdx = 0;
            this._list = new List<CurrentListObject>();
            this._shuffleList = new List<CurrentListObject>();
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
                _list.Add(new CurrentListObject() { Content = s, Index = this.findMinIdx() });
                if (this.Shuffle)
                    this.Random();
                this.ModifiedEvent(this, null);
            }
        }

        public void removeElement(int idx)
        {
            int idxshuffl = -1;

            if (idx >= 0 && idx < _list.Count)
            {
                try
                {
                    idxshuffl = this._shuffleList.FindIndex(song => song == this._list.ElementAt(idx));
                    this._shuffleList.Remove(this._list.ElementAt(idx));
                    this._list.RemoveAt(idx);
                    this.ModifiedEvent(this, null);
                    if (Shuffle)
                    {
                        if (idxshuffl == -1)
                        {
                            this.ResetList();
                            this.ChangedEvent(this, null);
                        }
                        else if (idxshuffl < _shuffleIdx)
                        {
                            this._shuffleIdx--;
                            this._idx = this._list.FindIndex(song => song == this._shuffleList.ElementAt(_shuffleIdx));
                            if (this._idx == -1)
                            {
                                this.ResetList();
                                this.ChangedEvent(this, null);
                            }
                        }
                        else if (idxshuffl == _shuffleIdx)
                        {
                            if (this._shuffleIdx >= this._shuffleList.Count)
                                this._shuffleIdx = 0;
                            this._idx = this._list.FindIndex(song => song == this._shuffleList.ElementAt(_shuffleIdx));
                            if (this._idx == -1)
                                this.ResetList();
                            this.ChangedEvent(this, null);
                        }
                        else 
                        {
                            this._idx = this._list.FindIndex(song => song == this._shuffleList.ElementAt(_shuffleIdx));
                            if (this._idx == -1)
                            {
                                this.ResetList();
                                this.ChangedEvent(this, null);
                            }
                        }
                        return;
                    }
                    if (idx < this._idx)
                        this._idx--;
                    else if (idx == this._idx)
                    {
                        if (this._idx >= this._list.Count)
                            this._idx = 0;
                        this.ChangedEvent(this, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "coucou");
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
                    this.Random();
                this.ModifiedEvent(this, null);
            }
        }

        public String getCurrentElement()
        {
            if (this._list.Count == 0)
                return ("Error: getCurrentElement: No element in list");
            if (this.Shuffle)
                return (this._shuffleList.ElementAt(_shuffleIdx).Content);
            return (_list.ElementAt(_idx).Content);
        }

        public String getNextElement()
        {
            if (_list.Count == 0)
                return ("Error: getNextElement: No element in list");
            if (this.Shuffle)
            {
                if (this._shuffleIdx + 1 >= _shuffleList.Count)
                    return (_shuffleList.ElementAt(0).Content);
                return (_shuffleList.ElementAt(_shuffleIdx + 1).Content);
            }
            if (_idx + 1 == _list.Count)
                return (_list.ElementAt(0).Content);
            return (_list.ElementAt(_idx + 1).Content);
        }

        public String moveToNextElement()
        {
            if (this._list.Count == 0)
                return ("Error: moveToNextElement: No element in list");
            if (this.Shuffle)
            {
                if (this._shuffleIdx + 1 >= this._shuffleList.Count)
                    this._shuffleIdx = 0;
                else
                    this._shuffleIdx++;
                this._idx = this._list.FindIndex(song => song == this._shuffleList.ElementAt(this._shuffleIdx));
                if (this._idx == -1)
                {
                    this._idx = 0;
                    return ("Error: moveToNextElement: Internal problem");
                }
                return (this._shuffleList.ElementAt(this._shuffleIdx).Content);
            }
            if (_idx + 1 >= _list.Count)
                _idx = 0;
            else
                ++_idx;
            return (_list.ElementAt(_idx).Content);
        }

        public String moveToIdx(int idx)
        {
            if (idx >= this._list.Count || idx < 0)
                return ("Error: moveToIdx: index out of bound");
            this._idx = idx;
            if (Shuffle)
            {
                this._shuffleIdx = this._shuffleList.FindIndex(song => song == this._list.ElementAt(this._idx));
                if (this._shuffleIdx == -1)
                {
                    this._shuffleIdx = 0;
                    return ("Error: moveToIdx: Internal problem");
                }
            }
            return (this._list.ElementAt(this._idx).Content);
        }

        public String getPrevElement()
        {
            if (_list.Count == 0)
                return ("Error: getPrevElement: No element in list");
            if (this.Shuffle)
            {
                if (this._shuffleIdx - 1 < 0)
                    return (_shuffleList.ElementAt(_shuffleList.Count - 1).Content);
                return (_shuffleList.ElementAt(_shuffleIdx - 1).Content);
            }
            if (_idx - 1 < 0)
                return (_list.ElementAt(_list.Count - 1).Content);
            return (_list.ElementAt(_idx - 1).Content);
        }

        public String moveToPrevElement()
        {
            if (this._list.Count == 0)
                return ("Error: moveToPrevElement: No element in list");
            if (this.Shuffle)
            {
                if (this._shuffleIdx - 1 < 0)
                    this._shuffleIdx = _shuffleList.Count - 1;
                else
                    this._shuffleIdx--;
                this._idx = this._list.FindIndex(song => song == this._shuffleList.ElementAt(this._shuffleIdx));
                if (this._idx == -1)
                {
                    this._idx = 0;
                    return ("Error: moveToPrevElement: Internal problem");
                }
                return (this._shuffleList.ElementAt(this._shuffleIdx).Content);
            }
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
            if (Shuffle)
            {
                if (_shuffleIdx + 1 >= _shuffleList.Count)
                    return (false);
                return (true);
            }
            if (_idx + 1 >= _list.Count)
                return (false);
            return (true);
        }

        public void ResetIdx()
        {
            if (_list.Count == 0)
            {
                this._idx = 0;
                this._shuffleIdx = 0;
                return;
            }
            if (Shuffle)
            {
                _shuffleIdx = 0;
                _idx = this._list.FindIndex(song => song == this._shuffleList.ElementAt(this._shuffleIdx));
                if (this._idx == -1)
                    this._idx = 0;
            }
            _idx = 0;
        }

        public void ResetList()
        {
            _idx = 0;
            _shuffleIdx = 0;
            _list.Clear();
            _shuffleList.Clear();
            this.ModifiedEvent(this, null);
        }

        public void Random()
        {
            CurrentListObject cur = null;

            if (this._list.Count > 0)
            {
                this._shuffleList.Clear();
                foreach (CurrentListObject clo in this._list)
                {
                    if (clo.IsPlaying)
                        cur = clo;
                    else
                        this._shuffleList.Add(clo);
                }
                this._shuffleList = this._shuffleList.OrderBy(song => Guid.NewGuid()).ToList();
                if (cur != null)
                    this._shuffleList.Insert(0, cur);
                _shuffleIdx = 0;
                this._idx = this._list.FindIndex(song => song == this._shuffleList.ElementAt(_shuffleIdx));
                if (this._idx == -1)
                {
                    this.ResetList();
                    this.ChangedEvent(this, null);
                }
            }
        }

        public List<CurrentListObject> getAllElement()
        {
            return (this._list);
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

        public void switchElementIdx(int idxfrom, int idxto)
        {
            CurrentListObject cur;

            if (idxfrom >= 0 && idxfrom < this._list.Count &&
                idxto >= 0 && idxto < this._list.Count && idxfrom != idxto)
            {
                cur = this._list.ElementAt(idxfrom);
                this._list.RemoveAt(idxfrom);
                this._list.Insert(idxto, cur);
                if (cur.IsPlaying)
                    this._idx = idxto;
                this.ModifiedEvent(this, null);
            }
        }
    }
}
