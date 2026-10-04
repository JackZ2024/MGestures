using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGestures.App.Backup
{
    internal class BackupException : Exception
    {
        public BackupException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }

        public BackupException(string message): base(message)
        {

        }
    }
}
