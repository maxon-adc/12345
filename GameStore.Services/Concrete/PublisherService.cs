﻿using AutoMapper;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Services.Concrete
{
	public class PublisherService : IPublisherService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IPublisherRepository _publisherRepository;

		public PublisherService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IPublisherRepository publisherRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_publisherRepository = publisherRepository;
		}

		public void Create(PublisherDto publisherDto)
		{
			var publisher = _mapper.Map<PublisherDto, Publisher>(publisherDto);
			_publisherRepository.Insert(publisher);
			_unitOfWork.Save();
		}

		public PublisherDto GetSingle(string companyName)
		{
			var publisher = _publisherRepository.GetSingle(companyName);
			var publisherDto = _mapper.Map<Publisher, PublisherDto>(publisher);

			return publisherDto;
		}

		public IEnumerable<PublisherDto> GetAll()
		{
			var publishers = _publisherRepository.GetAll();
			var publisherDtos = _mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherDto>>(publishers);

			return publisherDtos;
		}

		public void Update(PublisherDto publisherDto)
		{
			var publisher = _publisherRepository.GetSingle(publisherDto.CompanyName);
			_mapper.Map(publisherDto, publisher);
			_publisherRepository.Update(publisher);
			_unitOfWork.Save();
		}

		public void Delete(string companyName)
		{
			_publisherRepository.Delete(companyName);
			_unitOfWork.Save();
		}
	}
}