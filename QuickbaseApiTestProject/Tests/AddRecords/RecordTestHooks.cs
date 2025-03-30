using System.Net;
using Microsoft.Extensions.Options;
using QuickbaseApiTestProject.Drivers.Interfaces;
using QuickbaseApiTestProject.Drivers.XmlQuickBaseApi;
using QuickbaseApiTestProject.DTOs.ResponseDTOs;
using QuickbaseApiTestProject.TestUtilities;
using QuickbaseApiTestProject.Utilities;

namespace QuickbaseApiTestProject.Tests.AddRecords;

[TestFixture]
public class RecordTestHooks
{
    protected readonly string tableId = TestServicesProvider.GetService<IOptions<TestRunConfig>>().Value.TestTableId;
    protected IQuickbaseApi quickbaseApi = TestServicesProvider.GetService<IQuickbaseApi>();
    protected XmlRequestProvider requestProvider = TestServicesProvider.GetService<XmlRequestProvider>();
    protected List<TableRecord> tableRecordsBefore;
    
    [SetUp]
    public async Task SetUp()
    {
        await SetTableRecordsBefore();
    }

    private async Task SetTableRecordsBefore()
    {
        var request = requestProvider.DoQueryRequest();
        var tableRecordsResponse = await quickbaseApi.GetTableRecordsAsync(tableId, request);
        Assert.That(tableRecordsResponse.StatusCode == HttpStatusCode.OK, string.Format(CommonConstants.RequestFailedMessage, tableRecordsResponse.Body.ErrorText));
        tableRecordsBefore = tableRecordsResponse.Body.Records;
    }
}