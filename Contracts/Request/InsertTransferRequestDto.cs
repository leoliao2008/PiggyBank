namespace Contracts.Request;

public class InsertTransferRequestDto
{
    public int UserId { get; set; }
    public int Subject { get; set; }
    public float Amount { get; set; }
    public int Direction { get; set; }
    public string Comment { get; set; } = "";
    public string PicUrl { get; set; } = "";

    public long TransferDate { get; set; }
}