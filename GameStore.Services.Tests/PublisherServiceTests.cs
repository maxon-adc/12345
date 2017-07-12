﻿using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Concrete;
using GameStore.Services.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class PublisherServiceTests
	{
		private Mock<IUnitOfWork> _mockOfUow;
		private PublisherService _target;
		private List<Publisher> _publishers;
		private const int TestInt = 10;
		private const string TestString = "test";

		[TestInitialize]
		public void Initialize()
		{
			_mockOfUow = new Mock<IUnitOfWork>();
			_target = new PublisherService(_mockOfUow.Object);
		}

		[TestMethod]
		public void Create_CreatesPublisher_WhenAnyPublisherIsPassed()
		{
			_publishers = new List<Publisher>();
			_mockOfUow.Setup(m => m.PublisherRepository.Insert(It.IsAny<Publisher>()))
				.Callback<Publisher>(p => _publishers.Add(p));

			_target.Create(new PublisherDto());
			var result = _publishers.Count;

			Assert.IsTrue(result == 1);
		}

		[TestMethod]
		public void Create_CallsSaveOnce_WhenAnyPublisherIsPassed()
		{
			_publishers = new List<Publisher>();
			_mockOfUow.Setup(m => m.PublisherRepository.Insert(It.IsAny<Publisher>()))
				.Callback<Publisher>(p => _publishers.Add(p));

			_target.Create(new PublisherDto());

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void GetStingleby_ReturnsPublisher_WhenValidPublisherIdIsPassed()
		{
			var publisher = new Publisher
			{
				Id = TestInt
			};

			_mockOfUow.Setup(m => m.PublisherRepository.Get(TestInt)).Returns(publisher);

			var result = _target.GetSingleBy(TestInt).Id;

			Assert.IsTrue(result == TestInt);
		}

		[TestMethod]
		public void GetAll_ReturnsAllPublishers()
		{
			_publishers = new List<Publisher>
			{
				new Publisher(),
				new Publisher(),
				new Publisher()
			};

			_mockOfUow.Setup(m => m.PublisherRepository.Get()).Returns(_publishers);

			var result = _target.GetAll().ToList().Count;

			Assert.IsTrue(result == 3);
		}

		[TestMethod]
		public void GetSingleBy_ReturnsPublisher_WhenValidCompanyNameIsPassed()
		{
			var publisher = new Publisher
			{
				CompanyName = TestString
			};

			_mockOfUow.Setup(m => m.PublisherRepository.Get()).Returns(new List<Publisher>{publisher});

			var result = _target.GetSingleBy(TestString).CompanyName;

			Assert.IsTrue(result == TestString);
		}
	}
}
