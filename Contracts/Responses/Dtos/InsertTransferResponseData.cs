using Contracts.Request;

namespace Contracts.Responses.Dtos;

public class InsertTransferResponseData : InsertTransferRequestDto
{
    public long Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateTime { get; set; }
}