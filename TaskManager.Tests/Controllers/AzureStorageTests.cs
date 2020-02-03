using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TaskManager.AzureStorage;
using TaskManager.Exceptions;
using Xunit;

namespace TaskManager.Tests
{
    public class AzureStorageTests
    {
        class TestEntity : TableEntity
        {
            public TestEntity()
            {
            }

            public TestEntity(string partitionKey, string rowKey, string name)
            {
                PartitionKey = partitionKey;
                RowKey = rowKey;
                Name = name;
            }
            public string Name { get; set; }
        }

        private const string ConnectionString = @"DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;";
        private readonly ITableStorageContext _context;
        private readonly CloudTableClient _cloudClient;
        private readonly CloudTable _table;

        public AzureStorageTests()
        {
            _cloudClient = CloudStorageAccount.Parse(ConnectionString).CreateCloudTableClient();
            _table = _cloudClient.GetTableReference("Test");
            _table.DeleteIfExistsAsync().Wait();
            _table.CreateIfNotExistsAsync().Wait();

            _context = new TableStorageContext(ConnectionString);
        }


        [Fact]
        public async Task GetEntityByIdAndPartitionKeyTest()
        {
            //arrange
            var operation = TableOperation.Insert(new TestEntity("part1", "1", "Test 1"));
            await _table.ExecuteAsync(operation);
            operation = TableOperation.Insert(new TestEntity("part1", "2", "Test 2"));
            await _table.ExecuteAsync(operation);
            //act
            var result = await _context.GetAsync<TestEntity>("Test", "1", "part1");
            //assert
            Assert.Equal("Test 1", result.Name);
        }

        [Fact]
        public async Task GetAllEntitiesTest()
        {
            //arrange
            var operation = TableOperation.Insert(new TestEntity("part1", "1", "Test 1"));
            await _table.ExecuteAsync(operation);
            operation = TableOperation.Insert(new TestEntity("part1", "2", "Test 2"));
            await _table.ExecuteAsync(operation);
            operation = TableOperation.Insert(new TestEntity("part2", "3", "Test 3"));
            await _table.ExecuteAsync(operation);
            //act
            var result = await _context.GetAllAsync<TestEntity>("Test");
            //assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task AddEntityTest()
        {
            //act
            await _context.AddAsync("Test", new TestEntity("1", "1", "Test"));
            //assert
            var result = await _context.GetAllAsync<TestEntity>("Test");
            Assert.Single(result);
        }

        [Fact]
        public async Task UpdateEntityTest()
        {
            //arrange
            var operation = TableOperation.Insert(new TestEntity("1", "1", "Test 1"));
            await _table.ExecuteAsync(operation);
            //act
            await _context.UpdateAsync("Test", new TestEntity("1", "1", "Test changed"));
            //assert
            var result = await _context.GetAsync<TestEntity>("Test", "1", "1");
            Assert.Equal("Test changed", result.Name);
        }

        [Fact]
        public async Task DeleteEntityTest()
        {
            //arrange
            var operation = TableOperation.Insert(new TestEntity("part1", "1", "Test 1"));
            await _table.ExecuteAsync(operation);
            //act
            await _context.DeleteAsync<TestEntity>("Test", "1", "part1");
            //assert
            var result = await _context.GetAllAsync<TestEntity>("Test");
            Assert.Empty(result);
        }

        [Fact]
        public async Task DeletedEntityReturnsNullTest()
        {
            //arrange
            var operation = TableOperation.Insert(new TestEntity("part1", "1", "Test 1"));
            await _table.ExecuteAsync(operation);
            //act
            await _context.DeleteAsync<TestEntity>("Test", "1", "part1");
            //assert
            var result = await _context.GetAsync<TestEntity>("Test", "1", "part1");
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteNotExistingEntityThrowsNotFoundExceptionTest()
        {
            //arrange
            var operation = TableOperation.Insert(new TestEntity("part1", "2", "Test 1"));
            await _table.ExecuteAsync(operation);
            //act, assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _context.DeleteAsync<TestEntity>("Test", "1", "part1"));
        }

        [Fact]
        public async Task QueryEntitiesWithTakeTest()
        {
            //arrange
            for (var i = 1; i <= 10; i++)
            {
                var operation = TableOperation.Insert(new TestEntity("part1", i.ToString(), $"Test {i}"));
                await _table.ExecuteAsync(operation);
            }
            var tableQuery = new TableQuery<TestEntity>();
            tableQuery.Take(5);
            //act
            var result = await _context.QueryWithParametersAsync<TestEntity>("Test", tableQuery);
            //assert
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public async Task Query3EntitiesWithNameAndTakeTest()
        {
            //arrange
            for (var i = 1; i <= 10; i++)
            {
                string name = i % 2 == 0 ? "even" : "odd";

                var operation = TableOperation.Insert(new TestEntity("part1", i.ToString(), name));
                await _table.ExecuteAsync(operation);
            }
            var tableQuery = new TableQuery<TestEntity>();
            tableQuery.FilterString = "Name eq 'odd'";
            tableQuery.Take(3);
            //act
            var result = await _context.QueryWithParametersAsync<TestEntity>("Test", tableQuery);
            //assert
            Assert.Equal(3, result.Count());

            foreach (var item in result)
            {
                Assert.Equal("odd", item.Name);
            }
        }
    }
}
