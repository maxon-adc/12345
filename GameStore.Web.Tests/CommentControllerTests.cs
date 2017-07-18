﻿using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Controllers;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Tests
{
	[TestClass]
	public class CommentControllerTests
	{
		private Mock<ICommentService> _mockOfCommentService;
		private CommentController _target;
		private List<CommentDto> _comments;
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new WebProfile())));
		private const string ValidGameKey = "test";

		[TestInitialize]
		public void Initialize()
		{
			_mockOfCommentService = new Mock<ICommentService>();
			_target = new CommentController(_mockOfCommentService.Object, _mapper);
		}

		[TestMethod]
		public void New_SendsCommentToView_WhenModelStateIsInvalid()
		{
			_target.ModelState.AddModelError("test", "test");

			var result = ((PartialViewResult)_target.New(new CommentViewModel())).Model;

			Assert.IsInstanceOfType(result, typeof(CommentViewModel));
		}

		[TestMethod]
		public void New_ReturnsRedirectToRouteResult_WhenModelStateIsValid()
		{
			var result = _target.New(new CommentViewModel());

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[TestMethod]
		public void ListAll_ReturnsViewResult_WhenAnyGameKeyIsPassed()
		{
			var result = _target.ListAll(string.Empty);

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void ListAll_SendsAllCommentsToView_WhenValidGameKeyIsPassed()
		{
			_comments = new List<CommentDto>
			{
				new CommentDto(),
				new CommentDto()
			};

			_mockOfCommentService.Setup(m => m.GetBy(ValidGameKey)).Returns(_comments);

			var model = ((ViewResult)_target.ListAll(ValidGameKey)).Model;
			var result = ((AllCommentsViewModel)model).Comments.Count;

			Assert.IsTrue(result == 2);
		}
	}
}
