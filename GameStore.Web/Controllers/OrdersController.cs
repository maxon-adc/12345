﻿using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class OrdersController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;
		private const string CookieKey = "customerId";

		public OrdersController(IOrderService orderService,
			IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		public ActionResult Show()
		{
			var order = GetOrder();

			return View(order);
		}

		public ActionResult Update(string gameKey)
		{
			var orderViewModel = GetOrder();
			var orderDto = _mapper.Map<OrderViewModel, OrderDto>(orderViewModel);
			_orderService.Update(orderDto, gameKey);

			return RedirectToAction("Show");
		}

		[HttpGet]
		public ActionResult ListAll()
		{
			var allOrdersModel = new AllOrdersViewModel
			{
				Filter = new OrderFilterViewModel
				{
					From = DateTime.UtcNow,
					To = DateTime.UtcNow
				}
			};

			return View(allOrdersModel);
		}

		public ActionResult ListAll(AllOrdersViewModel allOrdersViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(allOrdersViewModel);
			}

			var filterDto = _mapper.Map<OrderFilterViewModel, OrderFilterDto>(allOrdersViewModel.Filter);
			allOrdersViewModel.Orders = _mapper.Map<IEnumerable<OrderDto>, List<OrderViewModel>>(_orderService.GetAll(filterDto));

			return View(allOrdersViewModel);
		}

		private OrderViewModel GetOrder()
		{
			OrderViewModel orderViewModel;

			if (Request.Cookies[CookieKey] != null)
			{
				var orderDto = _orderService.GetSingleBy(Request.Cookies[CookieKey].Value);
				orderViewModel = _mapper.Map<OrderDto, OrderViewModel>(orderDto);

				return orderViewModel;
			}

			var customerId = Guid.NewGuid().ToString();
			Response.Cookies.Add(new HttpCookie(CookieKey, customerId));
			orderViewModel = new OrderViewModel
			{
				CustomerId = customerId
			};

			_orderService.Create(_mapper.Map<OrderViewModel, OrderDto>(orderViewModel));
			orderViewModel = _mapper.Map<OrderDto, OrderViewModel>(_orderService.GetSingleBy(customerId));

			return orderViewModel;
		}
	}
}