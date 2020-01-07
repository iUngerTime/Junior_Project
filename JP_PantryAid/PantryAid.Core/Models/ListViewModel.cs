using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace PantryAid.Core.Models
{

    //Made with generics so that the Grocery list and the Pantry can reuse

    public class ListViewModel<T> : INotifyPropertyChanged
    {
        public ObservableCollection<T> ListView { get; set; }

        public ListViewModel()
        {
            ListView = new ObservableCollection<T>();
        }

        public void Add(T item)
        {
            ListView.Add(item);
        }

        //INotifyPropertyChanged Boilerplate code
        #region ListViewImplementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion
    }

}
