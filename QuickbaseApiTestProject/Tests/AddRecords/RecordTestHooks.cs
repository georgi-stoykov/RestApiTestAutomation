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
    
    [SetUp]
    public async Task SetUp()
    {
        await SetTableRecordsBefore();
        
        // ToDo: create table from the automation 
    }

    private async Task SetTableRecordsBefore()
    {
        var request = requestProvider.DoQueryRequest();
        var tableRecordsResponse = await quickbaseApi.GetTableRecordsAsync(tableId, request);
        Assert.That(tableRecordsResponse.StatusCode == HttpStatusCode.OK, string.Format(Constants.AssertionMessage.RequestFailed, tableRecordsResponse.Body.ErrorText));
    }
    
    protected async Task<TableRecord>? GetCreatedRecordAsync(Func<TableRecord, bool> recordFilter)
    {
        var request = requestProvider.DoQueryRequest();
        var tableRecords = await quickbaseApi.GetTableRecordsAsync(tableId, request);
        return tableRecords.Body.Records.SingleOrDefault(recordFilter);
    }
}