using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace Trainings
{
    public class TimetableController: IController
    {
        private ObservableCollection<Training> trainings = new ObservableCollection<Training>();
        private const string path = @"../../../trainings.txt";

        public ObservableCollection<Training> Trainings
        {
            get
            {
                return trainings;
            }
        }

        public List<string> Clients
        {
            get
            {
                HashSet<string> clients = new HashSet<string>();
                foreach (Training training in Trainings)
                    clients.Add(training.ClientName);
                return clients.ToList();    
            }
        }

        public int this[string clientName]
        {
            get
            {
                int cost = 0;
                bool isClientExist = false;
                foreach (Training training in Trainings)
                    if (training.ClientName == clientName)
                    {
                        cost += training.Price;
                        isClientExist = true;
                    }
                return isClientExist ? cost : -1;
            }
        }

        public void addUniqueTraining(Training training)
        {
            if (trainings.Contains(training))
                throw new FormatException("Это время уже занято!");
            trainings.Add(training);
            trainings.Sort();
        }

        public void edit(Training oldItem, Training newItem)
        {
            trainings.Remove(oldItem);
            addUniqueTraining(newItem);
        }

        public void saveData()
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create))) {
                foreach (Training training in trainings)
                    writer.Write(training);
            }
        }

        public void loadData()
        {
            if (File.Exists(path))
                loadData(path);
        }

        public void loadData(string path)
        {
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Training training = new Training();

                    training.ClientName = line;

                    line = reader.ReadLine();
                    switch (line)
                    {
                        case "Cash":
                            training.PayMethod = PayMethod.Cash;
                            break;
                        case "Card":
                            training.PayMethod = PayMethod.Card;
                            break;
                    }

                    line = reader.ReadLine();
                    training.TimeFrom = DateTime.Parse(line);

                    line = reader.ReadLine();
                    training.TimeTo = DateTime.Parse(line);

                    line = reader.ReadLine();
                    training.Price = int.Parse(line);

                    addUniqueTraining(training);
                }
            }
        }

    }
}
