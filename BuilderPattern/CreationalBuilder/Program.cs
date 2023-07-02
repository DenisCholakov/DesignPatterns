var car = CarBuilder.Create().OfType(CarType.Crossover).WithWeels(18).Build();

Console.WriteLine(car);

public enum CarType
{
    Sedan,
    Crossover
}

//when we need a specific order of building. Interface segregation
public class Car
{
    public CarType Type;
    public int WheelSize;

    public override string ToString()
    {
        return $"{Type} car with wheels size - {WheelSize}.";
    }
}


public interface ISpecifyCarType
{
    ISpecifyWheelSize OfType(CarType type);
}

public interface ISpecifyWheelSize
{
    IBuildCar WithWeels(int size);
}

public interface IBuildCar
{
    public Car Build();
}

public class CarBuilder
{
    private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
    {
        private Car car = new Car();

        public ISpecifyWheelSize OfType(CarType type)
        {
            car.Type = type;

            return this;
        }

        public IBuildCar WithWeels(int size)
        {
            switch (car.Type)
            {
                case CarType.Sedan when size < 15 || size > 17:
                case CarType.Crossover when size < 17 || size > 20:
                    throw new ArgumentException($"Wrong size of wheel for {car.Type}");
            }

            car.WheelSize = size;

            return this;
        }

        public Car Build()
        {
            return car;
        }
    }


    public static ISpecifyCarType Create()
    {
        return new Impl();
    }
}