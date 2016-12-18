namespace WebCollector.Actions
{
    using System;
    using System.Threading;
    using SoftwareControllerApi.Action;
    using SoftwareControllerLib.Action;
    using SoftwareControllerLib.Utils;

    /// <summary>
    /// Pauses the current thread for a random amount of time generated within the given limits.
    /// </summary>
    public class WaitAction : IWaitAction
    {
        private readonly Random m_Rand = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitAction"/> class with the minimum and maximum wait time. 
        /// </summary>
        /// <param name="min">The minimum number of milliseconds to wait for.</param>
        /// <param name="max">The maximum number of milliseconds to wait for.</param>
        public WaitAction(int min, int max)
        {
            if (min < 0) throw new ArgumentOutOfRangeException("min", "Cannot be a negative number");
            if (max < 0) throw new ArgumentOutOfRangeException("max", "Cannot be a negative number");
            if (max < min) throw new ArgumentException("The maximum value must be greater than or equal to the minimum value", "max");

            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitAction"/> class with the minimum and maximum wait time. 
        /// </summary>
        /// <param name="name">The name of the wait action.</param>
        /// <param name="min">The minimum number of milliseconds to wait for.</param>
        /// <param name="max">The maximum number of milliseconds to wait for.</param>
        public WaitAction(string name, int min, int max) : this(min, max)
        {
            if (name == null) throw new ArgumentNullException("name", "Cannot be null");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Cannot be empty of whitespace only", "name");

            Name = name;
        }

        /// <summary>
        /// Get the number of milliseconds to wait for.
        /// </summary>
        public int Milliseconds
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the minimum number of milliseconds to wait for.
        /// </summary>
        public int Minimum
        {
            get;
            set;
        }

        /// <summary>
        /// Get the maximum number of milliseconds to wait for.
        /// </summary>
        public int Maximum
        {
            get;
            set;
        }

        /// <summary>
        /// Get the name of the action.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Execute the wait action.
        /// </summary>
        /// <returns>The result of the wait action.</returns>
        public IResult Execute()
        {
            Milliseconds = m_Rand.Next(Minimum, Maximum);
            ConsoleOutput.Instance.Message(string.Format("Waiting for {0} ms", Milliseconds));
            Thread.Sleep(Milliseconds);

            return new Result(null, ActionState.SUCCESS);
        }
    }
}
