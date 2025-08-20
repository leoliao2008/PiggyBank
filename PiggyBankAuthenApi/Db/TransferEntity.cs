namespace PiggyBankAuthenApi.Db
{
    public class TransferEntity
    {
        //    SELECT TOP(1000) [Id]
        //  ,[UserId]
        // ,[Subject]
        // ,[Amount]
        // ,[Direction]
        //  ,[Comment]
        //  ,[PicUrl]
        // ,[CreateData]
        // ,[LastUpdateTime]
        //   FROM[PiggyBank].[dbo].[TransferRecordsTable]
        public int Id { get; set; } = 0;
        public int UserId { get; set; }
        public int Subject { get; set; }
        public float Amount { get; set; }
        public int Direction { get; set; }
        public string Comment { get; set; } = "";
        public string PicUrl { get; set; } = "";
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateTime { get; set; }
        
        public DateTime TransferDate { get; set; }
        
    }
}