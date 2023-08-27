using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainings
{
    public interface IController
    {
        ObservableCollection<Training> Trainings { get; }

        List<string> Clients { get; }

        int this[string clientName] { get; }

        void addUniqueTraining(Training training);

        void edit(Training oldItem, Training newItem);

        void saveData();

        void loadData();

        void loadData(string path);
    }
}
