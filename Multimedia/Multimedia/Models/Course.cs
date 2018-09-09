using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Multimedia.Models
{
    class Course : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string ObjectivesDescription { get; set; }

        public List<Objective> Objectives { get; set; }

        public string CareersDescription { get; set; }

        public List<Career> Careers { get; set; }

        public List<Contact> Contacts { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
