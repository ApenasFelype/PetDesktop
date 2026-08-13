using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDesktop
{
    public class PetNeeds
    {
        private PetController Controller;
        public double Hunger { get; private set; } = 100;
        public double Thirst { get; private set; } = 100;
        public double Sleep { get; set; } = 100;

        public PetNeeds(PetController controller)
        {
            Controller = controller;
        }
        public void UpdateNeeds()
            {
                Hunger -= 0.01;
                Thirst -= 0.015;
                Sleep -= 0.008;

                Hunger = Math.Max(Hunger, 0);
                Thirst = Math.Max(Thirst, 0);
                Sleep = Math.Max(Sleep, 0);
            }

        public void Feed()
        {
            Hunger += 30;
            Hunger = Math.Min(Hunger, 100);
        }

        public void Drink()
        {
            Thirst += 30;
            Thirst = Math.Min(Thirst, 100);
        }

        public void Rest()
        {
            Controller.ChangeState(PetState.Sleeping);
            
        }
    }
}
