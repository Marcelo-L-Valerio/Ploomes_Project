﻿using AutoMapper;
using Ploomers_Project_API.DTOs.ViewModels;
using Ploomers_Project_API.Mappers.DTOs.InputModels;
using Ploomers_Project_API.Models.Entities;
using Ploomers_Project_API.Repository;

namespace Ploomers_Project_API.Business.Implementations
{
    public class ClientBusinessImplementation : IClientBusiness
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public ClientBusinessImplementation(IClientRepository repository, IMapper mapper)
        {
            _clientRepository = repository;
            _mapper = mapper;
        }
        public ClientViewModel Create(ClientInputModel client)
        {
            var mappedClient = _mapper.Map<Client>(client);
            var clientEntity = _clientRepository.Create(mappedClient);

            var viewModel = _mapper.Map<ClientViewModel>(clientEntity);
            return viewModel;
        }

        public void Delete(Guid id)
        {
            _clientRepository.Delete(id);
        }

        public List<ClientViewModel> FindAll()
        {
            var clients = _clientRepository.FindAll();

            var viewModel = _mapper.Map<List<ClientViewModel>>(clients);

            viewModel.
                ForEach(client => 
                {
                    client.LastWeekTotal = client.LastWeekSales
                    .Sum(sale => sale.Total);
                });
            return viewModel;
        }

        public ClientViewModel FindById(Guid id)
        {
            var result = _clientRepository.FindById(id);

            var viewModel = _mapper.Map<ClientViewModel>(result);

            viewModel.LastWeekTotal = viewModel.LastWeekSales.Sum(sale => sale.Total);

            return viewModel;
        }

        public void Update(Guid id, ClientInputModel clientData)
        {
            var mappedClient = _mapper.Map<Client>(clientData);
            mappedClient.Id = id;
            var clientEntity = _clientRepository.Update(mappedClient);
        }

        public void AddContact(Guid id, ContactInputModel contact)
        {
            var mappedContact = _mapper.Map<Contact>(contact);
            _clientRepository.AddContact(id, mappedContact);
        }

        public void DeleteContact(Guid idcli, Guid idcont)
        {
            _clientRepository.DeleteContact(idcli, idcont);
        }
    }
}