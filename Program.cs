using System;

namespace Cosashilos2
{
    using System;
    using System.Threading;
    using Timers = System.Timers;

    namespace prioridadeshilos
    {

        class Test
        {
            static void Main()
            {
                PriorityTest priorityTest = new PriorityTest();

                Thread thread1 = new Thread(priorityTest.ThreadMethod);
                thread1.Name = "ThreadOne";
                Thread thread2 = new Thread(priorityTest.ThreadMethod);
                thread2.Name = "ThreadTwo";
                Thread thread3 = new Thread(priorityTest.ThreadMethod);
                thread3.Name = "ThreadThree";
                Thread thread4 = new Thread(priorityTest.ThreadMethod);
                thread4.Name = "ThreadFour";
                Thread thread5 = new Thread(priorityTest.ThreadMethod);
                thread5.Name = "ThreadFive";

                thread1.Start();
                Thread.Sleep(100);
                thread2.Start();
                Thread.Sleep(200);
                thread3.Start();
                Thread.Sleep(300);
                thread4.Start();
                Thread.Sleep(400);
                thread5.Start();
                Thread.Sleep(500);


                priorityTest.LoopSwitch = false;
            }
        }

        class PriorityTest
        {
            //Use the static modifier to declare a static member, which belongs to the type itself rather than to a specific object.
            //The volatile keyword indicates that a field might be modified by multiple threads that are executing at the same time.
            static volatile bool loopSwitch;
            //ThreadStatic indicates that the value of a static field is unique for each thread.
            [ThreadStatic] static long threadCount = 0;

            public PriorityTest()
            {
                loopSwitch = true;
            }

            public bool LoopSwitch
            {
                set { loopSwitch = value; }
            }

            public void ThreadMethod()
            {
                while (loopSwitch)
                {
                    threadCount++;
                }
                Console.WriteLine("{0,-11} with {1,11} priority " +
                    "has a count = {2,13}", Thread.CurrentThread.Name,
                    Thread.CurrentThread.Priority.ToString(),
                    threadCount.ToString("N0"));
            }
        }

    }
    // > dotnet run
    // ThreadThree with AboveNormal priority has a count =   639,008,945
    // ThreadOne   with      Normal priority has a count =   608,231,359
    // ThreadTwo   with BelowNormal priority has a count =   572,596,797
}
