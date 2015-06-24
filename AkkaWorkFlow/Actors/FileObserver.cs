using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;


namespace AkkWorkflow
{
    public class FileObserver : IDisposable
    {
        private static readonly NLog.Logger LogManager = NLog.LogManager.GetCurrentClassLogger();
        private readonly IActorRef directoryScannerActor;
        private readonly IActorRef fileReceiverActor;
        private FileSystemWatcher fileSystemWatcher;
        private List<WatcherChangeTypes> watcherNotificationChangeTypes = new List<WatcherChangeTypes>();
        private CompositeDisposable fileSystemWatcherDisposables = new CompositeDisposable();

        public FileObserver(IActorRef directoryScannerActor, IActorRef fileReceiverActor)
        {
            this.directoryScannerActor = directoryScannerActor;
            this.fileReceiverActor = fileReceiverActor;

            watcherNotificationChangeTypes.Add(WatcherChangeTypes.Changed);
            watcherNotificationChangeTypes.Add(WatcherChangeTypes.Created);
            watcherNotificationChangeTypes.Add(WatcherChangeTypes.Renamed);


           // StartListening();

           // directoryScannerActor.Tell(new Exception("lkjdlkasjdklj"));

           ThrowAfter();
        }



        private void ThrowAfter()
        {
            Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(x =>
            {
                directoryScannerActor.Tell(new Exception("lkjdlkasjdklj"));
            });
        }


       

        private void StartListening()
        {

            fileSystemWatcher = new FileSystemWatcher(FileUploadRunner.AceFxUploadSettings.FtpPath, "*.xml");
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.LastWrite |
                                             NotifyFilters.CreationTime;


            //NOTE : These RX subscriptions are here due to a well known issue with FileSystemWatcher class where it fires events twice
            //solution is to use Rx to Distinct the event stream. See here for more information 
            //http://stackoverflow.com/questions/1764809/filesystemwatcher-changed-event-is-raised-twice

            fileSystemWatcherDisposables.Add(Observable.FromEventPattern<FileSystemEventArgs>(fileSystemWatcher,
                "Changed")
                .Select(e => e.EventArgs)
                .Distinct(e => e.FullPath)
                .Subscribe(e =>
                {
                    LogManager.Info("FileObserver seen 'changed' file '{0}' telling [fileValidationActor]", e.FullPath);
                    fileReceiverActor.Tell(new ProcessFile(e.FullPath), ActorRefs.NoSender);
                }));


            fileSystemWatcherDisposables.Add(Observable.FromEventPattern<FileSystemEventArgs>(fileSystemWatcher,
                "Created")
                .Select(e => e.EventArgs)
                .Distinct(e => e.FullPath)
                .Subscribe(e =>
                {
                    LogManager.Info("FileObserver seen 'created' file '{0}' telling [fileValidationActor]", e.FullPath);
                    fileReceiverActor.Tell(new ProcessFile(e.FullPath), ActorRefs.NoSender);
                }));


            fileSystemWatcherDisposables.Add(Observable.FromEventPattern<FileSystemEventArgs>(fileSystemWatcher,
                "Renamed")
                .Select(e => e.EventArgs)
                .Distinct(e => e.FullPath)
                .Subscribe(e =>
                {
                    LogManager.Info("FileObserver seen 'renamed' file '{0}' telling [fileValidationActor]", e.FullPath);
                    fileReceiverActor.Tell(new ProcessFile(e.FullPath), ActorRefs.NoSender);
                }));



            fileSystemWatcher.Error += OnFileError;
            fileSystemWatcher.EnableRaisingEvents = true;



        }

        public void Dispose()
        {
            fileSystemWatcher.Error -= OnFileError;
            fileSystemWatcherDisposables.Dispose();
            fileSystemWatcher.Dispose();
        }


        private void OnFileError(object sender, ErrorEventArgs e)
        {
            LogManager.Error("FileObserver error '{0}' telling [fileReceiverActor]", e.GetException().Message);
            throw e.GetException();
        }


    }

}
