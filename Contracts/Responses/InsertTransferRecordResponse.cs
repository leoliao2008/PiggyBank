using Contracts.Responses.Dtos;
using Responses;

namespace Contracts.Responses;

public class InsertTransferRecordResponse:BaseResponse
{
    public InsertTransferResponseData? Data { get; set; }
}