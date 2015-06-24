using System.IO;
using Akka.Actor;
using Akka.TestKit.NUnit;
using FakeItEasy;
using NUnit.Framework;

namespace AkkaWorkFlow.Tests
{
    [TestFixture]
    public class DirectoryScannerTests : TestKit
    {
        [Test]
        public void should_request_file_processing_for_directory_when_started()
        {
            var io = A.Fake<DirectoryIO>();
            A.CallTo(() => io.GetExistingFiles()).Returns(new[] {"file1.txt", "file2.txt"});
            Sys.ActorOf(DirectoryScannerActor.Create(io, TestActor), "tds");


            ExpectMsg<ProcessFile>(x => x.FilePath == "file1.txt");
            ExpectMsg<ProcessFile>(x => x.FilePath == "file2.txt");
        }
    }
}