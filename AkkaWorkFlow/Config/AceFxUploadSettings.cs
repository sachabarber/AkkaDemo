using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaWorkFlow.Config
{
    public class AceFxUploadSettings
    {
        public string FtpPath { get; set; }
        public string FtpProcessedPath { get; set; }
        public string FtpRejectedPath { get; set; }
        public string FtpProcessedWithErrorsPath { get; set; }
        public string FtpUnhandledExceptionsPath { get; set; }
        public string FtpProcessingPath { get; set; }
        public string ProcessedFileSuffix { get; set; }
        public string RejectedFileSuffix { get; set; }
        public int FileCacheCount { get; set; }
        public Retries Retries { get; set; }
    }

    public class Retries
    {
        public int RetryCount { get; set; }
        public int DurationBetweenRetriesInSeconds { get; set; }
    }
}
