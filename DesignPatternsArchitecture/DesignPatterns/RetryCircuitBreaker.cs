using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public static class RetryHelper
    {
        public static void RetryOnException(int times, TimeSpan delay, Action operation)
        {
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    Console.WriteLine("Attempted " + attempts);
                    operation();
                    break;
                }
                catch (Exception e) 
                {
                    if (attempts == times)
                    {
                        throw;
                    }
                    
                    Task.Delay(delay).Wait();
                }
            }
            while (true);
        }
    }

    public class CircuitBreaker
    {
        private Action _currentAction;
        private int _failureCount = 0;
        private System.Timers.Timer _timer = null;
        private readonly int _timeout = 0;
        private readonly int _threshold = 0;

        private CircuitState State { get; set; }
        public enum CircuitState
        {
            Closed,
            Open,
            HalfOpen
        }
        public CircuitBreaker(int threshold, int timeout) 
        { 
            State = CircuitState.Closed;
           _timeout = timeout;
            _threshold = threshold;
        }

        public void ExecuteAction(Action action)
        {
            _currentAction = action;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _failureCount++;
                if(State == CircuitState.HalfOpen)
                {
                    return;
                }
                if(_failureCount <= _threshold)
                {
                    Console.WriteLine("Trying..."+_failureCount+"times");
                    Invoke();
                }

                else if (_failureCount > _threshold)
                {
                    Trip();
                }
            }
        }

        public void Invoke()
        {
            ExecuteAction(_currentAction);
        }

        public void Trip() 
        { 
            if(State != CircuitState.Open)
            {
                ChangeState(CircuitState.Open);
            }
            _timer = new System.Timers.Timer(_timeout);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        public void ChangeState(CircuitState state) { }
        public void Reset()
        {
            ChangeState(CircuitState.Closed);
            _timer.Stop();
        }

        private void TimerElapsed(object sender,  System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("One last try");
            if(State == CircuitState.Open)
            {
                ChangeState(CircuitState.HalfOpen);
                _timer.Elapsed -= TimerElapsed;
                _timer.Stop();
                Invoke();
            }
        }
    }
}
