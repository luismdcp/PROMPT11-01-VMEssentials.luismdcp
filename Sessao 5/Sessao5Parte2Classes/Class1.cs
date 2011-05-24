using System;

namespace Sessao5Parte2Classes
{
    interface IBoat
    {
        void Move();
        void Dock();
    }

    interface ICar
    {
        void Move();
        void UpGear();
        void DownGear();
    }

    class SailBoat : IBoat
    {
        public SailBoat() { Console.WriteLine("Is a SailBoat"); }
        void IBoat.Dock() { throw new NotImplementedException(); }
        void IBoat.Move() { throw new NotImplementedException(); }
        public bool AuxiliaryMotor { set; get; }
    }

    class SportCar : ICar
    {
        public SportCar() { Console.WriteLine("Is a SportCar"); }
        void ICar.Move() { throw new NotImplementedException(); }
        void ICar.UpGear() { throw new NotImplementedException(); }
        void ICar.DownGear() { throw new NotImplementedException(); }
        public int MaxRotations { set; get; }
    }
}