using System;
using Windows.ApplicationModel.Background;
using System.Runtime.InteropServices;

namespace BackgroundTaskWinMainComSample_CS
{
    // The TimeTriggeredTask must be visible to COM and must be given a GUID such
    // that the system can identify this entry point and launch it as necessary
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("087DC07B-FDA5-4C01-8BB7-18863C3EE597")]
    [ComSourceInterfaces(typeof(IBackgroundTask))]
    public class TimeTriggeredTask : IBackgroundTask
    {
        // This is the flag the cancellation handler signals so the Run thread may exit.
        private volatile int cleanupTask = 0;

        // The largest number up to which this task will calculate primes.
        private const int maxPrimeNumber = 100000;

        /// <summary>
        /// This method determines whether the specified number is a prime number.
        /// </summary>
        private bool IsPrimeNumber(int dividend)
        {
            bool isPrime = true;
            for (int divisor = dividend - 1; divisor > 1; divisor -= 1)
            {
                if ((dividend % divisor) == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            return isPrime;
        }

        /// <summary>
        /// This method returns the next prime number given the last calculated number.
        /// </summary>
        private int GetNextPrime(int previousNumber)
        {
            int currentNumber = previousNumber + 1;
            while (!IsPrimeNumber(currentNumber))
            {
                currentNumber += 1;
            }

            return currentNumber;
        }

        public TimeTriggeredTask()
        {
            return;
        }

        ~TimeTriggeredTask()
        {
            return;
        }

        /// <summary>
        /// This method (declared by IBackgroundTask) is the entry point method
        /// for the sample background task. Once this method returns, the system
        /// understands that this background task has completed.
        ///
        /// This method will calculate primes up to a predefined maximum value.
        /// When the system requests this background task be canceled, the
        /// cancellation handler will set a member flag such that this thread
        /// will stop calculating primes and return.
        /// </summary>
        [MTAThread]
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Start with the first applicable number.
            int currentNumber = 1;

            // Add the cancellation handler.
            taskInstance.Canceled += OnCanceled;

            // Calculate primes until a cancellation has been requested or until the maximum
            // number is reached.
            while ((cleanupTask == 0) && (currentNumber < maxPrimeNumber))
            {
                taskInstance.Progress = (uint)(currentNumber / maxPrimeNumber);

                // Compute the next prime number and add it to our queue.
                currentNumber = GetNextPrime(currentNumber);
            }
        }

        /// <summary>
        /// This method is the cancellation event handler method. This is called when the system requests
        /// the background task be canceled. this method will set a member flag such that the Run thread
        /// will stop calculating primes and flush the remaining values to disk.
        /// </summary>
        [MTAThread]
        public void OnCanceled(IBackgroundTaskInstance taskInstance, BackgroundTaskCancellationReason cancellationReason)
        {
            // Set the flag to indicate to the main thread that it should stop performing
            // work and exit.
            cleanupTask = 1;

            return;
        }
    }
}
