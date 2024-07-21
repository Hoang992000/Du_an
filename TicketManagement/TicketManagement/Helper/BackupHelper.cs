namespace RFIDApp.Helper
{
    public class BackupHelper
    {
        //private LoadingWindow window;
        //public bool cancelRequested = false;
        //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        //CancellationToken cancellationToken;
        //public async void StartBackup()
        //{
        //cancellationToken = cancellationTokenSource.Token;
        //DateTime datetime = DateTime.Now;
        //SaveFileDialog saveFileDialog = new SaveFileDialog();
        //saveFileDialog.Filter = "Tệp sao lưu (*.bak)|*.bak";
        //saveFileDialog.Title = "Pick Location To Save";
        //saveFileDialog.FileName = datetime.ToString("yyyy_MM_dd") + "_BackupDB.bak";
        //if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //{
        //    string backupFilePath = saveFileDialog.FileName;


        //    Backup bkpDBFull = new Backup();
        //    try
        //    {
        //        window = new LoadingWindow(cancellationTokenSource);
        //        window.SetTitle("Backup Process");
        //        window.ShowLoading();

        //        DatabaseConnectModel database = new DatabaseConnectModel();
        //        Server dbServer = new Server(new ServerConnection(database.ServerName, database.UserName, database.Password));

        //        bkpDBFull.Action = BackupActionType.Database;
        //        bkpDBFull.Database = database.Database;
        //        bkpDBFull.Devices.AddDevice(backupFilePath, DeviceType.File);
        //        bkpDBFull.BackupSetName = "RFID Database Backup";
        //        bkpDBFull.BackupSetDescription = "RFID database - Full Backup";
        //        await Task.Delay(2000);

        //        bkpDBFull.Initialize = false;

        //        bkpDBFull.PercentCompleteNotification = 1;
        //        bkpDBFull.PercentComplete += BkpDBFull_PercentComplete; ;
        //        bkpDBFull.Complete += BkpDBFull_Complete; ;
        //        cancellationToken.ThrowIfCancellationRequested();

        //        bkpDBFull.SqlBackup(dbServer);
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        cancelRequested = true;
        //        bkpDBFull.Abort();
        //    }

        //}

        // }

        //private async void BkpDBFull_Complete(object sender, ServerMessageEventArgs e)
        //{
        //    await Task.Delay(1000);
        //    window.Close();
        //    new CustomMessageBox("Backup Successfully!", MessageType.Success, MessageButtons.Ok).ShowDialog();

        //}

        //private void BkpDBFull_PercentComplete(object sender, PercentCompleteEventArgs e)
        //{
        //    window.SetProgressBarValue(e.Percent);
        //}
    }
}
