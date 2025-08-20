using Contracts.Request;

namespace Contracts.Responses.Dtos;

public class InsertTransferResponseData 
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public int Subject { get; set; }
    public float Amount { get; set; }
    public int Direction { get; set; }
    public string Comment { get; set; } = "";
    public string PicUrl { get; set; } = "";

    public DateTime TransferDate { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateTime { get; set; }
}