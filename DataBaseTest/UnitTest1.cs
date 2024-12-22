using BackendApiTest.Contracts.Animal;
using BackendApiTest.Controllers;
using Domain.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using BackendApiTest.Contracts.FeedingSchedule;
using System;
using BackendApiTest.Contracts.Fertilization;
using BackendApiTest.Contracts.Harvest;
using BackendApiTest.Contracts.Message;
using BackendApiTest.Contracts.News;
using BackendApiTest.Contracts.Payments;
using BackendApiTest.Contracts.Plant;
using BackendApiTest.Contracts.PollAnswer;
using BackendApiTest.Contracts.Poll;
using BackendApiTest.Contracts.PollOption;
using BackendApiTest.Contracts.ProductInShop;
using BackendApiTest.Contracts.Shop;
using BackendApiTest.Contracts.Snt;
using BackendApiTest.Contracts.SntEvent;
using BackendApiTest.Contracts.User;
using Domain.Interfaces;
using System.Threading.Tasks;
using BackendApiTest.Contracts.WateringSchedule;
using System.Linq.Expressions;

namespace DataBaseTest
{
    public class UnitTest1
    {
       
        public class AnimalsControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private readonly Mock<DbSet<Animal>> _mockAnimalsDbSet;
            private readonly AnimalsController _controller;

            public AnimalsControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();

                // ��������� ���������� ��������� ��� DbSet<Animal>
                var animals = new List<Animal>
            {
                new Animal { AnimalsId = 1, AnimalName = "Dog" },
                new Animal { AnimalsId = 2, AnimalName = "Cat" }
            }.AsQueryable();

                _mockAnimalsDbSet = CreateMockDbSet(animals);
                _mockContext.Setup(c => c.Animals).Returns(_mockAnimalsDbSet.Object);

                _controller = new AnimalsController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();

                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithAnimals()
            {
                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetAnimalsResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count);
            }

