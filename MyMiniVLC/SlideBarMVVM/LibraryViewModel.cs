using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SlideBarMVVM
{
    class LibraryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string myText;
        public string MyText
        {
            get
            {
                return myText;
            }
            set
            {
                if (this.myText != value)
                {
                    this.myText = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("MyTEXT"));
                }
            }
        }
    }

}
