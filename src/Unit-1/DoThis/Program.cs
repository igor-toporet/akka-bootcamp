using Akka.Actor;

namespace WinTail
{
    public static class Program
    {
        private static readonly ActorSystem MyActorSystem = ActorSystem.Create("MyActorSystem");

        public static void Main()
        {
            var consoleWriterActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleWriterActor()));
            var validationActor = MyActorSystem.ActorOf(Props.Create(() => new ValidationActor(consoleWriterActor)));
            var consoleReaderActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleReaderActor(validationActor)));

            // tell console reader to begin
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }
    }
}