            [Fact]
            public void GetById_ReturnsOkResultWithAnimal()
            {
                // Act
                var result = _controller.GetById(1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetAnimalsResponse>(okResult.Value);
                Assert.Equal("Dog", returnValue.AnimalName);
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewAnimal()
            {
                // Arrange
                var model = new CreateAnimalsRequest { AnimalName = "New Animal" };

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetAnimalsResponse>(okResult.Value);
                Assert.Equal("New Animal", returnValue.AnimalName);
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedAnimal()
            {
                // Arrange
                var model = new CreateAnimalsRequest { AnimalName = "Updated Dog" };

                // Act
                var result = _controller.Update(1, model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetAnimalsResponse>(okResult.Value);
                Assert.Equal("Updated Dog", returnValue.AnimalName);
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Act
                var result = _controller.Delete(1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal("Animal successfully deleted", okResult.Value);
            }
        }
    }

    public class UnitTest2
    {
        public class FeedingScheduleControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<FeedingSchedule>> _mockFeedingScheduleDbSet;
            private readonly FeedingScheduleController _controller;


            public FeedingScheduleControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockFeedingScheduleDbSet = new Mock<DbSet<FeedingSchedule>>();

                _mockContext.Setup(c => c.FeedingSchedules).Returns(_mockFeedingScheduleDbSet.Object);

                _controller = new FeedingScheduleController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();

                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(data.ToList().Add);

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithFeedingSchedules()
            {
                // Arrange
                var schedules = new List<FeedingSchedule>
            {
                new FeedingSchedule { FeedingId = 1, AnimalsId = 101, FeedingTime = new TimeSpan(8, 0, 0), FeedingType = "Morning", FeedingDate = new DateTime(2024, 12, 21) },
                new FeedingSchedule { FeedingId = 2, AnimalsId = 102, FeedingTime = new TimeSpan(18, 0, 0), FeedingType = "Evening", FeedingDate = new DateTime(2024, 12, 21) }
            }.AsQueryable();

                _mockFeedingScheduleDbSet = CreateMockDbSet(schedules);
                _mockContext.Setup(c => c.FeedingSchedules).Returns(_mockFeedingScheduleDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetFeedingScheduleResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count);
            }

            [Fact]
            public void GetById_ReturnsOkResultWithFeedingSchedule()
            {
                // Arrange
                var schedule = new FeedingSchedule { FeedingId = 1, AnimalsId = 101, FeedingTime = new TimeSpan(8, 0, 0), FeedingType = "Morning", FeedingDate = new DateTime(2024, 12, 21) };
                var schedules = new List<FeedingSchedule> { schedule }.AsQueryable();

                _mockFeedingScheduleDbSet = CreateMockDbSet(schedules);
                _mockContext.Setup(c => c.FeedingSchedules).Returns(_mockFeedingScheduleDbSet.Object);

                // Act
                var result = _controller.GetById(1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetFeedingScheduleResponse>(okResult.Value);
                Assert.Equal(new DateTime(2024, 12, 21), returnValue.FeedingDate);
                Assert.Equal(new TimeSpan(8, 0, 0), returnValue.FeedingTime);
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewFeedingSchedule()
            {
                // Arrange
                var model = new CreateFeedingScheduleRequest {AnimalsId = 103, FeedingTime = new TimeSpan(12, 0, 0), FeedingType = "Lunch", FeedingDate = new DateTime(2024, 12, 22) };

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetFeedingScheduleResponse>(okResult.Value);
                Assert.Equal(new DateTime(2024, 12, 22), returnValue.FeedingDate);
                Assert.Equal(new TimeSpan(12, 0, 0), returnValue.FeedingTime);
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedFeedingSchedule()
            {
                // Arrange
                var existingSchedule = new FeedingSchedule
                {
                    FeedingId = 4,
                    AnimalsId = 101,
                    FeedingTime = new TimeSpan(8, 0, 0),
                    FeedingType = "Morning",
                    FeedingDate = new DateTime(2024, 12, 21)
                };

                var schedules = new List<FeedingSchedule> { existingSchedule }.AsQueryable();

                // �������� DbSet
                _mockFeedingScheduleDbSet = CreateMockDbSet(schedules);
                _mockContext.Setup(c => c.FeedingSchedules).Returns(_mockFeedingScheduleDbSet.Object);

                var model = new CreateFeedingScheduleRequest
                {
                    AnimalsId = 101,
                    FeedingTime = new TimeSpan(10, 0, 0),
                    FeedingType = "Brunch",
                    FeedingDate = new DateTime(2024, 12, 21)
                };

                // Act
                var result = _controller.Update(model, 4, 101); // ID ������ ��������� � ������������ FeedingId

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result); // ��������, ��� ��������� - ��� OkObjectResult
                var returnValue = Assert.IsType<GetFeedingScheduleResponse>(okResult.Value); // �������� ���� ������������� �������
                Assert.Equal(new TimeSpan(10, 0, 0), returnValue.FeedingTime); // ��������, ��� FeedingTime ��������
                Assert.Equal("Brunch", returnValue.FeedingType); // ��������, ��� FeedingType ��������
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingSchedule = new FeedingSchedule
                {
                    FeedingId = 1,
                    AnimalsId = 101,
                    FeedingTime = new TimeSpan(8, 0, 0),
                    FeedingType = "Morning",
                    FeedingDate = new DateTime(2024, 12, 21)
                };
                var schedules = new List<FeedingSchedule> { existingSchedule }.AsQueryable();

                // �������� DbSet
                _mockFeedingScheduleDbSet = CreateMockDbSet(schedules);
                _mockContext.Setup(c => c.FeedingSchedules).Returns(_mockFeedingScheduleDbSet.Object);

                // Act
                var result = _controller.Delete(1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);  // ��������� ��� ������
                Assert.Equal("Feeding schedule successfully deleted", okResult.Value);  // �������� ���������
            }
        }
    }

    public class UnitTest3
    {
        public class FertilizationControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<Fertilization>> _mockFertilizationDbSet;
            private readonly FertilizationController _controller;

            public FertilizationControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockFertilizationDbSet = new Mock<DbSet<Fertilization>>();

                _mockContext.Setup(c => c.Fertilizations).Returns(_mockFertilizationDbSet.Object);

                _controller = new FertilizationController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();

                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(data.ToList().Add);

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithFertilizations()
            {
                // Arrange
                var fertilizations = new List<Fertilization>
            {
                new Fertilization { FertilizationId = 1, FertilizationDate = new DateTime(2024, 12, 20), FertilizerName = "Nitrogen", PlantId = 101 },
                new Fertilization { FertilizationId = 2, FertilizationDate = new DateTime(2024, 12, 21), FertilizerName = "Phosphate", PlantId = 102 }
            }.AsQueryable();

                _mockFertilizationDbSet = CreateMockDbSet(fertilizations);
                _mockContext.Setup(c => c.Fertilizations).Returns(_mockFertilizationDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetFertilizationResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count);
            }

            [Fact]
            public void GetById_ReturnsOkResultWithFertilization()
            {
                // Arrange
                var fertilization = new Fertilization { FertilizationId = 1, FertilizationDate = new DateTime(2024, 12, 20), FertilizerName = "Nitrogen", PlantId = 101 };
                var fertilizations = new List<Fertilization> { fertilization }.AsQueryable();

                _mockFertilizationDbSet = CreateMockDbSet(fertilizations);
                _mockContext.Setup(c => c.Fertilizations).Returns(_mockFertilizationDbSet.Object);

                // Act
                var result = _controller.GetById(1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetFertilizationResponse>(okResult.Value);
                Assert.Equal(new DateTime(2024, 12, 20), returnValue.FertilizationDate);
                Assert.Equal("Nitrogen", returnValue.FertilizerName);
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewFertilization()
            {
                // Arrange
                var model = new CreateFertilizationRequest { FertilizationDate = new DateTime(2024, 12, 22), FertilizerName = "Potassium", PlantId = 103 };

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetFertilizationResponse>(okResult.Value);
                Assert.Equal(new DateTime(2024, 12, 22), returnValue.FertilizationDate);
                Assert.Equal("Potassium", returnValue.FertilizerName);
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedFertilization()
            {
                // Arrange
                var existingFertilization = new Fertilization { FertilizationId = 1, FertilizationDate = new DateTime(2024, 12, 20), FertilizerName = "Nitrogen", PlantId = 101 };
                var fertilizations = new List<Fertilization> { existingFertilization }.AsQueryable();

                _mockFertilizationDbSet = CreateMockDbSet(fertilizations);
                _mockContext.Setup(c => c.Fertilizations).Returns(_mockFertilizationDbSet.Object);

                var model = new CreateFertilizationRequest { FertilizationDate = new DateTime(2024, 12, 21), FertilizerName = "Ammonia", PlantId = 101 };

                // Act
                var result = _controller.Update(model, 1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetFertilizationResponse>(okResult.Value);
                Assert.Equal(new DateTime(2024, 12, 21), returnValue.FertilizationDate);
                Assert.Equal("Ammonia", returnValue.FertilizerName);
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingFertilization = new Fertilization { FertilizationId = 1, FertilizationDate = new DateTime(2024, 12, 20), FertilizerName = "Nitrogen", PlantId = 101 };
                var fertilizations = new List<Fertilization> { existingFertilization }.AsQueryable();

                // �������� DbSet
                _mockFertilizationDbSet = CreateMockDbSet(fertilizations);
                _mockContext.Setup(c => c.Fertilizations).Returns(_mockFertilizationDbSet.Object);

                // Act
                var result = _controller.Delete(1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal("Fertilization successfully deleted", okResult.Value);  // ��������, ��� �������� ���������� �����
            }
        }
    }

    public class UnitTest4
    {
        public class HarvestControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<Harvest>> _mockHarvestDbSet;
            private readonly HarvestController _controller;

            public HarvestControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockHarvestDbSet = new Mock<DbSet<Harvest>>();
                _mockContext.Setup(c => c.Harvests).Returns(_mockHarvestDbSet.Object);
                _controller = new HarvestController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();

                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithHarvests()
            {
                // Arrange
                var harvests = new List<Harvest>
        {
            new Harvest { HarvestId = 1, HarvestDate = new DateTime(2024, 12, 20), PlantId = 101 },
            new Harvest { HarvestId = 2, HarvestDate = new DateTime(2024, 12, 21), PlantId = 102 }
        }.AsQueryable();

                _mockHarvestDbSet = CreateMockDbSet(harvests);
                _mockContext.Setup(c => c.Harvests).Returns(_mockHarvestDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetHarvestResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count);
            }

            [Fact]
            public void GetById_ReturnsOkResultWithHarvest()
            {
                // Arrange
                var harvest = new Harvest { HarvestId = 1, HarvestDate = new DateTime(2024, 12, 20), PlantId = 101 };
                var harvests = new List<Harvest> { harvest }.AsQueryable();

                _mockHarvestDbSet = CreateMockDbSet(harvests);
                _mockContext.Setup(c => c.Harvests).Returns(_mockHarvestDbSet.Object);

                // Act
                var result = _controller.GetById(1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetHarvestResponse>(okResult.Value);
                Assert.Equal(new DateTime(2024, 12, 20), returnValue.HarvestDate);
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewHarvest()
            {
                // Arrange
                var model = new CreateHarvestRequest { HarvestDate = new DateTime(2024, 12, 22), PlantId = 103 };

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetHarvestResponse>(okResult.Value);
                Assert.Equal(new DateTime(2024, 12, 22), returnValue.HarvestDate);
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedHarvest()
            {
                // Arrange
                var existingHarvest = new Harvest { HarvestId = 1, HarvestDate = new DateTime(2024, 12, 20), PlantId = 101 };
                var harvests = new List<Harvest> { existingHarvest }.AsQueryable();

                _mockHarvestDbSet = CreateMockDbSet(harvests);
                _mockContext.Setup(c => c.Harvests).Returns(_mockHarvestDbSet.Object);

                var model = new CreateHarvestRequest { HarvestDate = new DateTime(2024, 12, 21), PlantId = 101 };

                // Act
                var result = _controller.Update(model, 1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetHarvestResponse>(okResult.Value);
                Assert.Equal(new DateTime(2024, 12, 21), returnValue.HarvestDate);
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingHarvest = new Harvest { HarvestId = 1, HarvestDate = new DateTime(2024, 12, 20), PlantId = 101 };
                var harvests = new List<Harvest> { existingHarvest }.AsQueryable();

                // �������� DbSet
                _mockHarvestDbSet = CreateMockDbSet(harvests);
                _mockContext.Setup(c => c.Harvests).Returns(_mockHarvestDbSet.Object);

                // Act
                var result = _controller.Delete(1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal("Harvest successfully deleted", okResult.Value);  // ���������, ��� �������� ���������� �����
            }
        }
    }

    public class UnitTest5
    {
        public class MessageControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<Message>> _mockMessageDbSet;
            private readonly MessageController _controller;

            public MessageControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockMessageDbSet = new Mock<DbSet<Message>>();
                _mockContext.Setup(c => c.Messages).Returns(_mockMessageDbSet.Object);
                _controller = new MessageController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithMessages()
            {
                // Arrange
                var messages = new List<Message>
        {
            new Message { MessageId = 1, MessageText = "Test Message 1", MessageTime = new TimeSpan(10, 0, 0), MessageDate = new DateTime(2024, 12, 21) },
            new Message { MessageId = 2, MessageText = "Test Message 2", MessageTime = new TimeSpan(11, 0, 0), MessageDate = new DateTime(2024, 12, 22) }
        }.AsQueryable();

                _mockMessageDbSet = CreateMockDbSet(messages);
                _mockContext.Setup(c => c.Messages).Returns(_mockMessageDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetMessageResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count);
            }

            [Fact]
            public void GetById_ReturnsOkResultWithMessage()
            {
                // Arrange
                var message = new Message
                {
                    MessageId = 1,
                    MessageText = "Test Message 1",
                    MessageTime = new TimeSpan(10, 0, 0),
                    MessageDate = new DateTime(2024, 12, 21),
                    UserId = 101 // ��������� UserId, ����� ���� ��� ���������� � ������������
                };

                var messages = new List<Message> { message }.AsQueryable();

                // �������� DbSet
                _mockMessageDbSet = CreateMockDbSet(messages);
                _mockContext.Setup(c => c.Messages).Returns(_mockMessageDbSet.Object);

                // Act
                var result = _controller.GetById(1, 101); // messageId = 1, userId = 101

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetMessageResponse>(okResult.Value);
                Assert.Equal("Test Message 1", returnValue.MessageText); // ��������� ����� ���������
                Assert.Equal(new TimeSpan(10, 0, 0), returnValue.MessageTime); // ��������� �����
                Assert.Equal(new DateTime(2024, 12, 21), returnValue.MessageDate); // ��������� ����
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewMessage()
            {
                // Arrange
                var model = new CreateMessageRequest
                {
                    MessageText = "New Test Message",
                    MessageTime = new TimeSpan(12, 0, 0),
                    MessageDate = new DateTime(2024, 12, 22),
                    UserId = 101
                };

                var messages = new List<Message>().AsQueryable();
                _mockMessageDbSet = CreateMockDbSet(messages);
                _mockContext.Setup(c => c.Messages).Returns(_mockMessageDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetMessageResponse>(okResult.Value);
                Assert.Equal("New Test Message", returnValue.MessageText);
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedMessage()
            {
                // Arrange
                var existingMessage = new Message { MessageId = 1, MessageText = "Old Message", MessageTime = new TimeSpan(9, 0, 0), MessageDate = new DateTime(2024, 12, 21), UserId = 101 };
                var messages = new List<Message> { existingMessage }.AsQueryable();

                _mockMessageDbSet = CreateMockDbSet(messages);
                _mockContext.Setup(c => c.Messages).Returns(_mockMessageDbSet.Object);

                var model = new CreateMessageRequest
                {
                    MessageText = "Updated Message",
                    MessageTime = new TimeSpan(12, 0, 0),
                    MessageDate = new DateTime(2024, 12, 22),
                    UserId = 101
                };

                // Act
                var result = _controller.Update(model, 1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetMessageResponse>(okResult.Value);
                Assert.Equal("Updated Message", returnValue.MessageText);
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingMessage = new Message { MessageId = 1, MessageText = "Message to Delete", MessageTime = new TimeSpan(9, 0, 0), MessageDate = new DateTime(2024, 12, 21), UserId = 101 };
                var messages = new List<Message> { existingMessage }.AsQueryable();

                _mockMessageDbSet = CreateMockDbSet(messages);
                _mockContext.Setup(c => c.Messages).Returns(_mockMessageDbSet.Object);

                // Act
                var result = _controller.Delete(1, 101); // �������� �������������� ��� ��������

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest6
    {
        public class NewsControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<News>> _mockNewsDbSet;
            private readonly NewsController _controller;

            public NewsControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockNewsDbSet = new Mock<DbSet<News>>();
                _mockContext.Setup(c => c.News).Returns(_mockNewsDbSet.Object);
                _controller = new NewsController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithNewsList()
            {
                // Arrange
                var newsList = new List<News>
        {
            new News { NewsId = 1, NewsTitle = "News 1", NewsText = "Text of news 1", NewsDate = new DateTime(2024, 12, 21), UserId = 101 },
            new News { NewsId = 2, NewsTitle = "News 2", NewsText = "Text of news 2", NewsDate = new DateTime(2024, 12, 22), UserId = 102 }
        }.AsQueryable();

                _mockNewsDbSet = CreateMockDbSet(newsList);
                _mockContext.Setup(c => c.News).Returns(_mockNewsDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetNewsResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ��������, ��� ��������� 2 �������
            }

            [Fact]
            public void GetById_ReturnsOkResultWithNews()
            {
                // Arrange
                var news = new News { NewsId = 1, NewsTitle = "News 1", NewsText = "Text of news 1", NewsDate = new DateTime(2024, 12, 21), UserId = 101 };
                var newsList = new List<News> { news }.AsQueryable();

                // �������� DbSet
                _mockNewsDbSet = CreateMockDbSet(newsList);
                _mockContext.Setup(c => c.News).Returns(_mockNewsDbSet.Object);

                // Act
                var result = _controller.GetById(1, 101); // �������� �� newsId = 1 � userId = 101

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetNewsResponse>(okResult.Value);
                Assert.Equal("News 1", returnValue.NewsTitle); // ��������� �������� �������
                Assert.Equal("Text of news 1", returnValue.NewsText); // ��������� ����� �������
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewNews()
            {
                // Arrange
                var model = new CreateNewsRequest
                {
                    NewsTitle = "New News",
                    NewsText = "This is a new news article.",
                    NewsDate = new DateTime(2024, 12, 22),
                    UserId = 101
                };

                var newsList = new List<News>().AsQueryable();
                _mockNewsDbSet = CreateMockDbSet(newsList);
                _mockContext.Setup(c => c.News).Returns(_mockNewsDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetNewsResponse>(okResult.Value);
                Assert.Equal("New News", returnValue.NewsTitle); // ��������, ��� ��������� ������� �������������
                Assert.Equal("This is a new news article.", returnValue.NewsText); // �������� ������ �������
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedNews()
            {
                // Arrange
                var existingNews = new News
                {
                    NewsId = 1,
                    NewsTitle = "Old News Title",
                    NewsText = "Old news text.",
                    NewsDate = new DateTime(2024, 12, 21),
                    UserId = 101
                };

                var newsList = new List<News> { existingNews }.AsQueryable();
                _mockNewsDbSet = CreateMockDbSet(newsList);
                _mockContext.Setup(c => c.News).Returns(_mockNewsDbSet.Object);

                var model = new CreateNewsRequest
                {
                    NewsTitle = "Updated News Title",
                    NewsText = "Updated news text.",
                    NewsDate = new DateTime(2024, 12, 22),
                    UserId = 101
                };

                // Act
                var result = _controller.Update(model, 1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetNewsResponse>(okResult.Value);
                Assert.Equal("Updated News Title", returnValue.NewsTitle); // ���������, ��� ��������� ���������
                Assert.Equal("Updated news text.", returnValue.NewsText); // ���������, ��� ����� ���������
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingNews = new News
                {
                    NewsId = 1,
                    NewsTitle = "News to delete",
                    NewsText = "This news will be deleted.",
                    NewsDate = new DateTime(2024, 12, 21),
                    UserId = 101
                };

                var newsList = new List<News> { existingNews }.AsQueryable();
                _mockNewsDbSet = CreateMockDbSet(newsList);
                _mockContext.Setup(c => c.News).Returns(_mockNewsDbSet.Object);

                // Act
                var result = _controller.Delete(1, 101); // �������� �������������� ��� ��������

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest7
    {
        public class PaymentsControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<Payment>> _mockPaymentsDbSet;
            private readonly PaymentsController _controller;

            public PaymentsControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockPaymentsDbSet = new Mock<DbSet<Payment>>();
                _mockContext.Setup(c => c.Payments).Returns(_mockPaymentsDbSet.Object);
                _controller = new PaymentsController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithPaymentsList()
            {
                // Arrange
                var paymentsList = new List<Payment>
        {
            new Payment { PaymentsId = 1, PaymentType = "Credit", PaymentAmount = 1000, PaymentDate = new DateTime(2024, 12, 21), UserId = 101, PenaltyAmount = null },
            new Payment { PaymentsId = 2, PaymentType = "Debit", PaymentAmount = 1500, PaymentDate = new DateTime(2024, 12, 22), UserId = 102, PenaltyAmount = 200 }
        }.AsQueryable();

                _mockPaymentsDbSet = CreateMockDbSet(paymentsList);
                _mockContext.Setup(c => c.Payments).Returns(_mockPaymentsDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetPaymentsResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ��������, ��� �������� ������ �� 2 ��������
            }

            [Fact]
            public void GetById_ReturnsOkResultWithPayment()
            {
                // Arrange
                var payment = new Payment
                {
                    PaymentsId = 1,
                    PaymentType = "Credit",
                    PaymentAmount = 1000,
                    PaymentDate = new DateTime(2024, 12, 21),
                    UserId = 101,
                    PenaltyAmount = null
                };

                var paymentsList = new List<Payment> { payment }.AsQueryable();

                _mockPaymentsDbSet = CreateMockDbSet(paymentsList);
                _mockContext.Setup(c => c.Payments).Returns(_mockPaymentsDbSet.Object);

                // Act
                var result = _controller.GetById(1, 101); // �������� �� paymentsId = 1 � userId = 101

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPaymentsResponse>(okResult.Value);
                Assert.Equal("Credit", returnValue.PaymentType); // ��������� ��� �������
                Assert.Equal(1000, returnValue.PaymentAmount); // ��������� ����� �������
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewPayment()
            {
                // Arrange
                var model = new CreatePaymentsRequest
                {
                    PaymentType = "Debit",
                    PaymentAmount = 2000,
                    PaymentDate = new DateTime(2024, 12, 22),
                    UserId = 102,
                    PenaltyAmount = 300
                };

                var paymentsList = new List<Payment>().AsQueryable();
                _mockPaymentsDbSet = CreateMockDbSet(paymentsList);
                _mockContext.Setup(c => c.Payments).Returns(_mockPaymentsDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPaymentsResponse>(okResult.Value);
                Assert.Equal("Debit", returnValue.PaymentType); // ��������, ��� ��� ������� �������������
                Assert.Equal(2000, returnValue.PaymentAmount); // �������� ����� �������
                Assert.Equal(300, returnValue.PenaltyAmount); // �������� ������
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedPayment()
            {
                // Arrange
                var existingPayment = new Payment
                {
                    PaymentsId = 1,
                    PaymentType = "Credit",
                    PaymentAmount = 1000,
                    PaymentDate = new DateTime(2024, 12, 21),
                    UserId = 101,
                    PenaltyAmount = null
                };

                var paymentsList = new List<Payment> { existingPayment }.AsQueryable();
                _mockPaymentsDbSet = CreateMockDbSet(paymentsList);
                _mockContext.Setup(c => c.Payments).Returns(_mockPaymentsDbSet.Object);

                var model = new CreatePaymentsRequest
                {
                    PaymentType = "Debit",
                    PaymentAmount = 2000,
                    PaymentDate = new DateTime(2024, 12, 22),
                    UserId = 101,
                    PenaltyAmount = 300
                };

                // Act
                var result = _controller.Update(model, 1, 101);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPaymentsResponse>(okResult.Value);
                Assert.Equal("Debit", returnValue.PaymentType); // ��������, ��� ��� ������� ���������
                Assert.Equal(2000, returnValue.PaymentAmount); // ��������, ��� ����� ������� ����������
                Assert.Equal(300, returnValue.PenaltyAmount); // ��������, ��� ����� ���������
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingPayment = new Payment
                {
                    PaymentsId = 1,
                    PaymentType = "Credit",
                    PaymentAmount = 1000,
                    PaymentDate = new DateTime(2024, 12, 21),
                    UserId = 101,
                    PenaltyAmount = null
                };

                var paymentsList = new List<Payment> { existingPayment }.AsQueryable();
                _mockPaymentsDbSet = CreateMockDbSet(paymentsList);
                _mockContext.Setup(c => c.Payments).Returns(_mockPaymentsDbSet.Object);

                // Act
                var result = _controller.Delete(1, 101); // �������� �������������� ��� ��������

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest8
    {
        public class PlantControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<Plant>> _mockPlantDbSet;
            private readonly PlantController _controller;

            public PlantControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockPlantDbSet = new Mock<DbSet<Plant>>();
                _mockContext.Setup(c => c.Plants).Returns(_mockPlantDbSet.Object);
                _controller = new PlantController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithPlantsList()
            {
                // Arrange
                var plantsList = new List<Plant>
        {
            new Plant { PlantId = 1, PlantName = "Rose", PlantType = "Flower" },
            new Plant { PlantId = 2, PlantName = "Tulip", PlantType = "Flower" }
        }.AsQueryable();

                _mockPlantDbSet = CreateMockDbSet(plantsList);
                _mockContext.Setup(c => c.Plants).Returns(_mockPlantDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetPlantResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ��������, ��� �������� ������ �� 2 ��������
            }

            [Fact]
            public void GetById_ReturnsOkResultWithPlant()
            {
                // Arrange
                var plant = new Plant
                {
                    PlantId = 1,
                    PlantName = "Rose",
                    PlantType = "Flower"
                };

                var plantsList = new List<Plant> { plant }.AsQueryable();

                _mockPlantDbSet = CreateMockDbSet(plantsList);
                _mockContext.Setup(c => c.Plants).Returns(_mockPlantDbSet.Object);

                // Act
                var result = _controller.GetById(1); // �������� �� plantId = 1

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPlantResponse>(okResult.Value);
                Assert.Equal("Rose", returnValue.PlantName); // ��������� �������� ��������
                Assert.Equal("Flower", returnValue.PlantType); // ��������� ��� ��������
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewPlant()
            {
                // Arrange
                var model = new CreatePlantRequest
                {
                    PlantName = "Lily",
                    PlantType = "Flower"
                };

                var plantsList = new List<Plant>().AsQueryable();
                _mockPlantDbSet = CreateMockDbSet(plantsList);
                _mockContext.Setup(c => c.Plants).Returns(_mockPlantDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPlantResponse>(okResult.Value);
                Assert.Equal("Lily", returnValue.PlantName); // ��������, ��� �������� �������� �������������
                Assert.Equal("Flower", returnValue.PlantType); // ��������, ��� ��� �������� �������������
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedPlant()
            {
                // Arrange
                var existingPlant = new Plant
                {
                    PlantId = 1,
                    PlantName = "Rose",
                    PlantType = "Flower"
                };

                var plantsList = new List<Plant> { existingPlant }.AsQueryable();
                _mockPlantDbSet = CreateMockDbSet(plantsList);
                _mockContext.Setup(c => c.Plants).Returns(_mockPlantDbSet.Object);

                var model = new CreatePlantRequest
                {
                    PlantName = "Tulip",
                    PlantType = "Flower"
                };

                // Act
                var result = _controller.Update(model, 1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPlantResponse>(okResult.Value);
                Assert.Equal("Tulip", returnValue.PlantName); // ��������, ��� �������� �������� ����������
                Assert.Equal("Flower", returnValue.PlantType); // ��������, ��� ��� �������� ������� �������
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingPlant = new Plant
                {
                    PlantId = 1,
                    PlantName = "Rose",
                    PlantType = "Flower"
                };

                var plantsList = new List<Plant> { existingPlant }.AsQueryable();
                _mockPlantDbSet = CreateMockDbSet(plantsList);
                _mockContext.Setup(c => c.Plants).Returns(_mockPlantDbSet.Object);

                // Act
                var result = _controller.Delete(1); // �������� plantId = 1 ��� ��������

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest9
    {
        public class PollAnswerControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<PollAnswer>> _mockPollAnswerDbSet;
            private readonly PollAnswerController _controller;

            public PollAnswerControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockPollAnswerDbSet = new Mock<DbSet<PollAnswer>>();
                _mockContext.Setup(c => c.PollAnswers).Returns(_mockPollAnswerDbSet.Object);
                _controller = new PollAnswerController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithPollAnswersList()
            {
                // Arrange
                var pollAnswersList = new List<PollAnswer>
        {
            new PollAnswer { AnswerId = 1, UserId = 1, OptionId = 2 },
            new PollAnswer { AnswerId = 2, UserId = 1, OptionId = 3 }
        }.AsQueryable();

                _mockPollAnswerDbSet = CreateMockDbSet(pollAnswersList);
                _mockContext.Setup(c => c.PollAnswers).Returns(_mockPollAnswerDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetPollAnswerResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ���������, ��� �������� ������ �� 2 �������
            }

            [Fact]
            public void GetById_ReturnsOkResultWithPollAnswer()
            {
                // Arrange
                var pollAnswer = new PollAnswer { AnswerId = 1, UserId = 1, OptionId = 2 };
                var pollAnswersList = new List<PollAnswer> { pollAnswer }.AsQueryable();

                _mockPollAnswerDbSet = CreateMockDbSet(pollAnswersList);
                _mockContext.Setup(c => c.PollAnswers).Returns(_mockPollAnswerDbSet.Object);

                // Act
                var result = _controller.GetById(1, 1, 2, 1); // �������� �� answerId = 1, userId = 1, optionId = 2

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPollAnswerResponse>(okResult.Value);
                Assert.Equal(1, returnValue.AnswerId); // ��������, ��� ������������� ������ �������������
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewPollAnswer()
            {
                // Arrange
                var model = new CreatePollAnswerRequest
                {
                    UserId = 1,
                    OptionId = 2
                };

                var pollAnswersList = new List<PollAnswer>().AsQueryable();
                _mockPollAnswerDbSet = CreateMockDbSet(pollAnswersList);
                _mockContext.Setup(c => c.PollAnswers).Returns(_mockPollAnswerDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPollAnswerResponse>(okResult.Value);
                Assert.Equal(1, returnValue.UserId); // ��������, ��� userId ����������
                Assert.Equal(2, returnValue.OptionId); // ��������, ��� optionId ����������
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedPollAnswer()
            {
                // Arrange
                var existingPollAnswer = new PollAnswer
                {
                    AnswerId = 1,
                    UserId = 1,
                    OptionId = 2
                };

                var pollAnswersList = new List<PollAnswer> { existingPollAnswer }.AsQueryable();
                _mockPollAnswerDbSet = CreateMockDbSet(pollAnswersList);
                _mockContext.Setup(c => c.PollAnswers).Returns(_mockPollAnswerDbSet.Object);

                var model = new CreatePollAnswerRequest
                {
                    UserId = 1,
                    OptionId = 3
                };

                // Act
                var result = _controller.Update(model, 1, 1, 2, 1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPollAnswerResponse>(okResult.Value);
                Assert.Equal(3, returnValue.OptionId); // ��������, ��� optionId ���������
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingPollAnswer = new PollAnswer
                {
                    AnswerId = 1,
                    UserId = 1,
                    OptionId = 2
                };

                var pollAnswersList = new List<PollAnswer> { existingPollAnswer }.AsQueryable();
                _mockPollAnswerDbSet = CreateMockDbSet(pollAnswersList);
                _mockContext.Setup(c => c.PollAnswers).Returns(_mockPollAnswerDbSet.Object);

                // Act
                var result = _controller.Delete(1, 1, 2, 1); // �������� �������������� ��� ��������

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest10
    {
        public class PollControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<Poll>> _mockPollDbSet;
            private readonly PollController _controller;

            public PollControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockPollDbSet = new Mock<DbSet<Poll>>();
                _mockContext.Setup(c => c.Polls).Returns(_mockPollDbSet.Object);
                _controller = new PollController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithPollsList()
            {
                // Arrange
                var pollsList = new List<Poll>
        {
            new Poll { PollId = 1, PollQuestion = "Question 1", PollDate = DateTime.Now },
            new Poll { PollId = 2, PollQuestion = "Question 2", PollDate = DateTime.Now }
        }.AsQueryable();

                _mockPollDbSet = CreateMockDbSet(pollsList);
                _mockContext.Setup(c => c.Polls).Returns(_mockPollDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetPollResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ���������, ��� �������� ������ �� 2 �������
            }

            [Fact]
            public void GetById_ReturnsOkResultWithPoll()
            {
                // Arrange
                var poll = new Poll { PollId = 1, PollQuestion = "Question 1", PollDate = DateTime.Now };
                var pollsList = new List<Poll> { poll }.AsQueryable();

                _mockPollDbSet = CreateMockDbSet(pollsList);
                _mockContext.Setup(c => c.Polls).Returns(_mockPollDbSet.Object);

                // Act
                var result = _controller.GetById(1); // �������� �� pollId = 1

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPollResponse>(okResult.Value);
                Assert.Equal(1, returnValue.PollId); // ��������, ��� PollId ����������
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewPoll()
            {
                // Arrange
                var model = new CreatePollRequest
                {
                    PollQuestion = "New Poll Question"
                };

                var pollsList = new List<Poll>().AsQueryable();
                _mockPollDbSet = CreateMockDbSet(pollsList);
                _mockContext.Setup(c => c.Polls).Returns(_mockPollDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPollResponse>(okResult.Value);
                Assert.Equal("New Poll Question", returnValue.PollQuestion); // ��������, ��� PollQuestion ����������
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedPoll()
            {
                // Arrange
                var existingPoll = new Poll
                {
                    PollId = 1,
                    PollQuestion = "Old Question",
                    PollDate = DateTime.Now
                };

                var pollsList = new List<Poll> { existingPoll }.AsQueryable();
                _mockPollDbSet = CreateMockDbSet(pollsList);
                _mockContext.Setup(c => c.Polls).Returns(_mockPollDbSet.Object);

                var model = new CreatePollRequest
                {
                    PollQuestion = "Updated Question"
                };

                // Act
                var result = _controller.Update(model, 1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPollResponse>(okResult.Value);
                Assert.Equal("Updated Question", returnValue.PollQuestion); // ��������, ��� PollQuestion ���������
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingPoll = new Poll
                {
                    PollId = 1,
                    PollQuestion = "Question to Delete",
                    PollDate = DateTime.Now
                };

                var pollsList = new List<Poll> { existingPoll }.AsQueryable();
                _mockPollDbSet = CreateMockDbSet(pollsList);
                _mockContext.Setup(c => c.Polls).Returns(_mockPollDbSet.Object);

                // Act
                var result = _controller.Delete(1); // �������� pollId ��� ��������

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest11
    {
        public class PollOptionControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<PollOption>> _mockPollOptionDbSet;
            private readonly PollOptionController _controller;

            public PollOptionControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockPollOptionDbSet = new Mock<DbSet<PollOption>>();
                _mockContext.Setup(c => c.PollOptions).Returns(_mockPollOptionDbSet.Object);
                _controller = new PollOptionController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithPollOptionsList()
            {
                // Arrange
                var optionsList = new List<PollOption>
        {
            new PollOption { OptionId = 1, PollId = 1, OptionText = "Option 1" },
            new PollOption { OptionId = 2, PollId = 1, OptionText = "Option 2" }
        }.AsQueryable();

                _mockPollOptionDbSet = CreateMockDbSet(optionsList);
                _mockContext.Setup(c => c.PollOptions).Returns(_mockPollOptionDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetPollOptionResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ���������, ��� �������� ������ �� 2 ��������� ������
            }

            [Fact]
            public void GetById_ReturnsOkResultWithPollOption()
            {
                // Arrange
                var option = new PollOption { OptionId = 1, PollId = 1, OptionText = "Option 1" };
                var optionsList = new List<PollOption> { option }.AsQueryable();

                _mockPollOptionDbSet = CreateMockDbSet(optionsList);
                _mockContext.Setup(c => c.PollOptions).Returns(_mockPollOptionDbSet.Object);

                // Act
                var result = _controller.GetById(1, 1); // �������� �� optionId = 1 � pollId = 1

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPollOptionResponse>(okResult.Value);
                Assert.Equal(1, returnValue.OptionId); // ��������, ��� OptionId ����������
                Assert.Equal("Option 1", returnValue.OptionText); // ��������, ��� OptionText ����������
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewPollOption()
            {
                // Arrange
                var model = new CreatePollOptionRequest
                {
                    OptionText = "New Option",
                    PollId = 1
                };

                var optionsList = new List<PollOption>().AsQueryable();
                _mockPollOptionDbSet = CreateMockDbSet(optionsList);
                _mockContext.Setup(c => c.PollOptions).Returns(_mockPollOptionDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPollOptionResponse>(okResult.Value);
                Assert.Equal("New Option", returnValue.OptionText); // ��������, ��� OptionText ����������
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedPollOption()
            {
                // Arrange
                var existingOption = new PollOption
                {
                    OptionId = 1,
                    PollId = 1,
                    OptionText = "Old Option"
                };

                var optionsList = new List<PollOption> { existingOption }.AsQueryable();
                _mockPollOptionDbSet = CreateMockDbSet(optionsList);
                _mockContext.Setup(c => c.PollOptions).Returns(_mockPollOptionDbSet.Object);

                var model = new CreatePollOptionRequest
                {
                    OptionText = "Updated Option"
                };

                // Act
                var result = _controller.Update(model, 1, 1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetPollOptionResponse>(okResult.Value);
                Assert.Equal("Updated Option", returnValue.OptionText); // ��������, ��� OptionText ���������
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingOption = new PollOption
                {
                    OptionId = 1,
                    PollId = 1,
                    OptionText = "Option to Delete"
                };

                var optionsList = new List<PollOption> { existingOption }.AsQueryable();
                _mockPollOptionDbSet = CreateMockDbSet(optionsList);
                _mockContext.Setup(c => c.PollOptions).Returns(_mockPollOptionDbSet.Object);

                // Act
                var result = _controller.Delete(1, 1); // �������� optionId � pollId ��� ��������

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest12
    {
        public class ProductInShopControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<ProductInShop>> _mockProductInShopDbSet;
            private readonly ProductinShopController _controller;

            public ProductInShopControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockProductInShopDbSet = new Mock<DbSet<ProductInShop>>();
                _mockContext.Setup(c => c.ProductInShops).Returns(_mockProductInShopDbSet.Object);
                _controller = new ProductinShopController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithProductsInShopList()
            {
                // Arrange
                var productsInShopList = new List<ProductInShop>
        {
            new ProductInShop { ProductId = 1, ShopId = 1, ProductName = "Product 1", ProductPrice = 100 },
            new ProductInShop { ProductId = 2, ShopId = 1, ProductName = "Product 2", ProductPrice = 200 }
        }.AsQueryable();

                _mockProductInShopDbSet = CreateMockDbSet(productsInShopList);
                _mockContext.Setup(c => c.ProductInShops).Returns(_mockProductInShopDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetProductInShopResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ���������, ��� �������� ������ �� 2 �������
            }

            [Fact]
            public void GetById_ReturnsOkResultWithProductInShop()
            {
                // Arrange
                var productInShop = new ProductInShop { ProductId = 1, ShopId = 1, ProductName = "Product 1", ProductPrice = 100 };
                var productsInShopList = new List<ProductInShop> { productInShop }.AsQueryable();

                _mockProductInShopDbSet = CreateMockDbSet(productsInShopList);
                _mockContext.Setup(c => c.ProductInShops).Returns(_mockProductInShopDbSet.Object);

                // Act
                var result = _controller.GetById(1, 1); // �������� �� productId = 1 � shopId = 1

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetProductInShopResponse>(okResult.Value);
                Assert.Equal(1, returnValue.ProductId); // ��������, ��� ProductId ����������
                Assert.Equal("Product 1", returnValue.ProductName); // ��������, ��� ProductName ����������
                Assert.Equal(100, returnValue.ProductPrice); // ��������, ��� ProductPrice ����������
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewProductInShop()
            {
                // Arrange
                var model = new CreateProductInShopRequest
                {
                    ProductName = "New Product",
                    ProductPrice = 150,
                    ShopId = 1
                };

                var productsInShopList = new List<ProductInShop>().AsQueryable();
                _mockProductInShopDbSet = CreateMockDbSet(productsInShopList);
                _mockContext.Setup(c => c.ProductInShops).Returns(_mockProductInShopDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetProductInShopResponse>(okResult.Value);
                Assert.Equal("New Product", returnValue.ProductName); // ��������, ��� ProductName ����������
                Assert.Equal(150, returnValue.ProductPrice); // ��������, ��� ProductPrice ����������
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedProductInShop()
            {
                // Arrange
                var existingProductInShop = new ProductInShop
                {
                    ProductId = 1,
                    ShopId = 1,
                    ProductName = "Old Product",
                    ProductPrice = 100
                };

                var productsInShopList = new List<ProductInShop> { existingProductInShop }.AsQueryable();
                _mockProductInShopDbSet = CreateMockDbSet(productsInShopList);
                _mockContext.Setup(c => c.ProductInShops).Returns(_mockProductInShopDbSet.Object);

                var model = new CreateProductInShopRequest
                {
                    ProductName = "Updated Product",
                    ProductPrice = 200
                };

                // Act
                var result = _controller.Update(model, 1, 1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetProductInShopResponse>(okResult.Value);
                Assert.Equal("Updated Product", returnValue.ProductName); // ��������, ��� ProductName ���������
                Assert.Equal(200, returnValue.ProductPrice); // ��������, ��� ProductPrice ���������
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingProductInShop = new ProductInShop
                {
                    ProductId = 1,
                    ShopId = 1,
                    ProductName = "Product to Delete",
                    ProductPrice = 100
                };

                var productsInShopList = new List<ProductInShop> { existingProductInShop }.AsQueryable();
                _mockProductInShopDbSet = CreateMockDbSet(productsInShopList);
                _mockContext.Setup(c => c.ProductInShops).Returns(_mockProductInShopDbSet.Object);

                // Act
                var result = _controller.Delete(1, 1); // �������� productId � shopId ��� ��������

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest13
    {
        public class ShopControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<Shop>> _mockShopDbSet;
            private readonly ShopController _controller;

            public ShopControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockShopDbSet = new Mock<DbSet<Shop>>();
                _mockContext.Setup(c => c.Shops).Returns(_mockShopDbSet.Object);
                _controller = new ShopController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithShopsList()
            {
                // Arrange
                var shopsList = new List<Shop>
        {
            new Shop { ShopId = 1, ShopName = "Shop 1", City = "City 1", Street = "Street 1", HouseNumber = "1A" },
            new Shop { ShopId = 2, ShopName = "Shop 2", City = "City 2", Street = "Street 2", HouseNumber = "2B" }
        }.AsQueryable();

                _mockShopDbSet = CreateMockDbSet(shopsList);
                _mockContext.Setup(c => c.Shops).Returns(_mockShopDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetShopResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ���������, ��� �������� ������ �� 2 ���������
            }

            [Fact]
            public void GetById_ReturnsOkResultWithShop()
            {
                // Arrange
                var shop = new Shop { ShopId = 1, ShopName = "Shop 1", City = "City 1", Street = "Street 1", HouseNumber = "1A" };
                var shopsList = new List<Shop> { shop }.AsQueryable();

                _mockShopDbSet = CreateMockDbSet(shopsList);
                _mockContext.Setup(c => c.Shops).Returns(_mockShopDbSet.Object);

                // Act
                var result = _controller.GetById(1); // �������� �� shopId = 1

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetShopResponse>(okResult.Value);
                Assert.Equal(1, returnValue.ShopId); // ��������, ��� ShopId ����������
                Assert.Equal("Shop 1", returnValue.ShopName); // ��������, ��� ShopName ����������
                Assert.Equal("City 1", returnValue.City); // ��������, ��� City ����������
                Assert.Equal("Street 1", returnValue.Street); // ��������, ��� Street ����������
                Assert.Equal("1A", returnValue.HouseNumber); // ��������, ��� HouseNumber ����������
            }

            [Fact]
            public void Add_ReturnsOkResultWithNewShop()
            {
                // Arrange
                var model = new CreateShopRequest
                {
                    ShopName = "New Shop",
                    City = "New City",
                    Street = "New Street",
                    HouseNumber = "3C"
                };

                var shopsList = new List<Shop>().AsQueryable();
                _mockShopDbSet = CreateMockDbSet(shopsList);
                _mockContext.Setup(c => c.Shops).Returns(_mockShopDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetShopResponse>(okResult.Value);
                Assert.Equal("New Shop", returnValue.ShopName); // ��������, ��� ShopName ����������
                Assert.Equal("New City", returnValue.City); // ��������, ��� City ����������
                Assert.Equal("New Street", returnValue.Street); // ��������, ��� Street ����������
                Assert.Equal("3C", returnValue.HouseNumber); // ��������, ��� HouseNumber ����������
            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedShop()
            {
                // Arrange
                var existingShop = new Shop
                {
                    ShopId = 1,
                    ShopName = "Old Shop",
                    City = "Old City",
                    Street = "Old Street",
                    HouseNumber = "1A"
                };

                var shopsList = new List<Shop> { existingShop }.AsQueryable();
                _mockShopDbSet = CreateMockDbSet(shopsList);
                _mockContext.Setup(c => c.Shops).Returns(_mockShopDbSet.Object);

                var model = new CreateShopRequest
                {
                    ShopName = "Updated Shop",
                    City = "Updated City",
                    Street = "Updated Street",
                    HouseNumber = "2B"
                };

                // Act
                var result = _controller.Update(model, 1); // �������� shopId = 1

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetShopResponse>(okResult.Value);
                Assert.Equal("Updated Shop", returnValue.ShopName); // ��������, ��� ShopName ���������
                Assert.Equal("Updated City", returnValue.City); // ��������, ��� City ���������
                Assert.Equal("Updated Street", returnValue.Street); // ��������, ��� Street ���������
                Assert.Equal("2B", returnValue.HouseNumber); // ��������, ��� HouseNumber ���������
            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingShop = new Shop
                {
                    ShopId = 1,
                    ShopName = "Shop to Delete",
                    City = "City to Delete",
                    Street = "Street to Delete",
                    HouseNumber = "1A"
                };

                var shopsList = new List<Shop> { existingShop }.AsQueryable();
                _mockShopDbSet = CreateMockDbSet(shopsList);
                _mockContext.Setup(c => c.Shops).Returns(_mockShopDbSet.Object);

                // Act
                var result = _controller.Delete(1); // �������� shopId ��� ��������

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest14
    {
        public class SntControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<Snt>> _mockSntDbSet;
            private readonly SntController _controller;

            public SntControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockSntDbSet = new Mock<DbSet<Snt>>();
                _mockContext.Setup(c => c.Snts).Returns(_mockSntDbSet.Object);
                _controller = new SntController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithSntsList()
            {
                // Arrange
                var sntsList = new List<Snt>
        {
            new Snt { SntId = 1, SntName = "SNT 1", City = "City 1", Street = "Street 1", HouseNumber = "1A", ManagerFirstName = "John", ManagerLastName = "Doe", ManagerPhone = "123456789", UserId = 1 },
            new Snt { SntId = 2, SntName = "SNT 2", City = "City 2", Street = "Street 2", HouseNumber = "2B", ManagerFirstName = "Jane", ManagerLastName = "Doe", ManagerPhone = "987654321", UserId = 2 }
        }.AsQueryable();

                _mockSntDbSet = CreateMockDbSet(sntsList);
                _mockContext.Setup(c => c.Snts).Returns(_mockSntDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetSntResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ���������, ��� �������� ������ �� 2 ���
            }

            [Fact]
            public void GetById_ReturnsOkResultWithSnt()
            {
                // Arrange
                var snt = new Snt { SntId = 1, SntName = "SNT 1", City = "City 1", Street = "Street 1", HouseNumber = "1A", ManagerFirstName = "John", ManagerLastName = "Doe", ManagerPhone = "123456789", UserId = 1 };
                var sntsList = new List<Snt> { snt }.AsQueryable();

                _mockSntDbSet = CreateMockDbSet(sntsList);
                _mockContext.Setup(c => c.Snts).Returns(_mockSntDbSet.Object);

                // Act
                var result = _controller.GetById(1, 1); // �������� �� sntId = 1 � userId = 1

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetSntResponse>(okResult.Value);
                Assert.Equal(1, returnValue.SntId); // ��������, ��� SntId ����������
                Assert.Equal("SNT 1", returnValue.SntName); // ��������, ��� SntName ����������
                Assert.Equal("City 1", returnValue.City); // ��������, ��� City ����������
                Assert.Equal("Street 1", returnValue.Street); // ��������, ��� Street ����������
                Assert.Equal("1A", returnValue.HouseNumber); // ��������, ��� HouseNumber ����������
                Assert.Equal("John", returnValue.ManagerFirstName); // ��������, ��� ManagerFirstName ����������
                Assert.Equal("Doe", returnValue.ManagerLastName); // ��������, ��� ManagerLastName ����������
                Assert.Equal("123456789", returnValue.ManagerPhone); // ��������, ��� ManagerPhone ����������

            }

            [Fact]
            public void Add_ReturnsOkResultWithNewSnt()
            {
                // Arrange
                var model = new CreateSntRequest
                {
                    SntName = "New SNT",
                    City = "New City",
                    Street = "New Street",
                    HouseNumber = "3C",
                    ManagerFirstName = "Alice",
                    ManagerLastName = "Smith",
                    ManagerPhone = "1122334455",

                };

                var sntsList = new List<Snt>().AsQueryable();
                _mockSntDbSet = CreateMockDbSet(sntsList);
                _mockContext.Setup(c => c.Snts).Returns(_mockSntDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetSntResponse>(okResult.Value);
                Assert.Equal("New SNT", returnValue.SntName); // ��������, ��� SntName ����������
                Assert.Equal("New City", returnValue.City); // ��������, ��� City ����������
                Assert.Equal("New Street", returnValue.Street); // ��������, ��� Street ����������
                Assert.Equal("3C", returnValue.HouseNumber); // ��������, ��� HouseNumber ����������
                Assert.Equal("Alice", returnValue.ManagerFirstName); // ��������, ��� ManagerFirstName ����������
                Assert.Equal("Smith", returnValue.ManagerLastName); // ��������, ��� ManagerLastName ����������
                Assert.Equal("1122334455", returnValue.ManagerPhone); // ��������, ��� ManagerPhone ����������

            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedSnt()
            {
                // Arrange
                var existingSnt = new Snt
                {
                    SntId = 1,
                    SntName = "Old SNT",
                    City = "Old City",
                    Street = "Old Street",
                    HouseNumber = "1A",
                    ManagerFirstName = "John",
                    ManagerLastName = "Doe",
                    ManagerPhone = "123456789",
                    UserId = 1
                };

                var sntsList = new List<Snt> { existingSnt }.AsQueryable();
                _mockSntDbSet = CreateMockDbSet(sntsList);
                _mockContext.Setup(c => c.Snts).Returns(_mockSntDbSet.Object);

                var model = new CreateSntRequest
                {
                    SntName = "Updated SNT",
                    City = "Updated City",
                    Street = "Updated Street",
                    HouseNumber = "2B",
                    ManagerFirstName = "Jane",
                    ManagerLastName = "Smith",
                    ManagerPhone = "987654321",

                };

                // Act
                var result = _controller.Update(model, 1, 1); // �������� sntId = 1 � userId = 1

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetSntResponse>(okResult.Value);
                Assert.Equal("Updated SNT", returnValue.SntName); // ��������, ��� SntName ���������
                Assert.Equal("Updated City", returnValue.City); // ��������, ��� City ���������
                Assert.Equal("Updated Street", returnValue.Street); // ��������, ��� Street ���������
                Assert.Equal("2B", returnValue.HouseNumber); // ��������, ��� HouseNumber ���������
                Assert.Equal("Jane", returnValue.ManagerFirstName); // ��������, ��� ManagerFirstName ���������
                Assert.Equal("Smith", returnValue.ManagerLastName); // ��������, ��� ManagerLastName ���������
                Assert.Equal("987654321", returnValue.ManagerPhone); // ��������, ��� ManagerPhone ���������

            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingSnt = new Snt
                {
                    SntId = 1,
                    SntName = "SNT to Delete",
                    City = "City to Delete",
                    Street = "Street to Delete",
                    HouseNumber = "1A",
                    ManagerFirstName = "John",
                    ManagerLastName = "Doe",
                    ManagerPhone = "123456789",
                    UserId = 1
                };

                var sntsList = new List<Snt> { existingSnt }.AsQueryable();
                _mockSntDbSet = CreateMockDbSet(sntsList);
                _mockContext.Setup(c => c.Snts).Returns(_mockSntDbSet.Object);

                // Act
                var result = _controller.Delete(1, 1); // �������� sntId = 1 � userId = 1

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest15
    {
        public class SntEventControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private Mock<DbSet<SntEvent>> _mockSntEventDbSet;
            private readonly SntEventController _controller;

            public SntEventControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _mockSntEventDbSet = new Mock<DbSet<SntEvent>>();
                _mockContext.Setup(c => c.SntEvents).Returns(_mockSntEventDbSet.Object);
                _controller = new SntEventController(_mockContext.Object);
            }

            private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(item => data.ToList().Add(item)); // ��������� ������� � ������

                return mockSet;
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithSntEventsList()
            {
                // Arrange
                var sntEventsList = new List<SntEvent>
        {
            new SntEvent { EventId = 1, EventDate = DateTime.Now, EventName = "Event 1", EventLocation = "Location 1", SntId = 1, UserId = 1 },
            new SntEvent { EventId = 2, EventDate = DateTime.Now, EventName = "Event 2", EventLocation = "Location 2", SntId = 2, UserId = 2 }
        }.AsQueryable();

                _mockSntEventDbSet = CreateMockDbSet(sntEventsList);
                _mockContext.Setup(c => c.SntEvents).Returns(_mockSntEventDbSet.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetSntEventResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ���������, ��� �������� ������ �� 2 �������
            }

            [Fact]
            public void GetById_ReturnsOkResultWithSntEvent()
            {
                // Arrange
                var sntEvent = new SntEvent { EventId = 1, EventDate = DateTime.Now, EventName = "Event 1", EventLocation = "Location 1", SntId = 1, UserId = 1 };
                var sntEventsList = new List<SntEvent> { sntEvent }.AsQueryable();

                _mockSntEventDbSet = CreateMockDbSet(sntEventsList);
                _mockContext.Setup(c => c.SntEvents).Returns(_mockSntEventDbSet.Object);

                // Act
                var result = _controller.GetById(1, 1, 1); // �������� �� eventId = 1, sntId = 1 � userId = 1

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetSntEventResponse>(okResult.Value);
                Assert.Equal(1, returnValue.EventId); // ��������, ��� EventId ����������
                Assert.Equal("Event 1", returnValue.EventName); // ��������, ��� EventName ����������
                Assert.Equal("Location 1", returnValue.EventLocation); // ��������, ��� EventLocation ����������

            }

            [Fact]
            public void Add_ReturnsOkResultWithNewSntEvent()
            {
                // Arrange
                var model = new CreateSntEventRequest
                {
                    EventDate = DateTime.Now,
                    EventName = "New Event",
                    EventLocation = "New Location",

                };

                var sntEventsList = new List<SntEvent>().AsQueryable();
                _mockSntEventDbSet = CreateMockDbSet(sntEventsList);
                _mockContext.Setup(c => c.SntEvents).Returns(_mockSntEventDbSet.Object);

                // Act
                var result = _controller.Add(model);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetSntEventResponse>(okResult.Value);
                Assert.Equal("New Event", returnValue.EventName); // ��������, ��� EventName ����������
                Assert.Equal("New Location", returnValue.EventLocation); // ��������, ��� EventLocation ����������

            }

            [Fact]
            public void Update_ReturnsOkResultWithUpdatedSntEvent()
            {
                // Arrange
                var existingSntEvent = new SntEvent
                {
                    EventId = 1,
                    EventDate = DateTime.Now,
                    EventName = "Old Event",
                    EventLocation = "Old Location",
                    SntId = 1,
                    UserId = 1
                };

                var sntEventsList = new List<SntEvent> { existingSntEvent }.AsQueryable();
                _mockSntEventDbSet = CreateMockDbSet(sntEventsList);
                _mockContext.Setup(c => c.SntEvents).Returns(_mockSntEventDbSet.Object);

                var model = new CreateSntEventRequest
                {
                    EventDate = DateTime.Now,
                    EventName = "Updated Event",
                    EventLocation = "Updated Location",

                };

                // Act
                var result = _controller.Update(model, 1, 1, 1); // �������� eventId = 1, sntId = 1, userId = 1

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetSntEventResponse>(okResult.Value);
                Assert.Equal("Updated Event", returnValue.EventName); // ��������, ��� EventName ���������
                Assert.Equal("Updated Location", returnValue.EventLocation); // ��������, ��� EventLocation ���������

            }

            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var existingSntEvent = new SntEvent
                {
                    EventId = 1,
                    EventDate = DateTime.Now,
                    EventName = "Event to Delete",
                    EventLocation = "Location to Delete",
                    SntId = 1,
                    UserId = 1
                };

                var sntEventsList = new List<SntEvent> { existingSntEvent }.AsQueryable();
                _mockSntEventDbSet = CreateMockDbSet(sntEventsList);
                _mockContext.Setup(c => c.SntEvents).Returns(_mockSntEventDbSet.Object);

                // Act
                var result = _controller.Delete(1, 1, 1); // �������� eventId = 1, sntId = 1 � userId = 1

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ���������, ��� ��������� ���� OkResult
            }
        }
    }

    public class UnitTest16
    {
        public class UserControllerTests
        {
            private readonly Mock<IUserService> _mockUserService;
            private readonly UserController _controller;

            public UserControllerTests()
            {
                _mockUserService = new Mock<IUserService>();
                _controller = new UserController(_mockUserService.Object);
            }

            [Fact]
            public async Task GetAll_ReturnsOkResultWithUsersList()
            {
                // Arrange
                var usersList = new List<User>
        {
            new User { UserId = 1, FirstName = "����", LastName = "������", Username = "ivan123", Password = "12345", BirthDate = DateTime.Now },
            new User { UserId = 2, FirstName = "����", LastName = "������", Username = "peter123", Password = "12345", BirthDate = DateTime.Now }
        };

                _mockUserService.Setup(service => service.GetAll()).ReturnsAsync(usersList);

                // Act
                var result = await _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetUserResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ���������, ��� �������� ������ �� 2 �������������
            }

            [Fact]
            public async Task GetById_ReturnsOkResultWithUser()
            {
                // Arrange
                var user = new User { UserId = 1, FirstName = "����", LastName = "������", Username = "ivan123", Password = "12345", BirthDate = DateTime.Now };
                _mockUserService.Setup(service => service.GetById(1)).ReturnsAsync(user);

                // Act
                var result = await _controller.GetById(1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetUserResponse>(okResult.Value);
                Assert.Equal(1, returnValue.UserId); // ���������, ��� UserId ����������
                Assert.Equal("����", returnValue.FirstName); // ���������, ��� FirstName ����������
                Assert.Equal("������", returnValue.LastName); // ���������, ��� LastName ����������
                Assert.Equal("ivan123", returnValue.Username); // ���������, ��� Username ����������
            }

            [Fact]
            public async Task Add_ReturnsOkResult()
            {
                // Arrange
                var request = new CreateUserRequest
                {
                    FirstName = "������",
                    LastName = "�������",
                    Username = "sergey123",
                    Password = "12345",
                    BirthDate = DateTime.Now
                };

                _mockUserService.Setup(service => service.Create(It.IsAny<User>())).Returns(Task.CompletedTask);

                // Act
                var result = await _controller.Add(request);

                // Assert
                var okResult = Assert.IsType<OkResult>(result);
            }

            [Fact]
            public async Task Update_ReturnsOkResult()
            {
                // Arrange
                var user = new User { UserId = 1, FirstName = "����", LastName = "������", Username = "ivan123", Password = "12345", BirthDate = DateTime.Now };
                _mockUserService.Setup(service => service.Update(It.IsAny<User>())).Returns(Task.CompletedTask);

                // Act
                var result = await _controller.Update(user);

                // Assert
                var okResult = Assert.IsType<OkResult>(result);
            }

            [Fact]
            public async Task Delete_ReturnsOkResult()
            {
                // Arrange
                var userId = 1;
                _mockUserService.Setup(service => service.Delete(userId)).Returns(Task.CompletedTask);

                // Act
                var result = await _controller.Delete(userId);

                // Assert
                var okResult = Assert.IsType<OkResult>(result);
            }
        }
    }

    public class UnitTest17
    {
        public class WateringScheduleControllerTests
        {
            private readonly Mock<CoutryhouseeContext> _mockContext;
            private readonly WateringScheduleController _controller;

            public WateringScheduleControllerTests()
            {
                _mockContext = new Mock<CoutryhouseeContext>();
                _controller = new WateringScheduleController(_mockContext.Object);
            }

            [Fact]
            public void GetAll_ReturnsOkResultWithWateringSchedulesList()
            {
                // Arrange
                var wateringSchedules = new List<WateringSchedule>
        {
            new WateringSchedule { WateringScheduleId = 1, PlantId = 1, WateringDate = DateTime.Now, WateringTime = TimeSpan.FromHours(7) },
            new WateringSchedule { WateringScheduleId = 2, PlantId = 2, WateringDate = DateTime.Now, WateringTime = TimeSpan.FromHours(9) }
        };

                var dbSetMock = new Mock<DbSet<WateringSchedule>>();
                dbSetMock.As<IQueryable<WateringSchedule>>()
                    .Setup(m => m.Provider).Returns(wateringSchedules.AsQueryable().Provider);
                dbSetMock.As<IQueryable<WateringSchedule>>()
                    .Setup(m => m.Expression).Returns(wateringSchedules.AsQueryable().Expression);
                dbSetMock.As<IQueryable<WateringSchedule>>()
                    .Setup(m => m.ElementType).Returns(wateringSchedules.AsQueryable().ElementType);
                dbSetMock.As<IQueryable<WateringSchedule>>()
                    .Setup(m => m.GetEnumerator()).Returns(wateringSchedules.GetEnumerator());

                _mockContext.Setup(c => c.WateringSchedules).Returns(dbSetMock.Object);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<GetWateringScheduleResponse>>(okResult.Value);
                Assert.Equal(2, returnValue.Count); // ���������, ��� � ������ 2 ��������
            }

            [Fact]
            public void GetById_ReturnsOkResultWithWateringSchedule()
            {
                // Arrange
                var wateringSchedule = new WateringSchedule { WateringScheduleId = 1, PlantId = 1, WateringDate = DateTime.Now, WateringTime = TimeSpan.FromHours(7) };

                var dbSetMock = new Mock<DbSet<WateringSchedule>>();
                dbSetMock.As<IQueryable<WateringSchedule>>()
                    .Setup(m => m.Provider).Returns(new List<WateringSchedule> { wateringSchedule }.AsQueryable().Provider);
                dbSetMock.As<IQueryable<WateringSchedule>>()
                    .Setup(m => m.Expression).Returns(new List<WateringSchedule> { wateringSchedule }.AsQueryable().Expression);
                dbSetMock.As<IQueryable<WateringSchedule>>()
                    .Setup(m => m.ElementType).Returns(new List<WateringSchedule> { wateringSchedule }.AsQueryable().ElementType);
                dbSetMock.As<IQueryable<WateringSchedule>>()
                    .Setup(m => m.GetEnumerator()).Returns(new List<WateringSchedule> { wateringSchedule }.GetEnumerator());

                _mockContext.Setup(c => c.WateringSchedules).Returns(dbSetMock.Object);

                // Act
                var result = _controller.GetById(1, 1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetWateringScheduleResponse>(okResult.Value);
                Assert.Equal(1, returnValue.WateringScheduleId); // ���������, ��� WateringScheduleId ����������

            }

            [Fact]
            public void Add_ReturnsOkResultWithWateringSchedule()
            {
                // Arrange
                var request = new CreateWateringScheduleRequest
                {
                    WateringDate = DateTime.Now,
                    WateringTime = TimeSpan.FromHours(7),

                };

                var wateringSchedule = new WateringSchedule
                {
                    WateringScheduleId = 1, // ID ����� �������� �������������
                    PlantId = 1,
                    WateringDate = DateTime.Now,
                    WateringTime = TimeSpan.FromHours(7)
                };

                // ������ DbSet
                var dbSetMock = new Mock<DbSet<WateringSchedule>>();
                dbSetMock.Setup(m => m.Add(It.IsAny<WateringSchedule>())).Callback<WateringSchedule>(x => wateringSchedule.WateringScheduleId = 1); // ����������� ID


                _mockContext.Setup(c => c.WateringSchedules).Returns(dbSetMock.Object);

                // Act
                var result = _controller.Add(request);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetWateringScheduleResponse>(okResult.Value);
                Assert.Equal(1, returnValue.WateringScheduleId); // ��������� ������������ WateringScheduleId
            }

            [Fact]
            public void Update_ReturnsOkResultWithWateringSchedule()
            {
                // Arrange
                var request = new CreateWateringScheduleRequest
                {
                    WateringDate = DateTime.Now,
                    WateringTime = TimeSpan.FromHours(9), // ����� �����

                };

                var wateringSchedule = new WateringSchedule
                {
                    WateringScheduleId = 1,
                    PlantId = 1,
                    WateringDate = DateTime.Now,
                    WateringTime = TimeSpan.FromHours(7) // ������ �����
                };

                var dbSetMock = new Mock<DbSet<WateringSchedule>>();
                dbSetMock.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<WateringSchedule, bool>>>()))
                    .Returns(wateringSchedule); // ���������� ��������� ����������

                _mockContext.Setup(c => c.WateringSchedules).Returns(dbSetMock.Object);
                _mockContext.Setup(c => c.SaveChanges()).Returns(1); // ���������� �������� ����������

                // Act
                var result = _controller.Update(request, 1, 1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<GetWateringScheduleResponse>(okResult.Value);
                Assert.Equal(1, returnValue.WateringScheduleId); // ��������� ������������ WateringScheduleId
                Assert.Equal(TimeSpan.FromHours(9), returnValue.WateringTime); // ��������� ����������� �����
            }
            [Fact]
            public void Delete_ReturnsOkResult()
            {
                // Arrange
                var wateringSchedule = new WateringSchedule
                {
                    WateringScheduleId = 1,
                    PlantId = 1,
                    WateringDate = DateTime.Now,
                    WateringTime = TimeSpan.FromHours(7)
                };

                var dbSetMock = new Mock<DbSet<WateringSchedule>>();
                dbSetMock.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<WateringSchedule, bool>>>()))
                    .Returns(wateringSchedule); // ���������� ��������� ����������

                _mockContext.Setup(c => c.WateringSchedules).Returns(dbSetMock.Object);
                _mockContext.Setup(c => c.SaveChanges()).Returns(1); // ���������� �������� ����������

                // Act
                var result = _controller.Delete(1, 1);

                // Assert
                var okResult = Assert.IsType<OkResult>(result); // ��������� ��� ������������ OkResult
            }
        }
    }

    public class UnitTest18
    {
    }

    public class UnitTest19
    {
    }

    public class UnitTest20
    {
    }

    public class UnitTest21
    {
    }

    public class UnitTest22
    {
    }
}